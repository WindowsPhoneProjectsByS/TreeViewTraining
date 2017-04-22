using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace TreeViewTrainnig.Services
{
    class FileOperationService
    {
        public async Task<bool> LoadFileToView(TextBox view)
        {
            bool condition = true;

            Debug.WriteLine("LoadFileToView()");
            try
            {
                Debug.WriteLine("Próba pobrania pliku o lokalizacji: " + TreeViewPageViewModel.capsuleInfo.localization);
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(TreeViewPageViewModel.capsuleInfo.localization);
                view.Text = await FileIO.ReadTextAsync(file);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Nie udana próba pobrania pliku");
                Debug.WriteLine("Nie można było załadować danych do widoku");
                condition = false;
            }

            return condition;
        }

        public async Task<bool> SaveFromViewToFile(TextBox view)
        {
            bool condition = true;
            try
            {
                Debug.WriteLine("Próba pobrania pliku o lokalizacji: " + TreeViewPageViewModel.capsuleInfo.localization);
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(TreeViewPageViewModel.capsuleInfo.localization);
                await FileIO.WriteTextAsync(file, view.Text);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Nie udana próba pobrania pliku.");
                Debug.WriteLine("Brak możliwośći zapisania do pliku");
                condition = false;
            }

            return condition;
        }

        public async Task<bool> DeleteFile()
        {
            bool condition = true;

            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(TreeViewPageViewModel.capsuleInfo.localization);
                await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Próba usnięcia pliku zakończono nie powodzeniem: " + e.Message);
            }

            return condition;
        }
    }
}
