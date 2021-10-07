using Academy.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Academy.Repositories
{
    public interface ICourseRepository
    {
        List<CourseView> GetListCourse();
        List<Course> GetListCourseHome();
        Course GetCourseDetail(long Id);
        Course Add(Course courseModel);
        void Update(Course courseModel);
        Course Delete(long id);
    }
    public class CourseRepository : ICourseRepository
    {
        AcademyEntities db = new AcademyEntities();
        public Course Add(Course course)
        {

            db.Course.Add(course);
            db.SaveChanges();
            return course;
        }

        public Course Delete(long id)
        {
            var course = db.Course.Find(id);
            db.Course.Remove(course);
            db.SaveChanges();
            return course;
        }

        public List<CourseView> GetListCourse()
        {           
            var query = from c in db.Course join cc in db.CourseCategory on c.CourseCategoryId equals cc.Id
                        join t in db.Teacher on c.TeacherId equals t.Id
                        select new CourseView()
                        {
                            Id = c.Id,
                            Name=c.Name,
                            TeacherName =t.Name,
                            CourseCategoryName=cc.Name,
                            Description=c.Description,
                            Note=c.Note,
                            CountLesson=c.CountLesson,
                            Price=c.Price,
                            StartDate=c.StartDate,
                            EndDate=c.EndDate,
                            Status=c.Status,
                            Image=c.Image,
                            CountStudent=c.Student.Where(x=>x.Status==1).Count()
                        };
            return query.OrderBy(x => x.Id).ToList() ;
        }

        public List<Course> GetListCourseHome()
        {
            var lst = db.Course.OrderBy(y => y.CreateDate).Where(x=>x.Status==1).ToList();
            return lst;
        }

        public Course GetCourseDetail(long Id)
        {
            var lst = db.Course.SingleOrDefault(x => x.Id == Id);
            return lst;
        }

        public void Update(Course course)
        {
            db.Course.Attach(course);
            db.Entry(course).State = EntityState.Modified;
            db.Entry(course).Property(x => x.CreateDate).IsModified = false;
            db.SaveChanges();
        }
    }
}
