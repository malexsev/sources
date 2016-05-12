using System.Drawing;

namespace Cure.WebAdmin.Controls
{

    public partial class ResultBox : System.Web.UI.UserControl
    {
        public string TextGreen
        {
            get { return uxResultLabel.Text; }
            set
            {
                uxResultLabel.Text = value;
                uxResultLabel.ForeColor = Color.Green;
            }
        }

        public string TextRed
        {
            get { return uxResultLabel.Text; }
            set
            {
                uxResultLabel.Text = value;
                uxResultLabel.ForeColor = Color.Red;
            }
        }
    }
}