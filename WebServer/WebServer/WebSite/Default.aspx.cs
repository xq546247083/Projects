using System;

namespace Moqikaka.WebServer.Inuyasha.WebSite
{
    /// <summary>
    /// ����ҳ��
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Good");
        }
    }
}
