using MySql.Data.MySqlClient;

namespace MassageReservationDesktop;

public class UsersPage : TabPage
{
    private DataGridView grid = new();
    private TextBox fullName = new(), email = new(), password = new();
    private ComboBox role = new();

    public UsersPage()
    {
        Text = "Users";
        grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false };
        Theme.StyleGrid(grid);
        var panel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 78, Padding = new Padding(14), BackColor = Theme.Surface };
        fullName = new TextBox { Width = 160, PlaceholderText = "Full name" };
        email = new TextBox { Width = 180, PlaceholderText = "Email" };
        password = new TextBox { Width = 130, PlaceholderText = "New password" };
        role = new ComboBox { Width = 110, DropDownStyle = ComboBoxStyle.DropDownList };
        role.Items.AddRange(new[] { "customer", "admin" }); role.SelectedIndex = 0;
        Theme.StyleInput(fullName); Theme.StyleInput(email); Theme.StyleInput(password); Theme.StyleInput(role);
        var add = Theme.SecondaryButton("Add", 90);
        var update = Theme.SecondaryButton("Update", 90);
        var delete = Theme.SecondaryButton("Delete", 90);
        var refresh = Theme.SecondaryButton("Refresh", 90);
        add.Click += (_, _) => Add(); update.Click += (_, _) => UpdateRecord(); delete.Click += (_, _) => Delete(); refresh.Click += (_, _) => LoadData();
        grid.SelectionChanged += (_, _) => FillFields();
        panel.Controls.AddRange(new Control[] { fullName, email, password, role, add, update, delete, refresh });
        Controls.Add(grid); Controls.Add(panel); LoadData();
    }
    private int Id => grid.CurrentRow == null ? 0 : Convert.ToInt32(grid.CurrentRow.Cells["user_id"].Value);
    private void LoadData() => grid.DataSource = Db.Query("SELECT user_id, full_name, email, role, created_at FROM users ORDER BY user_id DESC");
    private void FillFields(){ if(grid.CurrentRow==null)return; fullName.Text=$"{grid.CurrentRow.Cells["full_name"].Value}"; email.Text=$"{grid.CurrentRow.Cells["email"].Value}"; role.Text=$"{grid.CurrentRow.Cells["role"].Value}"; password.Text=""; }
    private bool Valid(bool needPassword) => fullName.Text.Trim() != "" && email.Text.Trim() != "" && (!needPassword || password.Text.Trim() != "");
    private void Add(){ if(!Valid(true)){MessageBox.Show("Complete user details.");return;} Db.Execute("INSERT INTO users(full_name,email,password,role) VALUES(@n,@e,@p,@r)", new MySqlParameter("@n",fullName.Text),new MySqlParameter("@e",email.Text),new MySqlParameter("@p",PasswordHelper.Hash(password.Text)),new MySqlParameter("@r",role.Text)); LoadData(); }
    private void UpdateRecord(){ if(Id==0||!Valid(false))return; if(password.Text.Trim()=="") Db.Execute("UPDATE users SET full_name=@n,email=@e,role=@r WHERE user_id=@id", new MySqlParameter("@n",fullName.Text),new MySqlParameter("@e",email.Text),new MySqlParameter("@r",role.Text),new MySqlParameter("@id",Id)); else Db.Execute("UPDATE users SET full_name=@n,email=@e,password=@p,role=@r WHERE user_id=@id", new MySqlParameter("@n",fullName.Text),new MySqlParameter("@e",email.Text),new MySqlParameter("@p",PasswordHelper.Hash(password.Text)),new MySqlParameter("@r",role.Text),new MySqlParameter("@id",Id)); LoadData(); }
    private void Delete(){ if(Id==0)return; if(MessageBox.Show("Delete selected user?","Confirm",MessageBoxButtons.YesNo)!=DialogResult.Yes)return; Db.Execute("DELETE FROM users WHERE user_id=@id", new MySqlParameter("@id",Id)); LoadData(); }
}
