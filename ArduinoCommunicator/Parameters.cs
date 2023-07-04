using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Media;
using System.IO;
using ArduinoCommunicator.Properties;
using System.Windows;

namespace ArduinoCommunicator
{
    public class Parameters
    {
        #region Constructors
        public Parameters()
        {
            LoadSettings();
        }
        #endregion


        #region Connection

            internal int Baudrate { get; set; }

            internal Parity Parity { get; set; }

            internal StopBits StopBits { get; set; }

            internal char EndSignFromArduino { get; set; }

            internal char EndSignFromComputer { get; set; }

        #endregion


        #region General Functions
        internal int LoadSettings()
        {
            int rturn = 0;

            Baudrate = Settings.Default.Baudrate;
            Parity = (Parity)Settings.Default.Parity;
            StopBits = (StopBits)Settings.Default.Stopbits;
            EndSignFromArduino = (char)Settings.Default.EndsignArd;
            EndSignFromComputer = (char)Settings.Default.EndsignCom;
            ChangeTheme(Settings.Default.Theme);
            return rturn;
        }

        internal int SaveSettings()
        {
            int rturn = 0;

            Settings.Default.Baudrate = Baudrate;
            Settings.Default.Parity = (byte)Parity;
            Settings.Default.Stopbits = (byte)StopBits;
            Settings.Default.EndsignArd = (byte)EndSignFromArduino;
            Settings.Default.EndsignCom = (byte)EndSignFromComputer;


            return rturn;
        }
        #endregion


        #region Styles

        private Dictionary<string, Uri> ThemesNamesUriDict = new Dictionary<string, Uri>()
        {
            {"Standard", new Uri(".\\Themes\\Default.xaml", UriKind.RelativeOrAbsolute) },
            {"Dark", new Uri(".\\Themes\\Dark.xaml", UriKind.RelativeOrAbsolute) },
            {"Bright", new Uri(".\\Themes\\Bright.xaml", UriKind.RelativeOrAbsolute) },
            {"Blue", new Uri(".\\Themes\\Blue.xaml", UriKind.RelativeOrAbsolute) }
        };

        public List<string> GetThemeNames()
        {
            return new List<string>(ThemesNamesUriDict.Keys);
        }

        public void ChangeTheme(string themeName)
        {
            if (!ThemesNamesUriDict.ContainsKey(themeName)) throw new ThemeNotFoundException(themeName);
                
            System.Windows.Application.Current.Resources.MergedDictionaries[0].Source = ThemesNamesUriDict[themeName];


        }
        #endregion
    }


}