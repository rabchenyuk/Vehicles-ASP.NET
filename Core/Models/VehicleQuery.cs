using Vincent.Extensions;

namespace Vincent.Core.Models
{
    public class VehicleQuery : IQueryObject
    {
        public int? MakeId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
    }
}