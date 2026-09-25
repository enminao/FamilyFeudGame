using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FamilyFeud
{
    internal class TeamPrompt
    {
        public static int Ask(IWin32Window owner)
        {
            using (var f = new Form())
            {
                f.Text = "Family Feud";
                f.FormBorderStyle = FormBorderStyle.FixedDialog;
                f.StartPosition = FormStartPosition.CenterParent;
                f.ControlBox = false;          // no X button, so a team must be picked
                f.ShowInTaskbar = false;
                f.ClientSize = new Size(360, 140);

                var lbl = new Label
                {
                    Text = "Which team will go first?",
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Top,
                    Height = 70,
                    Font = new Font("Comic Sans MS", 14, FontStyle.Bold)
                };

                var btnTeam1 = new Button
                {
                    Text = "Team 1",
                    DialogResult = DialogResult.Yes,   // Yes = Team 1
                    Size = new Size(140, 40),
                    Location = new Point(190, 80)      // moved to the right
                };

                var btnTeam2 = new Button
                {
                    Text = "Team 2",
                    DialogResult = DialogResult.No,    // No = Team 2
                    Size = new Size(140, 40),
                    Location = new Point(30, 80)       // moved to the left
                };



                f.Controls.Add(lbl);
                f.Controls.Add(btnTeam1);
                f.Controls.Add(btnTeam2);

                return f.ShowDialog(owner) == DialogResult.Yes ? 1 : 2;
            }
        }

    }
}
