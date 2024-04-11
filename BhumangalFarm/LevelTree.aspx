<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LevelTree.aspx.cs" Inherits="OmPrabha.LevelTree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="hdf">
    <!-- Required meta tags -->
   <link href="https://fonts.googleapis.com/css2?family=Inter:wght@500&display=swap" rel="stylesheet">
    <title>Dolphin</title>
   <style>
       .trvBroker_1
       {
           font-size:1.2rem !important;
       }
       .trvBroker_2
       {
           padding:5px !important;
       }
   </style>
</head><body>
    <form id="form1" runat="server">
        <div id="BrokerTree">
            <asp:TreeView ID="trvBroker" runat="server" ExpandDepth="1" ImageSet="Simple" ShowLines="True">
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <NodeStyle CssClass="gridViewToolTip" Font-Names="Inter" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
            </asp:TreeView>
        </div>
    </form>
</body>
</html>