using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ReactiveUI;

namespace MoneyNote.Models
{
    public class Transaction : ReactiveObject
    {
        public int Id { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TransactionType Type { get; set; }
        public decimal Value { get; set; }
        public Category Category { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TransactionType
    {
        [EnumMember(Value = "None")]
        None,
        [EnumMember(Value = "Save")]
        Save,
        [EnumMember(Value = "Spend")]
        Spend,
        [EnumMember(Value = "Add")]
        Add
    }
}
