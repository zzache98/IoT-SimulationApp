using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.Temperature.Device.Models;

namespace WPF.Temperature.Device.SetupWizard
{

    public partial class SetupWizard_YourInformation : Page
    {
        private YourInformation _yourInformation;

        public SetupWizard_YourInformation(ref YourInformation yourInformation)
        {
            InitializeComponent();
            _yourInformation = yourInformation;
            tb_Email.Text = _yourInformation.Email;
            tb_FirstName.Text = _yourInformation.FirstName;
            tbLastName.Text = _yourInformation.LastName;
        }

        private void tb_Email_KeyUp(object sender, KeyEventArgs e)
        {
            _yourInformation.Email = tb_Email.Text;
        }

        private void tb_FirstName_KeyUp(object sender, KeyEventArgs e)
        {
            _yourInformation.FirstName = tb_FirstName.Text;
        }

        private void tbLastName_KeyUp(object sender, KeyEventArgs e)
        {
            _yourInformation.LastName = tbLastName.Text;
        }
    }
}
