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
        public Parameters parameters;
        public Options(ref Parameters param)
        {
            InitializeComponent();
            parameters = param;
            // this.DataContext = parameters;
            RefreshGUIToolValues();
        }

        private void RefreshGUIToolValues()
        {
            // Clear all items
            cbParity.Items.Clear();
            cbStopBits.Items.Clear();
            cbBaudRate.Items.Clear();
            cbResponseEndChar.Items.Clear();
            cbCommandEndChar.Items.Clear();
            cbTheme.Items.Clear();

            foreach (string theme in parameters.GetThemeNames())
            {
                cbTheme.Items.Add(theme);
            }

            // Add available Parity and StopBits values to comboboxes
            foreach (string i in Enum.GetNames(typeof(Parity))) cbParity.Items.Add(i);
            foreach (string i in Enum.GetNames(typeof(StopBits))) cbStopBits.Items.Add(i);

            // Add typical Baudrates to combobox
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

            // Add possible end chars to comboboxes
            for (int i = 0; i < 256; i++)
            {
                cbResponseEndChar.Items.Add("[" + i.ToString() + "]\t" + (char)i);
                cbCommandEndChar.Items.Add("[" + i.ToString() + "]\t" + (char)i);
            }


            // Set default values
            cbBaudRate.Text = Settings.Default.Baudrate.ToString();
            cbParity.SelectedIndex = Settings.Default.Parity;
            cbStopBits.SelectedIndex = Settings.Default.Stopbits;
            cbResponseEndChar.Text = "[" + Settings.Default.EndsignArd.ToString() + "]\t" + (char)(Settings.Default.EndsignArd);
            cbCommandEndChar.Text = "[" + Settings.Default.EndsignCom.ToString() + "]\t" + (char)(Settings.Default.EndsignCom);
            cbTheme.Text = Settings.Default.Theme;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                parameters.ChangeTheme(cbTheme.Text);
            }
            catch(Exception ex)
            { 
                MessageBox.Show(ex.Message, "Error loading theme", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Settings.Default.Baudrate = Int32.Parse(cbBaudRate.Text);
            Settings.Default.Parity = (byte)cbParity.SelectedIndex;
            Settings.Default.Stopbits = (byte)cbStopBits.SelectedIndex;
            Settings.Default.EndsignArd = (byte)(cbResponseEndChar.Text[cbResponseEndChar.Text.Length - 1]);
            Settings.Default.EndsignCom = (byte)(cbCommandEndChar.Text[cbCommandEndChar.Text.Length - 1]);
            Settings.Default.Theme = cbTheme.Text.Trim();
            Settings.Default.Save();

            DialogResult = true;
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Do you really want to reset all the parameters to default value?", "Arduino Communicator", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ResetToDefaultValues();
            }

        }

        private void ResetToDefaultValues()
        {
            cbBaudRate.Text = Settings.Default.default_Baudrate.ToString();
            cbParity.Text = Enum.GetName(typeof(Parity), Settings.Default.default_Parity);
            cbStopBits.Text = Enum.GetName(typeof(StopBits), Settings.Default.Stopbits);
            cbResponseEndChar.Text = "[" + Settings.Default.default_EndsignArd.ToString() + "]\t" + (char)(Settings.Default.default_EndsignArd);
            cbCommandEndChar.Text = "[" + Settings.Default.default_EndsignCom.ToString() + "]\t" + (char)(Settings.Default.default_EndsignCom);
            cbTheme.Text = Settings.Default.default_Theme.ToString();
        }
    }
}
