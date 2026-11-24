using System;
using System.Configuration;
using System.IO;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;

namespace PlacementDashboard
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindReport();
        }

        private void BindReport()
        {
            ReportDocument report = Session["PlacementReport"] as ReportDocument;
            if (report == null)
            {
                string configuredPath = ConfigurationManager.AppSettings["CrystalReportPath"];
                string reportPath = String.IsNullOrWhiteSpace(configuredPath)
                    ? null
                    : Server.MapPath(configuredPath);

                if (String.IsNullOrWhiteSpace(reportPath) || !File.Exists(reportPath))
                {
                    ShowError("Add the Crystal Reports definition at the configured path (CrystalReportPath in Web.config). See README.md for the required fields.");
                    return;
                }

                try
                {
                    report = new ReportDocument();
                    report.Load(reportPath);
                    report.SetDataSource(PlacementRepository.GetReportData());
                    Session["PlacementReport"] = report;
                }
                catch (Exception ex)
                {
                    if (report != null)
                    {
                        report.Close();
                        report.Dispose();
                    }
                    ShowError(HttpUtility.HtmlEncode(ex.Message));
                    return;
                }
            }

            crystalViewer.ReportSource = report;
            if (!IsPostBack)
                crystalViewer.DataBind();
        }

        private void ShowError(string message)
        {
            pnlReportError.Visible = true;
            litReportError.Text = " " + message;
        }
    }
}
