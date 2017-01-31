using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace VMS.App_Code
{
    public class DatabaseMSSQL
    {
        Hashtable connTable = new Hashtable();
        SqlConnection myConnection = null;
        SqlCommand cmd = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;

        public DatabaseMSSQL()
        { connTable.Add("DBA", "Data Source=localhost; Initial Catalog=VMS; User ID=sa; Password=Roger;"); }
        public SqlDataReader getList(string query, string dbName = "DBA")
        {
            myConnection = new SqlConnection(connTable[dbName].ToString());
            SqlCommand cmd = new SqlCommand(query, myConnection);
            SqlDataReader dr;
            myConnection.Open();
            dr = cmd.ExecuteReader();
            cmd = null;
            return dr;
        }
        public string ExecuteNonQuery(string query, string dbName = "DBA")
        {
            string rValue;
            myConnection = new SqlConnection(connTable[dbName].ToString());
            try
            {
                cmd = new SqlCommand(query, myConnection);
                myConnection.Open();
                rValue = cmd.ExecuteNonQuery().ToString();
                if (rValue != "-1")
                    return rValue;
                else throw new Exception();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                myConnection.Close();
                cmd = null;
                myConnection = null;
                query = null;
            }
        }
        public object getSingleValue(string query, string dbName = "DBA")
        {
            myConnection = new SqlConnection(connTable[dbName].ToString());

            try
            {
                cmd = new SqlCommand(query, myConnection);
                myConnection.Open();
                object retValue = cmd.ExecuteReader();
                return retValue;
            }
            catch (Exception ex)
            {
                return (object)ex.Message.ToString();
            }
            finally
            {
                myConnection.Close();
                cmd = null;
                myConnection = null;
                query = null;
                dbName = null;
            }
        }
        public DataSet GetDataSet(string query, string dbName = "DBA")
        {
            myConnection = new SqlConnection(connTable[dbName].ToString());
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand(query, myConnection);
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                if (ds.Tables[0].Rows.Count != 0)
                    return ds;
                else
                    return ds;
            }
            catch (Exception ex)
            { return null; }
            finally
            {
                adapter = null;
                cmd = null;
                myConnection = null;
                query = null;
            }
        }
        public DataTable GetDataTable(string query, string dbName = "DBA")
        {
            myConnection = new SqlConnection(connTable[dbName].ToString());
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand(query, myConnection);
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                if (dt.Rows.Count != 0)
                    return dt;
                else
                    return null;
            }
            catch
            { return null; }
            finally
            {
                adapter = null;
                cmd = null;
                myConnection = null;
                query = null;
            }
        }
    }
}