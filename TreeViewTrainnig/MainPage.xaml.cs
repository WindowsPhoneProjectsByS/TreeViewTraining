using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace TreeViewTrainnig
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Disabled;

            //createTestFoldersStructure();
            //createFilesInFolders();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private async void GoToCreatePage_Click(object sender, RoutedEventArgs e)
        {
            if (TreeViewPageViewModel.capsuleInfo.type != ItemType.Type.File)
            {
                Frame.Navigate(typeof(CreationPage));
            }
            else
            {
                MessageDialog msgDialog = new MessageDialog("Nie dozwolona operacja dodania dla pliku.");
                await msgDialog.ShowAsync();
            }         
        }


        private async void createTestFoldersStructure()
        {
            try
            {
                await ApplicationData.Current.LocalFolder.CreateFolderAsync("Notes\\Sport");
                await ApplicationData.Current.LocalFolder.CreateFolderAsync("Notes\\Programowanie");
                await ApplicationData.Current.LocalFolder.CreateFolderAsync("Notes\\Uczelnia");
                await ApplicationData.Current.LocalFolder.CreateFolderAsync("Notes\\Dziewczyna");
            }
            catch (Exception e)
            {
                Debug.WriteLine("Nie udało utoworzyć się folderów: ");
            }
        }

        private async void deleteAllFolders()
        {
            StorageFolder notesFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Notes");
            await notesFolder.DeleteAsync(StorageDeleteOption.PermanentDelete);
        }

        private async void createFilesInFolders()
        {
            try
            {
                StorageFolder mainFolder = ApplicationData.Current.LocalFolder;

                await mainFolder.CreateFileAsync("Notes\\Dziewczyna\\randka.txt");
                Debug.WriteLine("Utworzonon plik w folderze Dziewczyna: randka.txt");
                await mainFolder.CreateFileAsync("Notes\\Dziewczyna\\prezent.txt");
                Debug.WriteLine("Utworzonon plik w folderze Dziewczyna: prezent.txt");
                await mainFolder.CreateFileAsync("Notes\\Programowanie\\java.txt");
                Debug.WriteLine("Utworzonon plik w folderze Programowanie: java.txt");
                await mainFolder.CreateFileAsync("Notes\\Uczelnia\\stypendium.txt");
                Debug.WriteLine("Utworzonon plik w folderze Uczelnia: stypendium.txt");
                await mainFolder.CreateFileAsync("Notes\\Sport\\siatka.txt");
                Debug.WriteLine("Utworzonon plik w folderze Sport: siatka.txt");
            }
            catch (Exception e)
            {
                Debug.WriteLine("Problem podczas tworzenie plików w poszczególnych folderach.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteFileOrFolder(); 
        }

        private async void DeleteFileOrFolder()
        {
            ItemType.Type type = TreeViewPageViewModel.capsuleInfo.type;

            switch (type)
            {
                case ItemType.Type.File:
                    await DeleteFile();
                    break;
                case ItemType.Type.Folder:
                    await DeleteFolder();
                    break;
                case ItemType.Type.Main:
                    break;
            }

            Debug.WriteLine("Usuwanie itemu zakończone.");
        }

        private async Task DeleteFile()
        {
            
        }

        private async Task DeleteFolder()
        {
            string localization = TreeViewPageViewModel.capsuleInfo.localization;
            Debug.WriteLine("Usuwanie folderu");
            try
            {
                StorageFolder folder = await ApplicationData.Current.LocalFolder.GetFolderAsync(localization);
                await folder.DeleteAsync(StorageDeleteOption.PermanentDelete);
                Debug.WriteLine("Odświeżanie kontentu tree view");
                if (TreeViewPageModel == null)
                {
                    Debug.WriteLine("TreeViewPageModel jest pusty");
                }
                //TreeViewPageModel.fillTreeViewValues();
            }
            catch (IOException e)
            {
                Debug.WriteLine("Nie udało usunąć się folderu.");
                MessageDialog msg = new MessageDialog("Nie udało się usunąć folderu: " + e.Message);
                await msg.ShowAsync();
            }
            
        }

        private async void DeleteAll()
        {

        }


    }
}
