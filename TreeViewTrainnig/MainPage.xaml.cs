﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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

            this.NavigationCacheMode = NavigationCacheMode.Required;

            createTestFoldersStructure();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void createTestFoldersStructure()
        {
            await ApplicationData.Current.LocalFolder.CreateFolderAsync("Darek1");
            await ApplicationData.Current.LocalFolder.CreateFolderAsync("Darek1\\Darek1.1");
            await ApplicationData.Current.LocalFolder.CreateFolderAsync("Darek1\\Darek1.2");
            await ApplicationData.Current.LocalFolder.CreateFolderAsync("Darek1\\Darek1.2\\Darek1.2.1");
            await ApplicationData.Current.LocalFolder.CreateFolderAsync("Darek1\\Darek1.2\\Darek1.2.2");
            await ApplicationData.Current.LocalFolder.CreateFolderAsync("Darek2");
        }
    }
}
