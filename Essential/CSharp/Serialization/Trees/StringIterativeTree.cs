using BinaryTee;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BinarySearch
{
    public class StringIterativeTree : IterativeTree<string>, IXmlSerializable
    {
        public StringIterativeTree(IComparer<string> comparer) : base(comparer) { }
        public StringIterativeTree(ICollection<string> collection) : base(collection) { }
        public StringIterativeTree(ICollection<string> collection, IComparer<string> comparer)
            : base(collection, comparer) { }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Node<string>));
            serializer.Serialize(writer, Root);
        }

        public void ReadXml(XmlReader reader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Node<string>));
            Root =  (Node<string>)serializer.Deserialize(reader);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

    }
}
