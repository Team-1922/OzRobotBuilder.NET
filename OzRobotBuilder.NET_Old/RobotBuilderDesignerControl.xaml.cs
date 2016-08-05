﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Team1922.MVVM.Framework;

namespace Team1922.OzRobotBuilder.NET
{
    /// <summary>
    /// Interaction logic for RobotBuilderDesignerControl.xaml
    /// </summary>
    public partial class RobotBuilderDesignerControl : UserControl
    {
        public ViewModel ViewModel { get; }

        public RobotBuilderDesignerControl()
        {
            InitializeComponent();
        }

        public RobotBuilderDesignerControl(ViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            // wait until we're initialized to handle events
            //viewModel.ViewModelChanged += new EventHandler(ViewModelChanged);
        }

        /*internal void DoIdle()
        {
            // only call the view model DoIdle if this control has focus
            // otherwise, we should skip and this will be called again
            // once focus is regained
            RobotViewModel viewModel = DataContext as RobotViewModel;
            if (viewModel != null && this.IsKeyboardFocusWithin)
            {
                viewModel.DoIdle();
            }
        }

        private void ViewModelChanged(object sender, EventArgs e)
        {
            // this gets called when the view model is updated because the Xml Document was updated
            // since we don't get individual PropertyChanged events, just re-set the DataContext
            IViewModel viewModel = DataContext as IViewModel;
            DataContext = null; // first, set to null so that we see the change and rebind
            DataContext = viewModel;
        }

        private void treeContent_Loaded(object sender, RoutedEventArgs e)
        {
            var treeView = sender as TreeView;
            if (treeView != null)
            {
                // make sure that any top-level items that contain other items are expanded
                foreach (object item in treeView.Items)
                {
                    TreeViewItem treeItem = treeView.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                    treeItem.IsExpanded = true;
                }
            }
        }

        private void treeContent_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var viewModel = DataContext as IViewModel;
            var treeView = sender as TreeView;
            if ((viewModel != null) && (treeView != null))
            {
                // pass Selection events along to the view model so that the Properties window is updated
                viewModel.OnSelectChanged(treeView.SelectedItem);
            }
        }

        private void cbLocation_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as IViewModel;
            var comboBox = sender as ComboBox;
            if (!viewModel.IsLocationFieldSpecified)
            {
                // don't show selection in combobox if there was no data in file
                comboBox.SelectedIndex = -1;
            }
        }*/
    }
}