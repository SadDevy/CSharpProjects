using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Polynomials
{
    public class Polynomial : IEquatable<Polynomial>
    {
        private double[] coefficients;

        public int Degree { get { return GetDegree(coefficients); } }

        public int Length { get { return coefficients.Length; } }

        public double this[int index]
        {
            get
            {
                return coefficients[index];
            }
            private set
            {
                coefficients[index] = value;
            }
        }

        public Polynomial(params double[] coefficients)
        {
            if (coefficients == null)
                throw new ArgumentNullException(nameof(coefficients));

            if (coefficients.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(coefficients));

            this.coefficients = CloneNormalized(coefficients);
        }

        static private double[] CloneNormalized(double[] coefficients)
        {
            int degree = GetDegree(coefficients);

            double[] cloned = new double[degree + 1];
            Array.Copy(coefficients, cloned, degree + 1);

            return cloned;
        }

        private static int GetDegree(double[] coefficients)
        {
            for (int i = coefficients.Length - 1; i >= 0; i--)
                if (coefficients[i] != 0)
                    return i;
            return 0;
        }

        private static int GetDegree(Polynomial polynomial)
        {
            for (int i = polynomial.Degree; i >= 0; i--)
                if (polynomial[i] != 0)
                    return i;

            return 0;
        }

        static private double[] CloneNormalized(Polynomial polynomial)
        {
            int degree = GetDegree(polynomial);

            double[] cloned = new double[degree + 1];
            for (int i = 0; i <= degree; i++)
                cloned[i] = polynomial[i];

            return cloned;
        }

        public Polynomial(Polynomial polynomial)
        {
            CheckNull(polynomial, nameof(polynomial));
            coefficients = CloneNormalized(polynomial);
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is Polynomial))
                return false;

            Polynomial polynomial = (Polynomial)obj;
            return Equals(polynomial);
        }

        public bool Equals(Polynomial a)
        {
            if (a == null)
                return false;

            if (Degree != a.Degree)
                return false;

            for (int i = 0; i <= a.Degree; i++)
            {
                if (a[i] != coefficients[i])
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            int result = 0;
            for (int i = 0; i < coefficients.Length; i++)
                result ^= (result << 5) + (result >> 2) + i + coefficients[i].GetHashCode();

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CheckNull(Polynomial a, string aName)
        {
            if (a == null)
                throw new ArgumentNullException(aName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CheckNull(Polynomial a, string aName, Polynomial b, string bName)
        {
            CheckNull(a, aName);
            CheckNull(b, bName);
        }

        public static Polynomial operator +(Polynomial a, Polynomial b)
        {
            CheckNull(a, nameof(a), b, nameof(b));

            int maxLength = Math.Max(a.Degree, b.Degree) + 1;

            double[] result = new double[maxLength];
            for (int i = 0; i < maxLength; i++)
            {
                if (i <= a.Degree)
                {
                    result[i] += a[i];
                }

                if (i <= b.Degree)
                {
                    result[i] += b[i];
                }
            }

            return new Polynomial(result);
        }

        public static Polynomial operator -(Polynomial a, Polynomial b)
        {
            CheckNull(a, nameof(a), b, nameof(b));

            return a + b * (-1);
        }

        public static Polynomial operator *(Polynomial a, double value)
        {
            CheckNull(a, nameof(a));

            double[] result = new double[a.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = a[i];

            for (int i = 0; i < result.Length; i++)
                result[i] *= value;

            return new Polynomial(result);
        }

        public static Polynomial operator *(Polynomial a, Polynomial b)
        {
            CheckNull(a, nameof(a), b, nameof(b));

            double[] result = new double[a.Degree + b.Degree + 1];
            for (int i = 0; i <= a.Degree; i++)
                for (int j = 0; j <= b.Degree; j++)
                    result[i + j] += a[i] * b[j];

            return new Polynomial(result);
        }

        public static bool operator ==(Polynomial a, Polynomial b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (ReferenceEquals(a, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Polynomial a, Polynomial b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = Degree; i >= 0; i--)
            {
                bool needSign = i == Degree;
                string monomial = GetMonomial(needSign, "{0:0.###;-0.###;0}{1:x^0}",
                                             "{0: + 0.###; - 0.###; + 0}{1:x^0}", coefficients[i], i);

                result.Append(monomial);
            }

            return result.ToString();
        }

        private string GetMonomial(bool needSign, string signedFormat, string unsignedFormat, double value, double power)
        {
            string resultFormat = needSign ? signedFormat : unsignedFormat;
            return string.Format(resultFormat, value, power);
        }

        public string ToStringWhole()
        {
            if (Degree == 0)
                return 0.ToString();

            StringBuilder result = new StringBuilder();
            for (int i = Degree; i >= 0; i--)
            {
                if (coefficients[i] == 0)
                    continue;

                string monomial;
                if (i == Degree)
                {
                    monomial = string.Format("{0:0.###;-0.###;#}{1:x^#;x^#;#}", coefficients[Degree], Degree);
                }
                else
                {
                    bool needPower = i == 1;
                    monomial = GetMonomial(needPower, "{0: + 0.###; - 0.###;#}{1:x'}",
                                          "{0: + 0.###; - 0.###;#}{1:x^#;x^#;#}", coefficients[i], i);
                }

                result.Append(monomial);
            }

            return result.ToString();
        }
    }
}

