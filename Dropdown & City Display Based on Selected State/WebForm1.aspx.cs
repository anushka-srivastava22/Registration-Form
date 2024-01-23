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
                // Populate states on page load
                PopulateStates();
                GetData();
            }
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Populate cities based on the selected state
            PopulateCities(ddlState.SelectedValue);
        }

        private void PopulateStates()
        {
            DataTable statesTable = GetStates();

            // Populate states dropdown
            ddlState.DataSource = statesTable;
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "stateid";
            ddlState.DataBind();

            // Add default item
            ddlState.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- Select State -", ""));
        }

        private void PopulateCities(string selectedState)
        {
            DataTable citiesTable = GetCitiesByState(selectedState);

            // Populate cities dropdown
            ddlCity.DataSource = citiesTable;
            ddlCity.DataTextField = "cityname";
            ddlCity.DataValueField = "cityid";
            ddlCity.DataBind();

            // Add default item
            ddlCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- Select City -", ""));
        }

        private DataTable GetStates()
        {
            DataTable statesTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT stateid, statename FROM state";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(statesTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, throw, etc.)
                Console.WriteLine("Error: " + ex.ToString());
            }

            return statesTable;
        }

        private DataTable GetCitiesByState(string stateId)
        {
            DataTable citiesTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT cityid, cityname FROM city WHERE stateid = @stateId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@stateId", stateId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(citiesTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, throw, etc.)
                Console.WriteLine("Error: " + ex.ToString());
            }

            return citiesTable;
        }
        protected void btnReg_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    _connection.Open();

                    string insertQuery = "INSERT INTO userreg (name, id, state, city, address, number) VALUES (@Name, @Id, @State, @City, @Address, @Number)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, _connection))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Id", txtId.Text);
                        
                        cmd.Parameters.AddWithValue("@Address", txtAdd.Text);
                        cmd.Parameters.AddWithValue("@Number", txtNum.Text);
                        cmd.Parameters.AddWithValue("@State", ddlState.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@City", ddlCity.SelectedItem.Text);

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
                    SqlCommand cmd = new SqlCommand("SELECT name, id, state, city, address, number FROM userreg", _connection);
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
            try
            {
                // Check if the GridView has rows
                if (e.NewEditIndex >= 0 && e.NewEditIndex < GridView1.Rows.Count)
                {
                    GridViewRow row = GridView1.Rows[e.NewEditIndex];

                    // Check if the cells collection is not null
                    if (row.Cells.Count >= 6)
                    {
                        SetDropDownSelectedValues(ddlState, row.Cells[2].Text);
                        PopulateCities(row.Cells[2].Text);  // Assuming state information is in the third column (adjust if necessary)

                        // Set the initial value of ddlCity and populate cities
                        SetDropDownSelectedValues(ddlCity, row.Cells[3].Text); 

                        // Now, set other TextBox values
                        txtName.Text = row.Cells[0].Text;
                        txtId.Text = row.Cells[1].Text;
                        txtAdd.Text = row.Cells[4].Text;
                        txtNum.Text = row.Cells[5].Text;

                        // Save the index of the edited row in ViewState
                        ViewState["EditingRow"] = e.NewEditIndex;
                    }
                }
                else
                {
                    Response.Write("Error: Index is out of range.");
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Response.Write("Error: " + ex.Message);
            }
        }
        /*private void SetDropDownSelectedValues(DropDownList ddl, string selectedValue)
        {
            // Find the ListItem by value and set it as selected
            ListItem item = ddl.Items.FindByValue(selectedValue);
            if (item != null)
            {
                // Clear existing selection only if it's not the same as the selected value
                if (ddl.SelectedValue != selectedValue)
                {
                    ddl.ClearSelection();
                }

                item.Selected = true;
            }
        }*/

        // CODE RUNNING AS EXPECTED
        /*private void SetDropDownSelectedValues(DropDownList ddl, string selectedValue)
        {
            // Find the ListItem by value
            ListItem item = ddl.Items.FindByValue(selectedValue);

            // If the item is not found, add a default item with the stored value
            if (item == null)
            {
                ddl.Items.Insert(0, new ListItem(selectedValue, selectedValue));
                item = ddl.Items.FindByValue(selectedValue);
            }

            // Set the found or added item as selected
            if (item != null)
            {
                // Clear existing selection only if it's not the same as the selected value
                if (ddl.SelectedValue != selectedValue)
                {
                    ddl.ClearSelection();
                }

                item.Selected = true;
            }
        }
         
         protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (GridView1.EditIndex >= 0)
                {
                    using (SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                    {
                        _connection.Open();

                        string updateQuery = "UPDATE userreg SET name=@Name, state=@State, city=@City, address=@Address, number=@Number WHERE id=@Id";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, _connection))
                        {
                            cmd.Parameters.AddWithValue("@Name", txtName.Text);
                            cmd.Parameters.AddWithValue("@Id", txtId.Text);
                            cmd.Parameters.AddWithValue("@Address", txtAdd.Text);
                            cmd.Parameters.AddWithValue("@Number", txtNum.Text);
                            cmd.Parameters.AddWithValue("@State", ddlState.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@City", ddlCity.SelectedItem.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    GridView1.EditIndex = -1;
                    GetData();
                    ClearFormFields();

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
*/
        private void SetDropDownSelectedValues(DropDownList ddl, string selectedValue)
        {
            // Find the ListItem by value
            ListItem item = ddl.Items.FindByValue(selectedValue);

            // If the item is not found, add a default item with the stored value
            if (item == null)
            {
                ddl.Items.Insert(0, new ListItem(selectedValue, selectedValue));
                item = ddl.Items.FindByValue(selectedValue);
            }

            // Set the found or added item as selected
            if (item != null)
            {
                // Clear existing selection only if it's not the same as the selected value
                if (ddl.SelectedValue != selectedValue)
                {
                    ddl.ClearSelection();
                }

                item.Selected = true;
            }
        }


        // Update

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

                        string updateQuery = "UPDATE userreg SET name=@Name, state=@State, city=@City, address=@Address, number=@Number WHERE id=@Id";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, _connection))
                        {
                            cmd.Parameters.AddWithValue("@Name", txtName.Text);
                            cmd.Parameters.AddWithValue("@Id", txtId.Text);
                            cmd.Parameters.AddWithValue("@Address", txtAdd.Text);
                            cmd.Parameters.AddWithValue("@Number", txtNum.Text);
                            cmd.Parameters.AddWithValue("@State", ddlState.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@City", ddlCity.SelectedItem.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    GridView1.EditIndex = -1;
                    GetData();
                    ClearFormFields();

                    // Set the updated values in the dropdowns
                    SetDropDownSelectedValues(ddlState, "- Select State -");
                    SetDropDownSelectedValues(ddlCity, ddlCity.SelectedItem.Text);

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
            ddlState.SelectedIndex = 0;
            txtAdd.Text = string.Empty;
            txtNum.Text = string.Empty;
//            ddlState.Items.Add(new ListItem("- Select State -", ""));
            ddlCity.Items.Clear();
            ddlCity.Items.Add(new ListItem("- Select City -", ""));
        }
    }
}
