
using TinyJewel.DependencyInjection;
using TinyJewel.Droid.DependencyService;

[assembly: Xamarin.Forms.Dependency(typeof(FileSystemDroid))]
namespace TinyJewel.Droid.DependencyService
{
    public class FileSystemDroid : IFileSystem
    {
        public FileSystemDroid()
        {
        }

        public string GetFilePath()
        {
            return Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).ToString();
        }
    }
}
