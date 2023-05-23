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
using System.IO.Ports;
using System.Runtime.CompilerServices;
using ArduinoCommunicator.Properties;

namespace ArduinoCommunicator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Parameters parameters = new Parameters();
        SerialPort sPort = new SerialPort();

        string DataReceived = "";
        char EndSignFromArduino = (char)Settings.Default.EndsignArd;
        char EndSignFromComputer = (char)Settings.Default.EndsignCom;

        int commandOn = 0;
        string[] _commands;

        List<string> _commandsList = new List<string> { };
        
        public MainWindow()
        {
            InitializeComponent();
            //this.DataContext = Parameters;
            foreach(string _comports in SerialPort.GetPortNames()) cbComPort.Items.Add(_comports);
        }

        private void tbReplies_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(sPort.IsOpen) sPort.Close();
                sPort = new SerialPort();
                sPort.BaudRate = Settings.Default.Baudrate;
                sPort.Parity = (Parity)Settings.Default.Parity;
                sPort.StopBits = (StopBits)Settings.Default.Stopbits;

                sPort.PortName = cbComPort.Text;
                sPort.Open();
                sPort.DataReceived += SPort_DataReceived;
                WriteStatusLabel($"Connected to {sPort.PortName}");
                lblInfo.Content = $"Ready to send orders";
                btnDisconnect.IsEnabled = true;
                btnSend.IsEnabled = true;
                btnConnect.IsEnabled = false;
            }
            catch (Exception ex)
            {
                if (sPort.IsOpen) sPort.Close();
                WriteStatusLabel($"Not connected!");
                lblInfo.Content = ex.ToString();
                btnDisconnect.IsEnabled = false;
                btnSend.IsEnabled = false;
                btnConnect.IsEnabled = true;
            }
        }

        private void SPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            DataReceived += sPort.ReadExisting();
            if(DataReceived.Contains(EndSignFromArduino))
            {
                _commands = DataReceived.Split(EndSignFromArduino);
                WriteConsole($"Recvd.:\t{_commands[0]}");
                //Dispatcher.Invoke(() => tbReplies.Text += $"[{DateTime.Now.ToLongTimeString()}.{DateTime.Now.Millisecond}]\tRecvd.:\t{_commands[0]}{Environment.NewLine}");
                WorkOffCommands();
                
                    
                    

            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbCommands.Text.Trim().Length == 0) return;
                _commandsList.Clear();
                _commandsList = ReadCommands();
                WorkOffCommands();
            }
            catch (Exception ex)
            {
                lblInfo.Content = ex.ToString();
            }
        }

        private void WorkOffCommands()
        {
            if(commandOn == _commandsList.Count)
            {
                WriteConsole($"Completed.{Environment.NewLine}");
                commandOn = 0;
                WriteInfoLabel("Finished.");
                return;
            }
            WriteInfoLabel($"Command list working off: {_commandsList[commandOn]}");
            sPort.Write($"{_commandsList[commandOn]}{EndSignFromComputer}");
            WriteConsole($"Sent:\t{_commandsList[commandOn]}");
            commandOn++;

        }

        private List<string> ReadCommands()
        {
            List<string> rturn = new List<string>();
            foreach(string cmd in tbCommands.Text.Split(Environment.NewLine.ToCharArray()))
                if(cmd.Trim() != "") rturn.Add(cmd);
            return rturn;

        }

        private void WriteConsole(string text)
        {
            Dispatcher.Invoke(() => tbReplies.Text += $"[{DateTime.Now.ToLongTimeString()}.{DateTime.Now.Millisecond}]\t{text}{Environment.NewLine}");
        }

        private void WriteStatusLabel(string text)
        {
            Dispatcher.Invoke(() => lblStatus.Content = text);
        }

        private void WriteInfoLabel(string text)
        {
            Dispatcher.Invoke(() => lblInfo.Content = text);
        }

        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            if(sPort.IsOpen) sPort.Close();
            btnConnect.IsEnabled = true;
            btnDisconnect.IsEnabled = false;
            btnSend.IsEnabled = false;
            WriteInfoLabel("Disconnected");
            WriteStatusLabel("Not connected.");
        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            Options FRMoptions = new Options();

            bool? optionsResult = FRMoptions.ShowDialog();

            switch(optionsResult)
            {
                case true:  // Ueer clicked "Apply" in options window
  
                    break;

                case false: // User clicked "Cancel" in options window
  
                    break;

                default:    // Something else

                    break;
            }
            

        }


    }
}
