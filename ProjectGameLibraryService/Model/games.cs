using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class games:BaseEntity
    {
        public string code { get; set; }
        public string game_name { get; set; }
        public int min_age { get; set; }
        public int max_age { get; set; }
        public int price_for_ingury_case { get; set; }
        public int original_amount { get; set; }
        public int present_amount { get; set; }
        public string game_description { get; set; }
        public int amount_in_repair { get; set; }
        public kinds kind_code { get; set; }
        public string amount_participate { get; set; }
        public string electronic { get; set; }
        public override string[] GetKeyFields()
        {
            return new string[] { "code" };
        }

        public override string GetTableName()
        {
            return "games";
        }

        public override string ToString()
        {
            return code.ToString();
        }
        //public override string ToString()
        //{
        //    return game_name;
        //}
        //public override int ToString()
        //{
        //    return min_age;
        //}
        //public override int ToString()
        //{
        //    return max_age;
        //}
        //public override int ToString()
        //{
        //    return price_for_ingury_case;
        //}
        //public override int ToString()
        //{
        //    return original_amount;
        //} 
        //public override int ToString()
        //{
        //    return present_amount;
        //} 
        //public override int ToString()
        //{
        //    return game_description;
        //} 
        //public override int ToString()
        //{
        //    return amount_in_repair;
        //}
        //public override string ToString()
        //{
        //    return amount_participate;
        //} 
        //public override string ToString()
        //{
        //   return electronic;
        //}

        public override bool Equals(object obj)
        {
            if (obj is games)
                return (obj as games).code == this.code;
            //אם יש כמה מפתחות ראשיים בודקים על כולם
            return false;
        }

    }
}
