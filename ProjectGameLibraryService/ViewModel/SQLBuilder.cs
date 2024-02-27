using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModel;

namespace ViewModel
{
    public static class SQLBuilder
    {
        public static string InsertSQL(BaseEntity entity)
        {
            Type type = entity.GetType();
            string command = "Insert Into " + entity.GetTableName() + " (";
            string values = " Values (";
            foreach (var item in type.GetProperties())
            {
                string n = item.Name;
                var value = item.GetValue(entity);
                if (value is BaseEntity)
                {
                    string k = ((BaseEntity)value).GetKeyFields()[0];
                    value = value.GetType().GetProperty(k).GetValue(value);
                }
                if (value is string || value is DateTime)
                {
                  
                    command += n + " ,";
                    values += "'" + value + "', ";
                }

                if (value is int || value is double || value is bool)
                {
                    command += n + " ,";
                    values += value + " , ";
                }
            }

            command = command.Substring(0, command.Length - 2) + ")";
            values = values.Substring(0, values.Length - 2) + ")";
            return command + values;
        }

        public static string UpdateSQL(BaseEntity entity)
        {
            Type type = entity.GetType();
            string command = "Update " + entity.GetTableName() + " set ";
            foreach (var item in type.GetProperties())
            {
                string n = item.Name;
                var value = item.GetValue(entity);
                if (value is BaseEntity)
                {
                    string k = ((BaseEntity)value).GetKeyFields()[0];
                    value = value.GetType().GetProperty(k).GetValue(value);
                }

                if (value is string || value is DateTime)
                    command += n + " = '" + value + "', ";
                if (value is int || value is double || value is bool)
                    command += n + " = " + value + ", ";
            }
            string where = "";
            foreach (var item in entity.GetKeyFields())
            {
                if (where != "")
                    where += " And ";
                var value = entity.GetType().GetProperty(item).GetValue(entity);
                if (value is string || value is DateTime)
                    where += item + " = '" + value + "'";
                else
                    where += item + " = " + value;
            }
            command = command.Substring(0, command.Length - 2) + " Where " + where;
            return command;
        }

        public static string DeleteSQL(BaseEntity entity)
        {
            Type type = entity.GetType();
            string command = "Delete From " + entity.GetTableName() + " Where ";
            string where = "";
            foreach (var item in entity.GetKeyFields())
            {
                if (where != "")
                    where += " And ";
                var value = entity.GetType().GetProperty(item).GetValue(entity);
                if (value is string || value is DateTime)
                    where += item + " = '" + value + "'";
                else
                    where += item + " = " + value;
            }
            command += where;
            return command;
        }

    }
}
