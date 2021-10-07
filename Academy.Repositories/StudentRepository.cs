using Academy.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Repositories
{
    public interface IStudentRepository
    {
        List<Student> GetListStudent();
        StudentCourseDetailView GetStudentCourseDetail(long Id);
        Student GetStudentDetail(long Id);
        
        Student Add(Student student);
        void Update(Student student);
        Student Delete(long id);
    }
    public class StudentRepository : IStudentRepository
    {
        AcademyEntities db = new AcademyEntities();
        public Student Add(Student student)
        {
          
            db.Student.Add(student);
            db.SaveChanges();
            return student;
        }

        public Student Delete(long id)
        {
            var Student = db.Student.Find(id);
            db.Student.Remove(Student);
            db.SaveChanges();

            return Student;
        }

        public StudentCourseDetailView GetStudentCourseDetail(long Id)
        {
            var query = from s in db.Student
                        join c in db.Course on s.CourseId equals c.Id
                        join t in db.Teacher on c.TeacherId equals t.Id
                        select new StudentCourseDetailView()
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Phone=s.Phone,
                            Email=s.Email,
                            Gender=s.Gender,
                            Address=s.Address,
                            DOB=s.DOB,
                            Avatar=s.Avatar,
                            CourseName = c.Name,
                            TeacherName=t.Name,
                            CountLesson = c.CountLesson,
                            Price = c.Price,
                            StartDate = c.StartDate,
                            EndDate = c.EndDate
                        };
            return query.Where(x=>x.Id==Id).FirstOrDefault();
        }


        public List<Student> GetListStudent()
        {
            var lst = db.Student.OrderByDescending(y => y.CreateDate).ToList();
            return lst;
        }

        public Student GetStudentDetail(long Id)
        {
            var lst = db.Student.SingleOrDefault(x => x.Id == Id);
            return lst;
        }

        public void Update(Student student)
        {          
            db.Student.Attach(student);
            db.Entry(student).State = EntityState.Modified;
            db.Entry(student).Property(x => x.CreateDate).IsModified = false;
            db.SaveChanges();
        }
    }
}
