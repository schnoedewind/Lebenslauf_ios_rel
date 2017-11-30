using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System.IO;
using Xamarin.Forms;
using Lebenslauf.Droid;

[assembly: Dependency(typeof(SQLiteClient))]
namespace Lebenslauf.Droid
{
    public class SQLiteClient : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "DatabaseCV.db3";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            var path = Path.Combine(documentsPath, sqliteFilename);

            var conn = new SQLite.SQLiteConnection(path);
            
             return conn;
        }
    }
}