using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Model
{
    public class family : BaseEntity
    {
        public string family_code { get; set; }
        public string f_name { get; set; }
        public string num_tal { get; set; }
        public string num_pel1 { get; set; }
        public string num_pel2 { get; set; }
        public string street_name { get; set; }
        public int num_building { get; set; }
        public int num_apartment { get; set; }
        public string date_paying { get; set; }
        public bool need_to_return_game { get; set; }
        public int debt { get; set; } 
        public bool amail { get; set; }
        public override string[] GetKeyFields()
        {
            return new string[] { "family_code" };
        }

        public override string GetTableName()
        {
            return "family";
        }

        //public override string ToString()
        //{
        //    return num_pel1;
        //} 
        //public override string ToString()
        //{
        //    return num_pel2;
        //}
        //public override string ToString()
        //{
        //    return num_tal;
        //}
        //public override string ToString()
        //{
        //    return street_name;
        //}
        //public override string ToString()
        //{
        //    return num_building;
        //}
        //public override string ToString()
        //{
        //    return num_apartment;
        //}
        //public override string ToString()
        //{
        //    return f_name;
        //}
        //public override string ToString()
        //{
        //    return date_paying;
        //}

       

    }
}
