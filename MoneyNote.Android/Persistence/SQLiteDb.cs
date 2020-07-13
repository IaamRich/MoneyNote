using MoneyNote.Droid;
using MoneyNote.Persistence;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]

namespace MoneyNote.Droid
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteDb()
        {

        }
        public SQLiteAsyncConnection GetConnection()
        {
            try
            {
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var path = Path.Combine(documentsPath, "MySQLite.db3");

                return new SQLiteAsyncConnection(path);
                // ================================
                // string documentsPath = System.Environment
                //.GetFolderPath(System.Environment.SpecialFolder.Personal);
                // var path =
                // Path.Combine(documentsPath, Constants.csDataBaseName);
                // var isShouldGenerateTables = false;
                // if (!System.IO.File.Exists(path))
                //     isShouldGenerateTables = true;
                // var conn = new SQLite.SQLiteConnection(path);

                // if (isShouldGenerateTables)
                //     conn.CreateTable<InfoItem>();
                // return conn;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {

            }
            return null;

        }
    }
}
