using MySql.Data.MySqlClient;

namespace MassageReservationDesktop;

public class UsersPage : TabPage
{
    private DataGridView grid = new();

    private TextBox txtFullName = new();

    private TextBox txtEmail = new();

    private TextBox txtPassword = new();

    private ComboBox cmbRole = new();

    private Label lblTitle = new();

    public UsersPage()
    {
        // =====================================================
        // PAGE
        // =====================================================

        Text = "Users";

        BackColor = Color.FromArgb(245, 242, 238);

        Color primary =
            Color.FromArgb(123, 75, 58);

        Color lightBg =
            Color.FromArgb(246, 247, 250);

        // =====================================================
        // TITLE
        // =====================================================

        lblTitle = new Label
        {
            Text = "User Management",

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
        // TOP PANEL
        // =====================================================

        Panel topPanel = new Panel
        {
            Dock = DockStyle.Top,

            Height = 170,

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

            Location = new Point(25, 60)
        };

        txtFullName = new TextBox
        {
            Width = 220,

            Height = 40,

            Font = new Font(
                "Segoe UI",
                11
            ),

            Location = new Point(25, 85),

            BackColor = lightBg
        };

        // =====================================================
        // EMAIL
        // =====================================================

        Label lblEmail = new Label
        {
            Text = "Email Address",

            Font = new Font(
                "Segoe UI",
                10
            ),

            AutoSize = true,

            Location = new Point(270, 60)
        };

        txtEmail = new TextBox
        {
            Width = 250,

            Height = 40,

            Font = new Font(
                "Segoe UI",
                11
            ),

            Location = new Point(270, 85),

            BackColor = lightBg
        };

        // =====================================================
        // PASSWORD
        // =====================================================

        Label lblPassword = new Label
        {
            Text = "Password",

            Font = new Font(
                "Segoe UI",
                10
            ),

            AutoSize = true,

            Location = new Point(545, 60)
        };

        txtPassword = new TextBox
        {
            Width = 180,

            Height = 40,

            Font = new Font(
                "Segoe UI",
                11
            ),

            Location = new Point(545, 85),

            BackColor = lightBg,

            UseSystemPasswordChar = true
        };

        // =====================================================
        // ROLE
        // =====================================================

        Label lblRole = new Label
        {
            Text = "Role",

            Font = new Font(
                "Segoe UI",
                10
            ),

            AutoSize = true,

            Location = new Point(750, 60)
        };

        cmbRole = new ComboBox
        {
            Width = 140,

            Height = 40,

            Font = new Font(
                "Segoe UI",
                10
            ),

            Location = new Point(750, 85),

            DropDownStyle =
                ComboBoxStyle.DropDownList
        };

        cmbRole.Items.AddRange(
            new string[]
            {
                "customer",
                "admin"
            }
        );

        cmbRole.SelectedIndex = 0;

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
            1040
        );

        Button btnDelete = CreateButton(
            "Delete",
            Color.Firebrick,
            920,
            85
        );

        Button btnRefresh = CreateButton(
            "Refresh",
            Color.FromArgb(60, 120, 180),
            1040,
            85
        );

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

        topPanel.Controls.Add(txtFullName);

        topPanel.Controls.Add(lblEmail);

        topPanel.Controls.Add(txtEmail);

        topPanel.Controls.Add(lblPassword);

        topPanel.Controls.Add(txtPassword);

        topPanel.Controls.Add(lblRole);

        topPanel.Controls.Add(cmbRole);

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
    // BUTTON CREATOR
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
            grid.CurrentRow.Cells["user_id"]
                .Value
        );

    // =====================================================
    // LOAD DATA
    // =====================================================

    private void LoadData()
    {
        grid.DataSource = Db.Query(
            "SELECT user_id, full_name, email, role, created_at FROM users ORDER BY user_id DESC"
        );
    }

    // =====================================================
    // FILL FIELDS
    // =====================================================

    private void FillFields()
    {
        if (grid.CurrentRow == null)
            return;

        txtFullName.Text =
            grid.CurrentRow.Cells["full_name"]
                .Value
                .ToString();

        txtEmail.Text =
            grid.CurrentRow.Cells["email"]
                .Value
                .ToString();

        cmbRole.Text =
            grid.CurrentRow.Cells["role"]
                .Value
                .ToString();

        txtPassword.Text = "";
    }

    // =====================================================
    // VALIDATION
    // =====================================================

    private bool Valid(
        bool needPassword
    )
    {
        return
            txtFullName.Text.Trim() != "" &&
            txtEmail.Text.Trim() != "" &&
            (
                !needPassword ||
                txtPassword.Text.Trim() != ""
            );
    }

    // =====================================================
    // ADD USER
    // =====================================================

    private void Add()
    {
        if (!Valid(true))
        {
            MessageBox.Show(
                "Complete user details.",
                "Validation",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            return;
        }

        Db.Execute(
            "INSERT INTO users(full_name,email,password,role) VALUES(@n,@e,@p,@r)",

            new MySqlParameter(
                "@n",
                txtFullName.Text
            ),

            new MySqlParameter(
                "@e",
                txtEmail.Text
            ),

            new MySqlParameter(
                "@p",
                PasswordHelper.Hash(
                    txtPassword.Text
                )
            ),

            new MySqlParameter(
                "@r",
                cmbRole.Text
            )
        );

        LoadData();

        MessageBox.Show(
            "User added successfully."
        );
    }

    // =====================================================
    // UPDATE USER
    // =====================================================

    private void UpdateRecord()
    {
        if (Id == 0 || !Valid(false))
            return;

        if (txtPassword.Text.Trim() == "")
        {
            Db.Execute(
                "UPDATE users SET full_name=@n,email=@e,role=@r WHERE user_id=@id",

                new MySqlParameter(
                    "@n",
                    txtFullName.Text
                ),

                new MySqlParameter(
                    "@e",
                    txtEmail.Text
                ),

                new MySqlParameter(
                    "@r",
                    cmbRole.Text
                ),

                new MySqlParameter(
                    "@id",
                    Id
                )
            );
        }
        else
        {
            Db.Execute(
                "UPDATE users SET full_name=@n,email=@e,password=@p,role=@r WHERE user_id=@id",

                new MySqlParameter(
                    "@n",
                    txtFullName.Text
                ),

                new MySqlParameter(
                    "@e",
                    txtEmail.Text
                ),

                new MySqlParameter(
                    "@p",
                    PasswordHelper.Hash(
                        txtPassword.Text
                    )
                ),

                new MySqlParameter(
                    "@r",
                    cmbRole.Text
                ),

                new MySqlParameter(
                    "@id",
                    Id
                )
            );
        }

        LoadData();

        MessageBox.Show(
            "User updated successfully."
        );
    }

    // =====================================================
    // DELETE USER
    // =====================================================

    private void Delete()
    {
        if (Id == 0)
            return;

        if (
            MessageBox.Show(
                "Delete selected user?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            ) != DialogResult.Yes
        )
            return;

        Db.Execute(
            "DELETE FROM users WHERE user_id=@id",

            new MySqlParameter(
                "@id",
                Id
            )
        );

        LoadData();

        MessageBox.Show(
            "User deleted successfully."
        );
    }
}