using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using WinRTXamlToolkit.Tools;

namespace TreeViewTrainnig
{
    public class TreeViewPageViewModel : BindableBase, INotifyPropertyChanged
    {
        public RelayCommand<object> cmdTreeSelected { get; private set; }

        private int _itemId;
        private Random _rand = new Random();

        private List<Section> sections = new List<Section>();

        #region TreeItems 
        private ObservableCollection<TreeItemModel> _treeItems;
        public ObservableCollection<TreeItemModel> TreeItems
        {
            get { return _treeItems; }
            set { this.SetProperty(ref _treeItems, value); }
        }
        #endregion

        private string _SelectedItem = string.Empty;
        public string SelectedItem
        {
            get
            {
                return _SelectedItem;
            }

            set
            {
                if (_SelectedItem == value)
                { return; }

                _SelectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public TreeViewPageViewModel()
        {
            _itemId = 1;
            prepareApplication();
        }

        public async void prepareApplication()
        {
            await prepareViewModel();
            TreeItems = BuildTreeMy();
            cmdTreeSelected = new RelayCommand<object>(TreeViewItemSelectionChanged);
        }

        private ObservableCollection<TreeItemModel> BuildTreeMy()
        {
            var tree = new ObservableCollection<TreeItemModel>();

            Debug.WriteLine("Budowanie drzewa");
            Debug.WriteLine("Ilość sekcji: " + sections.Count);
                

            foreach (Section section in sections)
            {
                Debug.WriteLine("Nazwa folderu: " + section.folder.name);
                tree.Add(
                    new TreeItemModel
                    {
                        Branch = 0,
                        Depth = 0,
                        Text =  section.folder.name,

                    });
            }
            
            return tree;
        }

        private async Task prepareViewModel()
        {

            StorageFolder notesFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Notes");
            IReadOnlyList<StorageFolder> folders = await notesFolder.GetFoldersAsync();

            Debug.WriteLine("Ilość folderów: " + folders.Count);

            Debug.WriteLine("Analiza folderów: ");

            foreach (StorageFolder folder in folders)
            {
                Section section = new Section();
                section.folder.name = folder.Name;
                section.folder.localization = "Notes\\" + folder.Name;
                section.folder.type = ItemType.Type.Folder;
                section.files = await prepareFilesForFolder(folder);
                sections.Add(section);
            }
        }

        private async Task<List<Item>> prepareFilesForFolder(StorageFolder folder)
        {
            IReadOnlyList<StorageFile> files = await folder.GetFilesAsync();

            List<Item> filesSection = new List<Item>();

            foreach (StorageFile file in files)
            {
                Item item = new Item();
                item.name = file.Name;
                item.type = ItemType.Type.File;
                Debug.WriteLine(file.FileType);
                item.localization = "Notes\\" + folder.Name + "\\" + file.Name + "." + file.FileType;

                filesSection.Add(item);
            }

            return filesSection;
        }

        private void TreeViewItemSelectionChanged(object itm)
        {
            if (itm != null)
            {
                if (itm is TreeItemModel)
                {
                    TreeItemModel itmObj = (TreeItemModel)itm;

                    if (itmObj.Depth == 0)
                        itmObj.Branch = 0;

                    SelectedItem = itmObj.Text.ToString() + " is in position " + itmObj.Depth + " having " + itmObj.Branch + " Branches.";
                }
            }
        }
    }
}
