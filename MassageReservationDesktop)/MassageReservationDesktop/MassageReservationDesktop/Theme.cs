namespace MassageReservationDesktop;

public static class Theme
{
    public static readonly Color Primary = Color.FromArgb(113, 70, 222);
    public static readonly Color PrimaryDark = Color.FromArgb(79, 49, 154);
    public static readonly Color Accent = Color.FromArgb(236, 113, 146);
    public static readonly Color Background = Color.FromArgb(246, 244, 250);
    public static readonly Color Surface = Color.White;
    public static readonly Color Text = Color.FromArgb(38, 35, 50);
    public static readonly Color Muted = Color.FromArgb(111, 108, 125);
    public static readonly Color Border = Color.FromArgb(225, 221, 235);

    public static Font TitleFont(float size = 18) => new("Segoe UI", size, FontStyle.Bold);
    public static Font BodyFont(float size = 10) => new("Segoe UI", size, FontStyle.Regular);
    public static Font ButtonFont(float size = 10) => new("Segoe UI", size, FontStyle.Bold);

    public static void ApplyForm(Form form)
    {
        form.BackColor = Background;
        form.Font = BodyFont();
    }

    public static Label Title(string text, int left, int top, int size = 18)
    {
        return new Label
        {
            Text = text,
            Left = left,
            Top = top,
            AutoSize = true,
            Font = TitleFont(size),
            ForeColor = Text,
            BackColor = Color.Transparent
        };
    }

    public static Label MutedLabel(string text, int left, int top, int width = 180)
    {
        return new Label
        {
            Text = text,
            Left = left,
            Top = top,
            Width = width,
            Font = BodyFont(9),
            ForeColor = Muted,
            BackColor = Color.Transparent
        };
    }

    public static Button PrimaryButton(string text, int width = 120)
    {
        var btn = new Button
        {
            Text = text,
            Width = width,
            Height = 34,
            BackColor = Primary,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = ButtonFont(9),
            Cursor = Cursors.Hand
        };
        btn.FlatAppearance.BorderSize = 0;
        btn.FlatAppearance.MouseOverBackColor = PrimaryDark;
        return btn;
    }

    public static Button SecondaryButton(string text, int width = 110)
    {
        var btn = new Button
        {
            Text = text,
            Width = width,
            Height = 34,
            BackColor = Surface,
            ForeColor = Primary,
            FlatStyle = FlatStyle.Flat,
            Font = ButtonFont(9),
            Cursor = Cursors.Hand
        };
        btn.FlatAppearance.BorderColor = Border;
        btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 235, 250);
        return btn;
    }

    public static void StyleInput(Control control)
    {
        control.Font = BodyFont(10);
        control.Height = 30;
        control.BackColor = Color.White;
        control.ForeColor = Text;
    }

    public static void StyleGrid(DataGridView grid)
    {
        grid.BorderStyle = BorderStyle.None;
        grid.BackgroundColor = Surface;
        grid.GridColor = Border;
        grid.RowHeadersVisible = false;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Primary;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        grid.ColumnHeadersDefaultCellStyle.Font = ButtonFont(9);
        grid.ColumnHeadersHeight = 38;
        grid.DefaultCellStyle.Font = BodyFont(9);
        grid.DefaultCellStyle.ForeColor = Text;
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 216, 250);
        grid.DefaultCellStyle.SelectionForeColor = Text;
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 249, 253);
        grid.RowTemplate.Height = 32;
    }

    public static Panel Toolbar(int height = 70)
    {
        return new Panel
        {
            Dock = DockStyle.Top,
            Height = height,
            Padding = new Padding(14),
            BackColor = Surface
        };
    }
}
