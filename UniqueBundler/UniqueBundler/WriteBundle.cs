﻿using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

/*
* .ab (AssetBundle file)
* UTF-8
* 
* ----- Header -----
* int saveMode;
* string fileID;
* int toolVersion;
* int varsion;
* int assetNum;
* int headerSize;
* int metaDataSize;
* long totalAssetSize;
* int footerSize;
* 
* ----- MetaData -----
* // for the number of Asset
* {
*   string name;
*   int fieldNum;
*   long offset;
*   long assetSize;
* }
* 
* ----- Field -----
* // for the number of Asset
* {
*	bool isUse;
*   if byte[] : long length, data;
*	if array : int length, anyData;
*	else : anyData;
* }
* 
* ----- Footer -----
* // for the number of Asset
* {
*   string extension;
*   string className;
* }
*/

namespace UniqueBundler
{
    public class WriteBundle : FileManager
    {
        string[] assetNames;
        ClassFieldData[][] assetsDatas;
        string[] formats;
        string[] classNames;
        string saveFileName;

        int saveMode;
        string fileID = "uab";
        int toolVersion = 1;
        int version;
        int assetNum;
        const int headerSize = sizeof(int) * 7 + sizeof(long) + sizeof(byte) * 4;
        int metaDataSize;
        long totalAssetSize;
        int footerSize;

        public WriteBundle(int version, string[] assetNames, ClassFieldData[][] assetsDatas, string[] formats, string[] classNames, string saveFileName)
        {
            this.assetNames = assetNames;
            this.assetsDatas = assetsDatas;
            this.formats = formats;
            this.classNames = classNames;
            this.saveFileName = saveFileName;

            this.version = version;
            assetNum = assetsDatas.Length;
            metaDataSize = GetMetaDataSize();
            totalAssetSize = GetTotalAssetSize();
            footerSize = GetFooterSize();
        }

        public void NormalWrite()
        {
            saveMode = 0;
            using (BinaryWriter writer = new BinaryWriter(File.Open(saveFileName, FileMode.Create)))
            {
                writer.Write(saveMode);
                Write(writer);
            }
        }

        public void GZIPWrite()
        {
            saveMode = 1;
            using (FileStream fileStream = File.Open(saveFileName, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                writer.Write(saveMode);
                using (GZipStream gZipStream = new GZipStream(fileStream, CompressionMode.Compress))
                using (BinaryWriter gZipWriter = new BinaryWriter(gZipStream))
                    Write(gZipWriter);
            }
        }

        public void AESWrite(byte[] key, byte[] iv)
        {
            saveMode = 2;
            using (FileStream fileStream = File.Open(saveFileName, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                writer.Write(saveMode);
                using (AesManaged aes = new AesManaged { Key = key, IV = iv })
                using (CryptoStream cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (BinaryWriter cryptoWriter = new BinaryWriter(cryptoStream))
                    Write(cryptoWriter);
            }
        }

        public void GZIPandAESWrite(byte[] key, byte[] iv)
        {
            saveMode = 3;
            using (FileStream fileStream = File.Open(saveFileName, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                writer.Write(saveMode);

                using (AesManaged aes = new AesManaged { Key = key, IV = iv })
                using (CryptoStream cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (GZipStream compressionStream = new GZipStream(cryptoStream, CompressionMode.Compress))
                using (BinaryWriter encryptedWriter = new BinaryWriter(compressionStream))
                    Write(encryptedWriter);
            }
        }

        private void Write(BinaryWriter writer)
        {
            try
            {
                WriteHader(writer); // Hader
                WriteMetaData(writer); // MetaData
                WriteField(writer); //Field
                WriteFooter(writer); //Footer
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save file.\nError: " + ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #region Header
        private void WriteHader(BinaryWriter writer)
        {
            // SaveMode was already written
            writer.Write(fileID);
            writer.Write(toolVersion);
            writer.Write(version);
            writer.Write(assetNum);
            writer.Write(headerSize);
            writer.Write(metaDataSize);
            writer.Write(totalAssetSize);
            writer.Write(footerSize);
        }
        #endregion

        #region MetaData
        private void WriteMetaData(BinaryWriter writer)
        {
            long currentOffset = headerSize + metaDataSize;
            for (int i = 0; i < assetNum; i++)
            {
                writer.Write(assetNames[i]);
                writer.Write(assetsDatas[i].Length);
                writer.Write(currentOffset); // offset
                long assetSize = GetSize(assetsDatas[i]); // assetSize
                writer.Write(assetSize);

                currentOffset += assetSize;
            }
        }

        private int GetMetaDataSize()
        {
            int size = 0;
            for (int i = 0; i < assetNum; i++)
            {
                size += GetUTF8Size(assetNames[i]);
                size += sizeof(int);
                size += sizeof(long);
                size += sizeof(long);
            }
            return size;
        }
        #endregion

        #region Field
        private void WriteField(BinaryWriter writer)
        {
            foreach (var assetdatas in assetsDatas)
            {
                foreach(var data in assetdatas)
                {
                    writer.Write(data.isUse);
                    ObjectWrite(writer, data.data);
                }
            }
        }

        private long GetTotalAssetSize()
        {
            long size = 0;
            foreach (var assetdatas in assetsDatas)
                size += GetSize(assetdatas);
            return size;
        }

        private void ObjectWrite(BinaryWriter writer, object obj)
        {
            if (obj is int intValue)
                writer.Write(intValue);
            else if (obj is float floatValue)
                writer.Write(floatValue);
            else if (obj is double doubleValue)
                writer.Write(doubleValue);
            else if (obj is bool boolValue)
                writer.Write(boolValue);
            else if (obj is string stringValue)
                writer.Write(stringValue);
            else if (obj is byte[] byteArray)
            {
                if (byteArray.Length == 0)
                {
                    writer.Write(0);
                    writer.Write(0);
                    return;
                }
                string fileName = Encoding.UTF8.GetString(byteArray);
                if (File.Exists(fileName))
                {
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        writer.Write(fileStream.Length);
                        const int bufferSize = 4096;
                        byte[] buffer = new byte[bufferSize];
                        int bytesRead;

                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                            writer.Write(buffer, 0, bytesRead);
                    }
                }
            }
            else if (obj is List<object> objectList)
            {
                writer.Write(objectList.Count);
                foreach (var element in objectList)
                    ObjectWrite(writer, element);
            }
            else
                writer.Write(0);
        }
        #endregion

        #region Footer
        private void WriteFooter(BinaryWriter writer)
        {
            for (int i = 0; i < assetNum; i++)
            {
                writer.Write(formats[i]);
                writer.Write(classNames[i]);
            }
        }

        private int GetFooterSize()
        {
            int size = 0;
            for (int i = 0; i < assetNum; i++)
            {
                size += GetUTF8Size(formats[i]);
                size += GetUTF8Size(classNames[i]);
            }
            return size;
        }
        #endregion
    }
}
