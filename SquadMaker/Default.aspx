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
            <asp:Label ID="lblError" runat="server" ForeColor="Red" />
        </div>
        <div id="divWaitingList">
            <h3>Waiting List</h3>
            <asp:GridView ID="grdWaitList" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="Player" DataField="FullName" />
                    <asp:BoundField HeaderText="Skating" DataField="SkatingRating" />
                    <asp:BoundField HeaderText="Shooting" DataField="ShootingRating" />
                    <asp:BoundField HeaderText="Checking" DataField="CheckingRating" />
                </Columns>
            </asp:GridView>
        </div>
        <div id="divSquads">
            <h3>Squads</h3>
            <asp:Repeater ID="rptSquads" runat="server">
                <ItemTemplate>
                    <asp:GridView ID="grdSquad" runat="server" AutoGenerateColumns="false" ShowFooter="true">
                        <Columns>
                            <asp:BoundField HeaderText="Player" DataField="FullName" />
                            <asp:BoundField HeaderText="Skating" DataField="SkatingRating" />
                            <asp:BoundField HeaderText="Shooting" DataField="ShootingRating" />
                            <asp:BoundField HeaderText="Checking" DataField="CheckingRating" />
                        </Columns>
                    </asp:GridView>
                    <br />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
