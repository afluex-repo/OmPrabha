<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLevelTree.aspx.cs" Inherits="OmPrabha.AdminLevelTree" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="hdf">
    <!-- Required meta tags -->

    <title>Dolphin</title>
    <link rel="stylesheet" type="text/css" href="../../files/bower_components/bootstrap/css/bootstrap.min.css" />
        <script type="text/javascript" src="../../files/bower_components/bootstrap/js/bootstrap.min.js"></script>
</head>
<body>
    <div class="container">
        <div class="row">
        <div class="col-sm-12">
     <form id="form1" runat="server">
        <div class="row mt-4">
            <div class="col-md-3">
                <div class="form-group">
                   Login Id
                    <asp:TextBox ID="txtloginid" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <br />
                   <asp:Button ID="btnsearch" runat="server" Text="Get Details" CssClass="btn btn-success" OnClick="btnsearch_Click" />
                </div>
            </div>
        </div>
        <div id="BrokerTree">
            <asp:TreeView ID="trvBroker" runat="server" ExpandDepth="1" ImageSet="Simple" ShowLines="True">
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <NodeStyle CssClass="gridViewToolTip" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
            </asp:TreeView>
        </div>
    </form>
            </div>
    </div>
    </div>
</body>
</html>

