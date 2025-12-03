using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClickDesk.Forms.FAQ
{
    public class UCAccordionQuestion : Panel
    {
        private Button qButton;
        private Label answer;
        private Panel answerPanel;
        private Timer anim;
        private int target;

        private bool expanded = false;

        public UCAccordionQuestion(string q, string a)
        {
            this.Width = 860;
            this.BackColor = Color.FromArgb(239, 234, 226);
            this.Margin = new Padding(0, 0, 0, 4);

            qButton = new Button();
            qButton.Text = q + "   ▼";
            qButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            qButton.Dock = DockStyle.Top;
            qButton.Height = 40;
            qButton.FlatStyle = FlatStyle.Flat;
            qButton.Click += Toggle;

            answer = new Label();
            answer.Text = a;
            answer.Font = new Font("Segoe UI", 10);
            answer.AutoSize = true;
            answer.MaximumSize = new Size(820, 0);

            answerPanel = new Panel();
            answerPanel.Height = 0;
            answerPanel.Controls.Add(answer);

            anim = new Timer();
            anim.Interval = 15;
            anim.Tick += Animate;

            this.Controls.Add(answerPanel);
            this.Controls.Add(qButton);
        }

        private void Toggle(object s, EventArgs e)
        {
            expanded = !expanded;
            qButton.Text = expanded ? qButton.Text.Replace("▼", "▲") : qButton.Text.Replace("▲", "▼");
            target = expanded ? answer.Height + 16 : 0;
            anim.Start();
        }

        private void Animate(object s, EventArgs e)
        {
            if (expanded && answerPanel.Height < target)
            {
                answerPanel.Height += 15;
                if (answerPanel.Height >= target) anim.Stop();
            }
            else if (!expanded && answerPanel.Height > 0)
            {
                answerPanel.Height -= 15;
                if (answerPanel.Height <= 0) anim.Stop();
            }
        }
    }
}
