using System;

namespace ReinventLINQOperationsTests
{
    public class StudentTestResult : IComparable<StudentTestResult>, IEquatable<StudentTestResult>
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }

        public string TestSubject { get; private set; }
        public int TestScore { get; private set; }
        public DateTime Date { get; private set; }

        public StudentTestResult(string name, string lastName, string testSubject, int testScore, DateTime date)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (lastName == null)
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            if (testSubject == null)
            {
                throw new ArgumentNullException(nameof(testSubject));
            }

            Name = name;
            LastName = lastName;

            TestSubject = testSubject;
            Date = date;
            TestScore = testScore;
        }

        public int CompareTo(StudentTestResult other)
        {
            if (other == null)
            {
                return 1;
            }

            int result = TestScore.CompareTo(other.TestScore);
            if (result != 0)
            {
                return result;
            }

            result = TestSubject.CompareTo(other.TestSubject);
            if (result != 0)
            {
                return result;
            }

            result = Name.CompareTo(other.Name);
            if (result != 0)
            {
                return result;
            }

            result = LastName.CompareTo(other.LastName);
            if (result != 0)
            {
                return result;
            }

            result = Date.CompareTo(other.Date);
            if (result != 0)
            {
                return result;
            }

            return 0;
        }

        public bool Equals(StudentTestResult other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Name.Equals(other.Name) &&
                TestSubject.Equals(other.TestSubject) &&
                LastName.Equals(other.LastName) &&
                Date.Equals(other.Date) &&
                TestScore.Equals(other.TestScore);
        }

        public override string ToString()
        {
            return string.Format("{0} {1} / {2} [{3} / {4}]", Name, LastName, TestSubject, TestScore, Date);
        }
    }
}

