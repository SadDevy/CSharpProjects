using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;

namespace ExceptionRegeneration
{
    [Serializable]
    public class BaseException : ApplicationException
    {
        public static class DataKeys
        {
            public const string Created = "Created";
            public const string Rethrown = "Rethrown";
        }
        
        private const int constructorIndex = 2;

        public Guid Guid { get; private set; }
        public Version NetVersion { get; private set; }
        public string ConstructorName { get; private set; }
        public BaseException() : base()
        {
            Init();
        }

        public BaseException(string message) : base(message)
        {
            Init();
        }

        public BaseException(string message, Exception innerException) : base(message, innerException)
        {
            Init();
        }

        protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Guid = (Guid)info.GetValue(nameof(Guid), typeof(Guid));
            NetVersion = (Version)info.GetValue(nameof(NetVersion), typeof(Version));
            ConstructorName = (string)info.GetValue(nameof(ConstructorName), typeof(string));

            Data.Add(DataKeys.Created, DateTime.Now);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(Guid), Guid, typeof(Guid));
            info.AddValue(nameof(NetVersion), NetVersion, typeof(Version));
            info.AddValue(nameof(ConstructorName), ConstructorName, typeof(string));
        }

        private void Init()
        {
            Guid = Guid.NewGuid();
            NetVersion = Environment.Version;
            ConstructorName = GetMethodSignature(constructorIndex);

            Data.Add(DataKeys.Created, DateTime.Now);
        }
        private static string GetMethodSignature(int methodIndex)
        {
            StackTrace stackTrace = new StackTrace();

            MethodBase method = stackTrace.GetFrame(methodIndex).GetMethod();
            return method.ToString();
        }
    }
}
