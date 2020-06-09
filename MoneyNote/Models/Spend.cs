using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MoneyNote.Models
{
    public class Spend : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private int _amount;

        [MaxLength(7)]
        public int Amount
        {
            get { return _amount; }
            set
            {
                if (_amount == value)
                {
                    return;
                }
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        private void OnPropertyChanged([CallerMemberName]string AmountProperty = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(AmountProperty));
        }
        [MaxLength(50)]
        public string WhereText { get; set; }
        public DateTime TransactionDate { get; set; }

    }
}
