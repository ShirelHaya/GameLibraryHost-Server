using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Model;
using ViewModel;

namespace ProjectGameLibraryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {

        //דף החזרות
        public void ReturnGame(string todayDate, string code, string gameCode, string gameSituation)
        {
            throw new NotImplementedException();
        }
        public List<lending> GetListLending()
        {
            //var t = MyDB.lending.GetList();
            lendingDB lendingDB = new lendingDB();

            var t = lendingDB.GetList();
            return t;
        }
        public int ChangeReturn(string dateReterening, string subCode, string gameCode, string gameSituation)
        {
            familyDB familyDB = new familyDB();
            family family1 = familyDB.GetList().FirstOrDefault(x => x.family_code == subCode);
            family1.need_to_return_game = false;

            lending lending = MyDB.Lending.GetList().FirstOrDefault(x => x.subscribers_code.code == subCode);
            lending.game_code.code = gameCode;
            lending.game_situation = gameSituation;
            int NumberOfDays = 0;
            //אם יש לך חוב
            if (Convert.ToDateTime(lending.return_date) < Convert.ToDateTime(dateReterening))
            {
                NumberOfDays = Convert.ToInt32((DateTime.Now - Convert.ToDateTime(lending.return_date)).TotalDays);//Model.lending.reyurn...
                family1.debt = NumberOfDays / 7 * 5;

            }
            games games1 = MyDB.Games.GetList().FirstOrDefault(s => s.code == gameCode);
            games1.present_amount = (games1.present_amount) + 1;
            //  הוסף למלאי ויש אפשרות שינוי מצב משחק!נעשה לעיל!!

            MyDB.Games.Update(games1);
            MyDB.Games.SaveChanges();
            MyDB.Family.Update(family1);
            return MyDB.Family.SaveChanges();
            //לשלוח לעמוד תשלום
        }

        //דף השאלה
        public void Lending(string subscribersCode, string dateLending, string gameCode)
        {
            throw new NotImplementedException();
        }
        public int ChangeLending(string subCode, string dateLending, string gameCode,string situation)
        {
            games games1 = MyDB.Games.GetList().FirstOrDefault(X => X.code == gameCode);
            lending lending = new lending();
            lending.subscribers_code = MyDB.Subscribers.GetSubscribersByCode(subCode);
            lending.date_lending = DateTime.Today.ToString("dd/MM/yyyy");
            lending.game_code = new games() { code = gameCode };
            //lending.game_situation = lending.game_situation;
            lending.return_date = Convert.ToString( DateTime.Today.AddDays(14));
            games1.present_amount = (games1.present_amount) - 1;//עידכון מלאי משחק נוכחי פחות 1
            lending.game_situation = situation;
            //-----------------------------------------
            lending.code = MyDB.Lending.GetNextCode();
            MyDB.Lending.Add(lending);
            MyDB.Lending.SaveChanges();
            MyDB.Games.Update(games1);
            return MyDB.Games.SaveChanges();
        }

        //דף תשלום
        public void Paying(string datePaing, int sumPaing, string familyName, string nameStreet, int numStreet, int numApartment)
        {
            throw new NotImplementedException();
        }
        //פעולות דומות מאד וע"כ הן די דומות חוץ מקבלת משתנים
        public int ChangePaying(string datePaying, int sumPaying, string subCode, string status)
        {
            family family = MyDB.Family.GetList().FirstOrDefault(x => x.family_code == subCode);

            family.date_paying = datePaying;

            if (status == "m")
            {
                family.amail = true;
                if(family.debt!=0)
                    family.debt = (family.debt) - sumPaying;
            }

            else
            {
                family.debt = (family.debt) - sumPaying;
                family.amail = true;
            }
            MyDB.Family.Update(family);
            return MyDB.Family.SaveChanges();

        }

        //דף הצטרפות למנוי
        public int NewSubscribers(string familyName, string numId, string numTal, string numPel1, string numPel2, string nameStreet, int numStreet, int numApartment)
        {
            familyDB familyDB = new familyDB();
            family family1 = new family();

            //מס מנוי זה תז הורה וסיומת מס מנוי. במקרה שקיי'ם כבר, יש צורך בשינוי הסיומת מ-1 לאחר
            try
            {
                family1 = familyDB.GetList().FirstOrDefault(x => x.family_code == (numId + "1"));//////
                
                if (family1 == null)
                {
                    family1 = new family();
                    family1.family_code = (numId + "1");
                }
                else
                {
                    family1.family_code = numId + "2";
                }
                
            }
            catch
            {

            }
            family1.debt = 35;
            family1.amail = false;
            family1.f_name = familyName;
            family1.num_tal = numTal;
            family1.num_pel1 = numPel1;
            family1.num_pel2 = numPel2;
            family1.street_name = nameStreet;
            family1.num_building = numStreet;
            family1.num_apartment = numApartment;

            subscribers subscribers1 = new subscribers();
            subscribers1.code = family1.family_code;
            subscribers1.family_code = new family();
            subscribers1.family_code.family_code = family1.family_code;
            subscribers1.date_beginning = DateTime.Now.ToString();
            //צריך גם לטבלת מנוים
            MyDB.Family.Add(family1);
            int y = MyDB.Family.SaveChanges();
            MyDB.Subscribers.Add(subscribers1);

            int x2 = MyDB.Subscribers.SaveChanges();
            return x2;

        }


     
        //דף צפיה במנויי'ם
        public List<family> GetListFamily()
        {
            var t = MyDB.Family.GetList();
            return t;
        }
        public List<subscribers> GetListSubscribers()
        {
            List<subscribers> t = MyDB.Subscribers.GetList();
            return t;
        }

        public List<family> GetListSubscribersByLetter(string stratWith)
        {
            familyDB familyDB = new familyDB();
            var t = familyDB.GetListByLetter(stratWith);
            return t;
        }

        //מחזיר את כל המשפחות שיש להם את אותו שם משפחה
        public List<family> FindFamilyByName(string familyName)
        {
            var e = MyDB.Family.GetFamilyByName(familyName);
            return e;
        }
        //מחזיר את כל המשפחות שיש להם את אותו רחוב
        public List<family> FindListFamilyByStreet(string familyStreet)
        {
            var e = MyDB.Family.GetFamilyByStreet(familyStreet);
            return e;
        }
        public bool FindIfExsistSub(string code)
        {
            familyDB familyDB = new familyDB();
            family family1 = new family();
            family1 = familyDB.GetList().FirstOrDefault(x => x.family_code == code);
            if (family1 != null)
            {
                return true;
            }
            else
                return false;
        }
        public int DeleteSub(string code)
        {
            subscribers subscribers1 = new subscribers();
            familyDB familyDB = new familyDB();
            family family1 = new family();
            family1 = familyDB.GetList().FirstOrDefault(x => x.family_code == code);
            if (subscribers1 == null)
            {
                return 0;
            }
            MyDB.Family.Deleted(family1);
            int y = MyDB.Family.SaveChanges();
            MyDB.Subscribers.Deleted(subscribers1);
            int x2 = MyDB.Subscribers.SaveChanges();
            if (y > 0 && x2 > 0)
                return x2;
            else if (y == 0 && x2 > 0)
                return y;
            else 
                return x2;
        }

        //דף צפיה במשחקים
        public List<games> GetListGames()
        {
            var t = MyDB.Games.GetList();
            return t;
        }
        public bool FindIfExsistGame(string code)
        {
           
            gamesDB gamesDB = new gamesDB();
            games games1 = new games();
            games1 = gamesDB.GetList().FirstOrDefault(x => x.code == code);
            if (games1 != null)
            {
                return true;
            }
            else
                return false;
        }
        public List<games> FindGamesByName(string GameName)
        {
            var e = MyDB.Games.GetGamesByName(GameName);
            return e;
        }
        public List<games> FindListGamesByCode(string GameCode)
        {
            var e = MyDB.Games.GetGamesByName(GameCode);
            return e;
        }
        public List<games> GetListGamesByLetter(string startWith)//code
        {
            gamesDB gamesDB = new gamesDB();
            var t = gamesDB.GetListByLetterc(startWith);
            return t;
        }
        public int DeleteGame(string code)
        {
            gamesDB gamesDB = new gamesDB();
            games games1 = new games();
            games1 = gamesDB.GetList().FirstOrDefault(x => x.code == code);
            if (games1 == null)
            {
                return 0;
            }
            else
            {
                MyDB.Games.Deleted(games1);
                return MyDB.Games.SaveChanges();
            }
        }

        //מעדכן פריטים- עמוד עידכון
        public int UpDateSub(string code, string streetName, int numBiulding, int numApartment, string numTal, string numP1, string numP2)
        {
            family family = MyDB.Family.GetList().FirstOrDefault(x => x.family_code == code);
            if (streetName != "n")
                family.street_name = streetName;
            else
                family.street_name = family.street_name;
            if (numBiulding != 0)
                family.num_building = numBiulding;
            else
                family.num_building = family.num_building;
            if (numApartment != 0)
                family.num_apartment = numApartment;
            else
                family.num_apartment = family.num_apartment;
            if (numTal != "n")
                family.num_tal = numTal;
            else
                family.num_tal = family.num_tal;
            if (numP1 != "n")
                family.num_pel1 = numP1;
            else
                family.num_pel1 = family.num_pel1;
            if (numP2 != "n")
                family.num_pel2 = numP2;
            else
                family.num_pel2 = family.num_pel2;
            MyDB.Family.Update(family);
            return MyDB.Family.SaveChanges();
        }

        //הוספת משחקים
        public void RebuldingAmount(string code, int amoG)//עידכון כמות
        {
            int num = amoG;
            games games = MyDB.Games.GetList().FirstOrDefault(x => x.code == code);
            games.original_amount += amoG;
            MyDB.Games.Update(games);
            MyDB.Games.SaveChanges();
        }

        //הוספת משחק
        public int NewGame(string kind_code, string code, string game_name, int min_age, int max_age, int price_for_ingury_case, int original_amount, int present_amount, string game_description, int amount_in_repair, string amount_participate, bool electric)
        {
            int num = 0;
            gamesDB gamesDB = new gamesDB();
            games games1 = new games();

            kinds kinds1 = new kinds();
            games1.amount_in_repair = amount_in_repair;
            games1.amount_participate = amount_participate;
            games1.code = code;
            games1.game_description = game_description;
            games1.game_name = game_name;
            games1.max_age = max_age;
            games1.min_age = min_age;
            games1.original_amount = original_amount;
            games1.present_amount = present_amount;
            games1.price_for_ingury_case = price_for_ingury_case;
            kinds1.code = kind_code;
            games1.kind_code = MyDB.Kinds.GetKindByCode(kind_code);
            string s = "true";
            if (electric.ToString() != s)
                s = " false";
            games1.electronic = s;


            MyDB.Games.Add(games1);
            MyDB.Games.SaveChanges();
            return num;
        }

        public int UpDateGames(string code, int addAmount, int lessAmount)
        {
            gamesDB gamesDB = new gamesDB();
            games games = new games();
            games = MyDB.Games.GetGamesByCode(code);
            games.original_amount += addAmount;
            games.original_amount -= lessAmount;
            MyDB.Games.Update(games);
            return MyDB.Games.SaveChanges();
        }
        //דף כניסת מנוי
        public subscribers GetSubByCode(string code)
        {
            var e = MyDB.Subscribers.GetSubscribersByCode(code);
            return e;
        }

        List<games> IService1.FindGamesByName(string GameName)
        {
            var e = MyDB.Games.GetGamesByName(GameName);
            return e;
        }
        //
        public bool LendingCheck(string subCode,string gameCode)
        {
            
           lending lending = MyDB.Lending.GetList().FirstOrDefault(x => x.subscribers_code.code == subCode);
            if (lending.game_code.code == gameCode)
                return true;
            else
                return false;
        }
    }
}
