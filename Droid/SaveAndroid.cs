using System;
using System.IO;
using Android.Content;
using Java.IO;
using Xamarin.Forms;
using System.Threading.Tasks;
using Lebenslauf.Droid;
using Lebenslauf;
using Android.Widget;

[assembly: Dependency(typeof(SaveAndroid))]
namespace Lebenslauf.Droid
{
    class SaveAndroid : ISave
    {
        public void Save(string fileName, String contentType, MemoryStream stream)
        {
            string exception = string.Empty;
            string root = null;
            if (Android.OS.Environment.IsExternalStorageEmulated)
            {
                root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            }
            else
                root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            Java.IO.File myDir = new Java.IO.File(root + "/Lebenslauf");
            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, fileName);

            if (file.Exists()) file.Delete();

            try
            {
                FileOutputStream outs = new FileOutputStream(file);
                outs.Write(stream.ToArray());
                outs.Flush();
                outs.Close();
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            if (file.Exists() && contentType != "application/html")
            {
                Android.Net.Uri path = Android.Net.Uri.FromFile(file);
                string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(path, mimeType);
                //Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose App"));
                try
                {
                    Xamarin.Forms.Forms.Context.StartActivity(intent);
                }
                catch (Exception)
                {
                    Toast.MakeText(Xamarin.Forms.Forms.Context, "Keine Anwendung verfügbar zum Anzeigen von WORD", ToastLength.Long).Show();
                    var urlStore = Device.OnPlatform("https://itunes.apple.com/ca/app/id683290832?mt=8", "https://play.google.com/store/apps/details?id=com.microsoft.office.word", "");
                    Device.OpenUri(new Uri(urlStore));

                }

        }
        }
    }
}