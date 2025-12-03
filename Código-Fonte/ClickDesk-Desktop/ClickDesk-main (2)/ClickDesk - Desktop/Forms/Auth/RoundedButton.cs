using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ClickDesk.Forms.Auth
{
    public class RoundedButton : Button
    {
        private int _borderRadius = 8;
        private Color _hoverColor;
        private Color _clickColor;
        private bool _isHovered = false;
        private bool _isPressed = false;
        private Color _normalColor;

        [Category("Appearance")]
        public int BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color HoverColor
        {
            get => _hoverColor;
            set { _hoverColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color ClickColor
        {
            get => _clickColor;
            set { _clickColor = value; Invalidate(); }
        }

        public RoundedButton()
        {
            DoubleBuffered = true;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            
            BackColor = Color.FromArgb(250, 140, 43);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 11, FontStyle.Bold);
            
            _normalColor = BackColor;
            _hoverColor = Color.FromArgb(225, 120, 14);
            _clickColor = Color.FromArgb(225, 120, 14);
            
            Size = new Size(100, 40);
            Cursor = Cursors.Hand;
            TextAlign = ContentAlignment.MiddleCenter;
            FlatAppearance.MouseOverBackColor = _hoverColor;
            FlatAppearance.MouseDownBackColor = _clickColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Parent?.BackColor ?? Color.White);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Color currentColor = _normalColor;
            if (_isPressed)
                currentColor = _clickColor;
            else if (_isHovered)
                currentColor = _hoverColor;

            GraphicsPath path = CreateRoundedRectangle(ClientRectangle, _borderRadius);

            using (Brush brush = new SolidBrush(currentColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            if (Focused)
            {
                using (Pen focusPen = new Pen(Color.FromArgb(250, 140, 43), 2))
                {
                    e.Graphics.DrawPath(focusPen, path);
                }
            }

            TextRenderer.DrawText(
                e.Graphics,
                Text,
                Font,
                ClientRectangle,
                ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );

            path.Dispose();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _isHovered = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isHovered = false;
            _isPressed = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isPressed = true;
                Invalidate();
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isPressed = false;
            Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
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

        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(value);
            Invalidate();
        }
    }
}
