using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WebApplicationExercise.GetAndPost {
    /// <summary>
    /// UserInfoList 的摘要说明
    /// </summary>
    public class UserInfoList : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/html";
            string mySqlConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["mySqlConnStr"].ToString();
            string filePath = context.Request.MapPath("UserInfoList.html");
            using (MySqlConnection conn = new MySqlConnection(mySqlConnStr))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("select * from userinfo order by id desc",conn))
                {
                    //adapter.SelectCommand.CommandText = "select * from managerinfo order by MId desc";
                    //adapter.SelectCommand.Connection = conn;
                    DataTable dt =new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (DataRow row in dt.Rows)
                        {
                            sb.AppendFormat(
                                "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td><a href='ShowDetail.ashx?ID={0}'>Detail</a></td><td><a href='javascript:void(0)' onclick= rowDelete(\"{1}\",\"{0}\")>Delete</a></td><td><a href='ShowEditDetail.ashx?ID={0}'>Edit</a></td></tr>",
                                row["id"].ToString(), row["username"].ToString(), row["userpass"].ToString(),
                                row["email"].ToString(), row["regtime"].ToString());
                        }
                        string fileContent = System.IO.File.ReadAllText(filePath);
                        string sbstring = sb.ToString();
                        fileContent = fileContent.Replace("@tbady", sb.ToString());
                        context.Response.Write(fileContent);
                    }
                    else
                    {
                        context.Response.Write("No data");
                    }
                }
            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}