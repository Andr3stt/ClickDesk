using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ClickDesk.Forms.Auth
{
    public class InputText : Control
    {
        private TextBox _innerTextBox;
        private int _borderRadius = 6;
        private bool _isFocused = false;
        private Color _borderColor = Color.FromArgb(207, 207, 207);
        private Color _focusedBorderColor = Color.FromArgb(250, 140, 43);
        private Color _backgroundColor = Color.White;
        private bool _usePasswordChar = false;

        [Category("Appearance")]
        public int BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color FocusedBorderColor
        {
            get => _focusedBorderColor;
            set { _focusedBorderColor = value; Invalidate(); }
        }

        [Category("Behavior")]
        public bool UsePasswordChar
        {
            get => _usePasswordChar;
            set
            {
                _usePasswordChar = value;
                if (_innerTextBox != null) _innerTextBox.UseSystemPasswordChar = value;
            }
        }

        [Category("Behavior")]
        public int MaxLength { get; set; } = 32767;

        public override string Text
        {
            get => _innerTextBox?.Text ?? "";
            set { if (_innerTextBox != null) _innerTextBox.Text = value; }
        }

        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                if (_innerTextBox != null) _innerTextBox.Font = value;
            }
        }

        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                if (_innerTextBox != null) _innerTextBox.ForeColor = value;
            }
        }

        public InputText()
        {
            DoubleBuffered = true;
            Size = new Size(300, 42);
            BackColor = _backgroundColor;
            ForeColor = Color.FromArgb(21, 36, 27);
            Font = new Font("Segoe UI", 11);
            Margin = new Padding(0);
            Padding = new Padding(12, 8, 12, 8);

            _innerTextBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = _backgroundColor,
                ForeColor = Color.FromArgb(21, 36, 27),
                Font = Font,
                Multiline = false,
                MaxLength = MaxLength,
                UseSystemPasswordChar = _usePasswordChar,
            };

            _innerTextBox.GotFocus += InnerTextBox_GotFocus;
            _innerTextBox.LostFocus += InnerTextBox_LostFocus;
            _innerTextBox.TextChanged += InnerTextBox_TextChanged;

            Controls.Add(_innerTextBox);
        }

        private void InnerTextBox_GotFocus(object sender, EventArgs e)
        {
            _isFocused = true;
            Invalidate();
        }

        private void InnerTextBox_LostFocus(object sender, EventArgs e)
        {
            _isFocused = false;
            Invalidate();
        }

        private void InnerTextBox_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(BackColor);

            GraphicsPath path = CreateRoundedRectangle(ClientRectangle, _borderRadius);

            using (Brush brush = new SolidBrush(_backgroundColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            Color borderColor = _isFocused ? _focusedBorderColor : _borderColor;
            int borderWidth = _isFocused ? 2 : 1;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawPath(pen, path);
            }

            path.Dispose();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (_innerTextBox != null)
            {
                _innerTextBox.Left = Padding.Left;
                _innerTextBox.Top = Padding.Top;
                _innerTextBox.Width = Width - Padding.Left - Padding.Right;
                _innerTextBox.Height = Height - Padding.Top - Padding.Bottom;
            }

            Invalidate();
        }

        private GraphicsPath CreateRoundedRectangle(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            if (diameter > bounds.Width) diameter = bounds.Width;
            if (diameter > bounds.Height) diameter = bounds.Height;

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.X + bounds.Width - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.X + bounds.Width - diameter, bounds.Y + bounds.Height - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Y + bounds.Height - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        public void Clear()
        {
            _innerTextBox?.Clear();
        }

        public event EventHandler TextChanged;

        protected virtual void OnTextChanged(EventArgs e)
        {
            TextChanged?.Invoke(this, e);
        }
    }
}
