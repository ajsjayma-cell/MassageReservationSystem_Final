using MySql.Data.MySqlClient;

namespace MassageReservationDesktop;

public class ServicesPage : TabPage
{
    private DataGridView grid = new();
    private TextBox name = new(), desc = new(), price = new(), duration = new();

    public ServicesPage()
    {
        Text = "Services";
        grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false };
        Theme.StyleGrid(grid);
        var panel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 78, Padding = new Padding(14), BackColor = Theme.Surface };
        name = new TextBox { Width = 150, PlaceholderText = "Service name" };
        desc = new TextBox { Width = 220, PlaceholderText = "Description" };
        price = new TextBox { Width = 90, PlaceholderText = "Price" };
        duration = new TextBox { Width = 90, PlaceholderText = "Minutes" };
        Theme.StyleInput(name); Theme.StyleInput(desc); Theme.StyleInput(price); Theme.StyleInput(duration);
        var add = Theme.SecondaryButton("Add", 90);
        var update = Theme.SecondaryButton("Update", 90);
        var delete = Theme.SecondaryButton("Delete", 90);
        var refresh = Theme.SecondaryButton("Refresh", 90);
        add.Click += (_, _) => Add(); update.Click += (_, _) => UpdateRecord(); delete.Click += (_, _) => Delete(); refresh.Click += (_, _) => LoadData();
        grid.SelectionChanged += (_, _) => FillFields();
        panel.Controls.AddRange(new Control[] { name, desc, price, duration, add, update, delete, refresh });
        Controls.Add(grid); Controls.Add(panel); LoadData();
    }
    private int Id => grid.CurrentRow == null ? 0 : Convert.ToInt32(grid.CurrentRow.Cells["service_id"].Value);
    private void LoadData() => grid.DataSource = Db.Query("SELECT service_id, service_name, description, price, duration FROM services ORDER BY service_id DESC");
    private void FillFields(){ if(grid.CurrentRow==null)return; name.Text=$"{grid.CurrentRow.Cells["service_name"].Value}"; desc.Text=$"{grid.CurrentRow.Cells["description"].Value}"; price.Text=$"{grid.CurrentRow.Cells["price"].Value}"; duration.Text=$"{grid.CurrentRow.Cells["duration"].Value}"; }
    private bool Valid(out decimal p, out int d){ p = 0; d = 0; return name.Text.Trim() != "" && decimal.TryParse(price.Text,out p) && int.TryParse(duration.Text,out d); }
    private void Add(){ if(!Valid(out var p,out var d)){MessageBox.Show("Complete valid service details.");return;} Db.Execute("INSERT INTO services(service_name,description,price,duration) VALUES(@n,@ds,@p,@d)", new MySqlParameter("@n",name.Text),new MySqlParameter("@ds",desc.Text),new MySqlParameter("@p",p),new MySqlParameter("@d",d)); LoadData(); }
    private void UpdateRecord(){ if(Id==0||!Valid(out var p,out var d))return; Db.Execute("UPDATE services SET service_name=@n,description=@ds,price=@p,duration=@d WHERE service_id=@id", new MySqlParameter("@n",name.Text),new MySqlParameter("@ds",desc.Text),new MySqlParameter("@p",p),new MySqlParameter("@d",d),new MySqlParameter("@id",Id)); LoadData(); }
    private void Delete(){ if(Id==0)return; if(MessageBox.Show("Delete selected service?","Confirm",MessageBoxButtons.YesNo)!=DialogResult.Yes)return; Db.Execute("DELETE FROM services WHERE service_id=@id", new MySqlParameter("@id",Id)); LoadData(); }
}
