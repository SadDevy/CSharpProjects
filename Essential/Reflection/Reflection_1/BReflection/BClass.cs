namespace BReflection
{
    public class BClass : AClass
    {
        public BClass(int a) : base(a){ }

        public override int PublicMethod() 
            => protectedField + publicField;
    }
}
