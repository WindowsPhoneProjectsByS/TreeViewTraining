using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace TreeViewTrainnig.Services
{
    class TitleService
    {
        public void PrepareInfoTitles(TextBlock localizationInfo, TextBlock nameInfo)
        {
            localizationInfo.Text = "Lokalizacja: " + PrepeareLocalizationTitle();
            nameInfo.Text = "Nazwa: " + TreeViewPageViewModel.capsuleInfo.name;
        }

        private string PrepeareLocalizationTitle()
        {
            string localizationWitFileName = TreeViewPageViewModel.capsuleInfo.localization;
            string localization = localizationWitFileName.Substring(0, localizationWitFileName.LastIndexOf("\\"));

            return localization;

        }
    }
}
