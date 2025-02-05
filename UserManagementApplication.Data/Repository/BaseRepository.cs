﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementApplication.Data.Repository.Interfaces;

namespace UserManagementApplication.Data.Repository
{
    public class BaseRepository<TType, TId> : IRepository<TType, TId>
        where TType : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TType> dbSet;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<TType>();
        }
        public TType GetById(TId id)
        {
            TType entity = dbSet
                .Find(id);

            return entity;
        }

        public async Task<TType> GetByIdAsync(TId id)
        {
            TType entity = await dbSet
                .FindAsync(id);

            return entity;
        }

        public IEnumerable<TType> GetAll()
        {
            return this.dbSet.ToList();
        }

        public async Task<IEnumerable<TType>> GetAllAsync()
        {
            return await this.dbSet.ToListAsync();
        }

        public IQueryable<TType> GetAllAttached()
        {
            return this.dbSet.AsQueryable();
        }

        public void Add(TType item)
        {
            dbSet.Add(item);
        }

        public async Task AddAsync(TType item)
        {
            await dbSet.AddAsync(item);
        }


        public bool Update(TType item)
        {
            try
            {
                dbSet.Attach(item);
                _context.Entry(item).State = EntityState.Modified;

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TType item)
        {
            try
            {
                dbSet.Attach(item);
                _context.Entry(item).State = EntityState.Modified;

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
