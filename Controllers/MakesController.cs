using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vincent.Controllers.Resources;
using Vincent.Models;
using Vincent.Persistence;

namespace Vincent.Controllers
{
    public class MakesController : Controller
    {
        private readonly VincentDbContext context;
        private readonly IMapper mapper;
        public MakesController(VincentDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;

        }

        [HttpGet("api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await context.Makes.Include(m => m.Models).ToListAsync();

            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}