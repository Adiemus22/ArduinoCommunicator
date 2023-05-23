using ArduinoCommunicator.Properties;
using System;
using System.Collections.Generic;
using System.IO.Ports;
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
using System.Windows.Shapes;

namespace ArduinoCommunicator
{
    /// <summary>
    /// Interaktionslogik für Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        //Parameters parameters = new Parameters();
        public Options()
        {
            InitializeComponent();
            //this.DataContext = parameters;
            foreach (string i in Enum.GetNames(typeof(Parity))) cbParity.Items.Add(i);
            foreach (string i in Enum.GetNames(typeof(StopBits))) cbStopBits.Items.Add(i);
            cbBaudRate.Items.Add(110);
            cbBaudRate.Items.Add(300);
            cbBaudRate.Items.Add(1200);
            cbBaudRate.Items.Add(2400);
            cbBaudRate.Items.Add(4800);
            cbBaudRate.Items.Add(9600);
            cbBaudRate.Items.Add(19200);
            cbBaudRate.Items.Add(38400);
            cbBaudRate.Items.Add(57600);
            cbBaudRate.Items.Add(115200);
            for(int i = 0; i < 256; i++)
            {
                cbEndSignArd.Items.Add("[" + i.ToString() + "]\t" + (char)i);
                cbEndSignCom.Items.Add("[" + i.ToString() + "]\t" + (char)i);
            }
            for(int i = 0; i < Parameters.Themes.AllThemes.Count; i++)
            {
                cbTheme.Items.Add(Parameters.Themes.AllThemes[i].colName);
            }


            cbBaudRate.Text = Settings.Default.Baudrate.ToString();
            cbParity.SelectedIndex = Settings.Default.Parity;
            cbStopBits.SelectedIndex = Settings.Default.Stopbits;
            cbEndSignArd.Text = "[" + Settings.Default.EndsignArd.ToString() + "]\t" + (char)(Settings.Default.EndsignArd);
            cbEndSignCom.Text = "[" + Settings.Default.EndsignCom.ToString() + "]\t" + (char)(Settings.Default.EndsignCom);
            cbTheme.Text = Settings.Default.Theme;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.Baudrate = Int32.Parse(cbBaudRate.Text);
            Settings.Default.Parity = (byte)cbParity.SelectedIndex;
            Settings.Default.Stopbits = (byte)cbStopBits.SelectedIndex;
            Settings.Default.EndsignArd = (byte)(cbEndSignArd.Text[cbEndSignArd.Text.Length - 1]);
            Settings.Default.EndsignCom = (byte)(cbEndSignCom.Text[cbEndSignCom.Text.Length - 1]);
            Settings.Default.Theme = cbTheme.Text.Trim();
            Settings.Default.Save();

            Parameters.Themes.ChangeTheme(Settings.Default.Theme);

            DialogResult = true;
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Do you really want to reset all the parameters to default value?", "Arduino Communicator", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                cbBaudRate.Text = Settings.Default.defaultBaudrate.ToString();
                cbParity.Text = Enum.GetName(typeof(Parity), Settings.Default.defaultParity);
                cbStopBits.Text = Enum.GetName(typeof(StopBits), Settings.Default.Stopbits);
                cbEndSignArd.Text = "[" + Settings.Default.defaultEndsignArd.ToString() + "]\t" + (char)(Settings.Default.defaultEndsignArd);
                cbEndSignCom.Text = "[" + Settings.Default.defaultEndsignCom.ToString() + "]\t" + (char)(Settings.Default.defaultEndsignCom);
                cbTheme.Text = Settings.Default.defaultTheme.ToString();
            }

        }
    }
}
