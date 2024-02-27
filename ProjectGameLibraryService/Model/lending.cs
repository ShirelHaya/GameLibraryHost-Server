using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class lending : BaseEntity
    {
        public int code { get; set; }
        public string date_lending { get; set; }
        public games game_code { get; set; }
        public string return_date { get; set; }//תאריך החזרה
        public subscribers subscribers_code { get; set; }
        public string game_situation { get; set; }
        public override string[] GetKeyFields()
        {
            return new string[] { "code" };
        }

        public override string GetTableName()
        {
            return "lending";
        }

        public override string ToString()
        {
            return game_situation;
        }
       /* public override int ToString()
        {
            return game_code;
        }*/
    }
}
