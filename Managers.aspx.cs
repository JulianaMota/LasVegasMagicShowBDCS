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
    public partial class Managers : System.Web.UI.Page
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
        

        }

        //SETUP PAGE TO CONTROL LOGIN AND DISPLAY TABLES
        private void SetUpPage()
        {
            if (userlvl == 3)
            {
                ShowProgram();
                ShowActs();
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

        //SHOW MANAGER LOGGED IN
        public void ShowPerson()
        {
            sqlstring = "select Name, ManagerID from Managers where ManagerID = '" + id + "'";
            ManName.Text = "";
            ManID.Text = "";
            try
            {
                conn.Open();
                cmd = new SqlCommand(sqlstring, conn);

                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    ManID.Text = rdr.GetValue(1).ToString();
                    ManName.Text = rdr.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                LabelErrMan.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        //display program
        public void ShowProgram()
        {
            sqlstring = "select A.ActID, A.Title, A.Duration, P.ProgramID, P.SequenceNum From Program P, Acts A where P.ActID = A.ActID order by P.SequenceNum";

            try
            {
                conn.Open();

                cmd = new SqlCommand(sqlstring, conn);
                rdr = cmd.ExecuteReader();

                GridViewPorgram.DataSource = rdr;
                GridViewPorgram.DataBind();
            }
            catch (Exception ex)
            {
                LabelMessP.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        //display Acts
        public void ShowActs()
        {
            sqlstring = "select A.ActID, A.Title, A.Duration From Acts A order by A.ActID";

            try
            {
                conn.Open();

                cmd = new SqlCommand(sqlstring, conn);
                rdr = cmd.ExecuteReader();

                GridViewActs.DataSource = rdr;
                GridViewActs.DataBind();
            }
            catch (Exception ex)
            {
                LabelMessP.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        //Select Act from prohram
        protected void GridViewPorgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            divModal.Attributes.CssStyle.Add("display", "block");
            TextBoxSequence.Text = GridViewPorgram.SelectedRow.Cells[5].Text;
            actName.InnerText = GridViewPorgram.SelectedRow.Cells[2].Text;
            actID.InnerText = GridViewPorgram.SelectedRow.Cells[1].Text;
            divPForm.Attributes.CssStyle.Add("display", "block");
            divPAForm.Attributes.CssStyle.Add("display", "none");
        }

        //OPEN CHANGE SEQUENCE
        protected void ButtonChange_Click(object sender, EventArgs e)
        {
            divModal.Attributes.CssStyle.Add("display", "block");
            divPAForm.Attributes.CssStyle.Add("display", "block");
            ButtonAddAct.Attributes.CssStyle.Add("display", "none");
            divPForm.Attributes.CssStyle.Add("display", "none");
            ButtonSave.Attributes.CssStyle.Add("display", "block");

        }

        //CHANGE SEQUENCE
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            sqlstring = "select * from Program where SequenceNum = @Num";
            
            try
            {
                conn.Open();

                cmd = new SqlCommand(sqlstring, conn);
                cmd.Parameters.AddWithValue("@Num", Convert.ToInt32(TextBoxSequence.Text));
                

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                bool sequenceExist = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));

                if(sequenceExist == false)
                {
                    sqlstring = "Update Program set SequenceNum = @Num Where ActID = @ID";

                    cmd = new SqlCommand(sqlstring, conn);

                    cmd.Parameters.Add("@Num", SqlDbType.Int);
                    cmd.Parameters.Add("@ID", SqlDbType.Int);

                    cmd.Parameters["@Num"].Value = Convert.ToInt32(TextBoxSequence.Text);
                    cmd.Parameters["@ID"].Value = Convert.ToInt32(actID.InnerText);

                    cmd.ExecuteNonQuery();
                    LabelMessP.Text = "The Act " + actID.InnerText + " have change sequence number " + TextBoxSequence.Text;
                    divModal.Attributes.CssStyle.Add("display", "none");
                    LabelErrModal.Text = "";
                    
                }
                else
                {
                    LabelErrModal.Text = "You can't use an existing sequence number.";
                }
                TextBoxSequence.Text = string.Empty;

            }
            catch (Exception ex)
            {
                LabelMessP.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            ShowProgram();
        }

        protected void ButtonRemove_Click(object sender, EventArgs e)
        {
            sqlstring = "Delete from Program where ActID = @id";

            try
            {
                conn.Open();
                cmd = new SqlCommand(sqlstring, conn);
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = Convert.ToInt32(actID.InnerText);

                cmd.ExecuteNonQuery();
                LabelMessP.Text = "The Act " + actID.InnerText + " was removed from Program";
                divModal.Attributes.CssStyle.Add("display", "none");
            }
            catch (Exception ex)
            {
                LabelMessP.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            ShowProgram();
        }

        protected void GridViewActs_SelectedIndexChanged(object sender, EventArgs e)
        {
            divModal.Attributes.CssStyle.Add("display", "block");
            actName.InnerText = GridViewActs.SelectedRow.Cells[2].Text;
            actID.InnerText = GridViewActs.SelectedRow.Cells[1].Text;
            divPAForm.Attributes.CssStyle.Add("display", "block");
            ButtonSave.Attributes.CssStyle.Add("display", "none");
            divPForm.Attributes.CssStyle.Add("display", "none");
            ButtonAddAct.Attributes.CssStyle.Add("display", "block");
        }

        protected void ButtonAddAct_Click(object sender, EventArgs e)
        {
            sqlstring = "select * from Program where SequenceNum = @Num";

            try
            {
                conn.Open();

                cmd = new SqlCommand(sqlstring, conn);
                cmd.Parameters.AddWithValue("@Num", Convert.ToInt32(TextBoxSequence.Text));

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                bool sequenceExist = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));

                if (TextBoxSequence.Text != "" && sequenceExist == false)
                {
                    sqlstring = "insert into Program values (@ActID, @SequenceNum)";

                    cmd = new SqlCommand(sqlstring, conn);

                    cmd.Parameters.Add("@SequenceNum", SqlDbType.Int);
                    cmd.Parameters.Add("@ActID", SqlDbType.Int);

                    cmd.Parameters["@SequenceNum"].Value = Convert.ToInt32(TextBoxSequence.Text);
                    cmd.Parameters["@ActID"].Value = Convert.ToInt32(actID.InnerText);

                    LabelMessP.Text = actID.InnerText + " was added to the program with sequence number " + TextBoxSequence.Text;
                    cmd.ExecuteNonQuery();
                    divModal.Attributes.CssStyle.Add("display", "none");
                    LabelErrModal.Text = "";
                }
                else
                {
                    LabelErrModal.Text = "Sequence number missing or already exists.";
                }

            }
            catch (Exception ex)
            {
                LabelMessP.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            ShowProgram();
            TextBoxSequence.Text = string.Empty;
            
           
        }

        //close selection
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            divModal.Attributes.CssStyle.Add("display", "none");
            LabelErrModal.Text = "";
            divPForm.Attributes.CssStyle.Add("display", "none");
            divPAForm.Attributes.CssStyle.Add("display", "none");
            TextBoxSequence.Text = string.Empty;
        }
    }
}