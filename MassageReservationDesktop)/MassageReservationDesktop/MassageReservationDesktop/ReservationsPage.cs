using System.Data;
using MySql.Data.MySqlClient;

namespace MassageReservationDesktop;

public class ReservationsPage : TabPage
{
    private readonly MainForm main;
    private DataGridView grid = new();
    private ComboBox cmbStatus = new();

    public ReservationsPage(MainForm mainForm)
    {
        main = mainForm;
        Text = "Reservations";

        grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false };
        Theme.StyleGrid(grid);
        var panel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 62, Padding = new Padding(14), BackColor = Theme.Surface };
        var refresh = Theme.SecondaryButton("Refresh", 105);
        cmbStatus = new ComboBox { Width = 140, DropDownStyle = ComboBoxStyle.DropDownList };
        cmbStatus.Items.AddRange(new[] { "Pending", "Approved", "Completed", "Cancelled" });
        cmbStatus.SelectedIndex = 0;
        var update = Theme.PrimaryButton("Update Status", 135);
        var delete = Theme.SecondaryButton("Delete", 105);

        refresh.Click += (_, _) => LoadData();
        update.Click += (_, _) => UpdateStatus();
        delete.Click += (_, _) => DeleteSelected();

        panel.Controls.AddRange(new Control[] { refresh, new Label { Text = "Status:", AutoSize = true, Padding = new Padding(10, 8, 0, 0) }, cmbStatus, update, delete });
        Controls.Add(grid);
        Controls.Add(panel);
        LoadData();
    }

    private int SelectedReservationId()
    {
        if (grid.CurrentRow == null) return 0;
        return Convert.ToInt32(grid.CurrentRow.Cells["reservation_id"].Value);
    }

    private void LoadData()
    {
        string where = main.Role == "admin" ? "" : "WHERE r.user_id=@UserId";
        var sql = $@"SELECT r.reservation_id, u.full_name AS customer, s.service_name, t.full_name AS therapist,
                    r.reservation_date, r.reservation_time, r.status, r.created_at
                    FROM reservations r
                    JOIN users u ON r.user_id=u.user_id
                    JOIN services s ON r.service_id=s.service_id
                    JOIN therapists t ON r.therapist_id=t.therapist_id
                    {where}
                    ORDER BY r.reservation_date DESC, r.reservation_time DESC";
        grid.DataSource = main.Role == "admin" ? Db.Query(sql) : Db.Query(sql, new MySqlParameter("@UserId", main.UserId));
    }

    private void UpdateStatus()
    {
        if (main.Role != "admin") { MessageBox.Show("Only admin can update reservation status."); return; }
        var id = SelectedReservationId();
        if (id == 0) return;
        Db.Execute("UPDATE reservations SET status=@Status WHERE reservation_id=@Id", new MySqlParameter("@Status", cmbStatus.Text), new MySqlParameter("@Id", id));
        LoadData();
    }

    private void DeleteSelected()
    {
        var id = SelectedReservationId();
        if (id == 0) return;
        if (MessageBox.Show("Delete selected reservation?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        var sql = main.Role == "admin" ? "DELETE FROM reservations WHERE reservation_id=@Id" : "DELETE FROM reservations WHERE reservation_id=@Id AND user_id=@UserId";
        if (main.Role == "admin") Db.Execute(sql, new MySqlParameter("@Id", id));
        else Db.Execute(sql, new MySqlParameter("@Id", id), new MySqlParameter("@UserId", main.UserId));
        LoadData();
    }
}
