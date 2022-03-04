using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAdvanced
{
    class ProductCountByCategory
    {
        public int? CategoryId { get; set; }
        public int ProductCount { get; set; }

        public override string ToString()
        {
            return $"Kategori : {CategoryId}\tÜrün Sayısı :{ProductCount}";
        }

    }
}
