using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.DAO;
using WebApp.BLL;

namespace WebApp
{
    public partial class index : System.Web.UI.Page
    {
        protected string display = "display: none;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Try to get this user
                using (usuarioBLL bll = new usuarioBLL())
                {
                    usuario user = bll.GetUserByLoginAndPassword(email, password);

                    if (user != null)
                    {
                        // We have a user
                        Session["loggedin"] = user.id_usuario;
                        Session["userPrivilege"] = user.valor_nivel_acesso;
                        Session["userName"] = user.nome;
                    }
                    else
                    {
                        // User not found
                        display = "display: block;";
                        txtEmail.BorderColor = System.Drawing.Color.Red;
                        txtPassword.BorderColor = System.Drawing.Color.Red;
                    }
                }
            }

            if (Session["loggedin"] != null)
            {
                Response.Redirect("contacts.aspx");
            }
        }
    }
}