using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ReactiveUI;
namespace MoneyNote.Models
{
    public class Category : ReactiveObject // Reactive Object exists here to realize Fody.PropertyChanged to property "IsSelected"
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public CategoryType Type { get; set; }
        public bool IsSelected { get; set; }
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CategoryType
    {
        [EnumMember(Value = "None")]
        None,
        [EnumMember(Value = "Market")]
        Market,
        [EnumMember(Value = "Bar")]
        Bar,
        [EnumMember(Value = "Transport")]
        Transport,
        [EnumMember(Value = "Business")]
        Business,
        [EnumMember(Value = "Network")]
        Network,
        [EnumMember(Value = "Entertainment")]
        Entertainment,
        [EnumMember(Value = "Gift")]
        Gift,
        [EnumMember(Value = "Salary")]
        Salary,
        [EnumMember(Value = "Earnings")]
        Earnings,
        [EnumMember(Value = "Other")]
        Other
    }
}
