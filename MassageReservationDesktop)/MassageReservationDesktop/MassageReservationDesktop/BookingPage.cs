using System.Data;
using MySql.Data.MySqlClient;

namespace MassageReservationDesktop;

public class BookingPage : TabPage
{
    private readonly MainForm main;
    private ComboBox services = new(), therapists = new();
    private DateTimePicker date = new(), time = new();

    public BookingPage(MainForm mainForm)
    {
        main = mainForm;
        Text = "Book Reservation";
        BackColor = Theme.Background;

        var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(34), BackColor = Theme.Background };
        var card = new Panel { Left = 40, Top = 35, Width = 560, Height = 360, BackColor = Theme.Surface };
        var title = Theme.Title("Create Reservation", 30, 28, 20);
        var subtitle = Theme.MutedLabel("Select a service, therapist, date, and time.", 32, 65, 360);

        services = new ComboBox { Left = 190, Top = 112, Width = 310, DropDownStyle = ComboBoxStyle.DropDownList };
        therapists = new ComboBox { Left = 190, Top = 157, Width = 310, DropDownStyle = ComboBoxStyle.DropDownList };
        date = new DateTimePicker { Left = 190, Top = 202, Width = 310, Format = DateTimePickerFormat.Short, MinDate = DateTime.Today };
        time = new DateTimePicker { Left = 190, Top = 247, Width = 310, Format = DateTimePickerFormat.Time, ShowUpDown = true };
        Theme.StyleInput(services);
        Theme.StyleInput(therapists);
        Theme.StyleInput(date);
        Theme.StyleInput(time);

        var submit = Theme.PrimaryButton("Submit Reservation", 170);
        submit.Left = 190; submit.Top = 300;
        var reload = Theme.SecondaryButton("Reload Lists", 130);
        reload.Left = 370; reload.Top = 300;
        submit.Click += (_, _) => Submit();
        reload.Click += (_, _) => LoadLists();

        card.Controls.AddRange(new Control[] {
            title, subtitle,
            Theme.MutedLabel("Service", 60, 116, 100), services,
            Theme.MutedLabel("Therapist", 60, 161, 100), therapists,
            Theme.MutedLabel("Date", 60, 206, 100), date,
            Theme.MutedLabel("Time", 60, 251, 100), time,
            submit, reload
        });
        panel.Controls.Add(card);
        Controls.Add(panel);
        LoadLists();
    }

    private void LoadLists()
    {
        var serviceTable = Db.Query("SELECT service_id, CONCAT(service_name, ' - ₱', price, ' / ', duration, ' min') AS label FROM services ORDER BY service_name");
        services.DataSource = serviceTable; services.ValueMember = "service_id"; services.DisplayMember = "label";
        var therapistTable = Db.Query("SELECT therapist_id, CONCAT(full_name, ' - ', specialization) AS label FROM therapists WHERE availability_status='Available' ORDER BY full_name");
        therapists.DataSource = therapistTable; therapists.ValueMember = "therapist_id"; therapists.DisplayMember = "label";
    }

    private void Submit()
    {
        if (services.SelectedValue == null || therapists.SelectedValue == null) { MessageBox.Show("Select service and therapist."); return; }
        Db.Execute(@"INSERT INTO reservations(user_id, service_id, therapist_id, reservation_date, reservation_time, status)
                     VALUES(@u, @s, @t, @d, @tm, 'Pending')",
            new MySqlParameter("@u", main.UserId),
            new MySqlParameter("@s", services.SelectedValue),
            new MySqlParameter("@t", therapists.SelectedValue),
            new MySqlParameter("@d", date.Value.Date),
            new MySqlParameter("@tm", time.Value.ToString("HH:mm:ss")));
        MessageBox.Show("Reservation submitted. Status: Pending", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
