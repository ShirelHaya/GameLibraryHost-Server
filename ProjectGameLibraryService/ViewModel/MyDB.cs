using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
   public class MyDB
    {
        public static familyDB Family = new familyDB();
        public static gamesDB Games = new gamesDB();
        public static kindsDB Kinds = new kindsDB();
        public static lendingDB Lending = new lendingDB();
        public static subscribersDB Subscribers = new subscribersDB();
    }
}
