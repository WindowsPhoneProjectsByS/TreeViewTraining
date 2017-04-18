using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinRTXamlToolkit.Tools;

namespace TreeViewTrainnig
{
    public class TreeViewPageViewModel : BindableBase, INotifyPropertyChanged
    {
        public RelayCommand<object> cmdTreeSelected { get; private set; }

        private int _itemId;
        private Random _rand = new Random();

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
            TreeItems = BuildTreeMy();
            cmdTreeSelected = new RelayCommand<object>(TreeViewItemSelectionChanged);
        }

        private ObservableCollection<TreeItemModel> BuildTree(int depth, int branches)
        {
            var tree = new ObservableCollection<TreeItemModel>();

            if (depth > 0)
            {
                var depthIndices = Enumerable.Range(0, branches).Shuffle();

                for (int i = 0; i < branches; i++)
                {
                    var d = depthIndices[i] % depth;
                    var b = _rand.Next(branches / 2, branches);
                    tree.Add(
                        new TreeItemModel
                        {
                            Branch = b,
                            Depth = d,
                            Text = "Item " + _itemId++,
                            Children = BuildTree(d, b)
                        });
                }
            }
            return tree;
        }

        private ObservableCollection<TreeItemModel> BuildTreeMy()
        {
            var tree = new ObservableCollection<TreeItemModel>();

            
                for (int i = 0; i < 5; i++)
                {
                    //var d = 1;
                    //var b = 4;

                //for (int j = 0; i < 4; j++)
                //{
                    tree.Add(
                        new TreeItemModel
                        {
                            Branch = 0,
                            Depth = 0,
                            Text = "Item " + _itemId++,
                            //Children = BuildTree(d, b)
                        });
                //}
                    
                }
            
            return tree;
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
