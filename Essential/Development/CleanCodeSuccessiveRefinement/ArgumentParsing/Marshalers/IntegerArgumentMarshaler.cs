using System;
using System.Collections.Generic;

namespace ArgumentParsing.Marshalers
{
    public class IntegerArgumentMarshaler : IArgumentMarshaler
    {
        private int intValue = 0;
        public object Get() => intValue;

        public void Set(IEnumerator<string> currentArgument)
        {
            string parameter = null;
            try
            {
                parameter = currentArgument.Current;
                intValue = int.Parse(parameter);
            }
            catch (InvalidCastException ex)
            {
                throw new ArgsException();
            }
            catch (FormatException ex)
            {
                throw new ArgsException(ErrorCode.InvalidInteger, parameter);
            }
        }
    }
}
