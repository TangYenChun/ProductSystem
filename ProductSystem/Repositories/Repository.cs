using Microsoft.EntityFrameworkCore;
using ProductSystem.Models;
using ProductSystem.Repositories.Interface;
using System.Linq;

namespace ProductSystem.Repositories
{
    public class Repository : IRepository
    {
        private readonly NorthwindContext _context;
        public Repository(NorthwindContext context)
        {
            _context = context;
        }

        public void Create<T>(T entity) where T : class
        {
            _context.Entry<T>(entity).State = EntityState.Added;
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        NorthwindContext IRepository._context { get { return _context; } }
    }
}
