using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.DAO;
using WebApp.BLL;
using System.Data;

namespace WebApp
{
    public partial class contacts : System.Web.UI.Page
    {
        protected string userName = string.Empty;
        private int _adminLevel = 10;
        private SortDirection _sortDirection { 
            get {
                if (ViewState["dirState"] == null) ViewState["dirState"] = SortDirection.Ascending;
                return (SortDirection) ViewState["dirState"];
            }
            set {
                ViewState["dirState"] = value;
            }
        }
        private static string _sortExpression = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedin"] == null)
            {
                Response.Redirect("index.aspx");
            }

            userName = Session["userName"].ToString();
            //Session["userPrivilege"] = "1";
            if (!UserIsAdmin()) {
                pnlMnuAdmin.Visible = false;
                pnlSeller.Controls.Clear();
                pnlSeller.Controls.Add(new Literal { Text = "&nbsp;" });
            }

            if (!IsPostBack)
            {
                BindCities(ddlCity);
                BindClassifications(ddlClassification);
                if (ddlSeller != null) {
                    BindSellers(ddlSeller);
                }
            }
        }

        // Logout
        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session["loggedin"] = null;
            Session["userPrivilege"] = null;
            Session["userName"] = null;
            Response.Redirect("index.aspx");
        }

        // Populate & bind the cities dropdown list
        private void BindCities(DropDownList ddl) {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem { Text = "All cities", Value = "0" });
            ddl.AppendDataBoundItems = true;

            using (cidadeBLL bll = new cidadeBLL())
            {
                ddl.DataTextField = "nome";
                ddl.DataValueField = "id_cidade";
                ddl.DataSource = bll.GetCities();
                ddl.DataBind();                
            }
        }

        // Populate & bind the regions dropdown list
        private void BindRegioes(DropDownList ddl, int cityID)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem { Text = "All regions", Value = "0" });
            ddl.AppendDataBoundItems = true;

            using (regiaoBLL bll = new regiaoBLL())
            {
                ddl.DataTextField = "nome";
                ddl.DataValueField = "id_regiao";
                ddl.DataSource = bll.GetRegionsFromACity(cityID);
                ddl.DataBind();
            }
        }

        // Populate the region dropdown list on selecting a city
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            string cityID = ddl.SelectedValue;

            if (cityID != "0") {
                ddlRegion.Enabled = true;
                BindRegioes(ddlRegion, Convert.ToInt32(cityID));
            }
            else
            {
                ddlRegion.Items.Clear();
                ddlRegion.Items.Add(new ListItem { Text = "Select a City", Value = "0" });
                ddlRegion.Enabled = false;
            }
        }

        // Populate & bind the classification dropdown list
        private void BindClassifications(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem { Text = "All classifications", Value = "0" });
            ddl.AppendDataBoundItems = true;

            using (classificacaoBLL bll = new classificacaoBLL())
            {
                ddl.DataTextField = "nome";
                ddl.DataValueField = "id_classificacao";
                ddl.DataSource = bll.GetClassifications();
                ddl.DataBind();
            }
        }

        // Populate & bind the user dropdown list
        private void BindSellers(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem { Text = "All sellers", Value = "0" });
            ddl.AppendDataBoundItems = true;

            using (usuarioBLL bll = new usuarioBLL())
            {
                ddl.DataTextField = "nome";
                ddl.DataValueField = "id_usuario";
                ddl.DataSource = bll.GetUsersUpToAnAccessLevel(10);
                ddl.DataBind();
            }
        }

        // Search
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Set up the customer object that will serve as criteria for the search
            customer criteria = new customer();

            criteria.nome = txtName.Text.Trim();
            criteria.sexo = Convert.ToInt32(ddlGender.SelectedValue);
            criteria.id_cidade = Convert.ToInt32(ddlCity.SelectedValue);
            criteria.id_regiao = Convert.ToInt32(ddlRegion.SelectedValue);
            DateTime fromDate = DateTime.MinValue;
            DateTime.TryParse(txtFromDate.Text, out fromDate);
            criteria.fromLastPurchase = fromDate;
            DateTime toDate = DateTime.MinValue;
            DateTime.TryParse(txtToDate.Text, out toDate);
            criteria.toLastPurchase = toDate;
            criteria.id_classificacao = Convert.ToInt32(ddlClassification.SelectedValue);
            if (ddlSeller != null && ddlSeller.Items.Count > 0) {
                criteria.id_usuario = Convert.ToInt32(ddlSeller.SelectedValue);
            }
            else
            {
                criteria.id_usuario = Convert.ToInt32(Session["loggedin"].ToString());
            }
            List<customer> list;
            using(customerBLL bll = new customerBLL())
	        {
                 list = bll.GetCustomersByCriteria(criteria);
	        }

            grdContacts.AutoGenerateColumns = false;
            grdContacts.DataSource = list;
            grdContacts.DataBind();

            if (!UserIsAdmin())
            {
                grdContacts.Columns[7].Visible = false;
            }

            ViewState["dtSource"] = list;
        }

        protected void grdContacts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                // Seller
                if (!UserIsAdmin()) {
                    e.Row.Cells[7].Visible = false;
                }
            }
        }

        protected void grdContacts_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridView grdView = (GridView)sender;

            // Retrieve the saved list
            List<customer> list = (List<customer>)ViewState["dtSource"];

            if (_sortExpression != e.SortExpression)
            {
                // If we are sorting a different column, reset the sorting direction
                _sortDirection = SortDirection.Ascending;
            }
            else
            {
                // It's the same column, change the direction of the last sorting
                _sortDirection = ChangeDirection(_sortDirection);
            }
            _sortExpression = e.SortExpression;

            // Shorthands
            string upCaret = " " + Server.HtmlDecode("&#9650;");
            string downCaret = " " + Server.HtmlDecode("&#9660;");

            // Sort the generic list that will feed the gridview
            list = SortList(list);
            
            // Clean the header text
            foreach (DataControlField column in grdView.Columns)
            {
                string ht = column.HeaderText;
                if (ht.IndexOf(upCaret) >= 0 || ht.IndexOf(downCaret) >= 0)
                {
                    ht = ht.Substring(2);
                    column.HeaderText = ht;
                }
            }
            
            // Get the index and header text of the sorted column
            int i = GetColumnIndex(grdView);
            string headerText = grdView.Columns[i].HeaderText;

            // Rewrite the header text for the sorted column
            grdView.Columns[i].HeaderText = (_sortDirection == SortDirection.Ascending ? upCaret : downCaret) + headerText;

            // Rebind the gridview to the list
            grdView.DataSource = list;
            grdView.DataBind();
            
            // Save the re-sorted list
            ViewState["dtSource"] = list;
        }

        // Little helper for getting the index of the sorted column
        private int GetColumnIndex(GridView grdView)
        {
            int i = 0;
            foreach (DataControlField c in grdView.Columns) {
                if (c.SortExpression == _sortExpression) break;
                i++;
            }
            return i;
        }

        // Changes the direction of the sorting expression
        private SortDirection ChangeDirection(SortDirection previous)
        {
            if (previous == SortDirection.Ascending) return SortDirection.Descending;
            return SortDirection.Ascending;
        }

        // Sorts the gridview
        private List<customer> SortList(List<customer> list) {
            switch (_sortExpression)
            {
                case "classificacaoNome" :
                    if (_sortDirection == SortDirection.Ascending) return list.OrderBy(c => c.classificacaoNome).ToList();
                    else return list.OrderByDescending(c => c.classificacaoNome).ToList();
                case "phone" :
                    if (_sortDirection == SortDirection.Ascending) return list.OrderBy(c => c.phone).ToList();
                    else return list.OrderByDescending(c => c.phone).ToList();
                case "sexoNome" :
                    if (_sortDirection == SortDirection.Ascending) return list.OrderBy(c => c.sexoNome).ToList();
                    else return list.OrderByDescending(c => c.sexoNome).ToList();
                case "cidadeNome" :
                    if (_sortDirection == SortDirection.Ascending) return list.OrderBy(c => c.cidadeNome).ToList();
                    else return list.OrderByDescending(c => c.cidadeNome).ToList();
                case "regiaoNome" :
                    if (_sortDirection == SortDirection.Ascending) return list.OrderBy(c => c.regiaoNome).ToList();
                    else return list.OrderByDescending(c => c.regiaoNome).ToList();
                case "ultima_compra" :
                    if (_sortDirection == SortDirection.Ascending) return list.OrderBy(c => c.ultima_compra).ToList();
                    else return list.OrderByDescending(c => c.ultima_compra).ToList();
                case "usuarioNome" :
                    if (_sortDirection == SortDirection.Ascending) return list.OrderBy(c => c.usuarioNome).ToList();
                    else return list.OrderByDescending(c => c.usuarioNome).ToList();
                default : // Name
                    if (_sortDirection == SortDirection.Ascending) return list.OrderBy(c => c.nome).ToList();
                    else return list.OrderByDescending(c => c.nome).ToList();
            }
        }

        // Tests if user is an admin
        private bool UserIsAdmin() {
            return (Convert.ToInt32(Session["userPrivilege"].ToString()) == _adminLevel);
        }

        // Clean the form
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtName.Text = string.Empty;
            ddlGender.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            ddlCity_SelectedIndexChanged(ddlCity, null);
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            ddlClassification.SelectedIndex = 0;
            if(ddlSeller != null) ddlSeller.SelectedIndex = 0;
        }
    }
}