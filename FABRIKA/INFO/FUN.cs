using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FABRIKA.INFO
{
    class FUN
    {
        public string nameDB = Properties.Settings.Default.Connect;

        public int GetCount(string sqlQuery)
        {
            SqlConnection conn = new SqlConnection(nameDB);
            conn.Open();

            int x = 0;

            using (SqlCommand myCommand = new SqlCommand(sqlQuery, conn))
            {
                SqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read()) x++;
                reader.Close();
            }
            return x;
        }
        public int GetMax(string sql, string selectCol)
        {
            SqlConnection conn = new SqlConnection(nameDB);
            conn.Open();

            int max = 0, x;

            using (SqlCommand myCommand = new SqlCommand(sql, conn))
            {
                SqlDataReader reader = myCommand.ExecuteReader();

                while (reader.Read())
                {
                    x = reader.GetInt32(reader.GetOrdinal(selectCol));
                    if (x > max) max = x;
                };
                reader.Close();
            }
            return max;
        }
        public int GetMin(string sql, string selectCol)
        {
            SqlConnection conn = new SqlConnection(nameDB);
            conn.Open();

            int min = int.MaxValue, x;

            using (SqlCommand myCommand = new SqlCommand(sql, conn))
            {
                SqlDataReader reader = myCommand.ExecuteReader();

                while (reader.Read())
                {
                    x = reader.GetInt32(reader.GetOrdinal(selectCol));
                    if (x < min) min = x;
                };
                reader.Close();
            }
            return min;
        }
        public int GetAvg(string sql, string selectCol)
        {
            SqlConnection conn = new SqlConnection(nameDB);
            conn.Open();

            int avg = 0, count = GetCount(sql);

            using (SqlCommand myCommand = new SqlCommand(sql, conn))
            {
                SqlDataReader reader = myCommand.ExecuteReader();

                while (reader.Read())
                    avg += reader.GetInt32(reader.GetOrdinal(selectCol));

                avg /= count;
                reader.Close();
            }
            return avg;
        }
        public int GetSum(string sql, string selectCol)
        {
            SqlConnection conn = new SqlConnection(nameDB);
            conn.Open();

            int sum = 0;

            using (SqlCommand myCommand = new SqlCommand(sql, conn))
            {
                SqlDataReader reader = myCommand.ExecuteReader();

                while (reader.Read())
                    sum += reader.GetInt32(reader.GetOrdinal(selectCol));

                reader.Close();
            }
            return sum;
        }
        
        
        public DateTime GetMinDate(string sql, string selectCol)
        {
            SqlConnection conn = new SqlConnection(nameDB);
            conn.Open();

            DateTime min = DateTime.MaxValue, x;

            using (SqlCommand myCommand = new SqlCommand(sql, conn))
            {
                SqlDataReader reader = myCommand.ExecuteReader();

                while (reader.Read())
                {
                    x = reader.GetDateTime(reader.GetOrdinal(selectCol));
                    if (x < min) min = x;
                };
                reader.Close();
            }
            return min;
        }
        public DateTime GetMaxDate(string sql, string selectCol)
        {
            SqlConnection conn = new SqlConnection(nameDB);
            conn.Open();
            DateTime max = DateTime.MinValue, x;

            using (SqlCommand myCommand = new SqlCommand(sql, conn))
            {
                SqlDataReader reader = myCommand.ExecuteReader();

                while (reader.Read())
                {
                    x = reader.GetDateTime(reader.GetOrdinal(selectCol));
                    if (x > max) max = x;
                };
                reader.Close();
            }
            return max;
        }

        public DataTable FillDataSetQuery(string Table, string colName, string colValue, int id)
        {
            SqlConnection conDB = new SqlConnection(nameDB);//all
            conDB.Open();

            DataTable tabord = new DataTable();
            SqlDataAdapter dazak = new SqlDataAdapter($"select {colValue} from {Table} where {colName} = {id}", conDB);
            dazak.Fill(tabord);

            return tabord;
        }

        public string GetValue(string Table, string colName, string colValue, int id)
        {
            SqlConnection conn = new SqlConnection(nameDB);
            conn.Open();
            string myValue = "";
            string sql = string.Format($"select {colValue} from {Table} where {colName} = {id}");
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    myValue = reader.GetString(reader.GetOrdinal(colName));
                    MessageBox.Show(myValue);
                }
                    

                reader.Close();
            }

            return myValue;
        }


        public DataTable FillDataSetQuery(string myQuery)
        {
            SqlConnection conDB = new SqlConnection(nameDB);//all
            conDB.Open();

            DataTable tabord = new DataTable();
            SqlDataAdapter dazak = new SqlDataAdapter(myQuery, conDB);
            dazak.Fill(tabord);

            return tabord;
        }
        public DataTable FillDataSetTable(string tableName)
        {
            SqlConnection conDB = new SqlConnection(nameDB);
            conDB.Open();
            DataTable tabord = new DataTable();
            SqlDataAdapter dazak = new SqlDataAdapter("SELECT * FROM " + tableName, conDB);
            dazak.Fill(tabord);
            return tabord;
        }
        public BindingSource GetBS(string nameTable)
        {
            string nameDB = Properties.Settings.Default.Connect;
            SqlConnection cnn = new SqlConnection(nameDB);
            BindingSource myBS = new BindingSource();
            DataSet ds = new DataSet();
            cnn.Open();

            SqlDataAdapter dacust = new SqlDataAdapter("SELECT * FROM " + nameTable, cnn);
            dacust.Fill(ds, nameTable);
            myBS.DataSource = ds;
            myBS.DataMember = nameTable;

            return myBS;
        }
        public BindingSource GetBSQuery(string query)
        {
            string nameDB = Properties.Settings.Default.Connect;
            SqlConnection cnn = new SqlConnection(nameDB);
            BindingSource myBS = new BindingSource();
            DataSet ds = new DataSet();
            cnn.Open();

            SqlDataAdapter dacust = new SqlDataAdapter(query, cnn);
            dacust.Fill(ds, "Table");
            myBS.DataSource = ds;
            myBS.DataMember = "Table";

            return myBS;
        }


        public bool DeleteFrom(int delID, string Table, string colName)
        {

            SqlConnection conn = new SqlConnection(nameDB);
            conn.Open();
            string sql = string.Format($"Delete from {Table} where {colName} = '{delID}'");
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                { Exception error = new Exception("К сожалению!", ex); throw error; }
            }
        }
        public bool DeleteFrom(string sql)
        {

            SqlConnection conn = new SqlConnection(nameDB);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                { Exception error = new Exception("К сожалению!", ex); throw error; }
            }
        }


        public bool Add(string x1, int x2)
        {
            SqlConnection conn = new SqlConnection(nameDB);
            conn.Open();

            if (GetCount(x1) == 0)
            {
                string sql = "INSERT INTO Posada (posada,salary) " +
                    "values (@posada,@salary)";

                using (SqlCommand myCommand = new SqlCommand(sql, conn))
                {
                    myCommand.Parameters.AddWithValue("@posada", x1);
                    myCommand.Parameters.AddWithValue("@salary", x2);
                    myCommand.ExecuteNonQuery();
                }
                MessageBox.Show("Добавленно!");
                return true;
            }
            else MessageBox.Show("Данные были введены не верно!");
            return false;
        }
        public bool Add(string x1, string x2, string x3, string x4, bool edit, int i=0)
        {
            SqlConnection conn = new SqlConnection(nameDB);
            conn.Open(); string sql;
            try
            {
                int count = GetCount($"select * from Ordered where id_ord='{i}'");

                if (!edit) sql = "INSERT INTO Ordered (ordered,address,bank,rs) " +
                          "values (@ordered,@address,@bank,@rs)";
                else sql = "UPDATE Ordered Set address=@address,bank=@bank,rs=@rs where ordered=@ordered";

                if ((count > 0 && edit) || (count == 0 && !edit))
                {
                    using (SqlCommand myCommand = new SqlCommand(sql, conn))
                    {
                        myCommand.Parameters.AddWithValue("@ordered", x1);
                        myCommand.Parameters.AddWithValue("@address", x2);
                        myCommand.Parameters.AddWithValue("@bank", x3);
                        myCommand.Parameters.AddWithValue("@rs", x4);
                        myCommand.ExecuteNonQuery();
                    }
                    MessageBox.Show("Готово!");
                    return true;
                }
                else MessageBox.Show("Данные были введены не верно!");
                return false;
            }
            catch
            {
                return false;
            }           
        }

        public DataView Obnova(string tableName)
        {
            SqlConnection cnn = new SqlConnection(nameDB);
            cnn.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter dacust = new SqlDataAdapter("select * from " + tableName, cnn);
            dacust.Fill(ds, tableName);

            dacust.Update(ds.Tables[tableName]);
            ds.AcceptChanges();

            DataTable tabcust = ds.Tables[tableName];
            DataView myDataView = new DataView(tabcust);

            return myDataView;
        }
        public DataView SqlQuery(string sql_query)
        {
            SqlConnection cnn = new SqlConnection(nameDB);
            cnn.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter dacust = new SqlDataAdapter(sql_query, cnn);
            dacust.Fill(ds, "Table");

            dacust.Update(ds.Tables["Table"]);
            ds.AcceptChanges();

            DataTable tabcust = ds.Tables["Table"];
            DataView myDataView = new DataView(tabcust);

            return myDataView;
        }
        public DataView SortTable(string tableName, string colSort)
        {
            SqlConnection cnn = new SqlConnection(nameDB);
            cnn.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter dacust = new SqlDataAdapter("select * from " + tableName, cnn);
            dacust.Fill(ds, tableName);

            dacust.Update(ds.Tables[tableName]);
            ds.AcceptChanges();

            DataTable tabcust = ds.Tables[tableName];
            DataView myDataView = new DataView(tabcust);
            myDataView.Sort = colSort + " asc";

            return myDataView;
        }
    }
}
