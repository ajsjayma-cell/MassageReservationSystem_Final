using System.Data;
using MySql.Data.MySqlClient;

namespace MassageReservationDesktop;

public class ReservationsPage : TabPage
{
    private readonly MainForm main;

    private DataGridView grid = new();

    private ComboBox cmbStatus = new();

    private Label lblTotal = new();

    public ReservationsPage(MainForm mainForm)
    {
        main = mainForm;

        // =====================================================
        // PAGE
        // =====================================================

        Text = "Reservations";

        BackColor = Color.FromArgb(
            245,
            242,
            238
        );

        Color primary =
            Color.FromArgb(123, 75, 58);

        // =====================================================
        // TOP PANEL
        // =====================================================

        Panel topPanel = new Panel
        {
            Dock = DockStyle.Top,

            Height = 140,

            BackColor = Color.White
        };

        topPanel.Paint += (s, e) =>
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                topPanel.ClientRectangle,
                Color.FromArgb(225, 225, 225),
                ButtonBorderStyle.Solid
            );
        };

        // =====================================================
        // TITLE
        // =====================================================

        Label lblTitle = new Label
        {
            Text = "Reservation Management",

            Font = new Font(
                "Segoe UI",
                20,
                FontStyle.Bold
            ),

            ForeColor = Color.FromArgb(
                35,
                35,
                35
            ),

            AutoSize = true,

            Location = new Point(20, 18)
        };

        // =====================================================
        // SUBTITLE
        // =====================================================

        Label lblSubtitle = new Label
        {
            Text =
                "Manage customer bookings and reservation status.",

            Font = new Font(
                "Segoe UI",
               5
            ),

            ForeColor = Color.Gray,

            AutoSize = true,

            Location = new Point(24, 55)
        };

        // =====================================================
        // TOTAL LABEL
        // =====================================================

        lblTotal = new Label
        {
            Text = "Total Reservations: 0",

            Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Bold
            ),

            ForeColor = primary,

            AutoSize = true,

            Location = new Point(24, 92)
        };

        // =====================================================
        // STATUS LABEL
        // =====================================================

        Label lblStatus = new Label
        {
            Text = "Status",

            Font = new Font(
                "Segoe UI",
                10
            ),

            AutoSize = true,

            Location = new Point(360, 92)
        };

        // =====================================================
        // STATUS COMBOBOX
        // =====================================================

        cmbStatus = new ComboBox
        {
            Width = 180,

            Height = 38,

            Font = new Font(
                "Segoe UI",
                10
            ),

            Location = new Point(420, 86),

            DropDownStyle =
                ComboBoxStyle.DropDownList
        };

        cmbStatus.Items.AddRange(
            new string[]
            {
                "Pending",
                "Approved",
                "Completed",
                "Cancelled"
            }
        );

        cmbStatus.SelectedIndex = 0;

        // =====================================================
        // BUTTONS
        // =====================================================

        Button btnRefresh = CreateButton(
            "Refresh",
            Color.FromArgb(60, 120, 180),
            640
        );

        Button btnUpdate = CreateButton(
            "Update Status",
            primary,
            770
        );

        Button btnDelete = CreateButton(
            "Delete",
            Color.Firebrick,
            930
        );

        // =====================================================
        // EVENTS
        // =====================================================

        btnRefresh.Click += (_, _) =>
            LoadData();

        btnUpdate.Click += (_, _) =>
            UpdateStatus();

        btnDelete.Click += (_, _) =>
            DeleteSelected();

        // =====================================================
        // GRID
        // =====================================================

        grid = new DataGridView
        {
            Dock = DockStyle.Fill,

            BackgroundColor = Color.White,

            BorderStyle = BorderStyle.None,

            ReadOnly = true,

            AllowUserToAddRows = false,

            AllowUserToDeleteRows = false,

            AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill,

            SelectionMode =
                DataGridViewSelectionMode.FullRowSelect,

            MultiSelect = false,

            RowTemplate =
            {
                Height = 38
            },

            Font = new Font(
                "Segoe UI",
                10
            )
        };

        grid.ColumnHeadersDefaultCellStyle.Font =
            new Font(
                "Segoe UI",
                10,
                FontStyle.Bold
            );

        grid.ColumnHeadersHeight = 48;

        grid.EnableHeadersVisualStyles = false;

        grid.ColumnHeadersDefaultCellStyle.BackColor =
            primary;

        grid.ColumnHeadersDefaultCellStyle.ForeColor =
            Color.White;

        grid.DefaultCellStyle.SelectionBackColor =
            Color.FromArgb(225, 210, 205);

        grid.DefaultCellStyle.SelectionForeColor =
            Color.Black;

        grid.AlternatingRowsDefaultCellStyle.BackColor =
            Color.FromArgb(250, 249, 248);

        // =====================================================
        // ADD CONTROLS
        // =====================================================

        topPanel.Controls.Add(lblTitle);

        topPanel.Controls.Add(lblSubtitle);

        topPanel.Controls.Add(lblTotal);

        topPanel.Controls.Add(lblStatus);

        topPanel.Controls.Add(cmbStatus);

        topPanel.Controls.Add(btnRefresh);

        topPanel.Controls.Add(btnUpdate);

        topPanel.Controls.Add(btnDelete);

        Controls.Add(grid);

        Controls.Add(topPanel);

        // =====================================================
        // LOAD DATA
        // =====================================================

        LoadData();
    }

    // =====================================================
    // BUTTON DESIGN
    // =====================================================

    private Button CreateButton(
        string text,
        Color color,
        int x
    )
    {
        Button btn = new Button
        {
            Text = text,

            Width = 130,

            Height = 42,

            Location = new Point(x, 84),

            FlatStyle = FlatStyle.Flat,

            BackColor = color,

            ForeColor = Color.White,

            Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Bold
            ),

            Cursor = Cursors.Hand
        };

        btn.FlatAppearance.BorderSize = 0;

        btn.MouseEnter += (_, _) =>
        {
            btn.BackColor =
                ControlPaint.Dark(color);
        };

        btn.MouseLeave += (_, _) =>
        {
            btn.BackColor = color;
        };

        return btn;
    }

    // =====================================================
    // SELECTED ID
    // =====================================================

    private int SelectedReservationId()
    {
        if (grid.CurrentRow == null)
            return 0;

        return Convert.ToInt32(
            grid.CurrentRow.Cells["reservation_id"]
                .Value
        );
    }

    // =====================================================
    // LOAD DATA
    // =====================================================

    private void LoadData()
    {
        string where =
            main.Role == "admin"
            ? ""
            : "WHERE r.user_id=@UserId";

        string sql =
            $@"SELECT 
                r.reservation_id,
                u.full_name AS customer,
                s.service_name,
                t.full_name AS therapist,
                r.reservation_date,
                r.reservation_time,
                r.status,
                r.created_at
            FROM reservations r
            JOIN users u 
                ON r.user_id=u.user_id
            JOIN services s 
                ON r.service_id=s.service_id
            JOIN therapists t 
                ON r.therapist_id=t.therapist_id
            {where}
            ORDER BY 
                r.reservation_date DESC,
                r.reservation_time DESC";

        DataTable table =
            main.Role == "admin"
            ? Db.Query(sql)
            : Db.Query(
                sql,
                new MySqlParameter(
                    "@UserId",
                    main.UserId
                )
            );

        grid.DataSource = table;

        lblTotal.Text =
            $"Total Reservations: {table.Rows.Count}";
    }

    // =====================================================
    // UPDATE STATUS
    // =====================================================

    private void UpdateStatus()
    {
        if (main.Role != "admin")
        {
            MessageBox.Show(
                "Only admin can update reservation status.",
                "Access Denied",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            return;
        }

        int id =
            SelectedReservationId();

        if (id == 0)
            return;

        Db.Execute(
            "UPDATE reservations SET status=@Status WHERE reservation_id=@Id",

            new MySqlParameter(
                "@Status",
                cmbStatus.Text
            ),

            new MySqlParameter(
                "@Id",
                id
            )
        );

        LoadData();

        MessageBox.Show(
            "Reservation status updated successfully.",
            "Success",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );
    }

    // =====================================================
    // DELETE
    // =====================================================

    private void DeleteSelected()
    {
        int id =
            SelectedReservationId();

        if (id == 0)
            return;

        if (
            MessageBox.Show(
                "Delete selected reservation?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            ) != DialogResult.Yes
        )
            return;

        string sql =
            main.Role == "admin"
            ? "DELETE FROM reservations WHERE reservation_id=@Id"
            : "DELETE FROM reservations WHERE reservation_id=@Id AND user_id=@UserId";

        if (main.Role == "admin")
        {
            Db.Execute(
                sql,
                new MySqlParameter(
                    "@Id",
                    id
                )
            );
        }
        else
        {
            Db.Execute(
                sql,
                new MySqlParameter(
                    "@Id",
                    id
                ),
                new MySqlParameter(
                    "@UserId",
                    main.UserId
                )
            );
        }

        LoadData();

        MessageBox.Show(
            "Reservation deleted successfully.",
            "Deleted",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );
    }
}