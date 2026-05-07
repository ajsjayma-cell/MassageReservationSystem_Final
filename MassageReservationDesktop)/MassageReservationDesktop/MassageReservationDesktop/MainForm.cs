namespace MassageReservationDesktop;

public class MainForm : Form
{
    public int UserId { get; }

    public string FullName { get; }

    public string Role { get; }

    private TabControl tabs = new();

    public MainForm(
        int userId,
        string fullName,
        string role
    )
    {
        UserId = userId;

        FullName = fullName;

        Role = role;

        // =====================================================
        // FORM
        // =====================================================

        Text =
            $"Massage Reservation System | {FullName}";

        Size = new Size(1280, 760);

        MinimumSize = new Size(1100, 680);

        StartPosition =
            FormStartPosition.CenterScreen;

        BackColor =
            Color.FromArgb(245, 242, 238);

        Font = new Font(
            "Segoe UI",
            10
        );

        Color primary =
            Color.FromArgb(123, 75, 58);

        // =====================================================
        // HEADER
        // =====================================================

        Panel header = new Panel
        {
            Dock = DockStyle.Top,

            Height = 95,

            BackColor = primary
        };

        Label lblTitle = new Label
        {
            Text =
                "Massage Reservation Dashboard",

            Font = new Font(
                "Segoe UI",
                20,
                FontStyle.Bold
            ),

            ForeColor = Color.White,

            AutoSize = true,

            Location = new Point(30, 18)
        };

        Label lblSubtitle = new Label
        {
            Text =
                $"Welcome back, {FullName}  •  {Role.ToUpper()}",

            Font = new Font(
                "Segoe UI",
                10
            ),

            ForeColor = Color.FromArgb(
                230,
                230,
                230
            ),

            AutoSize = true,

            Location = new Point(33, 58)
        };

        // =====================================================
        // LOGOUT BUTTON
        // =====================================================

        Button btnLogout = new Button
        {
            Text = "Logout",

            Size = new Size(120, 42),

            Location = new Point(1120, 26),

            FlatStyle = FlatStyle.Flat,

            BackColor = Color.White,

            ForeColor = primary,

            Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Bold
            ),

            Cursor = Cursors.Hand
        };

        btnLogout.FlatAppearance.BorderSize = 0;

        btnLogout.MouseEnter += (_, _) =>
        {
            btnLogout.BackColor =
                Color.FromArgb(240, 240, 240);
        };

        btnLogout.MouseLeave += (_, _) =>
        {
            btnLogout.BackColor = Color.White;
        };

        btnLogout.Click += (_, _) =>
        {
            Hide();

            LoginForm login =
                new LoginForm();

            login.Show();

            Close();
        };

        header.Controls.Add(lblTitle);

        header.Controls.Add(lblSubtitle);

        header.Controls.Add(btnLogout);

        // =====================================================
        // TAB CONTROL
        // =====================================================

        tabs = new TabControl
        {
            Dock = DockStyle.Fill,

            Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Bold
            ),

            Padding = new Point(18, 10),

            Appearance = TabAppearance.Normal
        };

        // =====================================================
        // TAB PAGES
        // =====================================================

        tabs.TabPages.Add(
            new ReservationsPage(this)
        );

        if (Role == "admin")
        {
            tabs.TabPages.Add(
                new ServicesPage()
            );

            tabs.TabPages.Add(
                new TherapistsPage()
            );

            tabs.TabPages.Add(
                new UsersPage()
            );
        }
        else
        {
            tabs.TabPages.Add(
                new BookingPage(this)
            );
        }

        // =====================================================
        // ADD CONTROLS
        // =====================================================

        Controls.Add(tabs);

        Controls.Add(header);
    }
}