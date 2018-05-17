using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ntt_test.Model
{
    class DBInteractor
    {
        public static void ClearDB()
        {
            var db = new ApplicationContext();
            db.Links.Load();
            db.Links.RemoveRange(db.Links.Local);
            db.SaveChanges();
        }

        public static void AddLinksToDB(IEnumerable<Link> links)
        {
            var db = new ApplicationContext();
            db.Links.AddRange(links);
            db.SaveChanges();
        }
    }
}
