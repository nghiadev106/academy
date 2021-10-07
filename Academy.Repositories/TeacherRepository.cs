using Academy.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Repositories
{
    public interface ITeacherRepository
    {
        List<Teacher> GetListTeacherAdmin();
        List<TeacherView> GetListTeacherAndCouseAdmin();
        List<TeacherView> GetListTeacherHome();
        List<Teacher> GetListTeacherCourse();
        Teacher GetTeacherDetail(long Id);
        Teacher Add(Teacher teacher);
        void Update(Teacher teacher);
        Teacher Delete(long id);
    }
    public class TeacherRepository : ITeacherRepository
    {
        AcademyEntities db = new AcademyEntities();
        public Teacher Add(Teacher teacher)
        {

            db.Teacher.Add(teacher);
            db.SaveChanges();
            return teacher;
        }

        public Teacher Delete(long id)
        {
            var Teacher = db.Teacher.Find(id);
            db.Teacher.Remove(Teacher);
            db.SaveChanges();

            return Teacher;
        }

        public List<TeacherView> GetListTeacherAndCouseAdmin()
        {
            var query = from c in db.Course
                        join t in db.Teacher on c.TeacherId equals t.Id into e
                        from j in e.DefaultIfEmpty()
                        group c by new { j.Id, j.Name, j.Avatar, j.Description, j.DOB,j.Gender,j.Email,j.Address,j.Phone,j.Status} into g
                        select new TeacherView
                        {
                            courses = g.ToList(),
                            Id = g.Key.Id,
                            Name = g.Key.Name,
                            Avatar = g.Key.Avatar,
                            Description = g.Key.Description,
                            DOB=g.Key.DOB,
                            Gender=g.Key.Gender,
                            Email=g.Key.Email,
                            Address=g.Key.Address,
                            Phone=g.Key.Phone,
                            Status=g.Key.Status
                        };    
            return query.OrderBy(x => x.Id).ToList();
        }

        public List<Teacher> GetListTeacherAdmin()
        {
            var lst = db.Teacher.OrderBy(x => x.Name).ToList();
            return lst;
        }


        public List<Teacher> GetListTeacherCourse()
        {
            var lst = db.Teacher.Where(x => x.Status == 1).OrderBy(x => x.Name).ToList();
            return lst;
        }

        public List<TeacherView> GetListTeacherHome()
        {
            var query = from c in db.Course
                         join t in db.Teacher on c.TeacherId equals t.Id into e
                         from j in e.DefaultIfEmpty()
                         group c by new { j.Id, j.Name,j.Avatar,j.Description,j.Status } into g
                         select new TeacherView
                         {
                             courses = g.ToList(),
                             Id = g.Key.Id,
                             Name = g.Key.Name,
                             Avatar=g.Key.Avatar,
                             Description=g.Key.Description,
                             Status=g.Key.Status
                        };
            return query.Where(x=>x.Status==1).OrderBy(x => x.Id).Take(6).ToList();
        }


        public Teacher GetTeacherDetail(long Id)
        {
            var lst = db.Teacher.SingleOrDefault(x => x.Id == Id);
            return lst;
        }

        public void Update(Teacher teacher)
        {
            db.Teacher.Attach(teacher);
            db.Entry(teacher).State = EntityState.Modified;
            db.Entry(teacher).Property(x => x.CreateDate).IsModified = false;
            db.SaveChanges();
        }
    }
}
