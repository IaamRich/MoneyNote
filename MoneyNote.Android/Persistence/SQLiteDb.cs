using System;
using System.IO;
using MoneyNote.Droid;
using MoneyNote.Persistence;
using SQLite;
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
