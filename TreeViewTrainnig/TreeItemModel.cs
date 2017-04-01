using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using WinRTXamlToolkit.Imaging;

namespace TreeViewTrainnig
{
    public class TreeItemModel : BindableBase
    {
        #region Text 
        private string _text;
        /// <summary> 
        /// Gets or sets the text. 
        /// </summary> 
        public string Text
        {
            get { return _text; }
            set { this.SetProperty(ref _text, value); }
        }
        #endregion

        #region Depth 
        private int _depth;
        /// <summary> 
        /// Gets or sets the text. 
        /// </summary> 
        public int Depth
        {
            get { return _depth; }
            set { this.SetProperty(ref _depth, value); }
        }
        #endregion

        #region Branch 
        private int _branch;
        /// <summary> 
        /// Gets or sets the text. 
        /// </summary> 
        public int Branch
        {
            get { return _branch; }
            set { this.SetProperty(ref _branch, value); }
        }
        #endregion

        #region Children 
        private ObservableCollection<TreeItemModel> _children = new ObservableCollection<TreeItemModel>();
        /// <summary> 
        /// Gets or sets the child items. 
        /// </summary> 
        public ObservableCollection<TreeItemModel> Children
        {
            get { return _children; }
            set { this.SetProperty(ref _children, value); }
        }
        #endregion
    }
}
