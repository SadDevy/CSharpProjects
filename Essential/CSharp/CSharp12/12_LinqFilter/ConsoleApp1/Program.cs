using GeneralFilter;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {
        private static readonly MethodInfo OrderByMethod =
       typeof(Enumerable).GetMethods().Single(method =>
      method.Name == "OrderBy" && method.GetParameters().Length == 2);

        static void Main(string[] args)
        {
            StudentTestInfo a = new StudentTestInfo("Сергей", "Сергеев", "Test", DateTime.Now, 1);
            StudentTestInfo b = new StudentTestInfo("Петр", "Петров", "Test", DateTime.Now, 1);
            StudentTestInfo c = new StudentTestInfo("Семен", "Семенов", "Test", DateTime.Now, 1);
            StudentTestInfo d = new StudentTestInfo("Карл", "Семенов", "Test", DateTime.Now, 1);

            IEnumerable<StudentTestInfo> s = new[] {d, c, b, a };
            Filter<StudentTestInfo> st = new Filter<StudentTestInfo>();

            st.AndSortByAsc<int>(nameof(StudentTestInfo.Score));
            st.AndSortByAsc<string>(nameof(StudentTestInfo.TestName));
            st.AndSortByDesc<string>(nameof(StudentTestInfo.Name));

            foreach (var i in st.ApplySort(s))
                Console.WriteLine(i.Name + i.Score);

        }

        
    }
}
