using YamlDotNet.RepresentationModel;

namespace UniqueBundler
{
    public class FileManager
    {
        public FileManager()
        {
            LoadClassConfig();
        }

        #region Yaml for ClassFieldData
        public struct ClassFieldData
        {
            public string name;
            public object data;
            public bool isUse;
        }

        private void LoadClassConfig()
        {
            // Check file
            if (!File.Exists(classConfigPath)) CreateClassConfig();
            GetClassNames();
            GetDefaultClassFieldData();
        }

        public string[] GetClassNames()
        {
            if(classNames != null) return classNames;

            // Load yaml
            YamlStream yaml = LoadYaml();

            // Get top node
            YamlMappingNode mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

            // Get top node keys
            List<string> keys = new List<string>();
            foreach (var entry in mapping.Children)
                keys.Add(entry.Key.ToString());
            classNames = keys.ToArray();
            return classNames;
        }
        private string[] classNames;

        private ClassFieldData[][] GetDefaultClassFieldData()
        {
            if(classesDefaultFieldDatas != null) return classesDefaultFieldDatas;

            // Load yaml
            YamlStream yaml = LoadYaml();

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
                        data = ParseData(item.Value[0]),
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
            return classesDefaultFieldDatas[classIndex];
        }
        private ClassFieldData[][] classesDefaultFieldDatas;

        private static object ParseData(YamlNode node)
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
                    objects.Add(ParseData(element));
                return objects;
            }
            
            // Dictionary
            else if (node is YamlMappingNode mapping)
            {
                var dict = new Dictionary<string, object>();
                foreach (var entry in mapping)
                {
                    var key = ParseData(entry.Key).ToString();
                    var value = ParseData(entry.Value);
                    dict[key] = value;
                }
                return dict;
            }

            // Unkonow
            else
                return null;
        }

        private void CreateClassConfig()
        {
            string text = "None:\r\n  Data:\r\n    - !!binary\r\n    - true";
            File.WriteAllText(classConfigPath, text);
        }
        private const string classConfigPath = "ClassConfig.yaml";
        #endregion

        #region Yaml for Extension
        private void LoadClassExtension()
        {

        }

        private string[][] classesExtensions;
        #endregion

        #region Yaml
        private YamlStream LoadYaml()
        {
            YamlStream yaml = new YamlStream();
            using (var reader = new StreamReader(classConfigPath))
                yaml.Load(reader);
            return yaml;
        }
        #endregion

        public string[] GetLineValue(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            string assetName = Path.GetFileNameWithoutExtension(fi.Name);
            string extension = fi.Extension.Substring(1);
            string className = "None";
            string size = "a";
            string field = "a";
            return new string[] { assetName, extension, className, size, field };
        }

        /// <summary>
        /// Displays a file open dialog and returns the names of the selected files.
        /// </summary>
        /// <param name="filter">Filter string to use in file dialogs.</param>
        /// <param name="multiSelect">Allow multiple selection of files.</param>
        /// <returns>Names of the selected files. Empty string if canceled.</returns>
        public string[] GetNames(string filter, bool multiSelect)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = filter;
                openFileDialog.Multiselect = multiSelect;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    return openFileDialog.FileNames;
            }
            return Array.Empty<string>();
        }
        public readonly string AllFileFilter = "All files (*.*)|*.*";
        public readonly string ABFileFilter = "AssetBundle (*.ab*)|*.ab*";

        private string FormatFileSize(long bytes)
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
