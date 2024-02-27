using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public abstract class BaseDB
    {
        //הצהרות של משתנים  לחיבור לאקסס שלנו
        protected string connectionString;
        protected OleDbConnection connection;
        protected OleDbCommand command;
        protected OleDbDataReader reader;

        //לתוך הליסט יכנס מה שחוזר מהאקסס
        protected List<BaseEntity> list;

        //הוספה מחיקה עדכון - נטפל בהמשך
        protected List<BaseEntity> inserted = new List<BaseEntity>();
        protected List<BaseEntity> changed = new List<BaseEntity>();
        protected List<BaseEntity> deleted = new List<BaseEntity>();

        public BaseDB(string tableName)
        {
            connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + GetCurrentPath() + "Data\\project.accdb");
            command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "select * from " + tableName;
            list = new List<BaseEntity>();

        }
        public string GetCurrentPath()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            string[] arr = path.Split('\\');
            path = "";
            for (int i = 0; i < arr.Length - 3; i++)
            {
                path += arr[i] + "\\";
            }
            return path;
        }
        protected abstract BaseEntity CreateModel();
        protected void Select()
        {
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(CreateModel());
                }
            }
            catch (Exception ex) { }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

        }
        public void Add(BaseEntity item)
        {
            if (item != null)
            {
                list.Add(item);
                inserted.Add(item);
            }
        }
        public void Update(BaseEntity item)
        {
            if (item != null)
                changed.Add(item);
        }
        public void Deleted(BaseEntity item)
        {
            if (item != null)
            {
                deleted.Add(item);
                list.Remove(list.FirstOrDefault(x => x.Equals(item)));
            }
        }

        public int SaveChanges()
        {
            int records = 0;
            try
            {
                command.Connection = connection;
                connection.Open();
                foreach (var item in inserted)
                {
                    try
                    {
                        command.CommandText = SQLBuilder.InsertSQL(item);
                        records += command.ExecuteNonQuery();
                    }
                    catch
                    {

                    }
                }
                inserted.Clear();
                foreach (var item in changed)
                {
                    try
                    {
                        command.CommandText = SQLBuilder.UpdateSQL(item);
                        records += command.ExecuteNonQuery();
                    }
                    catch
                    {

                    }

                }
                changed.Clear();
                foreach (var item in deleted)
                {
                    try
                    {
                        command.CommandText = SQLBuilder.DeleteSQL(item);
                        records += command.ExecuteNonQuery();
                    }
                    finally { }
                }
                deleted.Clear();
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + "\nDataBase:" + command.CommandText);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return records;
        }
    }
}
