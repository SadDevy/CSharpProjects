using System.Text;

namespace MessagePublisherUI
{
    public abstract class Network
    {
        public string userName;
        public string password;

        public Network() { }

        public bool Post(string message)
        {
            if (LogIn(userName, password))
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                bool result = SendData(data);
                LogOut();

                return result;
            }

            return false;
        }

        public abstract bool LogIn(string userName, string password);
        public abstract bool SendData(byte[] data);
        public abstract void LogOut();
    }
}
