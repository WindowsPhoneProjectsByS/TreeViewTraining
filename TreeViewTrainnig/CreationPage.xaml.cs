using TreeViewTrainnig.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Text.RegularExpressions;
using Windows.UI.Popups;
using Windows.Storage;
using System.Diagnostics;
using System.Threading.Tasks;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TreeViewTrainnig
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreationPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public CreationPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }


        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            SetProperTitleInfo();
            SetProperLocalizationDisplay();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void SetProperTitleInfo()
        {
            switch (TreeViewPageViewModel.capsuleInfo.type)
            {
          
                case ItemType.Type.Folder:
                    TitleInfo.Text = "Utwórz plik";
                    break;
                case ItemType.Type.Main:
                    TitleInfo.Text = "Utwórz katalog";
                    break;
                default:
                    DefaultAction();
                    break;
            }
        }

        private void SetProperLocalizationDisplay()
        {
            LocalizationDisplay.Text = "Lokalizacja: " + TreeViewPageViewModel.capsuleInfo.localization.ToString();
        }


        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
           bool condition = checkCorrectionTitle();
            if (condition)
            {
                ChooseProperAction();
            }
            else
            {
                MessageDialog msg = new MessageDialog("Nazwa zawiera znaki niedozwolone.");
                await msg.ShowAsync();
            }
        }

        private async void ChooseProperAction()
        {
            switch (TreeViewPageViewModel.capsuleInfo.type)
            {
                case ItemType.Type.Folder:
                    await CreationFileAction();
                    break;
                case ItemType.Type.Main:
                    await CreationFolderAction();
                    break;
                default:
                    DefaultAction();
                    break;
            }
        }

        private void DefaultAction()
        {

        }

        private async Task CreationFileAction()
        {
            string localization = TreeViewPageViewModel.capsuleInfo.localization;
            StorageFolder folder = await ApplicationData.Current.LocalFolder.GetFolderAsync(localization);
            Debug.WriteLine("Nazwa pobranego folderu: " + folder.Name);
            try
            {
                string newName = NewName.Text + ".txt";
                await folder.CreateFileAsync(newName);
                DisplayWarningMessage("Utworzono plik");
            }
            catch (Exception e)
            {
                DisplayWarningMessage("Problem z utworzeniem pliku.");
                Debug.WriteLine("Problem z utworzeniem pliku");
            }
        }

        private async Task CreationFolderAction()
        {
            string localization = TreeViewPageViewModel.capsuleInfo.localization;
            StorageFolder folder = await ApplicationData.Current.LocalFolder.GetFolderAsync(localization);
            Debug.WriteLine("Nazwa pobranego folderu: " + folder.Name);
            try
            {
                string newName = NewName.Text;
                Debug.WriteLine("Próba utworzenia folderu.");
                await folder.CreateFolderAsync(newName);
                Debug.WriteLine("Utworzono folder.");
                DisplayWarningMessage("Utworzono folder");
            }
            catch (Exception e)
            {
                DisplayWarningMessage("Problem z utworzeniem folderu.");
                Debug.WriteLine("Problem z utworzeniem folder");
            }
        }

        private bool checkCorrectionTitle()
        {
            string newName = NewName.Text;
            Regex regex = new Regex("^[a-zA-Z0-9]+$");
            Match match = regex.Match(newName);

            bool condition = false;

            if (match.Success)
            {
                condition = true;
            }

            return condition;
        }

        private async void DisplayWarningMessage(string text)
        {
            MessageDialog msg = new MessageDialog(text);
            await msg.ShowAsync();
        }



    }
}
