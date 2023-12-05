/*
* MIT License
*
* Copyright(c) 2023 HiroshiMinakata
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files(the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and /or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions :
*
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*/

/*
* Click here for the link to the project on GitHub.
* https://github.com/HiroshiMinakata/UniqueBundler
*/

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

// AssetBundle.h

#pragma once
#include <fstream>
#include <string>
#include <unordered_map>
#include <vector>

#pragma region AssetReader
class AssetReader : public std::ifstream
{
public:
	AssetReader(const std::string& fileName, std::streampos offset = 0);

	template <typename T>
	void Read(T* data, bool getIsUse = true, bool skip = false)
	{
		if (getIsUse)
			skip = !IsUse();

		int a = 0;

		// Get string
		if constexpr (std::is_same<T, std::string>::value)
			ReadString(data, skip);
		// Get Bytes
		else if constexpr (std::is_same<T, std::vector<char>>::value)
			ReadBytes(data, skip);
		else if constexpr (is_vector_not_char<T>::value || is_array<T>::value)
		{
			// Get length
			int length = GetArrayLength();

			// Resize if T is a vector
			if constexpr (is_vector_not_char<T>::value)
				if (data->size() != length)
					data->resize(length);

			// Get vector datas or array datas
			for (int i = 0; i < length; i++)
			{
				if constexpr (std::is_pointer<typename std::remove_reference<decltype((*data)[i])>::type>::value)
					Read((*data)[i], false, skip); // Is ptr
				else
					Read(&(*data)[i], false, skip); // Not ptr
			}
		}
		else
			(skip) ? seekg(sizeof(T), std::ios::cur) : read(reinterpret_cast<char*>(data), sizeof(T));
		if (fail())
			throw std::runtime_error("Read error: Failed to read");
	}
private:
	// for vector and not char vector
	template <typename T>
	struct is_vector_not_char : std::false_type {};
	template <typename T>
	struct is_vector_not_char<std::vector<T>> : std::integral_constant<bool, !std::is_same<T, char>::value> {};

	// for array
	template <typename T>
	struct is_array : std::false_type {};
	template <typename T, std::size_t N>
	struct is_array<T[N]> : std::true_type {};

protected:
	bool IsUse();

	void ReadString(std::string* str, bool skip);
	void ReadBytes(std::vector<char>* bytes, bool skip);

	int Get7BitInt32();
	int GetArrayLength();
	int64_t GetBytesSize();

protected:
	const std::string fileName;
};
#pragma endregion

#pragma region AssetBundle
class AssetBundle : public AssetReader
{
public:
	AssetBundle(const std::string& fileName);
	~AssetBundle();

	template <typename T>
	T* Load(std::string name)
	{
		T* asset = new T;

		// Check have Decode
		if constexpr (std::is_invocable<decltype(&T::Decode), T*, const std::string&, const int64_t&>::value)
			asset->Decode(fileName, offsets[name]);
		else
			static_assert(false, "Decode method is missing in the class.");
		anyPtrMaps.emplace(name, asset);
		return asset;
	}
	void FileClose();

	void Unload(std::string name);
	void Unload(bool all);

private:
	int LoadHeader();
	void LoadMetaData(int assetNum);

private:
	std::unordered_map<std::string, int64_t> offsets;
	std::unordered_map<std::string, void*> anyPtrMaps;
};
#pragma endregion