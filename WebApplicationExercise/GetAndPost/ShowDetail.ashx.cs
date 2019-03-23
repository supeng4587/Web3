using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace WebApplicationExercise.GetAndPost {
    /// <summary>
    /// ShowDetail 的摘要说明
    /// </summary>
    public class ShowDetail : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/html";
            int id;
            if (int.TryParse(context.Request.QueryString["ID"], out id)) {
                string mySqlConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["mySqlConnStr"]
                    .ToString();
                using (MySqlConnection conn = new MySqlConnection(mySqlConnStr)) {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter("select * from userinfo where id = @id", conn)) {
                        MySqlParameter par = new MySqlParameter("@id", MySqlDbType.Int32);
                        par.Value = id;
                        adapter.SelectCommand.Parameters.Add(par);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0) {
                            string filePath = context.Request.MapPath("ShowDetailUserInfo.html");
                            string contentStr = System.IO.File.ReadAllText(filePath);
                            contentStr = contentStr.Replace("$ID", dt.Rows[0]["id"].ToString())
                                       .Replace("$UserName", dt.Rows[0]["username"].ToString())
                                       .Replace("$UserPass", dt.Rows[0]["userpass"].ToString())
                                       .Replace("$RegTime", dt.Rows[0]["regtime"].ToString())
                                       .Replace("$Email", dt.Rows[0]["email"].ToString());

                            context.Response.Write(contentStr);
                        }
                        else {
                            context.Response.Write("No data");
                        }


                    }
                }
            }
            else {
                context.Response.Redirect("Error.html");
            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}