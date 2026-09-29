using System.Reflection;
using System.Windows.Forms;

namespace FamilyFeud
{
    public static class FormHelper
    {
        public static void EnableDoubleBuffer(Control root)
        {
            foreach (Control c in root.Controls)
            {
                typeof(Control).GetProperty("DoubleBuffered",
                    BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(c, true, null);

                if (c.HasChildren) EnableDoubleBuffer(c);
            }
        }
    }
}