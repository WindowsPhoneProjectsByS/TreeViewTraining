using System;
using System.Collections.Generic;
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
            string color = Windows.Storage.ApplicationData.Current.LocalSettings.Values[localization].ToString();
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
                case "Aqua":
                    brush = new SolidColorBrush(Colors.Aqua);
                    break;
                case "Bisque":
                    brush = new SolidColorBrush(Colors.Bisque);
                    break;
                case "Blue":
                    brush = new SolidColorBrush(Colors.Blue);
                    break;
                case "Brown":
                    brush = new SolidColorBrush(Colors.Brown);
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
