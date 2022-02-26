using System.Collections.Generic;

namespace ArgumentParsing.Marshalers
{
    public interface IArgumentMarshaler
    {
        object Get();
        void Set(IEnumerator<string> currentArgument);
    }
}
