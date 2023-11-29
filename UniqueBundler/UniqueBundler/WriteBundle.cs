using System.IO.Compression;
using System.Text;
using static UniqueBundler.Form1;

/*
* .ab (AssetBundle file)
* UTF-8
* 
* ----- Header -----
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
*   string format;
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

        int version;
        int assetNum;
        const int headerSize = sizeof(int) * 5 + sizeof(long);
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
            Write();
        }

        public void CompressWrite()
        {
            string outputPath = saveFileName;
            saveFileName = Path.GetTempFileName();
            Write();
            CompressFile(saveFileName, outputPath);
            if (File.Exists(saveFileName))
                File.Delete(saveFileName);
        }

        private void Write()
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(saveFileName, FileMode.Create)))
                {
                    // Heder
                    writer.Write(version);
                    writer.Write(assetNum);
                    writer.Write(headerSize);
                    writer.Write(metaDataSize);
                    writer.Write(totalAssetSize);
                    writer.Write(footerSize);

                    // MetaData
                    WriteMetaData(writer);

                    // Data
                    WriteField(writer);

                    // Footer
                    WriteFooter(writer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save file.\nError: " + ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

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

        private static int GetUTF8Size(string str)
        {
            int utf8ByteCount = Encoding.UTF8.GetByteCount(str);
            int lengthByteCount = 1;
            int length = utf8ByteCount;
            while (length > 0x7F)
            {
                lengthByteCount++;
                length >>= 7;
            }

            return utf8ByteCount + lengthByteCount;
        }

        public void CompressFile(string inputFile, string outputFile)
        {
            using (FileStream originalFileStream = File.OpenRead(inputFile))
            using (FileStream compressedFileStream = File.Create(outputFile))
            using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                originalFileStream.CopyTo(compressionStream);
        }
    }
}
