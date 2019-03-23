using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace WebApplicationExercise.GetAndPost {
    /// <summary>
    /// AddUserInfo 的摘要说明
    /// </summary>
    public class AddUserInfo : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/html";
            string txtName = context.Request.Form["txtName"];
            string txtPass = context.Request.Form["txtPass"];
            string txtEMail = context.Request.Form["txtEMail"];

            string mySqlConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["mySqlConnStr"].ToString();
            using (MySqlConnection conn = new MySqlConnection(mySqlConnStr))
            {
                using (MySqlCommand command =new MySqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = "insert into userinfo (username,userpass,email,regtime) values(@username,@userpass,@email,@regtime)";
                    MySqlParameter[] par =
                    {
                        new MySqlParameter("@username", MySqlDbType.VarChar, 100),
                        new MySqlParameter("@userpass",MySqlDbType.VarChar,100),
                        new MySqlParameter("@email",MySqlDbType.VarChar,100),
                        new MySqlParameter("@regtime",MySqlDbType.DateTime), 
                    };

                    par[0].Value = txtName;
                    par[1].Value = txtPass;
                    par[2].Value = txtEMail;
                    par[3].Value = DateTime.Now;
                    command.Parameters.AddRange(par);
                    conn.Open();
                    if (command.ExecuteNonQuery() > 0)
                    {
                        context.Response.Redirect("UserInfoList.ashx");
                    }
                    else
                    {
                        context.Response.Redirect("Error.html");
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