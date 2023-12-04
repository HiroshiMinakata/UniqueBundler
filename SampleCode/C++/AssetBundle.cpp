// AssetBundle.cpp

#include "AssetBundle.h"

AssetBundle::AssetBundle(const std::string& fileName) : std::ifstream(fileName, std::ios::binary)
{
	if (!is_open())
		throw std::runtime_error("Can't open file: " + fileName);
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

void AssetBundle::Unload(std::string name)
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

bool AssetBundle::IsUse()
{
	bool isUse = false;
	read(reinterpret_cast<char*>(&isUse), sizeof(bool));
	if (fail())
		throw std::runtime_error("Read error: Failed to read IsUse");
	if (!isUse) return false;
	else return true;
}

void AssetBundle::ReadString(std::string* str)
{
	int strLength = Get7BitInt32();
	str->resize(strLength);
	read(reinterpret_cast<char*>(str->data()), strLength);
	if (fail())
		throw std::runtime_error("Read error: Failed to read string");
}

void AssetBundle::ReadBytes(std::vector<char>* bytes)
{
	int64_t size = GetBytesSize();
	bytes->resize(size);
	read(bytes->data(), size);
	if (fail())
		throw std::runtime_error("Read error: Failed to read bytes");
}

int AssetBundle::Get7BitInt32()
{
	int num = 0;
	int shift = 0;
	char byte;
	do {
		if (shift == 5 * 7)
			throw std::runtime_error("Read error: Bad format 7BitInt32");
		read(&byte, 1);
		if (fail())
			throw std::runtime_error("Read error: Failed to read 7-bit int");
		num |= (byte & 0x7F) << shift;
		shift += 7;
	} while (byte & 0x80);
	return num;
}

int AssetBundle::GetArrayLength()
{
	int length;
	read(reinterpret_cast<char*>(&length), sizeof(int));
	return length;
}

int64_t AssetBundle::GetBytesSize()
{
	int64_t size;
	read(reinterpret_cast<char*>(&size), sizeof(int64_t));
	return size;
}

int AssetBundle::LoadHeader()
{
	int saveMode;
	std::string fileID;
	int toolVesion;
	int version;
	int assetNum;
	int headerSize;
	int metaDataSize;
	int64_t totalAssetSize;
	int footerSize;

	read(reinterpret_cast<char*>(&saveMode),	   sizeof(int));
	read(reinterpret_cast<char*>(fileID.data()),   Get7BitInt32());
	read(reinterpret_cast<char*>(&toolVesion),	   sizeof(int));
	read(reinterpret_cast<char*>(&version),		   sizeof(int));
	read(reinterpret_cast<char*>(&assetNum),	   sizeof(int));
	read(reinterpret_cast<char*>(&headerSize),	   sizeof(int));
	read(reinterpret_cast<char*>(&metaDataSize),   sizeof(int));
	read(reinterpret_cast<char*>(&totalAssetSize), sizeof(int64_t));
	read(reinterpret_cast<char*>(&footerSize),	   sizeof(int));

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
		read(reinterpret_cast<char*>(assetName.data()), strLength);
		read(reinterpret_cast<char*>(&fieldNum),		sizeof(int));
		read(reinterpret_cast<char*>(&offset),			sizeof(int64_t));
		read(reinterpret_cast<char*>(&size),			sizeof(int64_t));
		offsets.emplace(assetName, offset);
	}
}