using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
   public class kindsDB:BaseDB
    {
        public kindsDB() : base("kinds")
        {
        }
        public List<kinds> GetList()
        {
            if (list.Count() == 0)
            {
                Select();
            }
            return list.Cast<kinds>().ToList();
        }
       

        protected override BaseEntity CreateModel()
        {
            kinds kinds = new kinds();
           // kinds.code =(int)reader["code"];
            kinds.code = (string) reader["code"];
            kinds.name_kind = reader["name_kind"].ToString();
            return kinds;
        }
        public kinds GetKindByCode(string code)
        {
            return GetList().FirstOrDefault(x => x.code == code);
        }
    }
}
