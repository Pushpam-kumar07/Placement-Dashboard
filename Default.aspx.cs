using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace PlacementDashboard
{
    public partial class Default : System.Web.UI.Page
    {
        private const string SortExpressionKey = "PlacementSortExpression";
        private const string SortDescendingKey = "PlacementSortDescending";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSummary();
                BindGrid();
            }
        }

        protected void FiltersChanged(object sender, EventArgs e)
        {
            gvPlacements.PageIndex = 0;
            BindGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvPlacements.PageIndex = 0;
            BindGrid();
        }

        protected void gvPlacements_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlacements.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void gvPlacements_Sorting(object sender, GridViewSortEventArgs e)
        {
            e.Cancel = true;
            bool descending = (string)ViewState[SortExpressionKey] == e.SortExpression &&
                ViewState[SortDescendingKey] != null && !(bool)ViewState[SortDescendingKey];
            ViewState[SortExpressionKey] = e.SortExpression;
            ViewState[SortDescendingKey] = descending;
            gvPlacements.PageIndex = 0;
            BindGrid();
        }

        protected string GetStatusCss(object status)
        {
            string value = Convert.ToString(status);
            if (value == "Placed") return "status-placed";
            if (value == "Interviewing") return "status-interviewing";
            return "status-applied";
        }

        private void BindSummary()
        {
            List<PlacementRecord> records = PlacementRepository.GetPlacements();
            List<PlacementRecord> placed = records.Where(r => r.Status == "Placed").ToList();
            int offers = placed.Count;
            int companyCount = placed.Select(r => r.Company).Distinct().Count();
            decimal average = offers == 0 ? 0 : placed.Average(r => r.PackageLpa);

            lblPlaced.Text = offers.ToString();
            lblRate.Text = (records.Count == 0 ? 0 : (decimal)offers * 100 / records.Count).ToString("0") + "%";
            lblOffers.Text = offers.ToString();
            lblCompanies.Text = companyCount.ToString();
            lblAveragePackage.Text = average.ToString("0.0");
            lblTotal.Text = records.Count.ToString();

            rptPipeline.DataSource = new[]
            {
                PipelineItem("Placed", records.Count(r => r.Status == "Placed"), records.Count, "bar-mint"),
                PipelineItem("Interviewing", records.Count(r => r.Status == "Interviewing"), records.Count, "bar-violet"),
                PipelineItem("Applied", records.Count(r => r.Status == "Applied"), records.Count, "bar-amber")
            };
            rptPipeline.DataBind();

            var employerCounts = placed.GroupBy(r => r.Company)
                .Select(g => new { Company = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ThenBy(g => g.Company)
                .Take(4)
                .ToList();
            string[] companyColors = { "company-mint", "company-blue", "company-violet", "company-amber" };
            var employerItems = employerCounts.Select((g, index) => new
            {
                g.Company,
                g.Count,
                Percent = offers == 0 ? 0 : (int)Math.Round((decimal)g.Count * 100 / offers),
                CssClass = companyColors[index],
                Initials = String.Concat(g.Company.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Take(2).Select(part => part[0]))
            });
            rptEmployers.DataSource = employerItems;
            rptEmployers.DataBind();
        }

        private void BindGrid()
        {
            IEnumerable<PlacementRecord> records = PlacementRepository.GetPlacements();
            if (!String.IsNullOrEmpty(ddlProgram.SelectedValue))
                records = records.Where(r => r.Program == ddlProgram.SelectedValue);
            if (!String.IsNullOrEmpty(ddlStatus.SelectedValue))
                records = records.Where(r => r.Status == ddlStatus.SelectedValue);
            if (!String.IsNullOrWhiteSpace(txtSearch.Text))
            {
                string query = txtSearch.Text.Trim();
                records = records.Where(r => r.StudentName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    r.StudentId.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    r.Company.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    r.Role.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            string sortExpression = ViewState[SortExpressionKey] as string;
            bool descending = ViewState[SortDescendingKey] != null && (bool)ViewState[SortDescendingKey];
            Func<PlacementRecord, IComparable> sortKey = null;
            if (sortExpression == "StudentName") sortKey = r => r.StudentName;
            else if (sortExpression == "Program") sortKey = r => r.Program;
            else if (sortExpression == "Company") sortKey = r => r.Company;
            else if (sortExpression == "Role") sortKey = r => r.Role;
            else if (sortExpression == "PackageLpa") sortKey = r => r.PackageLpa;
            else if (sortExpression == "Status") sortKey = r => r.Status;

            if (sortKey != null)
                records = descending ? records.OrderByDescending(sortKey) : records.OrderBy(sortKey);

            List<PlacementRecord> result = records.ToList();
            lblResultCount.Text = result.Count.ToString();
            gvPlacements.DataSource = result;
            gvPlacements.DataBind();
        }

        private static object PipelineItem(string label, int count, int total, string cssClass)
        {
            return new
            {
                Label = label,
                Count = count,
                WidthPercent = total == 0 ? 0 : Math.Max(5, (int)Math.Round((decimal)count * 100 / total)),
                CssClass = cssClass
            };
        }
    }
}
