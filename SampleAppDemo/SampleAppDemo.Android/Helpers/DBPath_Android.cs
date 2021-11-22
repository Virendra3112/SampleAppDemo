using SampleAppDemo.Droid.Helpers;
using SampleAppDemo.Helpers;
using Xamarin.Forms;

//[assembly: Dependency(typeof(DBPath_Android))]
namespace SampleAppDemo.Droid.Helpers
{
    public static class DBPath_Android //: IDBPath
    {
        public static string GetDatabasePath(string databaseName)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(path, databaseName);
        }
    }
}