<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Ocase.Main.Model.Case>" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<div>
	<% using(Html.BeginForm("Create", "Case")) { %>
		<fieldset>
			<table>
				<tr>
					<td>Title</td>
					<td><%= Html.TextBox("Title") %></td>
				</tr>
				<tr>
					<td colspan="2"><input type="submit" value="Create" /></td>
				</tr>
			</table>
			
		</fieldset>
	<% } %>
	</div>
</body>
