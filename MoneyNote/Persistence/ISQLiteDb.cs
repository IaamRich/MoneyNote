using SQLite;

namespace MoneyNote.Persistence
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
