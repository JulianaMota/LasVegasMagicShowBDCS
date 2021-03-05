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
    public partial class Secretaries : System.Web.UI.Page
    {
        int userlvl;
        int id;
        SqlConnection conn = new SqlConnection(@"data source = localhost; integrated security = true; database = LasVegasMagicStagedb");
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        string sqlstring = "";
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
            
            ButtonDeleteMage.Attributes.CssStyle.Add("display", "none");
            ButtonDeleteMan.Attributes.CssStyle.Add("display", "none");
            ButtonDeleteSecre.Attributes.CssStyle.Add("display", "none");
            ButtonUpdate.Attributes.CssStyle.Add("display", "none");
            ButtonUpdateMan.Attributes.CssStyle.Add("display", "none");
            ButtonUpdateSecre.Attributes.CssStyle.Add("display", "none");
        }

        //SETUP PAGE TO CONTROL LOGIN AND DISPLAY TABLES
        private void SetUpPage()
        {
            if (userlvl == 2)
            {
                ShowMagicians();
                ShowManager();
                ShowSecretaries();
                ShowPerson();
            }
            else
            {
                Response.Redirect("NotLogin.aspx");
            }
        }

        //LOGOUT
        protected void ButtonLogout_Click(object sender, EventArgs e)
        {
            userlvl = 0;
            Session["mylevel"] = userlvl;
            Response.Redirect("Index.aspx");
        }

        //SHOW SECRETARY LOGGED IN
        public void ShowPerson()
        {
            sqlstring = "select Name, SecretaryID from Secretaries where SecretaryID = '" + id + "'";
            SecretaryID.Text = "";
            SecreName.Text = "";
            try
            {
                conn.Open();
                cmd = new SqlCommand(sqlstring, conn);

                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    SecretaryID.Text = rdr.GetValue(1).ToString();
                    SecreName.Text = rdr.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                LabelMess.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        //DISPLAY MAGICIANS TABLE
        public void ShowMagicians()
        {
            sqlstring = "select * from Magicians";

            try
            {
                conn.Open();

                cmd = new SqlCommand(sqlstring, conn);
                rdr = cmd.ExecuteReader();

                GridViewMagicians.DataSource = rdr;
                GridViewMagicians.DataBind();
            }
            catch (Exception ex)
            {
                LabelMess.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        //DISPLAY SECRETARY TABLE
        public void ShowSecretaries()
        {
            sqlstring = "select * from Secretaries";

            try
            {
                conn.Open();

                cmd = new SqlCommand(sqlstring, conn);
                rdr = cmd.ExecuteReader();

                GridViewSecretaries.DataSource = rdr;
                GridViewSecretaries.DataBind();
            }
            catch (Exception ex)
            {
                LabelMess.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        //DISPLAY MANAGER TABLE
        public void ShowManager()
        {
            sqlstring = "select * from Managers";

            try
            {
                conn.Open();

                cmd = new SqlCommand(sqlstring, conn);
                rdr = cmd.ExecuteReader();

                GridViewManager.DataSource = rdr;
                GridViewManager.DataBind();
            }
            catch (Exception ex)
            {
                LabelMess.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        //OPEN MAGICIAN CREATION FORM
        protected void ButtonAddM_Click(object sender, EventArgs e)
        {
            divCU.Attributes.CssStyle.Add("display", "block");
            mageFields.Attributes.CssStyle.Add("display", "block");
            secrFields.Attributes.CssStyle.Add("display", "none");
            ButtonCreateM.Attributes.CssStyle.Add("display", "block");
            ButtonCreateMan.Attributes.CssStyle.Add("display", "none");
            ButtonCreateS.Attributes.CssStyle.Add("display", "none");
            
        }

        //CREATE MAGICIAN
        protected void ButtonCreateM_Click(object sender, EventArgs e)
        {
            sqlstring = "insert into Magicians values (@Name, @ArtistName, @Password, @ManagerID)";

            if(TextBoxName.Text != "" && TextBoxPass.Text != "" && TextBoxManID.Text != "")
            { 
           
                try
                {
                    conn.Open();

                    cmd = new SqlCommand(sqlstring, conn);

                    cmd.Parameters.Add("@Name", SqlDbType.Text);
                    cmd.Parameters.Add("@ArtistName", SqlDbType.Text);
                    cmd.Parameters.Add("@Password", SqlDbType.Text);
                    cmd.Parameters.Add("@ManagerID", SqlDbType.Int);

                    cmd.Parameters["@Name"].Value = TextBoxName.Text;
                    cmd.Parameters["@ArtistName"].Value = TextBoxAName.Text;
                    cmd.Parameters["@Password"].Value = TextBoxPass.Text;
                    cmd.Parameters["@ManagerID"].Value = Convert.ToInt32(TextBoxManID.Text);

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    LabelInfo.Text = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
                ShowMagicians();
                TextBoxName.Text = string.Empty;
                TextBoxAName.Text = string.Empty;
                TextBoxPass.Text = string.Empty;
                TextBoxManID.Text = string.Empty;
                divCU.Attributes.CssStyle.Add("display", "none");
                LabelInfo.Text = "";
            }
            else
            {
                LabelInfo.Text = "Password or Name or Manager id missing";
            }
        }

        //OPEN SECRETARY CREATION FORM
        protected void ButtonAddS_Click(object sender, EventArgs e)
        {
            divCU.Attributes.CssStyle.Add("display", "block");
            mageFields.Attributes.CssStyle.Add("display", "none");
            secrFields.Attributes.CssStyle.Add("display", "block");
            ButtonCreateM.Attributes.CssStyle.Add("display", "none");
            ButtonCreateMan.Attributes.CssStyle.Add("display", "none");
            ButtonCreateS.Attributes.CssStyle.Add("display", "block");
        }

        //CREATE SECRETARY
        protected void ButtonCreateS_Click(object sender, EventArgs e)
        {
            sqlstring = "insert into Secretaries values (@Name, @Password, @MagicianID)";

            if (TextBoxName.Text != "" && TextBoxPass.Text != "" && TextBoxMID.Text != "")
            {

                try
                {
                    conn.Open();

                    cmd = new SqlCommand(sqlstring, conn);

                    cmd.Parameters.Add("@Name", SqlDbType.Text);
                    cmd.Parameters.Add("@Password", SqlDbType.Text);
                    cmd.Parameters.Add("@MagicianID", SqlDbType.Int);

                    cmd.Parameters["@Name"].Value = TextBoxName.Text;
                    cmd.Parameters["@Password"].Value = TextBoxPass.Text;
                    cmd.Parameters["@MagicianID"].Value = Convert.ToInt32(TextBoxMID.Text);

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    LabelInfo.Text = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
                ShowSecretaries();
                TextBoxName.Text = string.Empty;
                TextBoxPass.Text = string.Empty;
                TextBoxMID.Text = string.Empty;
                divCU.Attributes.CssStyle.Add("display", "none");
                LabelInfo.Text = "";
            }
            else
            {
                LabelInfo.Text = "Password or Name or Magicain ID missing";
            }
        }

        //OPEN MANAGER CREATION
        protected void ButtonAddMan_Click(object sender, EventArgs e)
        {
            divCU.Attributes.CssStyle.Add("display", "block");
            mageFields.Attributes.CssStyle.Add("display", "none");
            secrFields.Attributes.CssStyle.Add("display", "none");

            ButtonCreateM.Attributes.CssStyle.Add("display", "none");
            ButtonCreateMan.Attributes.CssStyle.Add("display", "block");
            ButtonCreateS.Attributes.CssStyle.Add("display", "none");

        }

        // CREATE MANAGER
        protected void ButtonCreateMan_Click(object sender, EventArgs e)
        {
            sqlstring = "insert into Managers values (@Name, @Password)";

            if (TextBoxName.Text != "" && TextBoxPass.Text != "")
            {

                try
                {
                    conn.Open();

                    cmd = new SqlCommand(sqlstring, conn);

                    cmd.Parameters.Add("@Name", SqlDbType.Text);
                    cmd.Parameters.Add("@Password", SqlDbType.Text);

                    cmd.Parameters["@Name"].Value = TextBoxName.Text;
                    cmd.Parameters["@Password"].Value = TextBoxPass.Text;

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    LabelInfo.Text = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
                ShowManager();
                TextBoxName.Text = string.Empty;
                TextBoxPass.Text = string.Empty;
                divCU.Attributes.CssStyle.Add("display", "none");
                LabelInfo.Text = "";
            }
            else
            {
                LabelInfo.Text = "Password or Name missing";
            }
        }

        // SELECT MAGICIAN
        protected void GridViewMagicians_SelectedIndexChanged(object sender, EventArgs e)
        {
            divUD.Attributes.CssStyle.Add("display", "block");
            TextBoxUName.Text = GridViewMagicians.SelectedRow.Cells[2].Text;
            TextBoxUAAName.Text = (GridViewMagicians.SelectedRow.Cells[3].Text != "&nbsp;") ? GridViewMagicians.SelectedRow.Cells[3].Text :  "" ;
            TextBoxUPass.Text = GridViewMagicians.SelectedRow.Cells[4].Text;
            TextBoxUManID.Text = GridViewMagicians.SelectedRow.Cells[5].Text;

            personName.InnerText = GridViewMagicians.SelectedRow.Cells[2].Text;
            personId.InnerText = GridViewMagicians.SelectedRow.Cells[1].Text;

            ButtonDeleteMage.Attributes.CssStyle.Add("display", "block");
            ButtonUpdate.Attributes.CssStyle.Add("display", "block");

            divUpdate.Attributes.CssStyle.Add("display", "none");
        }

        //OPEN UPDATE Mage
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            divUD.Attributes.CssStyle.Add("display", "block");
            divUpdate.Attributes.CssStyle.Add("display", "block");
            ButtonUMage.Attributes.CssStyle.Add("display", "block");
            DivUmage.Attributes.CssStyle.Add("display", "block");
            DivUSecre.Attributes.CssStyle.Add("display", "none");
            ButtonUSecre.Attributes.CssStyle.Add("display", "none");
            ButtonUMan.Attributes.CssStyle.Add("display", "none");
            ButtonUMage.Attributes.CssStyle.Add("display", "block");

        }
        
        //UPDATE MAGE
        protected void ButtonUMage_Click(object sender, EventArgs e)
        {
            sqlstring = "Update Magicians set Name = @Name, ArtistName = @ArtistName, Password = @Password, ManagerID = @ManagerID Where MagicianID = @ID";

            try 
            {
                if (TextBoxUName.Text != "" && TextBoxUPass.Text != "" && TextBoxUManID.Text != "")
                {

                    conn.Open();

                    cmd = new SqlCommand(sqlstring, conn);

                    cmd.Parameters.Add("@Name", SqlDbType.Text);
                    cmd.Parameters.Add("@ArtistName", SqlDbType.Text);
                    cmd.Parameters.Add("@Password", SqlDbType.Text);
                    cmd.Parameters.Add("@ManagerID", SqlDbType.Int);
                    cmd.Parameters.Add("@ID", SqlDbType.Int);

                    cmd.Parameters["@Name"].Value = TextBoxUName.Text;
                    cmd.Parameters["@ArtistName"].Value = TextBoxUAAName.Text;
                    cmd.Parameters["@Password"].Value = TextBoxUPass.Text;
                    cmd.Parameters["@ManagerID"].Value = Convert.ToInt32(TextBoxUManID.Text);
                    cmd.Parameters["@ID"].Value = Convert.ToInt32(personId.InnerText);

                    cmd.ExecuteNonQuery();
                    LabelMessMage.Text = "The Magician " + personId.InnerText + " was updated";
                    LabelUInfo.Text = "";
                    divUD.Attributes.CssStyle.Add("display", "none");
                    
                }
                else
                {
                    LabelUInfo.Text= "Password or Name or Manager ID missing";
                }
            }
            catch (Exception ex)
            {
                LabelMessMage.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            ShowMagicians();
        }

        //DELETE MAGE
        protected void ButtonDeleteMage_Click(object sender, EventArgs e)
        {
            sqlstring = "Delete from Magicians where MagicianID = @id";

            try
            {
                conn.Open();
                cmd = new SqlCommand(sqlstring, conn);
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = Convert.ToInt32(personId.InnerText);

                cmd.ExecuteNonQuery();
                divUD.Attributes.CssStyle.Add("display", "none");
                LabelMessMage.Text = "The Magician " + personId.InnerText + " has been deleted";
            }
            catch (Exception ex)
            {
                LabelMessMage.Text = "The Magician " + personId.InnerText + " should not have any acts on the program or any secretary assigned";
            }
            finally
            {
                conn.Close();
            }
            ShowMagicians();
        }

        //SELECT SECRETARY
        protected void GridViewSecretaries_SelectedIndexChanged(object sender, EventArgs e)
        {
            divUD.Attributes.CssStyle.Add("display", "block");
            TextBoxUName.Text = GridViewSecretaries.SelectedRow.Cells[2].Text;
            TextBoxUPass.Text = GridViewSecretaries.SelectedRow.Cells[3].Text;
            TextBoxUMID.Text = GridViewSecretaries.SelectedRow.Cells[4].Text;

            personName.InnerText = GridViewSecretaries.SelectedRow.Cells[2].Text;
            personId.InnerText = GridViewSecretaries.SelectedRow.Cells[1].Text;

            ButtonDeleteSecre.Attributes.CssStyle.Add("display", "block");
            ButtonUpdateSecre.Attributes.CssStyle.Add("display", "block");

            divUpdate.Attributes.CssStyle.Add("display", "none");
        }

        //OPEN UPDATE SECRETARY
        protected void ButtonUpdateSecre_Click(object sender, EventArgs e)
        {
            divUD.Attributes.CssStyle.Add("display", "block");
            divUpdate.Attributes.CssStyle.Add("display", "block");
            ButtonUSecre.Attributes.CssStyle.Add("display", "block");
            DivUSecre.Attributes.CssStyle.Add("display", "block");
            DivUmage.Attributes.CssStyle.Add("display", "none");
            ButtonUSecre.Attributes.CssStyle.Add("display", "block");
            ButtonUMan.Attributes.CssStyle.Add("display", "none");
            ButtonUMage.Attributes.CssStyle.Add("display", "none");

        }

        //UPDATE SECRETARY
        protected void ButtonUSecre_Click(object sender, EventArgs e)
        {
            sqlstring = "Update Secretaries set Name = @Name, Password = @Password, MagicianID = @MagicianID Where SecretaryID = @ID";

            try
            {
                if (TextBoxUName.Text != "" && TextBoxUPass.Text != "" && TextBoxUMID.Text != "")
                {
                    conn.Open();

                    cmd = new SqlCommand(sqlstring, conn);

                    cmd.Parameters.Add("@Name", SqlDbType.Text);
                    cmd.Parameters.Add("@Password", SqlDbType.Text);
                    cmd.Parameters.Add("@MagicianID", SqlDbType.Int);
                    cmd.Parameters.Add("@ID", SqlDbType.Int);

                    cmd.Parameters["@Name"].Value = TextBoxUName.Text;
                    cmd.Parameters["@Password"].Value = TextBoxUPass.Text;
                    cmd.Parameters["@MagicianID"].Value = Convert.ToInt32(TextBoxUMID.Text);
                    cmd.Parameters["@ID"].Value = Convert.ToInt32(personId.InnerText);

                    cmd.ExecuteNonQuery();
                    LabelMessSecre.Text = "The Secretary " + personId.InnerText + " was updated";
                    divUD.Attributes.CssStyle.Add("display", "none");
                    
                }
                else
                {
                    LabelUInfo.Text = "Password or Name or Magician ID missing";
                }
            }
            catch (Exception ex)
            {
                LabelMessSecre.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            ShowSecretaries();
        }

        //DELETE SECRETARY
        protected void ButtonDeleteSecre_Click(object sender, EventArgs e)
        {
            sqlstring = "Delete from Secretaries where SecretaryID = @id";

            try
            {
                conn.Open();
                cmd = new SqlCommand(sqlstring, conn);
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = Convert.ToInt32(personId.InnerText);

                cmd.ExecuteNonQuery();
                divUD.Attributes.CssStyle.Add("display", "none");
                LabelMessSecre.Text = "The Secretary " + personId.InnerText + " has been deleted";
            }
            catch (Exception ex)
            {
                LabelMessSecre.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            ShowSecretaries();
        }

        //SELECT MANAGER
        protected void GridViewManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            divUD.Attributes.CssStyle.Add("display", "block");
            TextBoxUName.Text = GridViewManager.SelectedRow.Cells[2].Text;
            TextBoxUPass.Text = GridViewManager.SelectedRow.Cells[3].Text;

            personName.InnerText = GridViewManager.SelectedRow.Cells[2].Text;
            personId.InnerText = GridViewManager.SelectedRow.Cells[1].Text;

            ButtonDeleteMan.Attributes.CssStyle.Add("display", "block");
            ButtonUpdateMan.Attributes.CssStyle.Add("display", "block");

            divUpdate.Attributes.CssStyle.Add("display", "none");
          
        }

        //OPEN UPDATE MANAGER
        protected void ButtonUpdateMan_Click(object sender, EventArgs e)
        {
            divUD.Attributes.CssStyle.Add("display", "block");
            divUpdate.Attributes.CssStyle.Add("display", "block");
            ButtonUMan.Attributes.CssStyle.Add("display", "block");
            DivUmage.Attributes.CssStyle.Add("display", "none");
            DivUSecre.Attributes.CssStyle.Add("display", "none");
            ButtonUSecre.Attributes.CssStyle.Add("display", "none");
            ButtonUMan.Attributes.CssStyle.Add("display", "block");
            ButtonUMage.Attributes.CssStyle.Add("display", "none");

        }

        //UPDATE MANAGER
        protected void ButtonUMan_Click(object sender, EventArgs e)
        {
            sqlstring = "Update Managers set Name = @Name, Password = @Password Where ManagerID = @ID";

            try
            {
                if (TextBoxUName.Text != "" && TextBoxUPass.Text != "")
                {
                    conn.Open();

                    cmd = new SqlCommand(sqlstring, conn);

                    cmd.Parameters.Add("@Name", SqlDbType.Text);
                    cmd.Parameters.Add("@Password", SqlDbType.Text);
                    cmd.Parameters.Add("@ID", SqlDbType.Int);

                    cmd.Parameters["@Name"].Value = TextBoxUName.Text;
                    cmd.Parameters["@Password"].Value = TextBoxUPass.Text;
                    cmd.Parameters["@ID"].Value = Convert.ToInt32(personId.InnerText);

                    cmd.ExecuteNonQuery();
                    LabelMess.Text = "The Manager " + personId.InnerText + " was updated";
                    LabelUInfo.Text = "";
                    divUD.Attributes.CssStyle.Add("display", "none");
                }
                else
                {
                    LabelUInfo.Text = "Password or Name missing";
                }
            }
            catch (Exception ex)
            {
                LabelMess.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            ShowManager();
        }

        //DELETE MANAGER
        protected void ButtonDeleteMan_Click(object sender, EventArgs e)
        {
            sqlstring = "Delete from Managers where ManagerID = @id";

            try
            {
                conn.Open();
                cmd = new SqlCommand(sqlstring, conn);
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = Convert.ToInt32(personId.InnerText);

                cmd.ExecuteNonQuery();
                divUD.Attributes.CssStyle.Add("display", "none");
                LabelMess.Text = "The Manager " + personId.InnerText + " has been deleted";
            }
            catch (Exception ex)
            {
                LabelMessSecre.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            ShowManager();
        }

        //close create
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            divCU.Attributes.CssStyle.Add("display", "none");
            LabelInfo.Text = "";
        }

        protected void ButtonCancelUD_Click(object sender, EventArgs e)
        {
            divUD.Attributes.CssStyle.Add("display", "none");
            LabelUInfo.Text = "";
            DivUmage.Attributes.CssStyle.Add("display", "none");
            DivUSecre.Attributes.CssStyle.Add("display", "none");
            ButtonUSecre.Attributes.CssStyle.Add("display", "none");
            ButtonUMan.Attributes.CssStyle.Add("display", "none");
            ButtonUMage.Attributes.CssStyle.Add("display", "none");
        }
    }
}