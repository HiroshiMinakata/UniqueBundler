﻿/*
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

using System.Text;

namespace UniqueBundler
{
    public class LoadBundle : FileManager
    {
        string loadFileName;

        int version;
        int assetNum;
        int headerSize;
        int metaDataSize;
        long totalAssetSize;
        int footerSize;

        public struct MetaData
        {
            public string name;
            public int fieldNum;
            public long offset;
            public long assetSize;
        }
        public struct Footer
        {
            public string format;
            public string className;
        }

        public MetaData[] metaDatas;
        public ClassFieldData[][] assetsFieldDatas;
        public Footer[] footers;

        public LoadBundle(string loadFileName)
        {
            this.loadFileName = loadFileName;
        }

        public void NormalRead()
        {
            Read();
        }

        private void Read()
        {
            try
            {
                using (FileStream stream = new FileStream(loadFileName, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    // Heder
                    ReadHeader(reader);

                    metaDatas = new MetaData[assetNum];
                    footers = new Footer[assetNum];
                    assetsFieldDatas = new ClassFieldData[assetNum][];

                    // MetaData
                    ReadMetaData(reader);

                    // Footer
                    ReadFooter(reader, stream);

                    for (int i = 0; i < assetNum; i++)
                    {
                        int fieldNum = metaDatas[i].fieldNum;
                        assetsFieldDatas[i] = new ClassFieldData[fieldNum];
                    }

                    // Data
                    ReadField(reader, stream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load file.\nError: " + ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #region Header
        private void ReadHeader(BinaryReader reader)
        {
            version = reader.ReadInt32();
            assetNum = reader.ReadInt32();
            headerSize = reader.ReadInt32();
            metaDataSize = reader.ReadInt32();
            totalAssetSize = reader.ReadInt64();
            footerSize = reader.ReadInt32();
        }
        #endregion

        #region MetaData
        private void ReadMetaData(BinaryReader reader)
        {
            for (int i = 0; i < assetNum; i++)
            {
                metaDatas[i].name = reader.ReadString();
                metaDatas[i].fieldNum = reader.ReadInt32();
                metaDatas[i].offset = reader.ReadInt64();
                metaDatas[i].assetSize = reader.ReadInt64();
            }
        }
        #endregion

        #region Footer
        private void ReadFooter(BinaryReader reader, FileStream stream)
        {
            long offset = headerSize + metaDataSize + totalAssetSize;
            stream.Seek(offset, SeekOrigin.Begin);
            for (int i = 0; i < assetNum; i++)
            {
                footers[i].format = reader.ReadString();
                footers[i].className = reader.ReadString();
            }
        }
        #endregion

        #region Field
        private void ReadField(BinaryReader reader, FileStream stream)
        {
            long offset = headerSize + metaDataSize;
            stream.Seek(offset, SeekOrigin.Begin);
            for (int i = 0; i < assetNum; i++)
            {
                ClassFieldData[] sampleDatas = GetDefaultFieldDatas(footers[i].className);
                for (int j = 0; j < assetsFieldDatas[i].Length; j++)
                {
                    assetsFieldDatas[i][j].isUse = reader.ReadBoolean();
                    assetsFieldDatas[i][j].data = ReadObject(reader, sampleDatas[j].data);
                }
            }
        }

        private object ReadObject(BinaryReader reader, object sample)
        {
            if (sample is int)
                return reader.ReadInt32();
            else if (sample is float)
                return reader.ReadSingle();
            else if (sample is double)
                return reader.ReadDouble();
            else if (sample is bool)
                return reader.ReadBoolean();
            else if (sample is string)
                return reader.ReadString();
            else if (sample is byte[])
            {
                string fileName = ReadDataToTempFile(reader);
                return Encoding.UTF8.GetBytes(fileName);
            }
            else if (sample is List<object> samples)
            {
                int num = reader.ReadInt32();
                List<object> objs = new List<object>();
                for (int i = 0; i < num; i++)
                    objs.Add(ReadObject(reader, samples[0]));
                return objs;
            }
            else return null;
        }

        private string ReadDataToTempFile(BinaryReader reader)
        {
            long size = reader.ReadInt64();
            if (size == 0)
                return null;

            string tempFilePath = Path.GetTempFileName();
            using (FileStream fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
            {
                const int bufferSize = 4096;
                byte[] buffer = new byte[bufferSize];
                int bytesRead;

                while (size > 0)
                {
                    bytesRead = reader.Read(buffer, 0, (int)Math.Min(bufferSize, size));
                    fileStream.Write(buffer, 0, bytesRead);
                    size -= bytesRead;
                }
            }

            return tempFilePath;
        }
        #endregion
    }
}
