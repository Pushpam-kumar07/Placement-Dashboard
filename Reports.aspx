<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="PlacementDashboard.Reports" %>
<%@ Register Assembly="CrystalDecisions.Web" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Placement reports</title>
    <link href="Content/site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="report-shell">
            <header class="report-topbar">
                <a class="brand report-brand" href="Default.aspx"><span class="brand-mark">P</span><span>pathway<span class="brand-sub">CAREER SERVICES</span></span></a>
                <a class="button button-light" href="Default.aspx">← Back to dashboard</a>
            </header>
            <main class="report-main">
                <p class="eyebrow">PLACEMENT OFFICE / REPORTING</p>
                <div class="report-title-row"><div><h1>Placement reports</h1><p class="page-description">View, print, and export the placement summary with Crystal Reports.</p></div></div>
                <asp:Panel ID="pnlReportError" runat="server" CssClass="report-error" Visible="false">
                    <strong>Crystal Report could not be loaded.</strong>
                    <asp:Literal ID="litReportError" runat="server" />
                </asp:Panel>
                <section class="panel crystal-panel">
                    <CR:CrystalReportViewer ID="crystalViewer" runat="server" AutoDataBind="true"
                        ToolPanelView="None" HasCrystalLogo="false" HasToggleGroupTreeButton="false"
                        HasDrilldownTabs="false" EnableDatabaseLogonPrompt="false" />
                </section>
            </main>
        </div>
    </form>
</body>
</html>
