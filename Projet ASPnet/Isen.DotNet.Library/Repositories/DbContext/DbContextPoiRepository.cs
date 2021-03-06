using System.Collections.Generic;
using System.Linq;
using Isen.DotNet.Library.Data;
using Isen.DotNet.Library.Models.Base;
using Isen.DotNet.Library.Models.Implementation;
using Isen.DotNet.Library.Repositories.Base;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Isen.DotNet.Library.Repositories.DbContext
{
    public class DbContextPoiRepository :
        BaseDbContextRepository<Poi>, IPoiRepository
    {
        public DbContextPoiRepository(
            ILogger<DbContextPoiRepository> logger, 
            ApplicationDbContext context) 
            : base(logger, context)
        {
        }

         public override IQueryable<Poi> Includes(
            IQueryable<Poi> queryable)
                => queryable.Include(po => po.Address).ThenInclude(ad => ad.Commune).ThenInclude(d => d.Departement)
                .Include(po => po.Category);
                    
    }
}