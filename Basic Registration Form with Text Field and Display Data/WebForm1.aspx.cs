using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Registration
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetData();
            }
        }

        protected void btnReg_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    _connection.Open();

                    string insertQuery = "INSERT INTO userreg (name, id, age, address, number) VALUES (@Name, @Id, @Age, @Address, @Number)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, _connection))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Id", txtId.Text);
                        cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAdd.Text);
                        cmd.Parameters.AddWithValue("@Number", txtNum.Text);

                        cmd.ExecuteNonQuery();
                    }
                }

                GetData();
                ClearFormFields();

                Response.Write("<script>alert('Student registered successfully!')</script>");
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }

        private void GetData()
        {
            try
            {
                using (SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    _connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT name, id, age, address, number FROM userreg", _connection);
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    GridView1.DataSource = dr;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.NewEditIndex];

            if (row != null)
            {
                // Check if the cells collection is not null
                if (row.Cells.Count >= 5)
                {
                    txtName.Text = row.Cells[0].Text;
                    txtId.Text = row.Cells[1].Text;
                    txtAge.Text = row.Cells[2].Text;
                    txtAdd.Text = row.Cells[3].Text;
                    txtNum.Text = row.Cells[4].Text;

                    // Other logic as needed
                }
                
            }
        }


        // Update
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (GridView1.EditIndex >= 0)
                {
                    using (SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                    {
                        _connection.Open();

                        string updateQuery = "UPDATE userreg SET name=@name, age=@age, address=@address, number=@number WHERE id=@id";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, _connection))
                        {
                            cmd.Parameters.AddWithValue("@Name", txtName.Text);
                            cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                            cmd.Parameters.AddWithValue("@Address", txtAdd.Text);
                            cmd.Parameters.AddWithValue("@Number", txtNum.Text);
                            cmd.Parameters.AddWithValue("@Id", txtId.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    GridView1.EditIndex = -1;
                    GetData();
                    ClearFormFields();
                    txtId.Enabled = true;

                    Response.Write("<script>alert('Student information updated successfully!')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Please select a row to update.')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }

        private void ClearFormFields()
        {
            txtName.Text = string.Empty;
            txtId.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtAdd.Text = string.Empty;
            txtNum.Text = string.Empty;
        }
    }
}
