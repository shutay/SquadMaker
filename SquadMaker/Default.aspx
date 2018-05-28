<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SquadMaker._Default" Title="Squad Maker" MasterPageFile="~/SquadMaker.Master" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <div id="divGenerateSquad">
        <h3>Generate Squads</h3>
        Number of squads: <asp:TextBox runat="server" ID="txtNumSquads" />
        <asp:Button ID="btnGenerateSquads" runat="server" OnClick="btnGenerateSquads_Click" Text="Generate" />
        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" /><br />
        <asp:Label ID="lblError" runat="server" ForeColor="Red" />
        <br />
    </div>
    <div id="divSquads" runat="server" visible="false">
        <h3>Squads</h3>
        Number of squads: <asp:Label ID="lblNumSquads" runat="server" /><br />
        Number of players per squad: <asp:Label ID="lblNumPlayers" runat="server" /><br /><br />
        <asp:Repeater ID="rptSquads" runat="server">
            <ItemTemplate>
                <asp:GridView ID="grdSquad" runat="server" AutoGenerateColumns="false" ShowFooter="true" CssClass="defaultGrid">
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
    <div id="divWaitingList" runat="server">
        <h3>Waiting List</h3>
        Number of People in Waiting List: <asp:Label ID="lblNumWaitList" runat="server" /><br /><br />
        <asp:GridView ID="grdWaitList" runat="server" AutoGenerateColumns="false" CssClass="defaultGrid waitListGrid">
            <Columns>
                <asp:BoundField HeaderText="Player" DataField="FullName" />
                <asp:BoundField HeaderText="Skating" DataField="SkatingRating" />
                <asp:BoundField HeaderText="Shooting" DataField="ShootingRating" />
                <asp:BoundField HeaderText="Checking" DataField="CheckingRating" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
