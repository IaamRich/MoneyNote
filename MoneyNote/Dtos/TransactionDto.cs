using System.Collections.Generic;
using MoneyNote.Models;

namespace MoneyNote.Dtos
{
    public class TransactionDto
    {
        public List<Transaction> DataBase { get; set; }
    }
}
