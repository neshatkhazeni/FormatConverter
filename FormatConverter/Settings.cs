using FormatConverter.Models;

namespace FormatConverter
{
    public static class Settings
    {
        public static StorageSettings FtpSettings(string path)
        {
            return new StorageSettings()
            {
                Password = "khazeni",
                Username = "neshat",
                Path = path,
                Server = "ftp://161.97.145.3/",
            };
        }
    }
}
