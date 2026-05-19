using MySql.Data.MySqlClient;

namespace MassageReservationDesktop;

public class LoginForm : Form
{
    private TextBox txtEmail = new();
    private TextBox txtPassword = new();

    private Label lblMessage = new();

    public LoginForm()
    {
        // =====================================================
        // FORM
        // =====================================================

        Text = "Massage Reservation System | Admin Login";

        Size = new Size(1280, 760);

        StartPosition = FormStartPosition.CenterScreen;

        FormBorderStyle = FormBorderStyle.FixedSingle;

        MaximizeBox = false;

        BackColor = Color.FromArgb(245, 242, 238);

        Font = new Font("Segoe UI", 10);

        Color primary = Color.FromArgb(123, 75, 58);

        Color lightBg = Color.FromArgb(246, 247, 250);

        // =====================================================
        // LEFT PANEL
        // =====================================================

        Panel leftPanel = new Panel
        {
            Dock = DockStyle.Left,

            Width = 470,

            BackColor = primary
        };

        Label lblBrand = new Label
        {
            Text = "Massage\nReservation\nSystem",

            Font = new Font(
                "Segoe UI",
                24,
                FontStyle.Bold
            ),

            ForeColor = Color.White,

            AutoSize = false,

            Width = 350,

            Height = 220,

            Location = new Point(50, 90),

            BackColor = Color.Transparent
        };

       

        Label lblFooter = new Label
        {
            Text = "© 2026 Massage Reservation System",

            Font = new Font(
                "Segoe UI",
                9
            ),

            ForeColor = Color.FromArgb(
                220,
                220,
                220
            ),

            AutoSize = true,

            Location = new Point(55, 680),

            BackColor = Color.Transparent
        };

        leftPanel.Controls.Add(lblBrand);

       

        leftPanel.Controls.Add(lblFooter);

        // =====================================================
        // LOGIN CARD
        // =====================================================

        Panel card = new Panel
        {
            Size = new Size(520, 520),

            Location = new Point(640, 105),

            BackColor = Color.White
        };

        card.Paint += (s, e) =>
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                card.ClientRectangle,
                Color.FromArgb(225, 225, 225),
                ButtonBorderStyle.Solid
            );
        };

        // =====================================================
        // TITLE
        // =====================================================

        Label lblTitle = new Label
        {
            Text = "Admin Login",

            Font = new Font(
                "Segoe UI",
               23,
                FontStyle.Bold
            ),

            ForeColor = Color.FromArgb(30, 30, 30),

            Width = 520,

            Height = 60,

            Top = 35,

            TextAlign = ContentAlignment.MiddleCenter
        };

        Label lblSubtitle = new Label
        {
            Text =
                "Login to continue to the dashboard",

            Font = new Font(
                "Segoe UI",
                11
            ),

            ForeColor = Color.Gray,

            Width = 520,

            Height = 25,

            Top = 90,

            TextAlign = ContentAlignment.MiddleCenter
        };

        // =====================================================
        // EMAIL LABEL
        // =====================================================

        Label lblEmail = new Label
        {
            Text = "Email Address",

            Font = new Font(
                "Segoe UI",
                10
            ),

            ForeColor = Color.FromArgb(
                60,
                60,
                60
            ),

            AutoSize = true,

            Location = new Point(60, 155)
        };

        // EMAIL PANEL

        Panel emailPanel = new Panel
        {
            Size = new Size(400, 58),

            Location = new Point(60, 180),

            BackColor = lightBg
        };

        txtEmail = new TextBox
        {
            BorderStyle = BorderStyle.None,

            Font = new Font(
                "Segoe UI",
                12
            ),

            Width = 340,

            Location = new Point(18, 18),

            BackColor = lightBg,

            Text = ""
        };

        emailPanel.Controls.Add(txtEmail);

        // =====================================================
        // PASSWORD LABEL
        // =====================================================

        Label lblPassword = new Label
        {
            Text = "Password",

            Font = new Font(
                "Segoe UI",
                10
            ),

            ForeColor = Color.FromArgb(
                60,
                60,
                60
            ),

            AutoSize = true,

            Location = new Point(60, 260)
        };

        // PASSWORD PANEL

        Panel passwordPanel = new Panel
        {
            Size = new Size(400, 58),

            Location = new Point(60, 285),

            BackColor = lightBg
        };

        txtPassword = new TextBox
        {
            BorderStyle = BorderStyle.None,

            Font = new Font(
                "Segoe UI",
                12
            ),

            Width = 280,

            Location = new Point(18, 18),

            BackColor = lightBg,

            UseSystemPasswordChar = true,

            Text = ""
        };

        // SHOW PASSWORD BUTTON

        Button btnShow = new Button
        {
            Text = "👁",

            Width = 45,

            Height = 45,

            Location = new Point(345, 6),

            FlatStyle = FlatStyle.Flat,

            BackColor = lightBg,

            Cursor = Cursors.Hand
        };

        btnShow.FlatAppearance.BorderSize = 0;

        btnShow.Click += (_, _) =>
        {
            txtPassword.UseSystemPasswordChar =
                !txtPassword.UseSystemPasswordChar;
        };

        passwordPanel.Controls.Add(txtPassword);

        passwordPanel.Controls.Add(btnShow);

        // =====================================================
        // LOGIN BUTTON
        // =====================================================

        Button btnLogin = new Button
        {
            Text = "Login",

            Size = new Size(400, 55),

            Location = new Point(60, 380),

            FlatStyle = FlatStyle.Flat,

            BackColor = primary,

            ForeColor = Color.White,

            Font = new Font(
                "Segoe UI",
                13,
                FontStyle.Bold
            ),

            Cursor = Cursors.Hand
        };

        btnLogin.FlatAppearance.BorderSize = 0;

        btnLogin.MouseEnter += (_, _) =>
        {
            btnLogin.BackColor =
                Color.FromArgb(95, 60, 45);
        };

        btnLogin.MouseLeave += (_, _) =>
        {
            btnLogin.BackColor = primary;
        };

        // =====================================================
        // MESSAGE LABEL
        // =====================================================

        lblMessage = new Label
        {
            Width = 400,

            Height = 25,

            Location = new Point(60, 450),

            Font = new Font(
                "Segoe UI",
                9
            ),

            ForeColor = Color.Firebrick,

            TextAlign = ContentAlignment.MiddleCenter
        };

        // =====================================================
        // EVENTS
        // =====================================================

        btnLogin.Click += LoginButton_Click;

        AcceptButton = btnLogin;

        // =====================================================
        // ADD CONTROLS
        // =====================================================

        card.Controls.Add(lblTitle);

        card.Controls.Add(lblSubtitle);

        card.Controls.Add(lblEmail);

        card.Controls.Add(emailPanel);

        card.Controls.Add(lblPassword);

        card.Controls.Add(passwordPanel);

        card.Controls.Add(btnLogin);

        card.Controls.Add(lblMessage);

        Controls.Add(leftPanel);

        Controls.Add(card);
    }

    // =====================================================
    // LOGIN FUNCTION
    // =====================================================

    private void LoginButton_Click(  object? sender, EventArgs e
    )
    {
        try
        {
            var table = Db.Query(
                "SELECT user_id, full_name, email, password, role FROM users WHERE email=@Email AND role='admin' LIMIT 1",

                new MySqlParameter(
                    "@Email",
                    txtEmail.Text.Trim()
                )
            );

            if (
                table.Rows.Count == 0 ||

                !PasswordHelper.Verify(
                    txtPassword.Text,
                    table.Rows[0]["password"]
                        .ToString() ?? ""
                )
            )
            {
                lblMessage.Text =
                    "Invalid admin email or password.";

                return;
            }

            var dashboard = new MainForm(
                Convert.ToInt32(
                    table.Rows[0]["user_id"]
                ),

                table.Rows[0]["full_name"]
                    .ToString() ?? "Admin",

                table.Rows[0]["role"]
                    .ToString() ?? "admin"
            );

            Hide();

            dashboard.FormClosed += (_, _) =>
                Close();

            dashboard.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Database connection failed.\n\n" +
                ex.Message,

                "Connection Error",

                MessageBoxButtons.OK,

                MessageBoxIcon.Error
            );
        }
    }
}