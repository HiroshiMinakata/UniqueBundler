using System.Diagnostics;
using System.Text;
using YamlDotNet.RepresentationModel;

namespace UniqueBundler
{
    public class FileManager
    {
        public FileManager()
        {
            LoadClassConfig();
            LoadClassExtensions();
        }

        public struct ClassFieldData
        {
            public string name;
            public object data;
            public bool isUse;
        }

        #region ClassData
        private void LoadClassConfig()
        {
            // Check file
            if (!File.Exists(ClassConfigPath)) CreateClassConfig();
            GetClassNames();
            GetDefaultClassFieldData();
        }

        public string[] GetClassNames()
        {
            if(classNames != null) return classNames;

            // Load yaml
            YamlStream yaml = LoadYaml(ClassConfigPath);

            // Get top node
            YamlMappingNode mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

            // Get top node keys
            List<string> keys = new List<string>();
            foreach (var entry in mapping.Children)
                keys.Add(entry.Key.ToString());
            classNames = keys.ToArray();
            return classNames;
        }

        private ClassFieldData[][] GetDefaultClassFieldData()
        {
            if(classesDefaultFieldDatas != null) return classesDefaultFieldDatas;

            // Load yaml
            YamlStream yaml = LoadYaml(ClassConfigPath);

            // Get top node
            YamlMappingNode mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

            // Search types
            var classList = new List<ClassFieldData[]>();
            foreach (var entry in mapping.Children)
            {
                // Get field data
                var classFields = new List<ClassFieldData>();
                foreach (var item in ((YamlMappingNode)entry.Value).Children)
                {
                    // Parse data
                    var classFieldData = new ClassFieldData
                    {
                        name = item.Key.ToString(),
                        data = ParseNode(item.Value[0]),
                        isUse = bool.Parse(item.Value[1].ToString())
                    };
                    classFields.Add(classFieldData);
                }
                classList.Add(classFields.ToArray());
            }

            classesDefaultFieldDatas = classList.ToArray();
            return classesDefaultFieldDatas;
        }

        public ClassFieldData[] GetDefaultFieldDatas(string className)
        {
            int classIndex = Array.IndexOf(classNames, className);
            return GetDefaultClassFieldData()[classIndex];
        }

        private static object ParseNode(YamlNode node)
        {
            // Scalar
            if (node is YamlScalarNode scalar)
            {
                if (scalar.Tag == "tag:yaml.org,2002:binary")
                {
                    try { return Convert.FromBase64String(scalar.Value); }
                    catch { return null; }
                }
                else if (int.TryParse(scalar.Value, out int intValue))
                    return intValue;
                else if (scalar.Tag == "tag:yaml.org,2002:float" && float.TryParse(scalar.Value, out float floatValue))
                    return floatValue;
                else if (double.TryParse(scalar.Value, out double doubleValue))
                    return doubleValue;
                else if (bool.TryParse(scalar.Value, out bool boolValue))
                    return boolValue;
                else
                    return scalar.Value.ToString();
            }
            
            // List
            else if (node is YamlSequenceNode sequence)
            {
                List<object> objects = new List<object>();
                foreach (var element in sequence)
                    objects.Add(ParseNode(element));
                return objects;
            }
            
            // Dictionary
            else if (node is YamlMappingNode mapping)
            {
                var dict = new Dictionary<string, object>();
                foreach (var entry in mapping)
                {
                    var key = ParseNode(entry.Key).ToString();
                    var value = ParseNode(entry.Value);
                    dict[key] = value;
                }
                return dict;
            }

            // Unkonow
            else
                return null;
        }

        private static void CreateClassConfig()
        {
            string text = "None:\r\n  Data:\r\n    - !!binary\r\n    - true";
            File.WriteAllText(ClassConfigPath, text);
        }

        public static void OpenConfigFile()
        {
            if (!File.Exists(ClassConfigPath)) CreateClassConfig();
            Process.Start("notepad.exe", ClassConfigPath);
        }

        private string[] classNames;
        private ClassFieldData[][] classesDefaultFieldDatas;
        private const string ClassConfigPath = "ClassConfig.yaml";
        #endregion

        #region Extension
        private void LoadClassExtensions()
        {
            if (!File.Exists(ClassExtrensionsPath)) CreateClassesExtensions();
            GetClassesExtensions();
        }
        private static void CreateClassesExtensions()
        {
            string text = "None: [\"\"]";
            File.WriteAllText(ClassExtrensionsPath, text);
        }

        public static void OpenExtensionsFile()
        {
            if (!File.Exists(ClassExtrensionsPath)) CreateClassesExtensions();
            Process.Start("notepad.exe", ClassExtrensionsPath);
        }

        private string[][] GetClassesExtensions()
        {
            if (classesExtensions != null) return classesExtensions;
            YamlStream yaml = LoadYaml(ClassExtrensionsPath);
            var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

            var classesList = new List<string[]>();
            foreach (var entry in mapping)
            {
                string key = entry.Key.ToString();
                string[] extensions = ((YamlSequenceNode)entry.Value).Children
                                  .Select(node => node.ToString()).ToArray();
                classesList.Add(extensions);
            }

            classesExtensions = classesList.ToArray();
            return classesExtensions;
        }

        public string GetClassName(string extension)
        {
            int classIndex = 0;
            for (int i = 0; i < classesExtensions.Length; i++)
                if (Array.IndexOf(classesExtensions[i], extension) != -1)
                    classIndex = i;

            return GetClassNames()[classIndex];
        }

        private string[][] classesExtensions;
        private const string ClassExtrensionsPath = "Extensions.yaml"; 
        #endregion

        #region Yaml
        private YamlStream LoadYaml(string path)
        {
            YamlStream yaml = new YamlStream();
            using (var reader = new StreamReader(path))
                yaml.Load(reader);
            return yaml;
        }
        #endregion

        #region Data
        public static long GetSize(ClassFieldData[] classFieldDatas)
        {
            long size = 0;
            foreach (ClassFieldData classFieldData in classFieldDatas)
            {
                // classFieldData.isUse
                size += sizeof(bool);
                GetObjectSize(classFieldData.data, ref size);
            }

            return size;
        }

        private static void GetObjectSize(object obj, ref long size)
        {
            if (obj is int intValue)
                size += sizeof(int);
            else if (obj is float floatValue)
                size += sizeof(float);
            else if (obj is double doubleValue)
                size += sizeof(double);
            else if (obj is bool boolValue)
                size += sizeof(bool);
            else if (obj is string stringValue)
                size += sizeof(char) * stringValue.Length;
            else if (obj is byte[] byteArray)
            {
                size += sizeof(long);
                string fileName = Encoding.UTF8.GetString(byteArray);
                if (!File.Exists(fileName)) return;
                FileInfo fileInfo = new FileInfo(fileName);
                size += fileInfo.Length;
            }
            else if (obj is List<object> objectArray)
            {
                size += sizeof(int);
                foreach (var element in objectArray)
                    GetObjectSize(element, ref size);
            }
        }
        
        public static string GetFieldString(ClassFieldData[] classFieldDatas)
        {
            string str = "";

            foreach (ClassFieldData classFieldData in classFieldDatas)
            {
                str += classFieldData.name;
                str += " = ";
                Object2String(classFieldData.data, ref str);
                str += "\n";
            }

            return str;
        }

        public static void Object2String(object obj, ref string str, string separator = "")
        {
            if (obj is int intValue)
                str += intValue.ToString() + separator;
            else if (obj is float floatValue)
                str += floatValue.ToString() + separator;
            else if (obj is double doubleValue)
                str += doubleValue.ToString() + separator;
            else if (obj is bool boolValue)
                str += boolValue.ToString() + separator;
            else if (obj is string stringValue)
                str += stringValue + separator;
            else if (obj is byte[] byteArray)
            {
                string fileName = Encoding.UTF8.GetString(byteArray);
                if (!File.Exists(fileName)) return;
                FileInfo fileInfo = new FileInfo(fileName);
                str += FormatFileSize(fileInfo.Length) + separator;
            }
            else if (obj is List<object> objectArray)
            {
                str += "[";
                foreach (var element in objectArray)
                    Object2String(element, ref str, ", ");
                str = str.Substring(0, str.Length - 2);
                str += "]";
            }
        }
        public static object String2Object(string str, object sample)
        {
            if (sample is int && int.TryParse(str, out int intValue))
                return intValue;
            else if (sample is float && float.TryParse(str, out float floatValue))
                return floatValue;
            else if (sample is double && double.TryParse(str, out double doubleValue))
                return doubleValue;
            else if (sample is bool && bool.TryParse(str, out bool boolValue))
                return boolValue;
            else if (sample is byte[])
                return sample;
            else if (sample is List<object> samples)
            {
                if (str.StartsWith("[") && str.EndsWith("]"))
                {
                    str = str.Substring(1, str.Length - 2);
                    string[] elements = str.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    List<object> objects = new List<object>();
                    for (int i = 0; i < elements.Length; i++)
                    {
                        string trimmedElement = elements[i].Trim();
                        object elementObj = String2Object(trimmedElement, samples[i]);
                        objects.Add(elementObj);
                    }
                    return objects;
                }
            }
            return str;
        }

        #endregion

        public string[] GetLineValue(string fileName)
        {
            try
            {
                FileInfo fi = new FileInfo(fileName);
                string assetName = Path.GetFileNameWithoutExtension(fi.Name);
                string extension = fi.Extension.Substring(1);
                string className = GetClassName(extension);
                ClassFieldData[] fieldData = GetDefaultFieldDatas(className);
                fieldData[0].data = Encoding.UTF8.GetBytes(fileName);
                string size = FormatFileSize(GetSize(fieldData));
                string field = GetFieldString(fieldData);
                string isInclude = "true";
                return new string[] { assetName, extension, className, size, field , isInclude };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load file.\nError: " + ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        /// <summary>
        /// Displays a file open dialog and returns the names of the selected files.
        /// AllFileFilter, ABFileFilter
        /// </summary>
        /// <param name="filter">Filter string to use in file dialogs.</param>
        /// <param name="multiSelect">Allow multiple selection of files.</param>
        /// <returns>Names of the selected files. Empty string if canceled.</returns>
        public static string[] GetOpenFileNames(string filter, bool multiSelect)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = filter;
            openFileDialog.Multiselect = multiSelect;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                return openFileDialog.FileNames;
            else
                return new string[1] {""};
        }

        /// <summary>
        /// Displays a file save dialog and returns the names of the selected files.
        /// AllFileFilter, ABFileFilter
        /// </summary>
        /// <param name="fileName">Default name.</param>
        /// <param name="filter">Filter string to use in file dialogs.</param>
        /// <returns>Names of the selected files. Empty string if canceled.</returns>
        public static string GetSaveFileName(string fileName, string filter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = fileName;
            saveFileDialog.Filter = filter;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                return saveFileDialog.FileName;
            else
                return string.Empty;
        }
        public static string AllFileFilter = "All files (*.*)|*.*";
        public static string ABFileFilter = "AssetBundle (*.ab*)|*.ab*";

        public static string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;

            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return String.Format("{0:0.##} {1}", len, sizes[order]);
        }
    }
}
