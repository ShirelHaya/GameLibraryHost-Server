using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class gamesDB:BaseDB
    {
        public gamesDB() : base("Games")
        {
        }
        public List<games> GetList()
        {
            if (list.Count() == 0)
            {
                Select();
            }
            return list.Cast<games>().ToList();
        }
        protected override BaseEntity CreateModel()
        {
            games games = new games();
            games.game_name = reader["game_name"].ToString();
            games.code =reader["code"].ToString();
            games.min_age = (int)reader["min_age"];
            games.max_age = (int)reader["max_age"];
            games.price_for_ingury_case = (int)reader["price_for_ingury_case"];
            games.original_amount = (int)reader["original_amount"];
            games.present_amount = (int)reader["present_amount"];
            games.game_description = reader["game_description"].ToString();
            games.amount_in_repair = (int)reader["amount_in_repair"];
            games.kind_code = MyDB.Kinds.GetKindByCode((string)reader["kind_code"]);
            games.amount_participate = reader["amount_participate"].ToString();
            games.electronic = reader["electronic"].ToString();

            return games;
        }
        public games GetGamesByCode(string code)
        {
            return GetList().FirstOrDefault(x => x.code == code);
        }
        public List<games> GetGamesByName(string name)
        {
            return GetList().Where(x => x.game_name == name).ToList();
        }
        public List<games> GetListByLetterc(string stratWith)
        {
            return list.Cast<games>().Where(x => x.code.StartsWith(stratWith)).ToList();
        } 
        public List<games> GetListByLettern(string stratWith)
        {
            return list.Cast<games>().Where(x => x.game_name.StartsWith(stratWith)).ToList();
        }
    }
}
