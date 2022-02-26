using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Xml.Serialization;

namespace Entities.Models
{
    [Serializable]
    public partial class Test
    {
        public Test()
        {
        }

        [XmlIgnore]
        public int Id { get; set; }
        [XmlIgnore]
        public string Guid { get; set; }
        public string Name { get; set; }

        [XmlIgnore]
        public int? TheoryId { get; set; }

        [XmlIgnore]
        public int? ImageId { get; set; }
        public int TestTime { get; set; }
        public byte QuestionsCount { get; set; }
        public byte CorrectedAnswersCount { get; set; }
        public bool? TheoryIsShown { get; set; }

        public virtual Image Image { get; set; }
        public virtual Theory Theory { get; set; }
        public virtual List<Question> Questions { get; set; }

        public override bool Equals(object? obj)
        {
            if (!(obj is Test))
                return false;

            Test a = (Test)obj;
            bool result = Name == a.Name && TestTime == a.TestTime && QuestionsCount == a.QuestionsCount
                          && CorrectedAnswersCount == a.CorrectedAnswersCount && TheoryIsShown == a.TheoryIsShown
                          && Image == a.Image && Theory == a.Theory;

            if (Questions != null)
                result = result && Questions.SequenceEqual(a.Questions);

            return result;
        }

        public override int GetHashCode()
        {
            return TestTime.GetHashCode() ^ QuestionsCount.GetHashCode() ^ CorrectedAnswersCount.GetHashCode();
        }
    }
}
