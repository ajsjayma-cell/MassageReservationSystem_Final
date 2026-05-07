namespace MassageReservationDesktop;

public class MainForm : Form
{
    public int UserId { get; }
    public string FullName { get; }
    public string Role { get; }
    private TabControl tabs = new();

    public MainForm(int userId, string fullName, string role)
    {
        UserId = userId;
        FullName = fullName;
        Role = role;

        Text = $"Massage Reservation Desktop - {FullName} ({Role})";
        Width = 1120;
        Height = 720;
        MinimumSize = new Size(1000, 620);
        StartPosition = FormStartPosition.CenterScreen;
        Theme.ApplyForm(this);

        var header = new Panel { Dock = DockStyle.Top, Height = 82, BackColor = Theme.Primary, Padding = new Padding(24, 14, 24, 10) };
        var title = new Label
        {
            Text = "Massage Reservation Dashboard",
            AutoSize = true,
            Left = 24,
            Top = 14,
            Font = Theme.TitleFont(18),
            ForeColor = Color.White,
            BackColor = Color.Transparent
        };
        var subtitle = new Label
        {
            Text = $"Signed in as {FullName} • {Role.ToUpper()}",
            AutoSize = true,
            Left = 26,
            Top = 48,
            Font = Theme.BodyFont(9),
            ForeColor = Color.FromArgb(236, 231, 255),
            BackColor = Color.Transparent
        };
        header.Controls.AddRange(new Control[] { title, subtitle });

        tabs.Dock = DockStyle.Fill;
        tabs.Font = Theme.ButtonFont(9);
        tabs.Padding = new Point(16, 8);
        tabs.Appearance = TabAppearance.Normal;

        Controls.Add(tabs);
        Controls.Add(header);

        tabs.TabPages.Add(new ReservationsPage(this));
        if (Role == "admin")
        {
            tabs.TabPages.Add(new ServicesPage());
            tabs.TabPages.Add(new TherapistsPage());
            tabs.TabPages.Add(new UsersPage());
        }
        else
        {
            tabs.TabPages.Add(new BookingPage(this));
        }
    }
}
