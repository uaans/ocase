<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<List<Ocase.Main.Model.Case>>" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<div>
	<table>
		<tr>
			<th>Nr.</th>
			<th>Title</th>
			<th>Date</th>
			<th>Type</th>
			<th>User</th>
			<th>Unit</th>
		</tr>
		<% foreach(var model in Model) { %>
		<tr>
			<td><%= model.Title %></td>
			<td><%= model.YearAndSequenceNumber() %></td>
			<td><%= model.CreationDate.ToString() %></td>
			<td><%if(model.Type != null)%><%= model.Type.Name %></td>
			<td><%if(model.Responsible != null)%><%= model.Responsible.Name %></td>
			<td><%if(model.OrganizationUnit != null)%><%= model.OrganizationUnit.Name %></td>
		</tr>
		<% } %>
	</table>
	</div>
</body>
