using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class subscribers: BaseEntity
    {
        public string code { get; set; }
        public string date_beginning { get; set; }
        public family family_code { get; set; }
        public string situation { get; set; }
        public override string[] GetKeyFields()
        {
            return new string[] { "code" };
        }

        public override string GetTableName()
        {
            return "subscribers";
        }

        public override string ToString()
        {
            return situation;
        }
    }
}
