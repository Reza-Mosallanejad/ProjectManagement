using ProjectManagement.Data.Context;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Data.Repositories
{
    public class SplitRepository : BaseRepository<Split>, ISplitRepository
    {
        public SplitRepository(PMDbContext context) : base(context)
        {
        }
    }
}
