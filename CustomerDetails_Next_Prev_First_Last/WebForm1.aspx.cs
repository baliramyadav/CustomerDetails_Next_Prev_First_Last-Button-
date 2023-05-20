using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CustomerDetails_Next_Prev_First_Last
{
   
    public partial class WebForm1 : System.Web.UI.Page
    {
        DataSet ds;
        int RowIndex;
        //string constr = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {            
                if (Session["CustomerDS"] == null)
                {
                    SqlDataAdapter da = new SqlDataAdapter("Select Custid, Name, Balance, City, Status From tblCustomer Order By Custid", ReadCS.ConStr);
                    ds = new DataSet();
                    da.Fill(ds,"tblCustomer");
                    Session["CustomerDS"] = ds;
                    Session["RowIndex"] = 0;
                    ShowData();
                }
                else
                {
                    ds = (DataSet)Session["CustomerDS"];
                    RowIndex = (int)Session["RowIndex"];
                }
            LoadData();
        }
        private void LoadData()
        {            
            GridView1.DataSource = ds;
            GridView1.DataBind();           
        }
        private void ShowData()
        {
            //access data from DataSet
            txtId.Text = ds.Tables[0].Rows[RowIndex]["Custid"].ToString();
            txtName.Text = ds.Tables[0].Rows[RowIndex]["Name"].ToString();
            txtBalance.Text = ds.Tables[0].Rows[RowIndex]["Balance"].ToString();
            txtCity.Text = ds.Tables[0].Rows[RowIndex]["City"].ToString();
            cbStatus.Checked =(bool) ds.Tables[0].Rows[RowIndex]["Status"];
            Session["RowIndex"] = RowIndex;
        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            RowIndex = 0; ShowData();
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if(RowIndex>0)
            {
                RowIndex -= 1;
                ShowData();
            }
            else
            {
                Response.Write("<script>alert('First record of the table.')</script>");
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (RowIndex < ds.Tables[0].Rows.Count-1)
            {
                RowIndex += 1;
                ShowData();
            }
            else
            {
                Response.Write("<script>alert('Last record of the table.')</script>");
            }
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            RowIndex = ds.Tables[0].Rows.Count - 1;
            ShowData();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex=e.NewPageIndex;
            LoadData();
        }
    }
}