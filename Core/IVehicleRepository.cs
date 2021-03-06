using System.Collections.Generic;
using System.Threading.Tasks;
using Vincent.Core.Models;

namespace Vincent.Core
{
    public interface IVehicleRepository
    {
         Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
         Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery filter);
         void Add(Vehicle vehicle);
         void Remove(Vehicle vehicle);
    }
}