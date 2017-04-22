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
using TreeViewTrainnig.Services;
using Windows.UI.Popups;
using System.Threading.Tasks;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TreeViewTrainnig.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DisplayNotePage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private TitleService titleService = new TitleService();
        private FileOperationService fileService = new FileOperationService();
        private int saveModClickCount = 0;

        public DisplayNotePage()
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
            titleService.PrepareInfoTitles(LocalizationInfo, NameInfo);
            BackgroundColorService.ChangeBackgroundColor(LayoutRoot, TreeViewPageViewModel.capsuleInfo.localization);
            ContentDisplay.IsEnabled = false;
            LoadContentOfNote();
        }

        private async void LoadContentOfNote()
        {
            await fileService.LoadFileToView(ContentDisplay);
        }

        
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void LocalizationInfo_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }

        private void ModficationButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeModificationButtonTitle();
            SaveOrNotToFile();
            ChangeSaveModClickCount();
        }

        private void ChangeModificationButtonTitle()
        {
            if (saveModClickCount == 0)
            {
                ModficationButton.Content = "Zapisz";
                ContentDisplay.IsEnabled = true;
            }
            
        }

        private async void SaveOrNotToFile()
        {
            if (saveModClickCount == 1)
            {
                bool success = await fileService.SaveFromViewToFile(ContentDisplay);
                if (!success)
                {
                    MessageDialog msg = new MessageDialog("Zmiany nie zostały wprowadzone");
                    await msg.ShowAsync();
                }
            }
        }

        private void ChangeSaveModClickCount()
        {
            if (saveModClickCount == 1)
            {
                saveModClickCount = 0;
                ModficationButton.Content = "Modyfikuj";
                ContentDisplay.IsEnabled = false;
            }
            else
            {
                saveModClickCount++;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteFile();
        }

        private async void DeleteFile()
        {
            bool success = await fileService.DeleteFile();
            if (!success)
            {
                MessageDialog msg = new MessageDialog("Nie udało się usunąć pliku");
                await msg.ShowAsync();
            }
            else
            {
                Frame.Navigate(typeof(MainPage));
            }
        }

    }
}
