using Academy.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Repositories
{
    public interface INewCategoryRepository
    {
        List<NewCategory> GetListNewCategory();
        NewCategory GetNewCategoryDetail(long Id);
        NewCategory Add(NewCategory NewCategoryModel);
        void Update(NewCategory NewCategoryModel);
        NewCategory Delete(long id);
    }
    public class NewCategoryRepository : INewCategoryRepository
    {
        AcademyEntities db = new AcademyEntities();
        public NewCategory Add(NewCategory NewCategory)
        {
            db.NewCategory.Add(NewCategory);
            db.SaveChanges();
            return NewCategory;
        }

        public NewCategory Delete(long id)
        {
            var NewCategory = db.NewCategory.Find(id);
            db.NewCategory.Remove(NewCategory);
            db.SaveChanges();
            return NewCategory;
        }

        public List<NewCategory> GetListNewCategory()
        {
            var lst = db.NewCategory.OrderBy(y => y.Createdate).ToList();
            return lst;
        }

        public NewCategory GetNewCategoryDetail(long Id)
        {
            var lst = db.NewCategory.SingleOrDefault(x => x.Id == Id);
            return lst;
        }

        public void Update(NewCategory NewCategory)
        {
            db.NewCategory.Attach(NewCategory);
            db.Entry(NewCategory).State = EntityState.Modified;
            db.Entry(NewCategory).Property(x =>x.Createdate).IsModified = false;
            db.SaveChanges();
        }
    }
}
