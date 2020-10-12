using System;
using System.IO;

namespace ClassWork13
{
    public class FileLogWriter : AbstractLogWriter
    {
        private readonly string _fileName;
        public FileLogWriter(string path = "something.txt")
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException($"\"{nameof(path)}\" cannot be null");
            }
            _fileName = path;
        }
        protected override void Write(string message, Type logType)
        {
            var text = Message(message, logType);
            File.WriteAllText(_fileName, text);
        }
    }
}
