using Academy.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Repositories
{
    public interface IConfigSystemRepository
    {
        ConfigSystem GetConfigSystem(long Id);
        void Update(ConfigSystem ConfigSystemModel);
    }
    public class ConfigSystemRepository : IConfigSystemRepository
    {
        AcademyEntities db = new AcademyEntities();

        public ConfigSystem GetConfigSystem(long Id)
        {
            var lst = db.ConfigSystem.SingleOrDefault(x => x.Id == Id);
            return lst;
        }

        public void Update(ConfigSystem configSystem)
        {
           
            db.ConfigSystem.Attach(configSystem);
            db.Entry(configSystem).State = EntityState.Modified;
           // db.Entry(configSystem).Property(x => x.Infomation).IsModified = false;
            db.SaveChanges();
        }
    }
}
