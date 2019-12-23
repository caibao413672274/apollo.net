using Com.Ctrip.Framework.Apollo.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Ctrip.Framework.Apollo.Core.Utils
{
    public class Properties
    {
        private readonly Dictionary<string, string> _dict;

        public Properties() => _dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public Properties(IDictionary<string, string>? dictionary)
        {
            _dict = dictionary == null
                ? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                : new Dictionary<string, string>(dictionary, StringComparer.OrdinalIgnoreCase)

            ;
            decryptValues(_dict);
        }
        private void decryptValues(IDictionary<string, string>? dict) {

            //如果配置加密，则解密
            
            List<string> keys = new List<string>(dict!.Keys);
            foreach (string key in keys)
            {
                string value = dict[key];
                if (value.StartsWith("ENC(") && value.EndsWith(")"))
                {
                    try
                    {
                        String decryptValue = AthenaEncryptHelper.DecryptAES(value.Substring(4, value.Length - 5));

                        dict[key] = decryptValue;
                    }
                    catch (Exception ex)
                    {
                        //  ex.Message;
                    }
                }
            }
        }
        public Properties( Properties source) => _dict = source._dict;

        public Properties( string filePath)
        {
            if (File.Exists(filePath))
            {
                using var file = new StreamReader(filePath, Encoding.UTF8);
                using var reader = new JsonTextReader(file);
                _dict = new Dictionary<string, string>(new JsonSerializer().Deserialize<IDictionary<string, string>>(reader), StringComparer.OrdinalIgnoreCase);
            }
            else
                _dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            decryptValues(_dict);

        }

        public bool ContainsKey(string key) => _dict.ContainsKey(key);

        public string? GetProperty(string key)
        {
            _dict.TryGetValue(key, out var result);

            return result;
        }

        public ISet<string> GetPropertyNames() => new HashSet<string>(_dict.Keys);

        public void Store(string filePath)
        {
            using var file = new StreamWriter(filePath, false, Encoding.UTF8);
            new JsonSerializer().Serialize(file, _dict);
        }
    }
}
