using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class kinds: BaseEntity
    {
        public string code { get; set; }
        public string name_kind { get; set; }
        public override string[] GetKeyFields()
        {
            return new string[] { "code" };
        }

        public override string GetTableName()
        {
            return "kinds";
        }

        public override string ToString()
        {
            return name_kind;
        }
    }
}
