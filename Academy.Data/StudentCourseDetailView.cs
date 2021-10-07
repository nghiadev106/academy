using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Data
{
    public class StudentCourseDetailView
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }

        public Nullable<int> Gender { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }

        public string Avatar { get; set; }

        public string CourseName { get; set; }

        public string TeacherName { get; set; }

        public Nullable<int> CountLesson { get; set; }

        public Nullable<decimal> Price { get; set; }

        public Nullable<System.DateTime> StartDate { get; set; }

        public Nullable<System.DateTime> EndDate { get; set; }
    }
}
