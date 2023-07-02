using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
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
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace ArduinoCommunicator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Parameters parameters = new();
        SerialPort sPort = new SerialPort();

        string DataReceived = "";

        int commandCurrentIndex = 0;
        List<string> commandList = new List<string> { };
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this.parameters;
            foreach(string _comports in SerialPort.GetPortNames()) cbComPort.Items.Add(_comports);
        }

        private void tbReplies_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Connect();

                // Update status
                WriteStatusLabel($"Connected to {sPort.PortName}");
                WriteInfoLabel($"Ready to send orders");

                // Enable/Disable Buttons
                btnConnect.IsEnabled = false;
                btnDisconnect.IsEnabled = true;
                btnSend.IsEnabled = true;
            }
            catch (PortAlreadyOpenException ex)
            {
                WriteInfoLabel(ex.Message + " Please disconnect and try again.");                
            }
            catch (Exception ex)
            {
                WriteInfoLabel(ex.Message);
                btnConnect.IsEnabled = true;
                btnDisconnect.IsEnabled = false;
                btnSend.IsEnabled = false;
            }
        }

        private void Connect()
        {
            // Check if prerequirements are met
            if (sPort.IsOpen) throw new PortAlreadyOpenException();
                
            // New SerialPort object
            sPort = new SerialPort();

            // Apply saved settings
            sPort.BaudRate = Settings.Default.Baudrate;
            sPort.Parity = (Parity)Settings.Default.Parity;
            sPort.StopBits = (StopBits)Settings.Default.Stopbits;
            sPort.PortName = cbComPort.Text;

            // Add data reception event handler
            sPort.DataReceived += SPort_DataReceived;
            AddListeners();

            // Open Port
            sPort.Open();
        }

        private void SPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            DataReceived += sPort.ReadExisting();

            if(DataReceived.Contains(parameters.EndSignFromArduino))
            {
                string[] _message = DataReceived.Split(parameters.EndSignFromArduino);
                for(int i = 0; i < _message.Length - 1;  i++)
                {
                    OnDataReceivedRead(_message[i]);
                }
                DataReceived = _message[_message.Length - 1];
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                commandList = CreateListOfCommands(CreateListOfCommandsPreProcessing(tbCommands.Text));

                SendCommandsToArduino();

                return;
            }
            catch (CommandListEmptyException ex)
            {
                MessageBox.Show("Command list must not be empty!", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error); 
            }
            catch (Exception ex)
            {
                lblInfo.Content = ex.ToString();
            }
        }


        private void SendCommandsToArduino()
        {
            SendCommandsToArduino(this, new DataReceivedReadEventArgs());
        }

        private void SendCommandsToArduino(object sender, DataReceivedReadEventArgs e)
        {
            if (commandList.Count == 0) throw new CommandListEmptyException();
            
            if (commandCurrentIndex >= commandList.Count)
            {
                commandCurrentIndex = 0;
                commandList.Clear();
                WriteLogEntry("Finished.", DataReceivedReadEventArgs.Transfer.ComputerToComputer);
                WriteInfoLabel("Finished.");
            }
            else
            {
                try
                {
                    SendSingleCommandToArduino(commandList[commandCurrentIndex]);
                    commandCurrentIndex++;
                }
                catch(Exception ex)
                {
                    WriteLogEntry($"Could not send \"{commandList[commandCurrentIndex]}\": {ex.Message}", DataReceivedReadEventArgs.Transfer.ComputerToArduino_FAILED);
                    WriteInfoLabel($"Error: {ex}");
                }
            }
        }

        private void SendSingleCommandToArduino(string command, bool writeLogEntry = true)
        {
            if(sPort.IsOpen)
            {
                sPort.Write($"{command}{parameters.EndSignFromComputer}");

                if(writeLogEntry)
                {
                    WriteLogEntry($"Sent \"{command}\"", DataReceivedReadEventArgs.Transfer.ComputerToArduino);
                }
            }
        }

        private string CreateListOfCommandsPreProcessing(string command)
        {
            string rturn = command.Replace(".", "").Replace(" ", "");

            string strLoopTmp, strLoop;

            int factor;

            // Find loops and resolve
            int posOfBracketOpen, posOfBracketClose, posOfLineBegin;
            while(rturn.Contains("{"))
            {
                posOfBracketClose = rturn.IndexOf("}");
                if(posOfBracketClose < 0 )
                {
                    rturn = rturn.Replace("{", "");
                    break;
                }

                posOfBracketOpen = rturn.Substring(0, posOfBracketClose).LastIndexOf("{");
                if(posOfBracketOpen < 0 )
                {
                    rturn = rturn.Substring(0, posOfBracketOpen) + rturn.Substring(posOfBracketOpen + 1);
                    continue;
                }

                posOfLineBegin = rturn.Substring(0, posOfBracketOpen).LastIndexOf(Environment.NewLine);
                
                if (posOfLineBegin < 0) posOfLineBegin = 0;
                Debug.WriteLine((rturn.Substring(posOfLineBegin, posOfBracketOpen - posOfLineBegin)).Replace("{", "").Replace(Environment.NewLine, "").Trim());
                if(Int32.TryParse(rturn.Substring(posOfLineBegin, posOfBracketOpen - posOfLineBegin).Replace("{", "").Replace(Environment.NewLine, "").Trim(), out factor))
                {
                    strLoopTmp = $"{rturn.Substring(posOfBracketOpen, posOfBracketClose - posOfBracketOpen).Replace("}", "").Replace("{","")}";
                    strLoop = string.Empty;

                    for (int i = 0; i < factor; i++)
                    {
                        strLoop += $"{strLoopTmp}{Environment.NewLine}";
                    }
                    rturn = rturn.Replace(rturn.Substring(posOfLineBegin, posOfBracketClose - posOfLineBegin + 1), strLoop);
                }
                ;
            }
            
            
            return rturn;
        }
        
        private List<string> CreateListOfCommands(string commandText)
        {
            List<string> rturn = new List<string>();

            foreach (string cmd in commandText.Split(Environment.NewLine.ToCharArray()))
            {
                if (cmd.Trim() == "") continue;
                rturn.Add(cmd);
            }   

            return rturn;
        }

        private void WriteLogEntry(object sender, DataReceivedReadEventArgs e)
        {
            if(e != null)
            {
                string direction;
                if (e.Direction == DataReceivedReadEventArgs.Transfer.ArduinoToComputer) direction = "<---*";
                else if (e.Direction == DataReceivedReadEventArgs.Transfer.ComputerToArduino) direction = "--->*";
                else if (e.Direction == DataReceivedReadEventArgs.Transfer.ComputerToComputer) direction = ">---<";
                else if (e.Direction == DataReceivedReadEventArgs.Transfer.ComputerToArduino_FAILED) direction = "-X->*";
                else direction = ">???<";

                tbLog.Text = ($"[{e.DateTime.ToLongTimeString()}.{String.Format("{0:D3}", e.DateTime.Millisecond)}]" +
                        $"\t{direction}" +
                        $"\t{e.Message}" +
                        $"{Environment.NewLine}{tbLog.Text}");
            }
        }

        private void WriteLogEntry(string text, DataReceivedReadEventArgs.Transfer direction)
        {
            WriteLogEntry(this, new DataReceivedReadEventArgs(text, direction));
        }

        private void WriteStatusLabel(string text)
        {
            // Dispatcher.Invoke(() => lblStatus.Content = text);
            lblStatus.Content = text;
        }

        private void WriteInfoLabel(string text)
        {
            // Dispatcher.Invoke(() => lblInfo.Content = text);
            lblInfo.Content = text;
        }

        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Close connection
                Disconnect();

                // Update status
                WriteInfoLabel("Disconnected");
                WriteStatusLabel("Not connected.");

                // Enable/Disable buttons
                btnConnect.IsEnabled = true;
                btnDisconnect.IsEnabled = false;
                btnSend.IsEnabled = false;
            }
            catch (Exception ex)
            {
                WriteInfoLabel(ex.Message);
            }
        }

        private void Disconnect()
        {
            if (sPort.IsOpen) sPort.Close();


        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            Options FRMoptions = new Options(ref parameters);

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

        public class DataReceivedReadEventArgs : EventArgs
        {
            public enum Transfer
            {
                ArduinoToComputer,
                ComputerToArduino,
                ComputerToComputer,
                ComputerToArduino_FAILED
            }

            // Properties
            public string Message { get; set; }
            public DateTime DateTime { get; set; }
            public Transfer Direction { get; set; }

            // Constructors
            public DataReceivedReadEventArgs(string message, Transfer direction) : this(message)
            {
                Direction = direction;
            }

            public DataReceivedReadEventArgs(string message) : this()
            {
                Message = message;
            }

            public DataReceivedReadEventArgs()
            {
                Message = "";
                DateTime = DateTime.Now;
                Direction = Transfer.ArduinoToComputer;
            }
        }

        public event EventHandler<DataReceivedReadEventArgs> DataReceivedRead;

        protected virtual void OnDataReceivedRead(string Message)
        {
            Dispatcher.Invoke(() =>
            {
                DataReceivedRead?.Invoke(this, new DataReceivedReadEventArgs(Message));
            });
        }

        private void AddListeners()
        {
            if(DataReceivedRead != null) RemoveListeners();

            DataReceivedRead += WriteLogEntry;
            DataReceivedRead += SendCommandsToArduino;
        }

        private void RemoveListeners()
        {
            if (DataReceivedRead == null) return;

            DataReceivedRead -= WriteLogEntry;
            DataReceivedRead -= SendCommandsToArduino;

        }

        private void btnLoadCommands_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();

            if(openFileDialog.ShowDialog() == true 
                && (tbCommands.Text.Trim() == "" 
                    || MessageBox.Show("Do you want to overwrite your current commands?", "Overwrite commands?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes))
            {
                try
                {
                    string varifyFile;
                    string fileContent;
                    using (StreamReader fileReader = new StreamReader(openFileDialog.FileName))
                    {
                        varifyFile = fileReader?.ReadLine();
                        if (varifyFile == null || varifyFile.Trim() != "#ArduinoCommunicatorFile") 
                        {
                            throw new NoValidCommandFileException(openFileDialog.FileName); 
                        }

                        fileContent = fileReader.ReadToEnd();
                    }
                    tbCommands.Text = fileContent;


                }
                catch(NoValidCommandFileException ex)
                {
                    MessageBox.Show(ex.Message, "Error loading file", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error loading file", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
        }

        private void btnSaveCommands_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.AddExtension = true;
            saveFileDialog.CheckFileExists = true;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "Textfiles|*.txt|All Files|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter fileWriter = new(saveFileDialog.FileName))
                    {
                        fileWriter.WriteLine("#ArduinoCommunicatorFile");
                        fileWriter.Write(tbCommands.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error saving file", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }

        }

        private void btnSaveLog_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.AddExtension = true;
            saveFileDialog.CheckFileExists = true;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.DefaultExt = "log";
            saveFileDialog.Filter = "Log-Files|*.log|Textfiles|*.txt|All Files|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter fileWriter = new(saveFileDialog.FileName))
                    {
                        fileWriter.WriteLine("#ArduinoCommunicatorFile");
                        fileWriter.Write(tbLog.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error loading file", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }

        }

    }
}

