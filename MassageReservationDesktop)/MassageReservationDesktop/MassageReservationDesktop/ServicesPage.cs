using MySql.Data.MySqlClient;

namespace MassageReservationDesktop;

public class ServicesPage : TabPage
{
    private DataGridView grid = new();

    private TextBox txtName = new();

    private TextBox txtDescription = new();

    private TextBox txtPrice = new();

    private TextBox txtDuration = new();

    public ServicesPage()
    {
        // =====================================================
        // PAGE
        // =====================================================

        Text = "Services";

        BackColor = Color.FromArgb(245, 242, 238);

        Color primary =
            Color.FromArgb(123, 75, 58);

        Color lightBg =
            Color.FromArgb(246, 247, 250);

        // =====================================================
        // TOP PANEL
        // =====================================================

        Panel topPanel = new Panel
        {
            Dock = DockStyle.Top,

            Height = 185,

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
            Text = "Massage Services",

            Font = new Font(
                "Segoe UI",
                18,
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
        // SERVICE NAME
        // =====================================================

        Label lblName = new Label
        {
            Text = "Service Name",

            Font = new Font(
                "Segoe UI",
                10
            ),

            AutoSize = true,

            Location = new Point(25, 65)
        };

        txtName = new TextBox
        {
            Width = 220,

            Height = 40,

            Font = new Font(
                "Segoe UI",
                11
            ),

            Location = new Point(25, 90),

            BackColor = lightBg
        };

        // =====================================================
        // DESCRIPTION
        // =====================================================

        Label lblDescription = new Label
        {
            Text = "Description",

            Font = new Font(
                "Segoe UI",
                10
            ),

            AutoSize = true,

            Location = new Point(270, 65)
        };

        txtDescription = new TextBox
        {
            Width = 280,

            Height = 40,

            Font = new Font(
                "Segoe UI",
                11
            ),

            Location = new Point(270, 90),

            BackColor = lightBg
        };

        // =====================================================
        // PRICE
        // =====================================================

        Label lblPrice = new Label
        {
            Text = "Price",

            Font = new Font(
                "Segoe UI",
                10
            ),

            AutoSize = true,

            Location = new Point(580, 65)
        };

        txtPrice = new TextBox
        {
            Width = 120,

            Height = 40,

            Font = new Font(
                "Segoe UI",
                11
            ),

            Location = new Point(580, 90),

            BackColor = lightBg
        };

        // =====================================================
        // DURATION
        // =====================================================

        Label lblDuration = new Label
        {
            Text = "Duration (Minutes)",

            Font = new Font(
                "Segoe UI",
                10
            ),

            AutoSize = true,

            Location = new Point(730, 65)
        };

        txtDuration = new TextBox
        {
            Width = 150,

            Height = 40,

            Font = new Font(
                "Segoe UI",
                11
            ),

            Location = new Point(730, 90),

            BackColor = lightBg
        };

        // =====================================================
        // BUTTONS
        // =====================================================

        Button btnAdd = CreateButton(
            "Add",
            primary,
            920
        );

        Button btnUpdate = CreateButton(
            "Update",
            primary,
            920,
            82
        );

        Button btnDelete = CreateButton(
            "Delete",
            Color.Firebrick,
            1035
        );

        Button btnRefresh = CreateButton(
            "Refresh",
            Color.FromArgb(60, 120, 180),
            1035,
            82
        );

        // =====================================================
        // EVENTS
        // =====================================================

        btnAdd.Click += (_, _) => Add();

        btnUpdate.Click += (_, _) =>
            UpdateRecord();

        btnDelete.Click += (_, _) =>
            Delete();

        btnRefresh.Click += (_, _) =>
            LoadData();

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
                Height = 35
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

        grid.ColumnHeadersHeight = 45;

        grid.EnableHeadersVisualStyles = false;

        grid.ColumnHeadersDefaultCellStyle.BackColor =
            primary;

        grid.ColumnHeadersDefaultCellStyle.ForeColor =
            Color.White;

        grid.DefaultCellStyle.SelectionBackColor =
            Color.FromArgb(225, 210, 205);

        grid.DefaultCellStyle.SelectionForeColor =
            Color.Black;

        grid.SelectionChanged += (_, _) =>
            FillFields();

        // =====================================================
        // ADD CONTROLS
        // =====================================================

        topPanel.Controls.Add(lblTitle);

        topPanel.Controls.Add(lblName);

        topPanel.Controls.Add(txtName);

        topPanel.Controls.Add(lblDescription);

        topPanel.Controls.Add(txtDescription);

        topPanel.Controls.Add(lblPrice);

        topPanel.Controls.Add(txtPrice);

        topPanel.Controls.Add(lblDuration);

        topPanel.Controls.Add(txtDuration);

        topPanel.Controls.Add(btnAdd);

        topPanel.Controls.Add(btnUpdate);

        topPanel.Controls.Add(btnDelete);

        topPanel.Controls.Add(btnRefresh);

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
        int x,
        int y = 35
    )
    {
        Button btn = new Button
        {
            Text = text,

            Width = 95,

            Height = 40,

            Location = new Point(x, y),

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

        return btn;
    }

    // =====================================================
    // CURRENT ID
    // =====================================================

    private int Id =>
        grid.CurrentRow == null
        ? 0
        : Convert.ToInt32(
            grid.CurrentRow.Cells["service_id"]
                .Value
        );

    // =====================================================
    // LOAD DATA
    // =====================================================

    private void LoadData()
    {
        grid.DataSource = Db.Query(
            "SELECT service_id, service_name, description, price, duration FROM services ORDER BY service_id DESC"
        );
    }

    // =====================================================
    // FILL INPUTS
    // =====================================================

    private void FillFields()
    {
        if (grid.CurrentRow == null)
            return;

        txtName.Text =
            grid.CurrentRow.Cells["service_name"]
                .Value
                .ToString();

        txtDescription.Text =
            grid.CurrentRow.Cells["description"]
                .Value
                .ToString();

        txtPrice.Text =
            grid.CurrentRow.Cells["price"]
                .Value
                .ToString();

        txtDuration.Text =
            grid.CurrentRow.Cells["duration"]
                .Value
                .ToString();
    }

    // =====================================================
    // VALIDATION
    // =====================================================

    private bool Valid(
        out decimal p,
        out int d
    )
    {
        p = 0;

        d = 0;

        return
            txtName.Text.Trim() != "" &&
            decimal.TryParse(
                txtPrice.Text,
                out p
            ) &&
            int.TryParse(
                txtDuration.Text,
                out d
            );
    }

    // =====================================================
    // ADD
    // =====================================================

    private void Add()
    {
        if (!Valid(out var p, out var d))
        {
            MessageBox.Show(
                "Complete valid service details.",
                "Validation",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            return;
        }

        Db.Execute(
            "INSERT INTO services(service_name,description,price,duration) VALUES(@n,@ds,@p,@d)",

            new MySqlParameter(
                "@n",
                txtName.Text
            ),

            new MySqlParameter(
                "@ds",
                txtDescription.Text
            ),

            new MySqlParameter(
                "@p",
                p
            ),

            new MySqlParameter(
                "@d",
                d
            )
        );

        LoadData();

        MessageBox.Show(
            "Service added successfully."
        );
    }

    // =====================================================
    // UPDATE
    // =====================================================

    private void UpdateRecord()
    {
        if (
            Id == 0 ||
            !Valid(out var p, out var d)
        )
            return;

        Db.Execute(
            "UPDATE services SET service_name=@n,description=@ds,price=@p,duration=@d WHERE service_id=@id",

            new MySqlParameter(
                "@n",
                txtName.Text
            ),

            new MySqlParameter(
                "@ds",
                txtDescription.Text
            ),

            new MySqlParameter(
                "@p",
                p
            ),

            new MySqlParameter(
                "@d",
                d
            ),

            new MySqlParameter(
                "@id",
                Id
            )
        );

        LoadData();

        MessageBox.Show(
            "Service updated successfully."
        );
    }

    // =====================================================
    // DELETE
    // =====================================================

    private void Delete()
    {
        if (Id == 0)
            return;

        if (
            MessageBox.Show(
                "Delete selected service?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            ) != DialogResult.Yes
        )
            return;

        Db.Execute(
            "DELETE FROM services WHERE service_id=@id",

            new MySqlParameter(
                "@id",
                Id
            )
        );

        LoadData();

        MessageBox.Show(
            "Service deleted successfully."
        );
    }
}