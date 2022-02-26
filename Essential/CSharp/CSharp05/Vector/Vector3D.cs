using System;
using System.Runtime.CompilerServices;

namespace Vector
{
    public class Vector3D : IEquatable<Vector3D>
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public Vector3D(double x = 0, double y = 0, double z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is Vector3D))
                return false;

            Vector3D vector = (Vector3D)obj;
            return Equals(vector);
        }

        public bool Equals(Vector3D vector)
        {   
            if (vector == null)
                return false;

            return X == vector.X && Y == vector.Y && Z == vector.Z;
        }

        public override int GetHashCode()
        {
            return 33 * (X.GetHashCode() >> 1) + 17 * (Y.GetHashCode() >> 2) + (Z.GetHashCode() >> 3);
        }

        public override string ToString()
        {
            return string.Format("{{{0}; {1}; {2}}}", X, Y, Z);
        }

        public static Vector3D operator +(Vector3D a, Vector3D b)
        {
            CheckNull(a, nameof(a), b, nameof(b));
            return new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3D operator -(Vector3D a, Vector3D b)
        {
            CheckNull(a, nameof(a), b, nameof(b));
            return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static double operator *(Vector3D a, Vector3D b)
        {
            CheckNull(a, nameof(a), b, nameof(b));
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static Vector3D Cross(Vector3D a, Vector3D b)
        {
            CheckNull(a, nameof(a), b, nameof(b));

            double x = a.Y * b.Z - a.Z * b.Y;
            double y = a.X * b.Z - a.Z * b.X;
            double z = a.X * b.Y - a.Y * b.X;

            return new Vector3D(x, -y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CheckNull(Vector3D a, string aName, Vector3D b, string bName)
        {
            if (a == null)
                throw new ArgumentNullException(aName);

            if (b == null)
                throw new ArgumentNullException(bName);
        }

        public static bool operator ==(Vector3D a, Vector3D b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (ReferenceEquals(a, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Vector3D a, Vector3D b)
        {
            return !(a == b);
        }
    }
}
