using System;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;

namespace PlacementDashboard
{
    public class Global : HttpApplication
    {
        protected void Session_End(object sender, EventArgs e)
        {
            ReportDocument report = Session["PlacementReport"] as ReportDocument;
            if (report != null)
            {
                report.Close();
                report.Dispose();
            }
        }
    }
}
