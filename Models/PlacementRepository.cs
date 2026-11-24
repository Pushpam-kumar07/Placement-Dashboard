using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace PlacementDashboard
{
    public class PlacementRecord
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string Program { get; set; }
        public string Company { get; set; }
        public string Role { get; set; }
        public decimal PackageLpa { get; set; }
        public string Status { get; set; }
        public DateTime AppliedOn { get; set; }
        public DateTime? OfferDate { get; set; }
    }

    public static class PlacementRepository
    {
        public static List<PlacementRecord> GetPlacements()
        {
            return new List<PlacementRecord>
            {
                Record("PD2401", "Aarav Mehta", "Computer Science", "Northstar Labs", "Software Engineer", 18.5m, "Placed", "2026-01-18", "2026-03-04"),
                Record("PD2402", "Ananya Iyer", "Information Technology", "Vertex Systems", "Data Analyst", 12.0m, "Placed", "2026-01-22", "2026-03-12"),
                Record("PD2403", "Rohan Kapoor", "Computer Science", "Northstar Labs", "Product Engineer", 16.0m, "Interviewing", "2026-02-02", null),
                Record("PD2404", "Diya Nair", "Electronics", "Bluepeak Digital", "Graduate Engineer", 9.5m, "Placed", "2026-02-05", "2026-03-18"),
                Record("PD2405", "Kabir Shah", "Information Technology", "Vertex Systems", "Software Engineer", 11.0m, "Applied", "2026-02-08", null),
                Record("PD2406", "Mira Reddy", "Computer Science", "Cedar Analytics", "Associate Data Scientist", 14.0m, "Interviewing", "2026-02-11", null),
                Record("PD2407", "Ishaan Das", "Mechanical", "Bluepeak Digital", "Operations Associate", 7.8m, "Placed", "2026-02-15", "2026-03-20"),
                Record("PD2408", "Sara Fernandes", "Electronics", "Cedar Analytics", "Business Analyst", 10.5m, "Applied", "2026-02-19", null),
                Record("PD2409", "Vihaan Joshi", "Computer Science", "Northstar Labs", "Platform Engineer", 20.0m, "Placed", "2026-02-21", "2026-03-25"),
                Record("PD2410", "Tara Banerjee", "Information Technology", "Vertex Systems", "UX Engineer", 13.5m, "Interviewing", "2026-02-24", null),
                Record("PD2411", "Arjun Pillai", "Mechanical", "Bluepeak Digital", "Graduate Engineer", 8.2m, "Applied", "2026-02-28", null),
                Record("PD2412", "Nisha Kumar", "Electronics", "Cedar Analytics", "Data Analyst", 11.8m, "Placed", "2026-03-01", "2026-04-02")
            };
        }

        public static DataTable GetReportData()
        {
            DataTable table = new DataTable("Placements");
            table.Columns.Add("StudentId", typeof(string));
            table.Columns.Add("StudentName", typeof(string));
            table.Columns.Add("Program", typeof(string));
            table.Columns.Add("Company", typeof(string));
            table.Columns.Add("Role", typeof(string));
            table.Columns.Add("PackageLpa", typeof(decimal));
            table.Columns.Add("Status", typeof(string));
            table.Columns.Add("AppliedOn", typeof(DateTime));
            table.Columns.Add("OfferDate", typeof(DateTime));

            foreach (PlacementRecord record in GetPlacements())
            {
                table.Rows.Add(
                    record.StudentId,
                    record.StudentName,
                    record.Program,
                    record.Company,
                    record.Role,
                    record.PackageLpa,
                    record.Status,
                    record.AppliedOn,
                    record.OfferDate.HasValue ? (object)record.OfferDate.Value : DBNull.Value);
            }

            return table;
        }

        private static PlacementRecord Record(string id, string name, string program, string company, string role, decimal package, string status, string applied, string offer)
        {
            return new PlacementRecord
            {
                StudentId = id,
                StudentName = name,
                Program = program,
                Company = company,
                Role = role,
                PackageLpa = package,
                Status = status,
                AppliedOn = DateTime.ParseExact(applied, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                OfferDate = offer == null ? (DateTime?)null : DateTime.ParseExact(offer, "yyyy-MM-dd", CultureInfo.InvariantCulture)
            };
        }
    }
}
