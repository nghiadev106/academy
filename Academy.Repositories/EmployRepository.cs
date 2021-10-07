using Academy.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Repositories
{
    public interface IEmployRepository
    {
        List<Employ> GetListEmployAdmin();
        Employ GetEmployDetail(long Id);
        Employ Add(Employ employ);
        void Update(Employ employ);
        Employ Delete(long id);
    }
    public class EmployRepository : IEmployRepository
    {
        AcademyEntities db = new AcademyEntities();
        public Employ Add(Employ employ)
        {

            db.Employ.Add(employ);
            db.SaveChanges();
            return employ;
        }

        public Employ Delete(long id)
        {
            var Employ = db.Employ.Find(id);
            db.Employ.Remove(Employ);
            db.SaveChanges();

            return Employ;
        }

        public List<Employ> GetListEmployAdmin()
        {
            var lst = db.Employ.OrderByDescending(y => y.CreateDate).ToList();
            return lst;
        }

        public Employ GetEmployDetail(long Id)
        {
            var lst = db.Employ.SingleOrDefault(x => x.Id == Id);
            return lst;
        }

        public void Update(Employ employ)
        {
            db.Employ.Attach(employ);
            db.Entry(employ).State = EntityState.Modified;
            db.Entry(employ).Property(x => x.CreateDate).IsModified = false;
            db.SaveChanges();
        }
    }
}
