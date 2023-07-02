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

namespace ArduinoCommunicator
{
    enum Themes
    {
        Dark,
        Bright,
        Blue,
        Rainbow
    }

    public class Parameters
    {
        #region Constructors
        public Parameters()
        {
            //colBackGround1 = new SolidColorBrush(new System.Windows.Media.Color());
            //colBackGround2 = new SolidColorBrush(new System.Windows.Media.Color());
            //colBackGround3 = new SolidColorBrush(new System.Windows.Media.Color());
            //colBackGround4 = new SolidColorBrush(new System.Windows.Media.Color());
            //colForeGround1 = new SolidColorBrush(new System.Windows.Media.Color());
            //colForeGround2 = new SolidColorBrush(new System.Windows.Media.Color());
            //colForeGround3 = new SolidColorBrush(new System.Windows.Media.Color());
            //colForeGround4 = new SolidColorBrush(new System.Windows.Media.Color());
            //colButton1 = new SolidColorBrush(new System.Windows.Media.Color());
            //colButton2 = new SolidColorBrush(new System.Windows.Media.Color());
            //colButton3 = new SolidColorBrush(new System.Windows.Media.Color());
            //colButton4 = new SolidColorBrush(new System.Windows.Media.Color());
            //colAccent1 = new SolidColorBrush(new System.Windows.Media.Color());
            //colAccent2 = new SolidColorBrush(new System.Windows.Media.Color());
            //colAccent3 = new SolidColorBrush(new System.Windows.Media.Color());
            //colAccent4 = new SolidColorBrush(new System.Windows.Media.Color());
            //colFont1 = new SolidColorBrush(new System.Windows.Media.Color());
            //colFont2 = new SolidColorBrush(new System.Windows.Media.Color());
            //colFont3 = new SolidColorBrush(new System.Windows.Media.Color());
            //colFont4 = new SolidColorBrush(new System.Windows.Media.Color());
            //colComboBox1 = new SolidColorBrush(new System.Windows.Media.Color());
            //colComboBox2 = new SolidColorBrush(new System.Windows.Media.Color());
            //colComboBox3 = new SolidColorBrush(new System.Windows.Media.Color());
            //colComboBox4 = new SolidColorBrush(new System.Windows.Media.Color());
            colBackGroundMain = new SolidColorBrush(new System.Windows.Media.Color());
            colBackGroundSub = new SolidColorBrush(new System.Windows.Media.Color());
            colBackGroundSubSub = new SolidColorBrush(new System.Windows.Media.Color());
            colButtonBackground = new SolidColorBrush(new System.Windows.Media.Color());
            colButtonForeground = new SolidColorBrush(new System.Windows.Media.Color());
            colButtonBorder = new SolidColorBrush(new System.Windows.Media.Color());
            colButtonDisabledBackground = new SolidColorBrush(new System.Windows.Media.Color());
            colButtonDisabledForeground = new SolidColorBrush(new System.Windows.Media.Color());
            colButtonDisabledBorder = new SolidColorBrush(new System.Windows.Media.Color());
            colButtonMouseoverBackground = new SolidColorBrush(new System.Windows.Media.Color());
            colButtonMouseoverForeground = new SolidColorBrush(new System.Windows.Media.Color());
            colButtonMouseoverBorder = new SolidColorBrush(new System.Windows.Media.Color());
            colButtonPressedBackground = new SolidColorBrush(new System.Windows.Media.Color());
            colButtonPressedForeground = new SolidColorBrush(new System.Windows.Media.Color());
            colButtonPressedBorder = new SolidColorBrush(new System.Windows.Media.Color());
            colIconBackground = new SolidColorBrush(new System.Windows.Media.Color());
            colIconForeground = new SolidColorBrush(new System.Windows.Media.Color());
            colIconBorder = new SolidColorBrush(new System.Windows.Media.Color());
            colIconDisabledBackground = new SolidColorBrush(new System.Windows.Media.Color());
            colIconDisabledForeground = new SolidColorBrush(new System.Windows.Media.Color());
            colIconDisabledBorder = new SolidColorBrush(new System.Windows.Media.Color());
            colIconMouseoverBackground = new SolidColorBrush(new System.Windows.Media.Color());
            colIconMouseoverForeground = new SolidColorBrush(new System.Windows.Media.Color());
            colIconMouseoverBorder = new SolidColorBrush(new System.Windows.Media.Color());
            colIconPressedBackground = new SolidColorBrush(new System.Windows.Media.Color());
            colIconPressedForeground = new SolidColorBrush(new System.Windows.Media.Color());
            colIconPressedBorder = new SolidColorBrush(new System.Windows.Media.Color());
            colTextblockBackground = new SolidColorBrush(new System.Windows.Media.Color());
            colTextblockForeground = new SolidColorBrush(new System.Windows.Media.Color());
            colTextblockBorder = new SolidColorBrush(new System.Windows.Media.Color());
            colLabelBackground = new SolidColorBrush(new System.Windows.Media.Color());
            colLabelForeground = new SolidColorBrush(new System.Windows.Media.Color());
            colLabelBorder = new SolidColorBrush(new System.Windows.Media.Color());

            colThemeName = "Unloaded";

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


        #region Themes

        public SolidColorBrush colBackGroundMain { get; private set; }
        public SolidColorBrush colBackGroundSub { get; private set; }
        public SolidColorBrush colBackGroundSubSub { get; private set; }



        public SolidColorBrush colButtonBackground { get; private set; }
        public SolidColorBrush colButtonForeground { get; private set; }
        public SolidColorBrush colButtonBorder { get; private set; }

        public SolidColorBrush colButtonDisabledBackground { get; private set; }
        public SolidColorBrush colButtonDisabledForeground { get; private set; }
        public SolidColorBrush colButtonDisabledBorder { get; private set; }

        public SolidColorBrush colButtonMouseoverBackground { get; private set; }
        public SolidColorBrush colButtonMouseoverForeground { get; private set; }
        public SolidColorBrush colButtonMouseoverBorder { get; private set; }

        public SolidColorBrush colButtonPressedBackground { get; private set; }
        public SolidColorBrush colButtonPressedForeground { get; private set; }
        public SolidColorBrush colButtonPressedBorder { get; private set; }



        public SolidColorBrush colIconBackground { get; private set; }
        public SolidColorBrush colIconForeground { get; private set; }
        public SolidColorBrush colIconBorder { get; private set; }

        public SolidColorBrush colIconDisabledBackground { get; private set; }
        public SolidColorBrush colIconDisabledForeground { get; private set; }
        public SolidColorBrush colIconDisabledBorder { get; private set; }

        public SolidColorBrush colIconMouseoverBackground { get; private set; }
        public SolidColorBrush colIconMouseoverForeground { get; private set; }
        public SolidColorBrush colIconMouseoverBorder { get; private set; }

        public SolidColorBrush colIconPressedBackground { get; private set; }
        public SolidColorBrush colIconPressedForeground { get; private set; }
        public SolidColorBrush colIconPressedBorder { get; private set; }


        public SolidColorBrush colTextblockBackground { get; private set; }
        public SolidColorBrush colTextblockForeground { get; private set; }
        public SolidColorBrush colTextblockBorder { get; private set; }


        public SolidColorBrush colLabelBackground { get; private set; }
        public SolidColorBrush colLabelForeground { get; private set; }
        public SolidColorBrush colLabelBorder { get; private set; }


        public string colThemeName { get; private set; }
            //public SolidColorBrush Transparent { get; private set; }
            //public SolidColorBrush colBackGround1 { get; private set; }
            //public SolidColorBrush colBackGround2 { get; private set; }
            //public SolidColorBrush colBackGround3 { get; private set; }
            //public SolidColorBrush colBackGround4 { get; private set; }
            //public SolidColorBrush colForeGround1 { get; private set; }
            //public SolidColorBrush colForeGround2 { get; private set; }
            //public SolidColorBrush colForeGround3 { get; private set; }
            //public SolidColorBrush colForeGround4 { get; private set; }
            //public SolidColorBrush colButton1 { get; private set; }
            //public SolidColorBrush colButton2 { get; private set; }
            //public SolidColorBrush colButton3 { get; private set; }
            //public SolidColorBrush colButton4 { get; private set; }
            //public SolidColorBrush colAccent1 { get; private set; }
            //public SolidColorBrush colAccent2 { get; private set; }
            //public SolidColorBrush colAccent3 { get; private set; }
            //public SolidColorBrush colAccent4 { get; private set; }
            //public SolidColorBrush colFont1 { get; private set; }
            //public SolidColorBrush colFont2 { get; private set; }
            //public SolidColorBrush colFont3 { get; private set; }
            //public SolidColorBrush colFont4 { get; private set; }
            //public SolidColorBrush colComboBox1 { get; private set; }
            //public SolidColorBrush colComboBox2 { get; private set; }
            //public SolidColorBrush colComboBox3 { get; private set; }
            //public SolidColorBrush colComboBox4 { get; private set; }



        internal int ChangeTheme(string themeName)
            {
                StreamReader reader = new StreamReader("Themes.txt");
                bool isTheme = false;           // indicates if theme has been found
                string[] lineParts;             // left and right part of read line (separated by '=')
                int rturn = -1;                 // function return value

                // First, try to find right theme (Line: "Theme Name = Dark")
                while (!reader.EndOfStream && !isTheme)
                {
                    lineParts = reader.ReadLine().Split('=');
                    if (lineParts.Length < 2) { continue; }                     // This line does not contain '='
                    if (lineParts[0].Trim().StartsWith('#')) { continue; }      // This is a commentary line if it starts with '#'

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "themename"
                        && lineParts[1].Replace(" ", "").Trim().ToLower() == themeName.Replace(" ", "").Trim().ToLower())   // Check if the line indicates beginning of wanted theme properties
                    {
                        rturn = 0;          // Theme successfully found
                        isTheme = true;     // if so, indicate that theme was found
                    }
                }

                // If theme was found, check the next lines for the colour parameters
                while (!reader.EndOfStream && isTheme)
                {
                    lineParts = reader.ReadLine().Split('=');
                    if (lineParts.Length < 2) { continue; }                     // This line does not contain '='
                    if (lineParts[0].Trim().StartsWith('#')) { continue; }      // This is a commentary line if it starts with '#'
                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "themename") break;    // Next theme starting, so current one is done -> Exit while

                    // Search for the color label and its assignment in the Themes.txt, and assign casted value

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colbackgroundmain")
                    {
                        colBackGroundMain.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colbackgroundsub")
                    {
                        colBackGroundSub.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colbackgroundsubsub")
                    {
                        colBackGroundSubSub.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colbuttonbackground")
                    {
                        colButtonBackground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colbuttonforeground")
                    {
                        colButtonForeground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colbuttonborder")
                    {
                        colButtonBorder.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colbuttondisabledbackground")
                    {
                        colButtonDisabledBackground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colbuttondisabledforeground")
                    {
                        colButtonDisabledForeground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colbuttondisabledborder")
                    {
                        colButtonDisabledBorder.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colbuttonmouseoverbackground")
                    {
                        colButtonMouseoverBackground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colbuttonmouseoverforeground")
                    {
                        colButtonMouseoverForeground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colbuttonmouseoverborder")
                    {
                        colButtonMouseoverBorder.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colbuttonpressedbackground")
                    {
                        colButtonPressedBackground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colbuttonpressedforeground")
                    {
                        colButtonPressedForeground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colbuttonpressedborder")
                    {
                        colButtonPressedBorder.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "coliconbackground")
                    {
                        colIconBackground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "coliconforeground")
                    {
                        colIconForeground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "coliconborder")
                    {
                        colIconBorder.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colicondisabledbackground")
                    {
                        colIconDisabledBackground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colicondisabledforeground")
                    {
                        colIconDisabledForeground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "colicondisabledborder")
                    {
                        colIconDisabledBorder.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "coliconmouseoverbackground")
                    {
                        colIconMouseoverBackground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "coliconmouseoverforeground")
                    {
                        colIconMouseoverForeground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "coliconmouseoverborder")
                    {
                        colIconMouseoverBorder.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "coliconpressedbackground")
                    {
                        colIconPressedBackground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "coliconpressedforeground")
                    {
                        colIconPressedForeground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "coliconpressedborder")
                    {
                        colIconPressedBorder.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "coltextblockbackground")
                    {
                        colTextblockBackground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "coltextblockforeground")
                    {
                        colTextblockForeground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "coltextblockborder")
                    {
                        colTextblockBorder.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "collabelbackground")
                    {
                        colLabelBackground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "collabelforeground")
                    {
                        colLabelForeground.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }

                    if (lineParts[0].Replace(" ", "").Trim().ToLower() == "collabelborder")
                    {
                        colLabelBorder.Color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(lineParts[1].Replace(" ", "").Trim());
                        continue;
                    }



                }

            // return value;
            reader.Close();
                return rturn;

            }

            internal List<string> GetAvailableThemes()
            {
                List<string> rturn = new();
                string[] lineParts;             // left and right part of read line (separated by '=')

                using (StreamReader reader = new StreamReader("Themes.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        lineParts = reader.ReadLine().Split('=');
                        if (lineParts.Length < 2) { continue; }                     // This line does not contain '='
                        if (lineParts[0].Trim().StartsWith('#')) { continue; }      // This is a commentary line if it starts with '#'

                        if (lineParts[0].Replace(" ", "").Trim().ToLower() == "themename")  // if line describes theme name, add it to list
                        {
                            rturn.Add(lineParts[1].Trim());
                        }
                    }

                    return rturn;       // return list
                }


            }


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

            Settings.Default.Theme = colThemeName?.Trim();

            return rturn;
        }
        #endregion
    }


}