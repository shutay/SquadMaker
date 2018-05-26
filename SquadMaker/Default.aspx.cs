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
            Players allPlayers = PlayerAPI.GetAllPlayers();
            grdWaitList.DataSource = allPlayers.players;
            grdWaitList.DataBind();
        }

        protected void grdWaitList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            PlayerData playerData = e.Row.DataItem as PlayerData;
            ((Label)e.Row.FindControl("lblSkating")).Text = playerData.skills.Single(s => s.type == "Skating").rating.ToString();
            ((Label)e.Row.FindControl("lblShooting")).Text = playerData.skills.Single(s => s.type == "Shooting").rating.ToString();
            ((Label)e.Row.FindControl("lblChecking")).Text = playerData.skills.Single(s => s.type == "Checking").rating.ToString();
        }

        protected void btnGenerateSquads_Click(object sender, EventArgs e)
        {

        }
    }
}