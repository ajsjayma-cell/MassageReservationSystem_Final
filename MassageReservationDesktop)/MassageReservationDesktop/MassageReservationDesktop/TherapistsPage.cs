using MySql.Data.MySqlClient;

namespace MassageReservationDesktop;

public class TherapistsPage : TabPage
{
    private DataGridView grid = new();
    private TextBox name = new(), specialization = new();
    private ComboBox status = new();

    public TherapistsPage()
    {
        Text = "Therapists";
        grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false };
        Theme.StyleGrid(grid);
        var panel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 78, Padding = new Padding(14), BackColor = Theme.Surface };
        name = new TextBox { Width = 180, PlaceholderText = "Full name" };
        specialization = new TextBox { Width = 180, PlaceholderText = "Specialization" };
        status = new ComboBox { Width = 130, DropDownStyle = ComboBoxStyle.DropDownList };
        status.Items.AddRange(new[] { "Available", "Unavailable" }); status.SelectedIndex = 0;
        Theme.StyleInput(name); Theme.StyleInput(specialization); Theme.StyleInput(status);
        var add = Theme.SecondaryButton("Add", 90);
        var update = Theme.SecondaryButton("Update", 90);
        var delete = Theme.SecondaryButton("Delete", 90);
        var refresh = Theme.SecondaryButton("Refresh", 90);
        add.Click += (_, _) => Add(); update.Click += (_, _) => UpdateRecord(); delete.Click += (_, _) => Delete(); refresh.Click += (_, _) => LoadData();
        grid.SelectionChanged += (_, _) => FillFields();
        panel.Controls.AddRange(new Control[] { name, specialization, status, add, update, delete, refresh });
        Controls.Add(grid); Controls.Add(panel); LoadData();
    }
    private int Id => grid.CurrentRow == null ? 0 : Convert.ToInt32(grid.CurrentRow.Cells["therapist_id"].Value);
    private void LoadData() => grid.DataSource = Db.Query("SELECT therapist_id, full_name, specialization, availability_status FROM therapists ORDER BY therapist_id DESC");
    private void FillFields(){ if(grid.CurrentRow==null)return; name.Text=$"{grid.CurrentRow.Cells["full_name"].Value}"; specialization.Text=$"{grid.CurrentRow.Cells["specialization"].Value}"; status.Text=$"{grid.CurrentRow.Cells["availability_status"].Value}"; }
    private bool Valid() => name.Text.Trim() != "";
    private void Add(){ if(!Valid()){MessageBox.Show("Enter therapist name.");return;} Db.Execute("INSERT INTO therapists(full_name,specialization,availability_status) VALUES(@n,@s,@a)", new MySqlParameter("@n",name.Text),new MySqlParameter("@s",specialization.Text),new MySqlParameter("@a",status.Text)); LoadData(); }
    private void UpdateRecord(){ if(Id==0||!Valid())return; Db.Execute("UPDATE therapists SET full_name=@n,specialization=@s,availability_status=@a WHERE therapist_id=@id", new MySqlParameter("@n",name.Text),new MySqlParameter("@s",specialization.Text),new MySqlParameter("@a",status.Text),new MySqlParameter("@id",Id)); LoadData(); }
    private void Delete(){ if(Id==0)return; if(MessageBox.Show("Delete selected therapist?","Confirm",MessageBoxButtons.YesNo)!=DialogResult.Yes)return; Db.Execute("DELETE FROM therapists WHERE therapist_id=@id", new MySqlParameter("@id",Id)); LoadData(); }
}
