using System;
using System.IO;
using System.Text;

namespace File
{
    public class FileArray : IDisposable
    {
        private FileStream file;
        private StreamReader fileReader;
        private StreamWriter fileWriter;

        private bool _isDisposed = false;

        private int symbolsCount = 1;
        private int symbolIndex = 0;

        public char this[int index]
        {
            get
            {
                file.Seek(index, SeekOrigin.Begin);

                char[] buffer = new char[symbolsCount];
                if (fileReader.Read(buffer, 0, buffer.Length) == 0)
                    throw new InvalidOperationException("Символ не считан.");

                return buffer[symbolIndex];
            }

            set
            {
                file.Seek(index, SeekOrigin.Begin);

                fileWriter.Write(value);
                fileWriter.Flush();
            }
        }

        public long Length { get => file.Length; }

        private FileArray(string filePath, int length)
        {
            OpenStream(filePath, FileMode.Create);
            file.SetLength(length);
        }

        private FileArray(string filePath)
        {
            OpenStream(filePath, FileMode.Open);
        }

        private void OpenStream(string filePath, FileMode fileMode)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            file = new FileStream(filePath, fileMode, FileAccess.ReadWrite);

            Encoding encoding = Encoding.GetEncoding(1251);
            fileReader = new StreamReader(file, encoding);
            fileWriter = new StreamWriter(file, encoding);
        }

        public static FileArray Create(string filePath, int length)
        {
            if (length < 0)
                throw new InvalidOperationException("Размер файла не может быть отрицательным.");

            return new FileArray(filePath, length);
        }

        public static FileArray Read(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                throw new InvalidOperationException(string.Format("Файл {0} не существует.", filePath));

            return new FileArray(filePath);
        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                fileWriter.Close();
                fileReader.Close();
                file.Close();
            }

            _isDisposed = true;
        }
    }
}
