using FormatConverter.Interface;
using System.IO;

namespace FormatConverter.Implementations.Storages
{
    public class FileSystemStorage : IStorage
    {
        private readonly string path;

        public FileSystemStorage(string path)
        {
            this.path = path;
        }
        public string Read()
        {
            if (File.Exists(path))
                return File.ReadAllText(path);
            else
                throw new FileNotFoundException();
        }

        public void Write(string content)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path))) 
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, content);
        }
    }
}
