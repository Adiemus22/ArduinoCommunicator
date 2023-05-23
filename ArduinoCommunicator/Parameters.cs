using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoCommunicator
{
    enum Themes
    {
        Dark,
        Bright,
        Blue,
        Rainbow
    }


    static internal class Parameters
    {
        #region Constructors
        static Parameters()
        {
            


            Themes.colThemeNames = new List<string>();
            for(int i = 0; i < Themes.AllThemes.Count; i++)
            {
                Themes.colThemeNames.Add(Themes.AllThemes[i].colName);
            }
        }
        #endregion


        #region Connection
        static internal class Connection
        {
            static Connection()
            {

            }

            static internal int Baudrate { get; set; }

            static internal Parity Parity { get; set; }

            static internal StopBits StopBits { get; set; }

            static internal char EndSignFromArduino { get; set; }
            static internal char EndSignFromComputer { get; set; }
        }

        #endregion


        #region Themes
        static public class Themes
        {
            internal static List<string> colThemeNames { get; set; }

            /// <summary>
            /// Read-Only Theme Set.
            /// </summary>
            internal class Colours
            {
                //Constructor

                public Colours(string colName, Color colBackGround1, Color colBackGround2, Color colBackGround3, Color colForeGround1, Color colForeGround2, Color colForeGround3, Color colAccent1, Color colAccent2, Color colAccent3, Color colFont1, Color colFont2, Color colFont3)
                {
                    this.colName = colName;
                    this.colBackGround1 = colBackGround1;
                    this.colBackGround2 = colBackGround2;
                    this.colBackGround3 = colBackGround3;
                    this.colForeGround1 = colForeGround1;
                    this.colForeGround2 = colForeGround2;
                    this.colForeGround3 = colForeGround3;
                    this.colAccent1 = colAccent1;
                    this.colAccent2 = colAccent2;
                    this.colAccent3 = colAccent3;
                    this.colFont1 = colFont1;
                    this.colFont2 = colFont2;
                    this.colFont3 = colFont3;
                }



                public string colName { get; }
                public Color colBackGround1 { get; }
                public Color colBackGround2 { get; }
                public Color colBackGround3 { get; }
                public Color colForeGround1 { get; }
                public Color colForeGround2 { get; }
                public Color colForeGround3 { get; }
                public Color colAccent1 { get; }
                public Color colAccent2 { get; }
                public Color colAccent3 { get; }
                public Color colFont1 { get; }
                public Color colFont2 { get; }
                public Color colFont3 { get; }
            }

            static internal Colours? currentTheme { get; private set; }

            static internal readonly List<Colours> AllThemes = new()
            {
                // Add new themes here

                new Themes.Colours(colName: "Dark",

                colBackGround1: Color.FromArgb(0, 0, 0),
                colBackGround2: Color.FromArgb(64, 64, 64),
                colBackGround3: Color.FromArgb(128, 128, 128),

                colForeGround1: Color.FromArgb(255, 0, 0),
                colForeGround2: Color.FromArgb(160, 160, 160),
                colForeGround3: Color.FromArgb(192, 192, 192),

                colAccent1: Color.FromArgb(0, 0, 255),
                colAccent2: Color.FromArgb(0, 255, 0),
                colAccent3: Color.FromArgb(255, 0, 0),

                colFont1: Color.FromArgb(255, 255, 255),
                colFont2: Color.FromArgb(255, 255, 0),
                colFont3: Color.FromArgb(0, 255, 0)

            ),


                new Themes.Colours(colName: "Light",

                colBackGround1: Color.FromArgb(0, 0, 0),
                colBackGround2: Color.FromArgb(0, 0, 0),
                colBackGround3: Color.FromArgb(0, 0, 0),

                colForeGround1: Color.FromArgb(0, 0, 0),
                colForeGround2: Color.FromArgb(0, 0, 0),
                colForeGround3: Color.FromArgb(0, 0, 0),

                colAccent1: Color.FromArgb(0, 0, 0),
                colAccent2: Color.FromArgb(0, 0, 0),
                colAccent3: Color.FromArgb(0, 0, 0),

                colFont1: Color.FromArgb(0, 0, 0),
                colFont2: Color.FromArgb(0, 0, 0),
                colFont3: Color.FromArgb(0, 0, 0)

            ),



                new Themes.Colours(colName: "Blue",

                colBackGround1: Color.FromArgb(0, 0, 0),
                colBackGround2: Color.FromArgb(0, 0, 0),
                colBackGround3: Color.FromArgb(0, 0, 0),

                colForeGround1: Color.FromArgb(0, 0, 0),
                colForeGround2: Color.FromArgb(0, 0, 0),
                colForeGround3: Color.FromArgb(0, 0, 0),

                colAccent1: Color.FromArgb(0, 0, 0),
                colAccent2: Color.FromArgb(0, 0, 0),
                colAccent3: Color.FromArgb(0, 0, 0),

                colFont1: Color.FromArgb(0, 0, 0),
                colFont2: Color.FromArgb(0, 0, 0),
                colFont3: Color.FromArgb(0, 0, 0)

                ),



                new Themes.Colours(colName: "Rainbow",

                colBackGround1: Color.FromArgb(0, 0, 0),
                colBackGround2: Color.FromArgb(0, 0, 0),
                colBackGround3: Color.FromArgb(0, 0, 0),

                colForeGround1: Color.FromArgb(0, 0, 0),
                colForeGround2: Color.FromArgb(0, 0, 0),
                colForeGround3: Color.FromArgb(0, 0, 0),

                colAccent1: Color.FromArgb(0, 0, 0),
                colAccent2: Color.FromArgb(0, 0, 0),
                colAccent3: Color.FromArgb(0, 0, 0),

                colFont1: Color.FromArgb(0, 0, 0),
                colFont2: Color.FromArgb(0, 0, 0),
                colFont3: Color.FromArgb(0, 0, 0)

                )
            };

            static internal int ChangeTheme(int index)
            {
                if (index < 0 || index >= Themes.AllThemes.Count) return -1;

                currentTheme = Themes.AllThemes[index];
                return 0;
            }

            static internal int ChangeTheme(string themeName)
            {
                int rturn = -1;
                for (int i = 0; i < Themes.AllThemes.Count; i++)
                {
                    if (Themes.AllThemes[i].colName == themeName.Trim())
                    {
                        ChangeTheme(i);
                        rturn = 0;
                        break;
                    }
                }
                return rturn;
            }
        }
        #endregion

    }
}