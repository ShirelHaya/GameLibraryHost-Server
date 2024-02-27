using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
   public  class lendingDB:BaseDB
    {
        public string return_date;

        public lendingDB() : base("Lending")
        {
        }



        public List<lending> GetList()
        {
            if (list.Count() == 0)
            {
                Select();
            }
            return list.Cast<lending>().ToList();
        }
        protected override BaseEntity CreateModel()
        {
            lending lending = new lending();
            lending.code = (int)reader["code"];
            lending.date_lending = reader["date_lending"].ToString();
            lending.game_code = MyDB.Games.GetGamesByCode(reader["game_code"].ToString());
            lending.return_date = reader["return_date"].ToString();
            lending.subscribers_code =MyDB.Subscribers.GetSubscribersByCode(reader["subscribers_code"].ToString());
            lending.game_situation = reader["game_situation"].ToString();

            return lending;
        }
        public int GetNextCode()
        {
            return GetList().Max(s => s.code) + 1;
        }
        
    }
}
