using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace B.API.Controller
{

   public abstract class LookupControllerBase<TEntity>: AppControllerBase where TEntity : AppLookup
    {
        private readonly LookupRepository _lookupRepository;
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        private readonly DbSet<TEntity> _dbset;
        public LookupControllerBase(AppDbContext context, DbSet<TEntity> dbset, ILogger<LookupControllerBase<TEntity>> logger,  LookupRepository lookupRepository): base(context, logger)
        {
            _lookupRepository = lookupRepository;
            _context = context;
            _logger = logger;
            _dbset = dbset;
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<TEntity>> GetAll()
        {
            return Ok(_dbset.AsNoTracking().OrderBy(c => c.Name));
        }

        [Authorize]
        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<TEntity>> GetPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize,
            [FromQuery]string name
        ) 
        {
            var items = _lookupRepository.OrderBy<TEntity>(_lookupRepository.Filter<TEntity>(_dbset.AsNoTracking(), name), sortName);
            return Ok(_lookupRepository.Paginate(items, pageNumber, pageSize));
        }
        [Authorize]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<TEntity> Get(int id)
        {
            return Ok(_dbset.AsNoTracking().First(o => o.Id == id));
        }
        [Authorize]
        [HttpPost]
        public ActionResult<TEntity> Create([FromBody] TEntity item)
        {
            return Create<TEntity>(item, nameof(Create), (long id) => _dbset.AsNoTracking().First(o => o.Id == id));
        }
        protected ActionResult<TEntity> Update(int id, [FromBody] TEntity item)
        {
            return Update<TEntity>(id, item, (long id) => _dbset.AsNoTracking().First(o => o.Id == id));
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete (int id)
        {
            return Delete<TEntity>(id);
        }

    }

}