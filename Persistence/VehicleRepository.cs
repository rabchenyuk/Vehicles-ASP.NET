using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vincent.Core.Models;
using Vincent.Core;
using System.Linq;

namespace Vincent.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VincentDbContext context;
        public VehicleRepository(VincentDbContext context)
        {
            this.context = context;
        }

        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Vehicles.FindAsync(id);

            return await context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles(Filter filter)
        {
            var query = context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .AsQueryable();

            if (filter.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == filter.MakeId.Value);

            // Without any filters
            // return await context.Vehicles
            //     .Include(v => v.Features)
            //         .ThenInclude(vf => vf.Feature)
            //     .Include(v => v.Model)
            //         .ThenInclude(m => m.Make)
            //     .ToListAsync();

            return await query.ToListAsync();
        }

        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Remove(vehicle);
        }
    }
}