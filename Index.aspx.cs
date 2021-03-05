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
    public partial class Index : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"data source = localhost; integrated security = true; database = LasVegasMagicStagedb");
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        string sqlprogram = "";
        SqlCommand cmdTotal = null;
        string sqlTotalTime = "";

        int userlvl;
        int id;
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
                TextBoxPass.TextMode = TextBoxMode.Password;
                SetUpPage();
            }

            sqlprogram = "select P.SequenceNum, A.Title, A.Description, A.Picture, A.Duration From Program P, Acts A where P.ActID = A.ActID order by P.SequenceNum";
            sqlTotalTime = "select sum(A.Duration) as total From Program P, Acts A where P.ActID = A.ActID";
            ShowData();  
            }

        public void ShowData()
        {

            try
            {
                conn.Open();
                cmd = new SqlCommand(sqlprogram, conn);
                rdr = cmd.ExecuteReader();
                RepeaterProgram.DataSource = rdr;
                RepeaterProgram.DataBind();

                conn.Close();
                conn.Open();
                cmdTotal = new SqlCommand(sqlTotalTime, conn);

                object result = cmdTotal.ExecuteScalar();

                TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(result));

                string answer = string.Format("{0:D2}h:{1:D2}m",
                t.Hours,
                t.Minutes);

                LabelTotal.Text = answer;
                LabelTotal.DataBind();
            }
            catch (Exception ex)
            {
                LabelErrMess.Text = ex.Message;
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
                Response.Redirect("MagicianDetails.aspx");
            }
            else if(userlvl == 2)
            {
                Response.Redirect("Secretary.aspx");
            }
            else if(userlvl == 3)
            {
                Response.Redirect("Managers.aspx");
            }
        }

        protected void ButtonOpenL_Click(object sender, EventArgs e)
        {
            divLogin.Attributes.CssStyle.Add("display", "block");
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            

            SqlCommand cmdM = null;
            SqlCommand cmdS = null;
            SqlCommand cmdMN = null;

            string sqlmag = "select * from Magicians where Name = @usernameM and Password = @passwordM";
            string sqlsec = "select * from Secretaries where Name = @usernameS and Password = @passwordS";
            string sqlman = "select * from Managers where Name = @usernameMN and Password = @passwordMN";

            try
            {
                conn.Open();

                //For Magicians parameters and table
                cmdM = new SqlCommand(sqlmag, conn);
                cmdM.Parameters.AddWithValue("@usernameM", TextBoxUserName.Text);
                cmdM.Parameters.AddWithValue("@passwordM", TextBoxPass.Text);

                DataSet dsM = new DataSet();
                SqlDataAdapter daM = new SqlDataAdapter(cmdM);
                daM.Fill(dsM);

                //For Secretaries parameters and table
                cmdS = new SqlCommand(sqlsec, conn);
                cmdS.Parameters.AddWithValue("@usernameS", TextBoxUserName.Text);
                cmdS.Parameters.AddWithValue("@passwordS", TextBoxPass.Text);

                DataSet dsS = new DataSet();
                SqlDataAdapter daS = new SqlDataAdapter(cmdS);
                daS.Fill(dsS);

                //For Managers parameters and table
                cmdMN = new SqlCommand(sqlman, conn);
                cmdMN.Parameters.AddWithValue("@usernameMN", TextBoxUserName.Text);
                cmdMN.Parameters.AddWithValue("@passwordMN", TextBoxPass.Text);

                DataSet dsMN = new DataSet();
                SqlDataAdapter daMN = new SqlDataAdapter(cmdMN);
                daMN.Fill(dsMN);

                conn.Close();

                //to check if username and password is on the table
                bool loginSucessfulM = ((dsM.Tables.Count > 0) && (dsM.Tables[0].Rows.Count > 0));
                bool loginSucessfulS = ((dsS.Tables.Count > 0) && (dsS.Tables[0].Rows.Count > 0));
                bool loginSucessfulMN = ((dsMN.Tables.Count > 0) && (dsMN.Tables[0].Rows.Count > 0));


                if (loginSucessfulM)
                {
                    userlvl = 1;
                    id = Convert.ToInt32(dsM.Tables[0].Rows[0]["MagicianID"]);
                }
                else if(loginSucessfulS)
                {
                    userlvl = 2;
                    id = Convert.ToInt32(dsS.Tables[0].Rows[0]["SecretaryID"]);
                }
                else if (loginSucessfulMN)
                {
                    userlvl = 3;
                    id = Convert.ToInt32(dsMN.Tables[0].Rows[0]["ManagerID"]);
                }
                else
                {
                    userlvl = 0;
                    LabelErrMessage.Text = "Username or Password not correct.";
                }
                Session["userID"] = id;
                Session["mylevel"] = userlvl;
                TextBoxUserName.Text = string.Empty;
                SetUpPage();     

            }
            catch (Exception ex)
            {
                LabelErrMessage.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            divLogin.Attributes.CssStyle.Add("display", "none");
        }
    }
}