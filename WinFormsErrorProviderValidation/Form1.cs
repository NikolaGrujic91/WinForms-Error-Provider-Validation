using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinFormsErrorProviderValidation
{
    public partial class Form1 : Form
    {
        #region Constructors

        public Form1()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Events

        private void buttonOk_Click(object sender, EventArgs e)
        {
            var invalidInput = false;

            foreach (Control ctrl in this.groupBox.Controls)
            {
                if (this.errorProvider.GetError(ctrl) == string.Empty)
                {
                    continue;
                }

                invalidInput = true;
                break;
            }

            if (invalidInput)
            {
                MessageBox.Show("You still have invalid input.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.Close();
            }
        }

        private void textBox_Validating(object sender, CancelEventArgs e)
        {
            var ctrl = (Control)sender;
            this.errorProvider.SetError(ctrl, ctrl.Text == string.Empty ? "You must enter a first and last name." : string.Empty);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"\S+@\S+\.\S+");
            var ctrl = (Control)sender;
            this.errorProvider.SetError(ctrl, regex.IsMatch(ctrl.Text) ? string.Empty : "Not a valid email.");
        }

        #endregion
    }
}
