using ProductSystem.Models;
using System.Linq;

namespace ProductSystem.Repositories.Interface
{
    public interface IRepository
    {
        public NorthwindContext _context { get; }
        public void Create<T>(T entity) where T : class;
        public void Update<T>(T entity) where T : class;
        public void Delete<T>(T entity) where T : class;
        public IQueryable<T> GetAll<T>() where T : class;
        public void Save();

    }
}
