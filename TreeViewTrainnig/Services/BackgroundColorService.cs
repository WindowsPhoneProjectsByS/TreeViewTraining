using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TreeViewTrainnig
{
    class BackgroundColorService
    {
        public static void ChangeBackgroundColor(Grid grid, String localization)
        {
            Debug.WriteLine("ChangeBackgroundColor()");
            Debug.WriteLine("Odczytwanie koloru dla lokalizacji: " + localization);
            string color = "Coral";

            try
            {
                color = Windows.Storage.ApplicationData.Current.LocalSettings.Values[localization].ToString();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Nie znalziono koloru w zbiorze");
            }

            Debug.WriteLine("Wybrano kolor: " + color);
            
            grid.Background = GetProperColorForNote(color);
        }
        
        public static SolidColorBrush GetProperColorForNote(string color)
        {
            SolidColorBrush brush;

            switch (color)
            {
                case "Black":
                    brush = new SolidColorBrush(Colors.Black);
                    break;
                case "Cyan":
                    brush = new SolidColorBrush(Colors.Cyan);
                    break;
                case "Violet":
                    brush = new SolidColorBrush(Colors.Violet);
                    break;
                case "Yellow":
                    brush = new SolidColorBrush(Colors.Yellow);
                    break;
                case "SteelBlue":
                    brush = new SolidColorBrush(Colors.SteelBlue);
                    break;
                default:
                    brush = new SolidColorBrush(Colors.Coral);
                    break;
            }

            return brush;

        }

       

        public static void SaveBackGroundColorForNote(string localization, string color)
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values[localization] = color;
        }
    }
}
