using TaskTracker_DAL.Models;

namespace TaskTracker_DAL.Interfaces;

public interface IRepositoryBase<T> where T : class
{
    IQueryable<T> SortItems(IQueryable<T> items, QueryStringParameters parameters);
}
