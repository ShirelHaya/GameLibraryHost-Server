using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;


namespace ViewModel
{
    public class familyDB : BaseDB
    {
        public familyDB() : base("Family")
        {
        }
        public List<family> GetList()
        {
            if (list.Count() == 0)
            {
                Select();
            }
            return list.Cast<family>().ToList();
        }

        public List<family> GetListByLetter(string stratWith)
        {
            return list.Cast<family>().Where(x => x.f_name.StartsWith(stratWith)).ToList();
        }
        protected override BaseEntity CreateModel()
        {
            family family = new family();
            try
            {
                //string family_codeString = reader["family_code"].ToString();
                //int family_codeInt = 0;
                //int.TryParse(family_codeString, out family_codeInt);
                //if (family_codeInt == 0)
                //{
                //    return new family();
                //}
                //family.family_code = family_codeInt.ToString();
                family.family_code = reader["family_code"].ToString();
                family.f_name = reader["f_name"].ToString();
                family.num_tal = reader["num_tal"].ToString();
                family.num_pel1 = reader["num_pel1"].ToString();
                family.num_pel2 = reader["num_pel2"].ToString();
                family.street_name = reader["street_name"].ToString();
                family.num_building = (int)reader["num_building"];
                family.num_apartment = (int)reader["num_apartment"];
                family.debt = (int)reader["debt"];
                family.need_to_return_game = (bool)reader["need_to_return_game"];
                family.amail = (bool)reader["amail"];
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return family;
        }
        public family GetFamilyByCode(String code)
        {
            var e = GetList().FirstOrDefault(x => x.family_code == code);
            return e;
        }
         public List<family> GetFamilyByName(string name)
        {
            return GetList().Where(x => x.f_name == name).ToList();
        }
         public List<family> GetFamilyByStreet(string Street)
        {
            return GetList().Where(x => x.street_name == Street).ToList();
        }
      
        public List<family> GetListSubscribers()
        {
            if (list.Count() == 0)
            {
                Select();
            }
            return list.Cast<family>().ToList();
        }
      
    }
}
