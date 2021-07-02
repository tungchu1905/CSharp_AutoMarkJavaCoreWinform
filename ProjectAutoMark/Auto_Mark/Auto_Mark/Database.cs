using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Auto_Mark
{
    class Database
    {
        internal static SqlConnection getConnection()
        {
            string strCon = ConfigurationManager.ConnectionStrings["Project"].ToString();
            return new SqlConnection(strCon);
        }
        internal static DataTable getDataSql(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, getConnection());
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet(); // database Cache
            ds.Clear();
            da.Fill(ds);
            return ds.Tables[0];
        }

        

        internal static DataTable getAccount(string user, string pass)
        {
            return getDataSql("select * from Account where username ='" + user + "' and password = '" + pass + "'");
        }

        internal static void Execute(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, getConnection());
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        internal static DataTable getClass()
        {
            return getDataSql("select * from Class");
        }
        internal static DataTable getAllStudent()
        {
            return getDataSql("select * from ScoreStudent");
        }
        internal static List<string> listStudentByClass(string clName)
        {
            List<string> list = new List<string>();
            DataTable dataTable = Database.getDataSql("select StudentID from ScoreStudent where className = '" + clName + "'");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                list.Add(dataRow["StudentID"].ToString());
            }
            return list;
        }



        internal static void InsertStudentID(string id, string classname)
        {
            Execute("insert into ScoreStudent values('" + id + "','1','" + classname + "','','','" + id + "')");
        }
        internal static void InsertClassID(string id,string aid)
        {
            Execute("insert into Class values('" + id + "','"+aid+"')");
        }
        
        internal static void DeleteStudent(string classId)
        {
            Execute("delete ScoreStudent where className = '" + classId + "'");
        }    
        internal static void DeleteClass(string aid)
        {
            Execute("delete Class where accountID = '" + aid + "'");
        }

        internal static DataTable getAllClassByAccId(object aId)
        {
            return getDataSql("select * from Class where accountID = '"+aId+"' ");
        }
        internal static void UpdateScore(float totalMark, string scoreDetail, string studentId)
        {
            string sql = "UPDATE ScoreStudent SET totalScore = '"+totalMark+"', scoreDetail = '"+scoreDetail+"' WHERE StudentID = '"+studentId+"'";
            Execute(sql);
        }
        internal static object search(string txtSearch)
        {
            string sql = "select * from ScoreStudent where StudentID like '%" + txtSearch + "%'";
            return Database.getDataSql(sql);
        }
    }
}
