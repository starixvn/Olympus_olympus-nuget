using System;
using System.Collections.Generic;

namespace Olympus.SimpleDataApi
{
    public static class DictionaryExtension
    {
        public static Dictionary<string, object> SafeAdd(this Dictionary<string, object> dictionary, string key, object value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }

            return dictionary;
        }

        public static Dictionary<string, object> CloneCaseInSensitive(this Dictionary<string, object> dictionary)
        {
            var clonedDictionary = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

            foreach (var key in dictionary.Keys)
            {
                var lowerCaseKey = key.ToLower();
                if (!clonedDictionary.ContainsKey(lowerCaseKey))
                {
                    clonedDictionary.Add(lowerCaseKey, dictionary[key]);
                }
            }

            return clonedDictionary;
        }
    }
}
