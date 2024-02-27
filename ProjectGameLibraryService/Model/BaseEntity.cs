using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
     public abstract class BaseEntity
    {
        public abstract string GetTableName();
        public abstract string[] GetKeyFields();
    }
}
