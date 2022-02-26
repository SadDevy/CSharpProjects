namespace BReflection
{
    public class AClass
    {
        private int privateField;
        internal int internalField;
        protected int protectedField;
        public int publicField;
        static int staticField;

        private int privateProperty { get; set; }
        internal int internalProperty { get; set; }
        protected int protectedProperty { get; set; }
        public int publicProperty { get; set; }
        static int staticProperty { get; set; }

        public AClass(int a)
        {
            privateField = a;
            internalField = a;
            protectedField = a;
            publicField = a;
            staticField = a;

            privateProperty = a;
            internalProperty = a;
            protectedProperty = a;
            publicProperty = a;
            staticProperty = a;
        }

        public virtual int PublicMethod() => publicField;
        private int PrivateMethod() => privateField;
        internal int InternalMethod() => internalField;
        protected int ProtectedMethod() => protectedField;
        static int StaticMethod() => staticField;
    }
}
