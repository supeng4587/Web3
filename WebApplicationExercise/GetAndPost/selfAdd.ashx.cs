using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationExercise.getAndpost {
    /// <summary>
    /// selfAdd 的摘要说明
    /// </summary>
    public class selfAdd : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/html";
            string filePath = context.Request.MapPath("selfAdd.html");
            string fileContext = System.IO.File.ReadAllText(filePath);
            int num;
            int.TryParse(context.Request.Form["txtName"], out num);
            num++;
            fileContext = fileContext.Replace("$txtName", num.ToString());
            context.Response.Write(fileContext);
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}