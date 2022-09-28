using ProjectManagement.Domain.Common;
using ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Domain.Repositories
{

    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> All { get; }

        Task<TEntity> GetById(int id);

        Task<OperationResult> AddAsync(TEntity entity);

        OperationResult Update(TEntity entity);

        Task<OperationResult> Remove(int id);

        Task<OperationResult> SaveAsync(OperationResult opr);
    }
}
