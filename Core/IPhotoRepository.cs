using System.Collections.Generic;
using System.Threading.Tasks;
using Vincent.Core.Models;

namespace Vincent.Core
{
    public interface IPhotoRepository
    {
         Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}