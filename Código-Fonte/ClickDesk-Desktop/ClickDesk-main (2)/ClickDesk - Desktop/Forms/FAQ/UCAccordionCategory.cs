using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk.Forms.FAQ
{
    public class UCAccordionCategory : Panel
    {
        private Button header;
        private Panel content;
        private FlowLayoutPanel list;
        private Timer anim;
        private int targetHeight;
        private bool expanded = false;

        public UCAccordionCategory(string title)
        {
            this.Width = 900;
            this.BackColor = Color.Transparent;
            this.Margin = new Padding(0, 0, 0, 18);

            header = new Button();
            header.Text = title + "   ▼";
            header.BackColor = Color.FromArgb(242, 138, 26);
            header.ForeColor = Color.White;
            header.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            header.Height = 48;
            header.Dock = DockStyle.Top;
            header.FlatStyle = FlatStyle.Flat;
            header.Click += Toggle;

            list = new FlowLayoutPanel();
            list.FlowDirection = FlowDirection.TopDown;
            list.WrapContents = false;
            list.AutoSize = true;
            list.Dock = DockStyle.Top;

            content = new Panel();
            content.BackColor = Color.White;
            content.Height = 0;
            content.Controls.Add(list);

            anim = new Timer();
            anim.Interval = 15;
            anim.Tick += Animate;

            this.Controls.Add(content);
            this.Controls.Add(header);
        }

        public void AddQuestion(UCAccordionQuestion q)
        {
            list.Controls.Add(q);
        }

        private void Toggle(object s, EventArgs e)
        {
            expanded = !expanded;
            header.Text = header.Text.Contains("▼") ? header.Text.Replace("▼", "▲") : header.Text.Replace("▲", "▼");
            targetHeight = expanded ? list.Height : 0;
            anim.Start();
        }

        private void Animate(object s, EventArgs e)
        {
            if (expanded && content.Height < targetHeight)
            {
                content.Height += 20;
                if (content.Height >= targetHeight) anim.Stop();
            }
            else if (!expanded && content.Height > 0)
            {
                content.Height -= 20;
                if (content.Height <= 0) anim.Stop();
            }
        }

        public void SetExpanded(bool exp)
        {
            expanded = exp;
            header.Text = exp ? header.Text.Replace("▼", "▲") : header.Text.Replace("▲", "▼");
            targetHeight = exp ? list.Height : 0;
            content.Height = targetHeight;
        }
    }
}
