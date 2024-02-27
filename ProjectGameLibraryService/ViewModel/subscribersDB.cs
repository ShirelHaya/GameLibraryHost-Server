using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class subscribersDB : BaseDB
    {
        public subscribersDB() : base("Subscribers")
        {
        }
        public List<subscribers> GetList()
        {
            if (list.Count() == 0)
            {
                Select();
            }
            return list.Cast<subscribers>().ToList();
        }

        protected override BaseEntity CreateModel()
        {
            subscribers subscribers = new subscribers();
            subscribers.code = reader["code"].ToString();
            subscribers.date_beginning = reader["date_beginning"].ToString();
            subscribers.family_code = MyDB.Family.GetFamilyByCode(reader["family_code"].ToString());
            return subscribers;
        }
        public subscribers GetSubscribersByCode(string code)
        {
            return GetList().FirstOrDefault(x => x.code == code);
        }

    }
}
