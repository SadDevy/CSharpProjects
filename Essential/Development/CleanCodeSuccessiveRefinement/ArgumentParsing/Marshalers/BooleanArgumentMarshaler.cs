using System.Collections.Generic;

namespace ArgumentParsing.Marshalers
{
    public class BooleanArgumentMarshaler : IArgumentMarshaler
    {
        private bool booleanValue = false;
        public object Get() => booleanValue;

        public void Set(IEnumerator<string> currentArgument) => booleanValue = true;
    }
}
