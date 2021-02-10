using System;

using TinyJewel.DependencyInjection;

namespace TinyJewel.iOS.DependencyService
{
    public class FileSystemIOS : IFileSystem
    {
        public FileSystemIOS()
        {
        }

        public string GetFilePath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal).ToString();
        }
    }
}
