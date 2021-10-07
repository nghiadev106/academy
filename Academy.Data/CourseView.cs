using System;

namespace Academy.Data
{
    public class CourseView
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string TeacherName { get; set; }

        public string CourseCategoryName { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public Nullable<int> CountLesson { get; set; }
        public Nullable<int> CountStudent { get; set; }

        public Nullable<decimal> Price { get; set; }

        public string Image { get; set; }

        public Nullable<System.DateTime> StartDate { get; set; }

        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }


        public Nullable<int> Status { get; set; }
    }
}
