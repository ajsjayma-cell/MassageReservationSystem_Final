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

        Size = new Size(1150, 700);

        StartPosition = FormStartPosition.CenterScreen;

        FormBorderStyle = FormBorderStyle.FixedSingle;

        MaximizeBox = false;

        BackColor = Color.FromArgb(245, 242, 238);

        Font = new Font("Segoe UI", 10);

        // =====================================================
        // COLORS
        // =====================================================

        Color primary = Color.FromArgb(123, 75, 58);

        Color lightBg = Color.FromArgb(248, 248, 248);

        // =====================================================
        // LEFT PANEL
        // =====================================================

        Panel leftPanel = new Panel
        {
            Dock = DockStyle.Left,

            Width = 430,

            BackColor = primary
        };

        // TITLE

        Label lblBrand = new Label
        {
            Text = "Massage\nReservation\nSystem",

            Font = new Font(
                "Segoe UI",
                30,
                FontStyle.Bold
            ),

            ForeColor = Color.White,

            Location = new Point(45, 90),

            Size = new Size(330, 180)
        };

        // SUBTITLE

        Label lblDesc = new Label
        {
            Text =
                "Secure admin desktop panel for\n" +
                "managing reservations, therapists,\n" +
                "services, schedules, and reports.",

            Font = new Font(
                "Segoe UI",
                12
            ),

            ForeColor = Color.FromArgb(
                230,
                255,
                255,
                255
            ),

            Location = new Point(50, 320),

            Size = new Size(320, 120)
        };

        // FOOTER

        Label lblFooter = new Label
        {
            Text = "© 2026 Massage Reservation System",

            Font = new Font(
                "Segoe UI",
                9
            ),

            ForeColor = Color.FromArgb(
                220,
                255,
                255,
                255
            ),

            AutoSize = true,

            Location = new Point(50, 630)
        };

        leftPanel.Controls.Add(lblBrand);

        leftPanel.Controls.Add(lblDesc);

        leftPanel.Controls.Add(lblFooter);

        // =====================================================
        // LOGIN CARD
        // =====================================================

        Panel card = new Panel
        {
            Size = new Size(500, 500),

            Location = new Point(560, 95),

            BackColor = Color.White
        };

        // BORDER

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
                28,
                FontStyle.Bold
            ),

            ForeColor = Color.FromArgb(35, 35, 35),

            Width = 500,

            Height = 60,

            Top = 45,

            TextAlign = ContentAlignment.MiddleCenter
        };

        // SUBTITLE

        Label lblSubtitle = new Label
        {
            Text =
                "Login to continue to the dashboard",

            Font = new Font(
                "Segoe UI",
                11
            ),

            ForeColor = Color.Gray,

            Width = 500,

            Height = 25,

            Top = 102,

            TextAlign = ContentAlignment.MiddleCenter
        };

        // =====================================================
        // EMAIL LABEL
        // =====================================================

        Label lblEmail = new Label
        {
            Text = "Admin Email",

            Font = new Font(
                "Segoe UI",
                10
            ),

            ForeColor = Color.FromArgb(60, 60, 60),

            AutoSize = true,

            Location = new Point(60, 165)
        };

        // EMAIL PANEL

        Panel emailPanel = new Panel
        {
            Size = new Size(380, 55),

            Location = new Point(60, 190),

            BackColor = lightBg
        };

        txtEmail = new TextBox
        {
            BorderStyle = BorderStyle.None,

            Font = new Font(
                "Segoe UI",
                12
            ),

            Width = 330,

            Location = new Point(18, 18),

            BackColor = lightBg,

            Text = "admin@example.com"
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

            ForeColor = Color.FromArgb(60, 60, 60),

            AutoSize = true,

            Location = new Point(60, 265)
        };

        // PASSWORD PANEL

        Panel passwordPanel = new Panel
        {
            Size = new Size(380, 55),

            Location = new Point(60, 290),

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

        // SHOW BUTTON

        Button btnShow = new Button
        {
            Text = "👁",

            Width = 45,

            Height = 45,

            Location = new Point(325, 5),

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

            Size = new Size(380, 55),

            Location = new Point(60, 375),

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

        // HOVER

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
            Width = 380,

            Height = 25,

            Location = new Point(60, 445),

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

    private void LoginButton_Click(
        object? sender,
        EventArgs e
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