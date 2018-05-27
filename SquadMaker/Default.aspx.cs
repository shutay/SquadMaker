using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SquadMaker.Model;
using SquadMaker.Service;

namespace SquadMaker
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PlayerList allPlayers = PlayerAPI.GetAllPlayers();
                grdWaitList.DataSource = allPlayers.Players;
                grdWaitList.DataBind();
            }
        }

        protected void btnGenerateSquads_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            int numsquads;
            if (!Int32.TryParse(txtNumSquads.Text, out numsquads))
            {
                lblError.Text = "Invalid number of squads. Number of Squads should be an integer.";
                return;
            }
            if (numsquads < 1)
            {
                lblError.Text = "Please choose 1 or more squads to create.";
                return;
            }

            List<PlayerData> playerList = PlayerAPI.GetAllPlayers().Players;
            if (numsquads > playerList.Count())
            {
                lblError.Text = "There are not enough players for the number of squads chosen";
                return;
            }

            SquadData squadData = new SquadData(playerList);
            squadData.GenerateSquads(numsquads);

            rptSquads.DataSource = squadData.Squads;
            rptSquads.DataBind();

            int i = 0;
            foreach (RepeaterItem item in rptSquads.Items)
            {
                GridView grd = item.FindControl("grdSquad") as GridView;
                grd.DataSource = squadData.Squads[i];
                grd.DataBind();
                grd.FooterRow.Cells[0].Text = "Average";
                grd.FooterRow.Cells[1].Text = Math.Round(squadData.Squads[i].Average(ss => ss.SkatingRating),2).ToString();
                grd.FooterRow.Cells[2].Text = Math.Round(squadData.Squads[i].Average(ss => ss.ShootingRating),2).ToString();
                grd.FooterRow.Cells[3].Text = Math.Round(squadData.Squads[i].Average(ss => ss.CheckingRating),2).ToString();
                i++;
            }

            grdWaitList.DataSource = squadData.RemainingPlayers;
            grdWaitList.DataBind();
        }
    }
}