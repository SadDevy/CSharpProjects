using System;

namespace ArgumentParsing
{
    public class ArgsException : Exception
    {
        private char errorArgumentId = '\0';
        private string errorParameter = "TILT";
        private ErrorCode errorCode = ErrorCode.Ok;

        public ArgsException() { }

        public ArgsException(string message) : base(message) { }

        public ArgsException(ErrorCode errorCode, string errorParameter)
        {
            this.errorCode = errorCode;
            this.errorParameter = errorParameter;
        }

        public ArgsException(ErrorCode errorCode, char errorArgumentId, string errorParameter)
            : this(errorCode, errorParameter)
        {
            this.errorArgumentId = errorArgumentId;
        }

        public char GetErrorArgumentId() => errorArgumentId;

        public void SetErrorArgumentId(char errorArgumentId) => this.errorArgumentId = errorArgumentId;

        public string GetErrorParameter() => errorParameter;

        public void SetErrorParameter(string errorParameter) => this.errorParameter = errorParameter;

        public ErrorCode GetErrorCode() => errorCode;

        public void SetErrorCode(ErrorCode errorCode) => this.errorCode = errorCode;

        public string ErrorMessage()
        {
            switch (errorCode)
            {
                case ErrorCode.Ok:
                    throw new Exception("TILT: Should not get here.");
                case ErrorCode.UnexpectedArgument:
                    return string.Format("Argument {0} unexpected.", errorArgumentId);
                case ErrorCode.MissingString:
                    return String.Format("Could not find string parameter for {0}.",
                        errorArgumentId);
                case ErrorCode.InvalidInteger:
                    return String.Format("Argument {0} expects an integer but was {1}.",
                        errorArgumentId, errorParameter);
                case ErrorCode.MissingInteger:
                    return String.Format("Could not find integer parameter for {0}.",
                        errorArgumentId);
                case ErrorCode.InvalidDouble:
                    return string.Format("Argument {0} expects a double but was {1}",
                        errorArgumentId, errorParameter);
                case ErrorCode.MissingDouble:
                    return string.Format("Could not find double parameter for {0}.", errorArgumentId);
            }
            return string.Empty;
        }
    }
}
