using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace TreeViewTrainnig
{
    public static class SelectionChangedTrigger
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(SelectionChangedTrigger), new PropertyMetadata(null, OnCommandChanged));

        public static readonly DependencyProperty CommandParamaterProperty =
            DependencyProperty.RegisterAttached("CommandParamater", typeof(object), typeof(SelectionChangedTrigger), new PropertyMetadata(null));

        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        public static object GetCommandParamater(DependencyObject obj)
        {
            return (object)obj.GetValue(CommandParamaterProperty);
        }

        public static void SetCommandParamater(DependencyObject obj, object value)
        {
            obj.SetValue(CommandParamaterProperty, value);
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WinRTXamlToolkit.Controls.TreeView)
            {
                WinRTXamlToolkit.Controls.TreeView Trv = (WinRTXamlToolkit.Controls.TreeView)d;
                if (Trv == null) return;
                Trv.SelectedItemChanged += TreeView_SelectedItemChanged;
            }
            else if (d is TextBox)
            {
                TextBox txtBx = (TextBox)d;
                if (txtBx == null) return;
                txtBx.LostFocus += txtBx_LostFocus;
                txtBx.SelectionChanged += TxtBox_SelectionChanged;
            }
            else
            {
                Selector s = d as Selector;
                if (s == null) return;
                s.SelectionChanged += s_SelectionChanged;
            }
        }

        private static void txtBx_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox s = (TextBox)sender;
            ICommand cmd = s.GetValue(SelectionChangedTrigger.CommandProperty) as ICommand;

            object param = s;
            if (cmd != null && cmd.CanExecute(param))
                cmd.Execute(param);
        }

        private static void TxtBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox s = (TextBox)sender;

            ICommand cmd = s.GetValue(SelectionChangedTrigger.CommandProperty) as ICommand;
            object param = s;
            if (cmd != null && cmd.CanExecute(param))
                cmd.Execute(param);
        }

        static void s_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            Selector s = (Selector)sender;
            ICommand cmd = s.GetValue(SelectionChangedTrigger.CommandProperty) as ICommand;
            object param = s.GetValue(SelectionChangedTrigger.CommandParamaterProperty);
            if (cmd != null && cmd.CanExecute(param))
                cmd.Execute(param);
        }

        static void TreeView_SelectedItemChanged(object sender, WinRTXamlToolkit.Controls.RoutedPropertyChangedEventArgs<object> e)
        {
            WinRTXamlToolkit.Controls.TreeView s = (WinRTXamlToolkit.Controls.TreeView)sender;
            ICommand cmd = s.GetValue(SelectionChangedTrigger.CommandProperty) as ICommand;
            object param = e.NewValue;
            if (cmd != null && cmd.CanExecute(param))
                cmd.Execute(param);
        }
    }
}
