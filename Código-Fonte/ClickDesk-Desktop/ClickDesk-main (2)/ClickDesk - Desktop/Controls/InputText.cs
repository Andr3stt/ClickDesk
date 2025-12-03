using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ClickDesk.Utils;

namespace ClickDesk.Forms.Auth
{
    /// <summary>
    /// TextBox personalizado com estilo moderno, placeholder, bordas arredondadas e efeito foco.
    /// Implementado 100% em C# com GDI+.
    /// </summary>
    public class InputText : Control
    {
        private TextBox _innerTextBox;
        private string _placeholder = "Placeholder";
        private int _borderRadius = 6;
        private bool _isFocused = false;
        private Color _borderColor = StyleManager.Colors.Border;
        private Color _focusedBorderColor = StyleManager.Colors.FocusColor;
        private Color _backgroundColor = StyleManager.Colors.Background;
        private bool _usePasswordChar = false;
        private int _maxLength = 32767;

        [Category("Appearance")]
        [Description("Placeholder text")]
        public string Placeholder
        {
            get => _placeholder;
            set { _placeholder = value; _innerTextBox?.Invalidate(); }
        }

        [Category("Appearance")]
        [Description("Raio das bordas arredondadas")]
        public int BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = value; Invalidate(); }
        }

        [Category("Appearance")]
        [Description("Cor da borda")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        [Description("Cor da borda ao focar")]
        public Color FocusedBorderColor
        {
            get => _focusedBorderColor;
            set { _focusedBorderColor = value; Invalidate(); }
        }

        public override string Text
        {
            get => _innerTextBox?.Text ?? "";
            set { if (_innerTextBox != null) _innerTextBox.Text = value; }
        }

        [Category("Behavior")]
        [Description("Máximo de caracteres")]
        public int MaxLength
        {
            get => _maxLength;
            set
            {
                _maxLength = value;
                if (_innerTextBox != null) _innerTextBox.MaxLength = value;
            }
        }

        [Category("Behavior")]
        [Description("Usar caractere de senha")]
        public bool UsePasswordChar
        {
            get => _usePasswordChar;
            set
            {
                _usePasswordChar = value;
                if (_innerTextBox != null) _innerTextBox.UseSystemPasswordChar = value;
            }
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
            ForeColor = StyleManager.Colors.TextPrimary;
            Font = StyleManager.Fonts.Body;
            Margin = new Padding(0);
            Padding = new Padding(12, 8, 12, 8);

            // Criar TextBox interno
            _innerTextBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = _backgroundColor,
                ForeColor = StyleManager.Colors.TextPrimary,
                Font = Font,
                Multiline = false,
                MaxLength = _maxLength,
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

            // Criar caminho arredondado
            GraphicsPath path = CreateRoundedRectangle(ClientRectangle, _borderRadius);

            // Preencher com background
            using (Brush brush = new SolidBrush(_backgroundColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            // Desenhar borda
            Color borderColor = _isFocused ? _focusedBorderColor : _borderColor;
            int borderWidth = _isFocused ? 2 : 1;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawPath(pen, pen);
            }

            path.Dispose();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Não pintar background padrão
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (_innerTextBox != null)
            {
                _innerTextBox.Left = Padding.Left;
                _innerTextBox.Top = Padding.Top;
                _innerTextBox.Width = Width - Padding.Left - Padding.Right;
                _innerTextBox.Height = Height - Padding.Top - Padding.Bottom;
            }
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

        public void Focus()
        {
            _innerTextBox?.Focus();
        }

        public void Clear()
        {
            _innerTextBox?.Clear();
        }

        public void SelectAll()
        {
            _innerTextBox?.SelectAll();
        }

        public event EventHandler TextChanged;

        protected virtual void OnTextChanged(EventArgs e)
        {
            TextChanged?.Invoke(this, e);
        }
    }
}
