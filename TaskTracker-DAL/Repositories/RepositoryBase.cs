using System.Reflection;
using System.Linq.Dynamic.Core;
using System.Text;
using TaskTracker_DAL.Interfaces;
using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    public IQueryable<T> SortItems(IQueryable<T> items, QueryStringParameters parameters)
    {
        if (!string.IsNullOrEmpty(parameters.Sort))
        {
            var sortFields = parameters.Sort.Split(',');
            StringBuilder orderQueryBuilder = new();

            PropertyInfo[] propertyInfo = typeof(Project).GetProperties();

            foreach (var field in sortFields)
            {
                string sortOrder = "ascending";
                string sortField = field.Trim();

                if (sortField.StartsWith('-'))
                {
                    sortField = sortField.TrimStart('-');
                    sortOrder = "descending";
                }

                PropertyInfo? property = propertyInfo.FirstOrDefault(x =>
                    x.Name.Equals(sortField,
                    StringComparison.OrdinalIgnoreCase));

                if (property is null)
                {
                    continue;
                }
                else
                {
                    orderQueryBuilder.Append($"{property.Name.ToString()} {sortOrder}, ");
                }

                string orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

                if (!string.IsNullOrWhiteSpace(orderQuery))
                {
                    items = items.OrderBy(orderQuery);
                }
            }
        }

        return items;
    }
}
