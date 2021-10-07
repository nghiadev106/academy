using Academy.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Repositories
{
    public interface ICourseCategoryRepository
    {
        List<CourseCategory> GetListCourseCategory();
        CourseCategory GetCourseCategoryDetail(long Id);
        CourseCategory Add(CourseCategory courseCategoryModel);
        void Update(CourseCategory courseCategoryModel);
        CourseCategory Delete(long id);
    }
    public class CourseCategoryRepository : ICourseCategoryRepository
    {
        AcademyEntities db = new AcademyEntities();
        public CourseCategory Add(CourseCategory courseCategory)
        {

            db.CourseCategory.Add(courseCategory);
            db.SaveChanges();
            return courseCategory;
        }

        public CourseCategory Delete(long id)
        {
            var CourseCategory = db.CourseCategory.Find(id);
            db.CourseCategory.Remove(CourseCategory);
            db.SaveChanges();

            return CourseCategory;
        }
      
        public List<CourseCategory> GetListCourseCategory()
        {
            var lst = db.CourseCategory.OrderBy(y => y.Createdate).ToList();
            return lst;
        }

        public CourseCategory GetCourseCategoryDetail(long Id)
        {
            var lst = db.CourseCategory.SingleOrDefault(x => x.Id == Id);
            return lst;
        }

        public void Update(CourseCategory courseCategory)
        {
            db.CourseCategory.Attach(courseCategory);
            db.Entry(courseCategory).State = EntityState.Modified;
            db.Entry(courseCategory).Property(x => x.Createdate).IsModified = false;
            db.SaveChanges();
        }
    }
}
