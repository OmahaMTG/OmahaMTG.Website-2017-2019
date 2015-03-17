using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg
{
    public class PagedSet<T>
    {

        public PagedSet(int skip, int take, IQueryable<T> records)
        {
            Skiped = skip;
            Taken = take;
            Records = records.Skip(skip).Take(take);
            TotalRecords = records.Count();
        }

        public PagedSet(int totalRecords, int skipped, int taken, IEnumerable<T> records)
        {
            Skiped = skipped;
            Taken = taken;
            Records = records;
            TotalRecords = totalRecords;
        }

        public int TotalRecords { get; private set; }
        public int Skiped { get; private set; }
        public int Taken { get; private set; }
        public IEnumerable<T> Records { get; private set; }
        public int Page { get { return (Skiped / Taken) + 1; } }
        public int TotalPages { get { return (TotalRecords / Taken) + 1; } }
    }
}
