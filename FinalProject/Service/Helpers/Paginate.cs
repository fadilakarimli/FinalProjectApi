using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    public class Paginate<T>
    {
        public IEnumerable<T> Datas { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }

        public Paginate(IEnumerable<T> datas, int pageCount, int currentPage)
        {
            Datas = datas;
            PageCount = pageCount;
            CurrentPage = currentPage;
        }
    }
}
