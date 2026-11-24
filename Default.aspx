<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PlacementDashboard.Default" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Placement dashboard</title>
    <link href="Content/site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="app-shell">
            <aside class="sidebar">
                <a class="brand" href="Default.aspx">
                    <span class="brand-mark">P</span>
                    <span>pathway<span class="brand-sub">CAREER SERVICES</span></span>
                </a>
                <div class="nav-caption">WORKSPACE</div>
                <a class="nav-link active" href="Default.aspx"><span class="nav-icon">▦</span> Overview</a>
                <a class="nav-link" href="#placements"><span class="nav-icon">◷</span> Placements</a>
                <a class="nav-link" href="Reports.aspx"><span class="nav-icon">▤</span> Reports</a>
                <div class="sidebar-bottom">
                    <div class="season-card">
                        <span class="season-label">CURRENT SEASON</span>
                        <strong>Class of 2026</strong>
                        <span class="season-dot"><i></i> Recruiting in progress</span>
                    </div>
                    <div class="profile">
                        <span class="avatar">PS</span>
                        <span><strong>Placement Office</strong><small>Administrator</small></span>
                    </div>
                </div>
            </aside>

            <main class="main-content">
                <header class="topbar">
                    <div class="breadcrumb">Workspace <span>/</span> <strong>Overview</strong></div>
                    <div class="topbar-right"><span class="live-dot"></span> Data updated just now <span class="topbar-divider"></span><span class="topbar-date">2025–26 Academic Year</span></div>
                </header>

                <section class="page-heading">
                    <div>
                        <p class="eyebrow">PLACEMENT OFFICE</p>
                        <h1>Good morning, team <span class="wave">✳</span></h1>
                        <p class="page-description">Here’s how your students are doing this placement season.</p>
                    </div>
                    <a class="button button-dark" href="Reports.aspx"><span>↗</span> View reports</a>
                </section>

                <section class="metrics" aria-label="Placement summary">
                    <article class="metric-card">
                        <div class="metric-top"><span class="metric-label">Students placed</span><span class="metric-icon mint">↗</span></div>
                        <div class="metric-value"><asp:Label ID="lblPlaced" runat="server" /></div>
                        <div class="metric-foot"><span class="positive">↑ 12%</span> <span>vs. last season</span></div>
                        <div class="metric-rule"><i class="rule-mint"></i></div>
                    </article>
                    <article class="metric-card">
                        <div class="metric-top"><span class="metric-label">Placement rate</span><span class="metric-icon violet">◎</span></div>
                        <div class="metric-value"><asp:Label ID="lblRate" runat="server" /></div>
                        <div class="metric-foot"><span class="positive">↑ 4.8%</span> <span>vs. last season</span></div>
                        <div class="metric-rule"><i class="rule-violet"></i></div>
                    </article>
                    <article class="metric-card">
                        <div class="metric-top"><span class="metric-label">Offers received</span><span class="metric-icon amber">✳</span></div>
                        <div class="metric-value"><asp:Label ID="lblOffers" runat="server" /></div>
                        <div class="metric-foot"><span class="neutral">Across</span> <asp:Label ID="lblCompanies" runat="server" /> <span>companies</span></div>
                        <div class="metric-rule"><i class="rule-amber"></i></div>
                    </article>
                    <article class="metric-card">
                        <div class="metric-top"><span class="metric-label">Average package</span><span class="metric-icon blue">₹</span></div>
                        <div class="metric-value"><asp:Label ID="lblAveragePackage" runat="server" /><small> LPA</small></div>
                        <div class="metric-foot"><span class="positive">↑ 8.2%</span> <span>vs. last season</span></div>
                        <div class="metric-rule"><i class="rule-blue"></i></div>
                    </article>
                </section>

                <section class="insights-grid">
                    <article class="panel funnel-panel">
                        <div class="panel-heading">
                            <div><h2>Placement pipeline</h2><p>Student progress at a glance</p></div>
                            <span class="panel-kicker">CLASS OF 2026</span>
                        </div>
                        <asp:Repeater ID="rptPipeline" runat="server">
                            <ItemTemplate>
                                <div class="pipeline-row">
                                    <div class="pipeline-label"><span><%# Eval("Label") %></span><strong><%# Eval("Count") %></strong></div>
                                    <div class="pipeline-track"><span class='<%# Eval("CssClass") %>' style='<%# "width:" + Eval("WidthPercent") + "%" %>'></span></div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="pipeline-foot"><span>All candidates</span><strong><asp:Label ID="lblTotal" runat="server" /></strong></div>
                    </article>
                    <article class="panel employers-panel">
                        <div class="panel-heading">
                            <div><h2>Top employers</h2><p>Offers by company</p></div>
                            <a class="text-link" href="Reports.aspx">Full report <span>→</span></a>
                        </div>
                        <asp:Repeater ID="rptEmployers" runat="server">
                            <ItemTemplate>
                                <div class="employer-row">
                                    <span class="company-mark <%# Eval("CssClass") %>"><%# Eval("Initials") %></span>
                                    <span class="employer-name"><strong><%# Eval("Company") %></strong><small><%# Eval("Count") %> offers</small></span>
                                    <span class="employer-value"><%# Eval("Percent") %>%</span>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </article>
                </section>

                <section class="panel placements-panel" id="placements">
                    <div class="panel-heading table-heading">
                        <div><h2>Recent placements</h2><p>Track applications and offers across your campus</p></div>
                        <span class="record-count"><asp:Label ID="lblResultCount" runat="server" /> records</span>
                    </div>
                    <div class="filters">
                        <div class="search-wrap"><span>⌕</span><asp:TextBox ID="txtSearch" runat="server" CssClass="search-input" placeholder="Search students or companies" /></div>
                        <asp:DropDownList ID="ddlProgram" runat="server" CssClass="filter-select" AutoPostBack="true" OnSelectedIndexChanged="FiltersChanged">
                            <asp:ListItem Text="All programs" Value="" />
                            <asp:ListItem Text="Computer Science" Value="Computer Science" />
                            <asp:ListItem Text="Information Technology" Value="Information Technology" />
                            <asp:ListItem Text="Electronics" Value="Electronics" />
                            <asp:ListItem Text="Mechanical" Value="Mechanical" />
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="filter-select" AutoPostBack="true" OnSelectedIndexChanged="FiltersChanged">
                            <asp:ListItem Text="All statuses" Value="" />
                            <asp:ListItem Text="Placed" Value="Placed" />
                            <asp:ListItem Text="Interviewing" Value="Interviewing" />
                            <asp:ListItem Text="Applied" Value="Applied" />
                        </asp:DropDownList>
                        <asp:Button ID="btnSearch" runat="server" CssClass="button button-light" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                    <div class="table-wrap">
                        <asp:GridView ID="gvPlacements" runat="server" AutoGenerateColumns="false" CssClass="data-grid" GridLines="None"
                            AllowPaging="true" PageSize="6" AllowSorting="true" OnPageIndexChanging="gvPlacements_PageIndexChanging"
                            OnSorting="gvPlacements_Sorting" EmptyDataText="No placements match these filters.">
                            <Columns>
                                <asp:BoundField DataField="StudentName" HeaderText="STUDENT" SortExpression="StudentName" />
                                <asp:BoundField DataField="Program" HeaderText="PROGRAM" SortExpression="Program" />
                                <asp:BoundField DataField="Company" HeaderText="COMPANY" SortExpression="Company" />
                                <asp:BoundField DataField="Role" HeaderText="ROLE" SortExpression="Role" />
                                <asp:BoundField DataField="PackageLpa" HeaderText="PACKAGE" SortExpression="PackageLpa" DataFormatString="₹{0:0.0} LPA" />
                                <asp:TemplateField HeaderText="STATUS" SortExpression="Status">
                                    <ItemTemplate><span class='status-pill <%# GetStatusCss(Eval("Status")) %>'><i></i><%# Eval("Status") %></span></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="grid-pager" />
                            <HeaderStyle CssClass="grid-header" />
                            <RowStyle CssClass="grid-row" />
                        </asp:GridView>
                    </div>
                </section>
                <footer class="footer"><span>Pathway Placement Office</span><span>Academic year 2025–26 <i>·</i> Internal dashboard</span></footer>
            </main>
        </div>
    </form>
</body>
</html>
