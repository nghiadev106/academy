using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Data
{
    public class TeacherView
    {
        public List<Course> courses { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
    }
}
