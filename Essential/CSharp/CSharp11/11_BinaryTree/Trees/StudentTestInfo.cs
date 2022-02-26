using System;

namespace BinarySearch
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
    }
}
