using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Data
{
    public class CourseCategoryView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
    }
}
