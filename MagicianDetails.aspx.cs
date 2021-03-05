using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

namespace LasVegasMagicShowBDCS
{
    public partial class MagicianDetails : System.Web.UI.Page
    {
        int userlvl;
        int id;
        SqlConnection conn = new SqlConnection(@"data source = localhost; integrated security = true; database = LasVegasMagicStagedb");
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        string sqlsel = "";
        string sqlmage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            try
            {
                userlvl = (int)Session["mylevel"];
                id = (int)Session["userID"];
            }
            catch (NullReferenceException nre)
            {
                userlvl = 0;
                Session["mylevel"] = userlvl;
            }
            finally
            {
                SetUpPage();
            }


        }

        public void ShowData()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand(sqlsel, conn);

                rdr = cmd.ExecuteReader();
                RepeaterActs.DataSource = rdr;
                RepeaterActs.DataBind();
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        private void SetUpPage()
        {
            if(userlvl == 1)
            {
                sqlsel = "select * from Acts where MagicianID = '"+id+"'";
                sqlmage = "select Name, ArtistName from Magicians where MagicianID = '" + id + "'";
                ShowData();
                ShowMage();
            }
            else
            {
                Response.Redirect("NotLogin.aspx");
            }
        }

        public void ShowMage()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand(sqlmage, conn);

                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    MageName.Text = rdr.GetValue(0).ToString();
                    ArtistName.Text =  rdr.GetValue(1).ToString();
                }
            }
            catch(Exception ex)
            {
                Label1.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }


        protected void ButtonLogout_Click(object sender, EventArgs e)
        {
            userlvl = 0;
            Session["mylevel"] = userlvl;
            Response.Redirect("Index.aspx");
        }

        protected void RepeaterActs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "Edit")
            { 
                TextBoxTitle.Text = (e.Item.FindControl("Title") as Label).Text.Trim();
                TextBoxDesc.Text = (e.Item.FindControl("Desc") as Label).Text.Trim();
                TextBoxDuration.Text = (e.Item.FindControl("Duration") as Label).Text.Trim();
                TextBoxImage.Text = (e.Item.FindControl("Picture") as Label).Text.Trim();
                string actid = e.CommandArgument.ToString();
                LabelID.Text = actid;

                divCreateUpadate.Attributes.CssStyle.Add("display", "block");
                ButtonUpdate.Attributes.CssStyle.Add("display", "block");
                ButtonCreate.Attributes.CssStyle.Add("display", "none");

            }
            if(e.CommandName == "Delete")
            {

                string sqldel = "Delete from Acts where Acts.ActID = @idNum";
                SqlCommand cmddel = null;

                try
                {
                    conn.Open();

                    cmddel = new SqlCommand(sqldel, conn);
                    cmddel.Parameters.AddWithValue("@idNum", e.CommandArgument);
                    cmddel.ExecuteNonQuery();

                    Label1.Text = "act " + e.CommandArgument.ToString() + " was deleted.";
                    
                }
                catch (Exception ex)
                {
                    Label1.Text = "This act is on the program at the moment. To Delete the manager has to remove it from the program first.";
                }
                finally
                {
                    conn.Close();
                }

                ShowData();

            }

        }

        protected void ButtonCreate_Click(object sender, EventArgs e)
        {
            SqlCommand cmdcreate = null;
            string sqlcreate = "insert into Acts values (@Title, @MagicianID, @Description, @Picture, @Duration)";

            if (TextBoxTitle.Text != "" && TextBoxDuration.Text != "")
            {

                try
                {
                    conn.Open();
                    cmdcreate = new SqlCommand(sqlcreate, conn);

                    cmdcreate.Parameters.Add("@Title", SqlDbType.Text);
                    cmdcreate.Parameters.Add("@MagicianID", SqlDbType.Int);
                    cmdcreate.Parameters.Add("@Description", SqlDbType.Text);
                    cmdcreate.Parameters.Add("@Picture", SqlDbType.Text);
                    cmdcreate.Parameters.Add("@Duration", SqlDbType.Int);

                    cmdcreate.Parameters["@Title"].Value = TextBoxTitle.Text;
                    cmdcreate.Parameters["@MagicianID"].Value = id;
                    cmdcreate.Parameters["@Description"].Value = TextBoxDesc.Text;
                    cmdcreate.Parameters["@Picture"].Value = TextBoxImage.Text;
                    cmdcreate.Parameters["@Duration"].Value = Convert.ToInt32(TextBoxDuration.Text) * 60;

                    cmdcreate.ExecuteNonQuery();

                    Label1.Text = "Act added";
                    LabelCU.Text = "";

                }
                catch (Exception ex)
                {
                    LabelCU.Text = ex.Message;
                }
                finally
                {
                    conn.Close();
                }

                ShowData();
                TextBoxDesc.Text = string.Empty;
                TextBoxDuration.Text = string.Empty;
                TextBoxImage.Text = string.Empty;
                TextBoxTitle.Text = string.Empty;
                divCreateUpadate.Attributes.CssStyle.Add("display", "none");
            }
            else
            {
                LabelCU.Text = "You need to fill at least title and duration.";
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand cmdUpdate = null;

            string sqlupd = "update Acts set Title = @title, MagicianID= @mageid, Description = @desc, Picture = @pic, Duration = @dur where ActID = @actid";
            try
            {
                if (TextBoxTitle.Text != "" && TextBoxDuration.Text != "")
                {
                    conn.Open();

                    cmdUpdate = new SqlCommand(sqlupd, conn);

                    cmdUpdate.Parameters.Add("@title", SqlDbType.Text);
                    cmdUpdate.Parameters.Add("@mageid", SqlDbType.Int);
                    cmdUpdate.Parameters.Add("@desc", SqlDbType.Text);
                    cmdUpdate.Parameters.Add("@pic", SqlDbType.Text);
                    cmdUpdate.Parameters.Add("@dur", SqlDbType.Int);
                    cmdUpdate.Parameters.Add("@actid", SqlDbType.Int);

                    cmdUpdate.Parameters["@title"].Value = TextBoxTitle.Text;
                    cmdUpdate.Parameters["@mageid"].Value = id;
                    cmdUpdate.Parameters["@desc"].Value = TextBoxDesc.Text;
                    cmdUpdate.Parameters["@pic"].Value = TextBoxImage.Text;
                    cmdUpdate.Parameters["@dur"].Value = Convert.ToInt32(TextBoxDuration.Text) * 60;
                
                    cmdUpdate.Parameters["@actid"].Value = LabelID.Text;

                    cmdUpdate.ExecuteNonQuery();
                    Label1.Text = "Act " + LabelID.Text + " was apdated";
                    LabelCU.Text = "";
                    divCreateUpadate.Attributes.CssStyle.Add("display", "none");
                }
                else
                {
                    LabelCU.Text = "You need to fill at least title and duration.";
                }
            }
            catch (Exception ex)
            {
                LabelCU.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            ShowData();
            TextBoxDesc.Text = string.Empty;
            TextBoxDuration.Text = string.Empty;
            TextBoxImage.Text = string.Empty;
            TextBoxTitle.Text = string.Empty;
            
        }

        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            if (FileUploadImg.HasFile)
            {
                if(TextBoxImage.Text != "")
                {
                    FileUploadImg.SaveAs(Server.MapPath("~/Pictures/") + TextBoxImage.Text.Trim());
                    LabelFileinfo.Text = "Image added to Picture folder.";

                }
                else
                {
                    LabelFileinfo.Text = "Filename missing.";
                }

            }
            else
            {
                LabelFileinfo.Text = "Wrong file name.";

            }
        }

        protected void ButtonOpenCreate_Click(object sender, EventArgs e)
        {
            divCreateUpadate.Attributes.CssStyle.Add("display", "block");
            ButtonUpdate.Attributes.CssStyle.Add("display", "none");
            ButtonCreate.Attributes.CssStyle.Add("display", "block");
            TextBoxDesc.Text = string.Empty;
            TextBoxDuration.Text = string.Empty;
            TextBoxImage.Text = string.Empty;
            TextBoxTitle.Text = string.Empty;
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            divCreateUpadate.Attributes.CssStyle.Add("display", "none");
            ButtonCreate.Attributes.CssStyle.Add("display", "none");
            TextBoxDesc.Text = string.Empty;
            TextBoxDuration.Text = string.Empty;
            TextBoxImage.Text = string.Empty;
            TextBoxTitle.Text = string.Empty;
            LabelCU.Text = "";
        }
    }
}