using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Entities.Models
{
    [Serializable]
    public partial class Image
    {
        public Image()
        {
        }

        [XmlIgnore]
        public int Id { get; set; }
        public byte[] Img { get; set; }

        [XmlIgnore]
        public virtual List<Test> Tests { get; set; }

        public override bool Equals(object? obj)
        {
            if (!(obj is Image))
                return false;

            Image a = (Image)obj;
            return Img.SequenceEqual(a.Img);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Img.GetHashCode();
        }
    }
}
