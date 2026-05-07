using MySql.Data.MySqlClient;

namespace MassageReservationDesktop;

public class TherapistsPage : TabPage
{
    private DataGridView grid = new();

    private TextBox txtName = new();

    private TextBox txtSpecialization = new();

    private ComboBox cmbStatus = new();

    public TherapistsPage()
    {
        // =====================================================
        // PAGE
        // =====================================================

        Text = "Therapists";

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

            Height = 165,

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
            Text = "Therapist Management",

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
        // FULL NAME
        // =====================================================

        Label lblName = new Label
        {
            Text = "Full Name",

            Font = new Font(
                "Segoe UI",
                10
            ),

            AutoSize = true,

            Location = new Point(25, 65)
        };

        txtName = new TextBox
        {
            Width = 250,

            Height = 40,

            Font = new Font(
                "Segoe UI",
                11
            ),

            Location = new Point(25, 90),

            BackColor = lightBg
        };

        // =====================================================
        // SPECIALIZATION
        // =====================================================

        Label lblSpecialization = new Label
        {
            Text = "Specialization",

            Font = new Font(
                "Segoe UI",
                10
            ),

            AutoSize = true,

            Location = new Point(310, 65)
        };

        txtSpecialization = new TextBox
        {
            Width = 260,

            Height = 40,

            Font = new Font(
                "Segoe UI",
                11
            ),

            Location = new Point(310, 90),

            BackColor = lightBg
        };

        // =====================================================
        // STATUS
        // =====================================================

        Label lblStatus = new Label
        {
            Text = "Availability",

            Font = new Font(
                "Segoe UI",
                10
            ),

            AutoSize = true,

            Location = new Point(605, 65)
        };

        cmbStatus = new ComboBox
        {
            Width = 170,

            Height = 40,

            Font = new Font(
                "Segoe UI",
                10
            ),

            Location = new Point(605, 90),

            DropDownStyle =
                ComboBoxStyle.DropDownList
        };

        cmbStatus.Items.AddRange(
            new string[]
            {
                "Available",
                "Unavailable"
            }
        );

        cmbStatus.SelectedIndex = 0;

        // =====================================================
        // BUTTONS
        // =====================================================

        Button btnAdd = CreateButton(
            "Add",
            primary,
            810
        );

        Button btnUpdate = CreateButton(
            "Update",
            primary,
            930
        );

        Button btnDelete = CreateButton(
            "Delete",
            Color.Firebrick,
            810,
            85
        );

        Button btnRefresh = CreateButton(
            "Refresh",
            Color.FromArgb(60, 120, 180),
            930,
            85
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

        topPanel.Controls.Add(lblSpecialization);

        topPanel.Controls.Add(txtSpecialization);

        topPanel.Controls.Add(lblStatus);

        topPanel.Controls.Add(cmbStatus);

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

            Width = 100,

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
            grid.CurrentRow.Cells["therapist_id"]
                .Value
        );

    // =====================================================
    // LOAD DATA
    // =====================================================

    private void LoadData()
    {
        grid.DataSource = Db.Query(
            "SELECT therapist_id, full_name, specialization, availability_status FROM therapists ORDER BY therapist_id DESC"
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
            grid.CurrentRow.Cells["full_name"]
                .Value
                .ToString();

        txtSpecialization.Text =
            grid.CurrentRow.Cells["specialization"]
                .Value
                .ToString();

        cmbStatus.Text =
            grid.CurrentRow.Cells["availability_status"]
                .Value
                .ToString();
    }

    // =====================================================
    // VALIDATION
    // =====================================================

    private bool Valid()
    {
        return txtName.Text.Trim() != "";
    }

    // =====================================================
    // ADD
    // =====================================================

    private void Add()
    {
        if (!Valid())
        {
            MessageBox.Show(
                "Enter therapist name.",
                "Validation",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            return;
        }

        Db.Execute(
            "INSERT INTO therapists(full_name,specialization,availability_status) VALUES(@n,@s,@a)",

            new MySqlParameter(
                "@n",
                txtName.Text
            ),

            new MySqlParameter(
                "@s",
                txtSpecialization.Text
            ),

            new MySqlParameter(
                "@a",
                cmbStatus.Text
            )
        );

        LoadData();

        MessageBox.Show(
            "Therapist added successfully."
        );
    }

    // =====================================================
    // UPDATE
    // =====================================================

    private void UpdateRecord()
    {
        if (Id == 0 || !Valid())
            return;

        Db.Execute(
            "UPDATE therapists SET full_name=@n,specialization=@s,availability_status=@a WHERE therapist_id=@id",

            new MySqlParameter(
                "@n",
                txtName.Text
            ),

            new MySqlParameter(
                "@s",
                txtSpecialization.Text
            ),

            new MySqlParameter(
                "@a",
                cmbStatus.Text
            ),

            new MySqlParameter(
                "@id",
                Id
            )
        );

        LoadData();

        MessageBox.Show(
            "Therapist updated successfully."
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
                "Delete selected therapist?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            ) != DialogResult.Yes
        )
            return;

        Db.Execute(
            "DELETE FROM therapists WHERE therapist_id=@id",

            new MySqlParameter(
                "@id",
                Id
            )
        );

        LoadData();

        MessageBox.Show(
            "Therapist deleted successfully."
        );
    }
}