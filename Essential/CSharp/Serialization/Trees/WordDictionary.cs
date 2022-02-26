using System.Collections.Generic;
using System.Xml.Serialization;

namespace BinarySearch
{
    [XmlRoot(ElementName = "Dictionary")]
    public class WordDictionary : KeyValuePairTree<string, KeyValuePairTree<string, string[]>>
    {
        public WordDictionary() : base() { }
        public WordDictionary(IComparer<KeyValue<string, KeyValuePairTree<string, string[]>>> comparer)
                               : base(comparer) { }
        public WordDictionary(ICollection<KeyValue<string, KeyValuePairTree<string, string[]>>> collection) 
                               : base(collection) { }
        public WordDictionary(ICollection<KeyValue<string, KeyValuePairTree<string, string[]>>> collection, 
                               IComparer<KeyValue<string, KeyValuePairTree<string, string[]>>> comparer)
                               : base(collection, comparer) { }

        public bool TryGetWords(string key, string language, out string[] words)
        {
            words = null;

            if (key == null || language == null)
                return false;

            KeyValuePairTree<string, string[]> values;
            if (!TryGetValue(key, out values))
                return false;

            if (!values.TryGetValue(language, out words))
                return false;

            return true;
        }
    }
}
