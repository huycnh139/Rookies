using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.ViewModel.Catalog.Common
{
    public class PageResult<T>
    {
        public List<T> Items { set; get; }

        public int TotalRecord {set; get; }
    }
}
