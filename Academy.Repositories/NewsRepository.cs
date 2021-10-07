using Academy.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Repositories
{
    public interface INewsRepository
    {
        List<NewsView> GetListNews();
        List<News> GetListNewsHome();
        News GetNewsDetail(long Id);
        News Add(News NewsModel);
        void Update(News NewsModel);
        News Delete(long id);
    }
    public class NewsRepository : INewsRepository
    {
        AcademyEntities db = new AcademyEntities();
        public News Add(News News)
        {

            db.News.Add(News);
            db.SaveChanges();
            return News;
        }

        public News Delete(long id)
        {
            var News = db.News.Find(id);
            db.News.Remove(News);
            db.SaveChanges();
            return News;
        }

        public List<NewsView> GetListNews()
        {
            var query = from c in db.News
                        join cc in db.NewCategory on c.NewCategoryId equals cc.Id
                        select new NewsView()
                        {
                            Id = c.Id,
                            Title = c.Title,
                            Detail = c.Detail,
                            NewCategoryName = cc.Name,
                            Description = c.Description,
                            Image = c.Image,
                            Type = c.Type,
                            Status = c.Status
                        };
            return query.OrderBy(x => x.Id).ToList();
        }

        public List<News> GetListNewsHome()
        {
            var lst = db.News.OrderBy(y => y.CreateDate).Where(x => x.Status == 1).Take(8).ToList();
            return lst;
        }

        public News GetNewsDetail(long Id)
        {
            var lst = db.News.SingleOrDefault(x => x.Id == Id);
            return lst;
        }

        public void Update(News News)
        {
            db.News.Attach(News);
            db.Entry(News).State = EntityState.Modified;
            db.Entry(News).Property(x => x.CreateDate).IsModified = false;
            db.SaveChanges();
        }
    }
}
