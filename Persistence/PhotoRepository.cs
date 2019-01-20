using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vincent.Core;
using Vincent.Core.Models;

namespace Vincent.Persistence
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly VincentDbContext context;
        public PhotoRepository(VincentDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return await context.Photos
            .Where(p => p.VehicleId == vehicleId)
            .ToListAsync();
        }
    }
}