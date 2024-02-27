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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        //דף החזרות
        [OperationContract]
        void ReturnGame(string todayDate, string code, string gameCode, string gameSituation);//no active!
        [OperationContract]
        int ChangeReturn(string dateReterening, string subCode, string gameCode, string gameSituation);
        [OperationContract]
        List<lending> GetListLending();

        //דף השאלות
        [OperationContract]
        void Lending(string subscribersCode, string dateLending, string gameCode); /*no actual!*/
        [OperationContract]
        int ChangeLending(string subCode, string dateLending, string gameCode, string situation);


        //דף תשלום
        [OperationContract]
        void Paying(string datePaing, int sumPaing, string familyName, string nameStreet, int numStreet, int numApartment);
        [OperationContract]
        int ChangePaying(string datePaying, int sumPaying, string subCode, string status);//עידכון תשלום כמו הפעולה שמעל רק מקבלת פחות

        //דף הצטרפות למנוי
        [OperationContract]
        int NewSubscribers(string familyName, string numId, string numTal, string numPel1, string numPel2, string nameStreet, int numStreet, int numApartment);
        //יש לעשות שיקרה---מצטרף בסטטוס מושהה ועובר מידית לעמוד תשלום, ואז משתנה הסטטוס-בעת התשלום


        //דף צפיה במנויי'ם
        [OperationContract]
        List<family> GetListFamily();
        [OperationContract]
        List<subscribers> GetListSubscribers();
        [OperationContract]
        List<family> FindFamilyByName(string familyName);
        [OperationContract]
        List<family> FindListFamilyByStreet(string familyStreet);
        [OperationContract]
        bool FindIfExsistSub(string code);
        [OperationContract]
        List<family> GetListSubscribersByLetter(string startWith);
        [OperationContract]
        int DeleteSub(string code);                                     //look! it need to be on enother new page!

        //דף רשימת משחקים

        [OperationContract]
        List<games> GetListGames();
        [OperationContract]
        bool FindIfExsistGame(string code);
        [OperationContract]
        List<games> FindGamesByName(string GameName);
        [OperationContract]
        List<games> FindListGamesByCode(string GameCode);
        [OperationContract]
        List<games> GetListGamesByLetter(string startWith);
        [OperationContract]
        int DeleteGame(string code);
        //עידכון מנויי'ם

        [OperationContract]
        int UpDateSub(string code, string streetName, int numBiulding, int numApartment, string numTal, string numP1, string numP2);

        //הוספת משחקים
        [OperationContract]
        void RebuldingAmount(string code, int amoG);//עידכון כמות במלאי

        [OperationContract]
        //בניי'ת משחק חדש
        int NewGame(string kind_code, string code, string game_name, int min_age, int max_age, int price_for_ingury_case, int original_amount, int present_amount, string game_description, int amount_in_repair, string amount_participate, bool electric);

        [OperationContract]
        int UpDateGames(string code, int addAmount, int lessAmount);

        //כניסת מנוי עם סיסמא
        [OperationContract]
        subscribers GetSubByCode(string code);
        //
        bool LendingCheck(string subCode, string gameCode);
        //עד כאן העידכון
    }


}
