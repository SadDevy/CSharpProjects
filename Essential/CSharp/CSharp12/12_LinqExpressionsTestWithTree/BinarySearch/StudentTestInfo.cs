using System;

namespace BinarySearch
{
    public class StudentTestInfo : IComparable<StudentTestInfo>, IEquatable<StudentTestInfo>
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
            if (Score.CompareTo(a.Score) != 0)
                return Score.CompareTo(a.Score);

            if (Name.CompareTo(a.Name) != 0)
                return Name.CompareTo(a.Name);

            if (Surname.CompareTo(a.Surname) != 0)
                return Surname.CompareTo(a.Surname);

            if (TestName.CompareTo(a.TestName) != 0)
                return TestName.CompareTo(a.TestName);

            return PassingDate.CompareTo(a.PassingDate);
        }

        public bool Equals(StudentTestInfo a)
        {
            if (a == null)
                return false;

            return CompareTo(a) == 0;
        }
    }
}
