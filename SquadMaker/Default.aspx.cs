using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using SquadMaker.BLL;
using SquadMaker.Model;
using SquadMaker.Service;

namespace SquadMaker
{
    public partial class _Default : System.Web.UI.Page
    {
        private IPlayerAPI _API = new TestPlayerAPI();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PlayerList allPlayers = _API.GetAllPlayers();
                grdWaitList.DataSource = allPlayers.Players;
                grdWaitList.DataBind();

                lblNumWaitList.Text = allPlayers.Players.Count().ToString();
            }
        }

        protected void btnGenerateSquads_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            int numsquads;
            if (!Int32.TryParse(txtNumSquads.Text, out numsquads))
            {
                lblError.Text = "Invalid number of squads. Number of squads should be an integer.";
                return;
            }
            if (numsquads < 1)
            {
                lblError.Text = "Please choose 1 or more squads to create.";
                return;
            }

            List<PlayerData> playerList = _API.GetAllPlayers().Players;
            if (numsquads > playerList.Count())
            {
                lblError.Text = "There are not enough players for the number of squads chosen";
                return;
            }

            List<PlayerData> remainingPlayers;
            List<PlayerData>[] squads = Squads.GenerateSquads(numsquads, playerList, out remainingPlayers);

            divSquads.Visible = squads.Count() > 0;
            lblNumSquads.Text = squads.Count().ToString();
            lblNumPlayers.Text = (squads.Count() > 0) ? squads[0].Count.ToString() : "0";

            rptSquads.DataSource = squads;
            rptSquads.DataBind();

            int i = 0;
            foreach (RepeaterItem item in rptSquads.Items)
            {
                GridView grd = item.FindControl("grdSquad") as GridView;
                grd.DataSource = squads[i];
                grd.DataBind();
                grd.FooterRow.Cells[0].Text = "Average";
                grd.FooterRow.Cells[1].Text = Math.Round(squads[i].Average(ss => ss.SkatingRating),2).ToString();
                grd.FooterRow.Cells[2].Text = Math.Round(squads[i].Average(ss => ss.ShootingRating),2).ToString();
                grd.FooterRow.Cells[3].Text = Math.Round(squads[i].Average(ss => ss.CheckingRating),2).ToString();
                i++;
            }

            divWaitingList.Visible = remainingPlayers.Count() > 1;
            lblNumWaitList.Text = remainingPlayers.Count().ToString();
            grdWaitList.DataSource = remainingPlayers;
            grdWaitList.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtNumSquads.Text = "";

            divWaitingList.Visible = true;
            lblNumWaitList.Text = _API.GetAllPlayers().Players.Count().ToString();
            grdWaitList.DataSource = _API.GetAllPlayers().Players;
            grdWaitList.DataBind();

            divSquads.Visible = false;
            rptSquads.DataSource = null;
            rptSquads.DataBind();
        }
    }
}