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
    public class FeaturesController : Controller
  {
    private readonly VincentDbContext context;
    private readonly IMapper mapper;
    public FeaturesController(VincentDbContext context, IMapper mapper)
    {
      this.mapper = mapper;
      this.context = context;
    }

    [HttpGet("/api/features")]
    public async Task<IEnumerable<FeatureResource>> GetFeatures()
    {
      var features = await context.Features.ToListAsync();
      
      return mapper.Map<List<Feature>, List<FeatureResource>>(features); 
    }
  }
}