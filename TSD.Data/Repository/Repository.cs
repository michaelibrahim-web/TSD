using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Interfaces.Repository;
using global::TSD.Data;
using Microsoft.EntityFrameworkCore;
namespace TSD.Domain.Entities;



    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly TSD_DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(TSD_DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // Note: EF Core tracks changes automatically, so Update just ensures tracking is active.
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            // This is usually called by a Unit of Work or the Service layer, 
            // but included here for completeness.
            return await _context.SaveChangesAsync();
        }
    }
