using System.Collections.Generic;
using I18NPortable;

namespace MoneyNote.Models
{
    public class Categories
    {
        public static II18N Strings => I18N.Current;
        public static List<Category> GetAll(int minId, int maxId)
        {
            var list = new List<Category>
            {
                //spend types
                new Category { Id = 0, Type = CategoryType.Market, Name = Strings["type_market"], Image = "market_shop.png", IsSelected = false},
                new Category { Id = 1, Type = CategoryType.Bar, Name = Strings["type_bar"], Image = "restaurant_bar_bistro.png", IsSelected = false},
                new Category { Id = 2, Type = CategoryType.Transport, Name = Strings["type_transport"], Image = "transport.png", IsSelected = false},
                new Category { Id = 3, Type = CategoryType.Business, Name = Strings["type_business"], Image = "business.png", IsSelected = false},
                new Category { Id = 4, Type = CategoryType.Network, Name = Strings["type_network"], Image = "network_products.png", IsSelected = false},
                new Category { Id = 5, Type = CategoryType.Entertainment, Name = Strings["type_entertainment"], Image = "entertainment.png", IsSelected = false},
                //add types
                new Category { Id = 6, Type = CategoryType.Salary, Name = Strings["type_salary"], Image = "salary.png", IsSelected = false},
                new Category { Id = 7, Type = CategoryType.Earnings, Name = Strings["type_earnings"], Image = "additional_earnings.png", IsSelected = false},
                new Category { Id = 8, Type = CategoryType.Gift, Name = Strings["type_gift"], Image = "gift.png", IsSelected = false},
                new Category { Id = 9, Type = CategoryType.Other, Name = Strings["type_other"], Image = "other.png", IsSelected = false}
            };
            var answer = new List<Category>();
            foreach (var item in list)
            {
                if (item.Id >= minId && item.Id <= maxId) answer.Add(item);
            }
            return answer;
        }
    }
}
