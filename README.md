# ASP.NET Registration Form with Dropdowns

This project demonstrates how to create a registration form using ASP.NET with SQL Server as the backend. The form includes features like text fields for basic information, as well as dropdowns for selecting states and cities from a database.

## Features

### 1. Basic Registration Form:

#### Step 1: Setting Up the Project
1. Open Microsoft Visual Studio.
2. Create a new ASP.NET Empty Web Application project.
3. Add a Web Form named `WebForm1.aspx`.

#### Step 2: Configure Database Connection
1. Open SQL Server Management Studio.
2. Execute SQL scripts in the "SQL Scripts" folder to create the necessary database and table.

#### Step 3: Design the Registration Form
- Open `WebForm1.aspx` and design the form to capture basic information like name, age, address, and contact number.

### 2. Displaying City and State Names:

#### Step 4: Dropdown & City Display Based on Selected State
- Implement dropdowns for selecting states and cities dynamically.
- Use SQL queries to fetch state and city names along with corresponding IDs from the database.


// Example SQL Query
SELECT StateID, StateName FROM States;
SELECT CityID, CityName FROM Cities WHERE StateID = @SelectedStateID;


## 3. Updated Registration Form:

### Step 5: Enhanced Registration Form

1. **Modify `WebForm1.aspx`:**
   - Open the `WebForm1.aspx` file in your project.

2. **Include Dropdowns for Selecting States and Cities:**
   - Integrate dropdown controls for selecting states and cities in the registration form.
   - Use the following ASP.NET markup as an example:

     
     <!-- State Dropdown -->
     <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
         <!-- Populate dropdown dynamically in code-behind -->
     </asp:DropDownList>
    
     <!-- City Dropdown -->
     <asp:DropDownList ID="ddlCity" runat="server">
         <!-- Populate dropdown dynamically based on selected state -->
     </asp:DropDownList>
     ```

3. **Populate Dropdowns Dynamically in Code-Behind:**
   - Open the associated code-behind file (e.g., `WebForm1.aspx.cs`).
   - Use C# code to populate the state dropdown during page load:

   
     protected void Page_Load(object sender, EventArgs e)
     {
         if (!IsPostBack)
         {
             // Populate state dropdown
             // Example: ddlState.DataSource = YourStateDataSource; ddlState.DataBind();
         }
     }
     ```

   - Handle the state dropdown's `SelectedIndexChanged` event to populate the city dropdown:

     protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
     {
         // Populate city dropdown based on the selected state
         // Example: ddlCity.DataSource = YourCityDataSource; ddlCity.DataBind();
     }
     

4. **Handle Dropdown Events:**
   - Implement code to handle events when the state dropdown selection changes.
   - Open the code-behind file and use C# code to handle the event:

    
     protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
     {
         // Implement logic to handle state dropdown selection change
     }
     

5. **Testing:**
   - Run the application in Visual Studio.
   - Verify that the registration form includes dynamic dropdowns for selecting states and cities.

**Note:** Make sure to replace placeholder comments with actual code and data sources from your project.

## Usage

### Registration:

1. **Fill in the Basic Details:**
   - Open the registration form (`WebForm1.aspx`) in your web application.
   - Fill in the basic details such as name, age, address, and contact number.

2. **Select a State from the Dropdown:**
   - Utilize the dropdown control to select the appropriate state from the available options.

3. **Corresponding Cities Dropdown:**
   - Based on the selected state, the city dropdown will dynamically populate with the corresponding cities.

### Updating Information:

- You can easily edit or update information using the registration form.

## Contributing

Contributions are welcome! If you have suggestions, bug reports, or enhancements, please follow these steps:

1. Check if the issue or enhancement is already reported.
2. If not, open a new issue describing the problem or enhancement.
3. Fork the repository and create a branch for your contribution.
4. Make your changes and submit a pull request.

Let's collaborate to make this project even better!

## License

This project is licensed under the [MIT License](LICENSE).
