using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Entities.Models
{
    [Serializable]
    public partial class Theory
    {
        public Theory()
        {
        }

        [XmlIgnore]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        [XmlIgnore]
        public virtual List<Test> Tests { get; set; }

        public override bool Equals(object? obj)
        {
            if (!(obj is Theory))
                return false;

            Theory a = (Theory)obj;
            return Description == a.Description && Url == a.Url;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Description.GetHashCode() ^ Url.GetHashCode();
        }
    }
}
