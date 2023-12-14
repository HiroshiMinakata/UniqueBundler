// AssetBundle.cpp

#include "AssetBundle.h"

#pragma region AssetReader
AssetReader::AssetReader(const std::string& fileName, std::streampos offset)
	: std::ifstream(fileName, std::ios::binary), fileName(fileName)
{
#if _DEBUG
	if (!is_open())
		throw std::runtime_error("Can't open file: " + fileName);
#endif
	seekg(offset);
}

bool AssetReader::IsUse()
{
	bool isUse = false;
	read(reinterpret_cast<char*>(&isUse), sizeof(bool));
#if _DEBUG
	if (fail())
		throw std::runtime_error("Read error: Failed to read IsUse");
#endif
	return isUse;
}

void AssetReader::ReadString(std::string* str, bool skip)
{
	int strLength = Get7BitInt32();
	if (skip)
	{
		seekg(strLength, std::ios::cur);
		return;
	}
	str->resize(strLength);
	read(&(*str)[0], strLength);
#if _DEBUG
	if (fail())
		throw std::runtime_error("Read error: Failed to read string");
#endif
}

void AssetReader::ReadBytes(std::vector<char>* bytes, bool skip)
{
	int64_t size = GetBytesSize();
	if (skip)
	{
		seekg(size, std::ios::cur);
		return;
	}
	bytes->resize(size);
	read(bytes->data(), size);
#if _DEBUG
	if (fail())
		throw std::runtime_error("Read error: Failed to read bytes");
#endif
}

int AssetReader::Get7BitInt32()
{
	int num = 0;
	int shift = 0;
	char byte;
	do {
#if _DEBUG
		if (shift == 5 * 7)
			throw std::runtime_error("Read error: Bad format 7BitInt32");
#endif
		read(&byte, 1);
#if _DEBUG
		if (fail())
			throw std::runtime_error("Read error: Failed to read 7-bit int");
#endif
		num |= (byte & 0x7F) << shift;
		shift += 7;
	} while (byte & 0x80);
	return num;
}

int AssetReader::GetArrayLength()
{
	int length;
	read(reinterpret_cast<char*>(&length), sizeof(int));
	return length;
}

int64_t AssetReader::GetBytesSize()
{
	int64_t size;
	read(reinterpret_cast<char*>(&size), sizeof(int64_t));
	return size;
}
#pragma endregion

#pragma region AssetBundle
AssetBundle::AssetBundle(const std::string& fileName) : AssetReader(fileName)
{
	offsets.clear();
	int assetNum = LoadHeader();
	LoadMetaData(assetNum);
}

AssetBundle::~AssetBundle()
{
	Unload(true);
}

void AssetBundle::FileClose()
{
	close();
}

void AssetBundle::Unload(const std::string& name)
{
	void* ptr = anyPtrMaps[name];
	delete ptr;
	anyPtrMaps.erase(name);
	offsets.erase(name);
}

void AssetBundle::Unload(bool all)
{
	if (!all) return;
	for (auto& anyPtrMap : anyPtrMaps)
		delete anyPtrMap.second;
	anyPtrMaps.clear();
	offsets.clear();
}

int AssetBundle::LoadHeader()
{
#if _DEBUG
	int saveMode;
	std::string fileID;
	int toolVesion;
	int version;
#endif
	int assetNum;
#if _DEBUG
	int headerSize;
	int metaDataSize;
	int64_t totalAssetSize;
	int footerSize;
#endif

#if _DEBUG
	read(reinterpret_cast<char*>(&saveMode), sizeof(int));
	read(&fileID[0], Get7BitInt32());
	read(reinterpret_cast<char*>(&toolVesion), sizeof(int));
	read(reinterpret_cast<char*>(&version), sizeof(int));
	read(reinterpret_cast<char*>(&assetNum), sizeof(int));
	read(reinterpret_cast<char*>(&headerSize), sizeof(int));
	read(reinterpret_cast<char*>(&metaDataSize), sizeof(int));
	read(reinterpret_cast<char*>(&totalAssetSize), sizeof(int64_t));
	read(reinterpret_cast<char*>(&footerSize), sizeof(int));
#else
	const unsigned short assetNumPos = sizeof(int) + 4 + sizeof(int) * 2;
	seekg(assetNumPos);
	read(reinterpret_cast<char*>(&assetNum), sizeof(int));
	const unsigned short headerEndPos = sizeof(int) + 4 + sizeof(int) * 5 + sizeof(int64_t) + sizeof(int);
	seekg(headerEndPos);
#endif

	return assetNum;
}

void AssetBundle::LoadMetaData(int assetNum)
{
	for (int i = 0; i < assetNum; i++)
	{
		std::string assetName;
		int fieldNum;
		int64_t offset;
		int64_t size;

		int strLength = Get7BitInt32();
		assetName.resize(strLength);
		read(&assetName[0], strLength);
#if _DEBUG
		read(reinterpret_cast<char*>(&fieldNum), sizeof(int));
#else
		seekg(sizeof(int), std::ios::cur);
#endif
		read(reinterpret_cast<char*>(&offset), sizeof(int64_t));
#if _DEBUG
		read(reinterpret_cast<char*>(&size), sizeof(int64_t));
#else
		seekg(sizeof(int64_t), std::ios::cur);
#endif
		offsets.emplace(assetName, offset);
	}
}
#pragma endregion