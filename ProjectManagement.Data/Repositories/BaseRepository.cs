using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data.Context;
using ProjectManagement.Domain.Common;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private PMDbContext _context;
        private DbSet<TEntity> _entities;

        public BaseRepository(PMDbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> All => _entities.Where(e => !e.IsDelete);

        public virtual async Task<OperationResult> AddAsync(TEntity entity)
        {
            var opr = new OperationResult();
            try
            {
                await _entities.AddAsync(entity);
            }
            catch (Exception ex)
            {
                opr = OperationResult.Fail(exception: ex);
            }
            return opr;
        }

        public virtual async Task<OperationResult> Remove(int id)
        {
            var opr = new OperationResult();
            try
            {
                var entity = await GetById(id);

                if (entity == null)
                    return OperationResult.Fail("Not Found");

                entity.IsDelete = true;
                _entities.Update(entity);
            }
            catch (Exception ex)
            {
                opr = OperationResult.Fail(exception: ex);
            }
            return opr;
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await All.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<OperationResult> SaveAsync(OperationResult opr)
        {
            try
            {
                if (!opr.Status)
                    return opr;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                opr = OperationResult.Fail(exception: ex);
            }
            return opr;
        }

        public virtual OperationResult Update(TEntity entity)
        {
            var opr = new OperationResult();
            try
            {
                _entities.Update(entity);
            }
            catch (Exception ex)
            {
                opr = OperationResult.Fail(exception: ex);
            }
            return opr;
        }
    }
}
