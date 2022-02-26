using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralFilter
{
    public class StudentTestInfo : IComparable<StudentTestInfo>
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string TestName { get; private set; }
        public DateTime PassingDate { get; private set; }
        public int Score { get; private set; }

        public StudentTestInfo(string name, string surname, string testName, DateTime passingDate, int score)
        {
            Name = name;
            Surname = surname;
            TestName = testName;
            PassingDate = passingDate;
            Score = score;
        }

        public int CompareTo(StudentTestInfo a)
        {
            int result = Score.CompareTo(a.Score);
            if (result != 0) return result;

            result = Name.CompareTo(a.Name);
            if (result != 0) return result;

            result = Surname.CompareTo(a.Surname);
            if (result != 0) return result;

            result = TestName.CompareTo(a.TestName);
            if (result != 0) return result;

            return PassingDate.CompareTo(a.PassingDate);
        }

        public override string ToString()
        {
            return string.Format("{0} {1} at {2} passed {3} with score {4}", Name, Surname, PassingDate, TestName, Score);
        }
    }
}
