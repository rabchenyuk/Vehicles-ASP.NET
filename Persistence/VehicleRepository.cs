using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vincent.Core.Models;
using Vincent.Core;
using System.Linq;
using System;
using System.Linq.Expressions;

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

        public async Task<IEnumerable<Vehicle>> GetVehicles(VehicleQuery queryObj)
        {
            #region Filtering
            // Server-side filtering
            var query = context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .AsQueryable();

            if (queryObj.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);

            // Without any filters
            // -----------------------------
            // return await context.Vehicles
            //     .Include(v => v.Features)
            //         .ThenInclude(vf => vf.Feature)
            //     .Include(v => v.Model)
            //         .ThenInclude(m => m.Make)
            //     .ToListAsync();
            #endregion

            #region Sorting
            // Server-side sorting
            // Poor way
            // if (queryObj.SortBy == "make")
            //     query = (queryObj.IsSortAscending) ? query.OrderBy(v => v.Model.Make.Name) : query.OrderByDescending(v => v.Model.Make.Name);
            
            // if (queryObj.SortBy == "model")
            //     query = (queryObj.IsSortAscending) ? query.OrderBy(v => v.Model.Name) : query.OrderByDescending(v => v.Model.Name);
            
            // if (queryObj.SortBy == "contactName")
            //     query = (queryObj.IsSortAscending) ? query.OrderBy(v => v.ContactName) : query.OrderByDescending(v => v.ContactName);
            
            // if (queryObj.SortBy == "id")
            //     query = (queryObj.IsSortAscending) ? query.OrderBy(v => v.Id) : query.OrderByDescending(v => v.Id);

            // Server-side sorting
            // Better way
            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
                ["id"] = v => v.Id
            };

            if (queryObj.IsSortAscending)
                query = query.OrderBy(columnsMap[queryObj.SortBy]);
            else
                query = query.OrderByDescending(columnsMap[queryObj.SortBy]);
            
            #endregion

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