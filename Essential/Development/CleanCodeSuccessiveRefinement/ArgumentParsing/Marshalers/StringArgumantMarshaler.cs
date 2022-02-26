using System;
using System.Collections.Generic;

namespace ArgumentParsing.Marshalers
{
    public class StringArgumentMarshaler : IArgumentMarshaler
    {
        private string stringValue = string.Empty;
        public object Get() => stringValue;

        public void Set(IEnumerator<string> currentArgument)
        {
            try
            {
                stringValue = currentArgument.Current;
            }
            catch (InvalidCastException ex)
            {
                throw new ArgsException(ErrorCode.MissingString, stringValue);
            }
        }
    }
}
