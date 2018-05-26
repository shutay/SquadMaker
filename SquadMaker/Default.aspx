<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SquadMaker._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Squad Maker</title>
</head>
<body>
    <form runat="server">
        <div id="divGenerateSquad">
            <h3>Generate Squads</h3>
            Number of squads: <asp:TextBox runat="server" ID="txtNumSquads" />
            <asp:Button ID="btnGenerateSquads" runat="server" OnClick="btnGenerateSquads_Click" Text="Generate" />
        </div>
        <div id="divWaitingList">
            <h3>Waiting List</h3>
            <asp:GridView ID="grdWaitList" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdWaitList_RowDataBound">
                <Columns>
                    <asp:BoundField HeaderText="Player" DataField="FullName" />
                     <asp:TemplateField HeaderText="Skating">
                        <ItemTemplate>
                            <asp:Label ID="lblSkating" runat="server" Text='' />
                        </ItemTemplate>
                     </asp:TemplateField>
                    <asp:TemplateField HeaderText="Shooting">
                        <ItemTemplate>
                            <asp:Label ID="lblShooting" runat="server" Text='' />
                        </ItemTemplate>
                     </asp:TemplateField>
                    <asp:TemplateField HeaderText="Checking">
                        <ItemTemplate>
                            <asp:Label ID="lblChecking" runat="server" Text='' />
                        </ItemTemplate>
                     </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="divSquads"></div>
    </form>
</body>
</html>
