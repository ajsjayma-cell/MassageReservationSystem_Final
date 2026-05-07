namespace MassageReservationDesktop;

public static class Theme
{
    // =====================================================
    // COLORS
    // =====================================================

    public static readonly Color Primary =
        Color.FromArgb(123, 75, 58);

    public static readonly Color PrimaryDark =
        Color.FromArgb(95, 58, 45);

    public static readonly Color Accent =
        Color.FromArgb(191, 141, 114);

    public static readonly Color Background =
        Color.FromArgb(245, 242, 238);

    public static readonly Color Surface =
        Color.White;

    public static readonly Color Text =
        Color.FromArgb(35, 35, 35);

    public static readonly Color Muted =
        Color.FromArgb(120, 120, 120);

    public static readonly Color Border =
        Color.FromArgb(225, 225, 225);

    public static readonly Color Success =
        Color.FromArgb(46, 125, 50);

    public static readonly Color Danger =
        Color.FromArgb(211, 47, 47);

    // =====================================================
    // FONTS
    // =====================================================

    public static Font TitleFont(
        float size = 18
    )
    {
        return new Font(
            "Segoe UI",
            size,
            FontStyle.Bold
        );
    }

    public static Font BodyFont(
        float size = 10
    )
    {
        return new Font(
            "Segoe UI",
            size,
            FontStyle.Regular
        );
    }

    public static Font ButtonFont(
        float size = 10
    )
    {
        return new Font(
            "Segoe UI",
            size,
            FontStyle.Bold
        );
    }

    // =====================================================
    // APPLY FORM
    // =====================================================

    public static void ApplyForm(
        Form form
    )
    {
        form.BackColor = Background;

        form.Font = BodyFont(10);

        form.ForeColor = Text;
    }

    // =====================================================
    // TITLE LABEL
    // =====================================================

    public static Label Title(
        string text,
        int left,
        int top,
        int size = 20
    )
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

    // =====================================================
    // MUTED LABEL
    // =====================================================

    public static Label MutedLabel(
        string text,
        int left,
        int top,
        int width = 220
    )
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

    // =====================================================
    // PRIMARY BUTTON
    // =====================================================

    public static Button PrimaryButton(
        string text,
        int width = 120
    )
    {
        Button btn = new Button
        {
            Text = text,

            Width = width,

            Height = 42,

            BackColor = Primary,

            ForeColor = Color.White,

            FlatStyle = FlatStyle.Flat,

            Font = ButtonFont(10),

            Cursor = Cursors.Hand
        };

        btn.FlatAppearance.BorderSize = 0;

        btn.FlatAppearance.MouseOverBackColor =
            PrimaryDark;

        return btn;
    }

    // =====================================================
    // SECONDARY BUTTON
    // =====================================================

    public static Button SecondaryButton(
        string text,
        int width = 120
    )
    {
        Button btn = new Button
        {
            Text = text,

            Width = width,

            Height = 42,

            BackColor = Surface,

            ForeColor = Primary,

            FlatStyle = FlatStyle.Flat,

            Font = ButtonFont(10),

            Cursor = Cursors.Hand
        };

        btn.FlatAppearance.BorderColor =
            Border;

        btn.FlatAppearance.BorderSize = 1;

        btn.FlatAppearance.MouseOverBackColor =
            Color.FromArgb(240, 235, 230);

        return btn;
    }

    // =====================================================
    // STYLE INPUT
    // =====================================================

    public static void StyleInput(
        Control control
    )
    {
        control.Font = BodyFont(11);

        control.Height = 42;

        control.BackColor =
            Color.FromArgb(246, 247, 250);

        control.ForeColor = Text;

        if (control is TextBox txt)
        {
            txt.BorderStyle = BorderStyle.FixedSingle;
        }

        if (control is ComboBox cmb)
        {
            cmb.FlatStyle = FlatStyle.Flat;
        }
    }

    // =====================================================
    // STYLE GRID
    // =====================================================

    public static void StyleGrid(
        DataGridView grid
    )
    {
        grid.BorderStyle = BorderStyle.None;

        grid.BackgroundColor = Surface;

        grid.GridColor = Border;

        grid.RowHeadersVisible = false;

        grid.EnableHeadersVisualStyles = false;

        grid.AllowUserToAddRows = false;

        grid.AllowUserToDeleteRows = false;

        grid.ReadOnly = true;

        grid.MultiSelect = false;

        grid.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;

        // HEADER

        grid.ColumnHeadersDefaultCellStyle.BackColor =
            Primary;

        grid.ColumnHeadersDefaultCellStyle.ForeColor =
            Color.White;

        grid.ColumnHeadersDefaultCellStyle.Font =
            ButtonFont(10);

        grid.ColumnHeadersHeight = 45;

        // ROWS

        grid.DefaultCellStyle.Font =
            BodyFont(10);

        grid.DefaultCellStyle.ForeColor =
            Text;

        grid.DefaultCellStyle.SelectionBackColor =
            Color.FromArgb(225, 210, 205);

        grid.DefaultCellStyle.SelectionForeColor =
            Text;

        grid.AlternatingRowsDefaultCellStyle.BackColor =
            Color.FromArgb(250, 249, 248);

        grid.RowTemplate.Height = 35;
    }

    // =====================================================
    // TOOLBAR PANEL
    // =====================================================

    public static Panel Toolbar(
        int height = 80
    )
    {
        return new Panel
        {
            Dock = DockStyle.Top,

            Height = height,

            Padding = new Padding(14),

            BackColor = Surface
        };
    }

    // =====================================================
    // CARD PANEL
    // =====================================================

    public static Panel Card()
    {
        Panel panel = new Panel
        {
            BackColor = Surface
        };

        panel.Paint += (s, e) =>
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                panel.ClientRectangle,
                Border,
                ButtonBorderStyle.Solid
            );
        };

        return panel;
    }
}