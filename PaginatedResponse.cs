using System.Collections.Generic;
using System.Linq;

namespace InsightDash.API
{
    public class PaginatedResponse<T>
    {
        public PaginatedResponse(IEnumerable<T> data, int i, int length)
        {
            // This allows queries to skip a result by the length of the results
            // then give the first length results found
            Data = data.Skip((i - 1) * length
            ).Take(length).ToList();
            Total = data.Count();
        }

        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}