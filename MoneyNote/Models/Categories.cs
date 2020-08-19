using System.Collections.Generic;
using I18NPortable;

namespace MoneyNote.Models
{
    public static class Categories
    {
        public static II18N Strings => I18N.Current;
        public static List<CategoryDto> GetAllSpendingCategories()
        {
            return new List<CategoryDto>
            {
                new CategoryDto { Id = 0, Type = CategoryType.Market, Name = Strings["type_market"], Image = "market_shop.png"},
                new CategoryDto { Id = 1, Type = CategoryType.Bar, Name = Strings["type_bar"], Image = "restaurant_bar_bistro.png"},
                new CategoryDto { Id = 2, Type = CategoryType.Transport, Name = Strings["type_transport"], Image = "transport.png"},
                new CategoryDto { Id = 3, Type = CategoryType.Business, Name = Strings["type_business"], Image = "business.png"},
                new CategoryDto { Id = 4, Type = CategoryType.Network, Name = Strings["type_network"], Image = "network_products.png"},
                new CategoryDto { Id = 5, Type = CategoryType.Entertainment, Name = Strings["type_entertainment"], Image = "entertainment.png"},
            };
        }
        public static List<CategoryDto> GetAllAddingCategories()
        {
            return new List<CategoryDto>
            {
                new CategoryDto { Id = 0, Type = CategoryType.Salary, Name = Strings["type_salary"], Image = "salary.png"},
                new CategoryDto { Id = 1, Type = CategoryType.Earnings, Name = Strings["type_earnings"], Image = "additional_earnings.png"},
                new CategoryDto { Id = 2, Type = CategoryType.Gift, Name = Strings["type_gift"], Image = "gift.png"},
                new CategoryDto { Id = 3, Type = CategoryType.Other, Name = Strings["type_other"], Image = "other.png"}
            };
        }
    }
}
