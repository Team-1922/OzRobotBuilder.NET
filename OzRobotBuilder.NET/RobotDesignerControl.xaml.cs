/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.ViewModels;

namespace Team1922.OzRobotBuilder.NET
{
    /// <summary>
    /// Interaction logic for RobotDesignerControl.xaml
    /// </summary>
    public partial class RobotDesignerControl : UserControl
    {
        public RobotDesignerControl()
        {
            InitializeComponent();
        }

        public RobotDesignerControl(ViewModel viewModel)
        {
            try
            {
                DataContext = viewModel;
                InitializeComponent();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            // wait until we're initialized to handle events
            viewModel.ViewModelChanged += new EventHandler(ViewModelChanged);
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedElement")
            {
                var test = ((DataContext as ViewModel));
                var test1 = test?.SelectedElement as ViewModelBase;
                tbEditor.ItemsSource = test1;
            }
        }

        internal void DoIdle()
        {
            // only call the view model DoIdle if this control has focus
            // otherwise, we should skip and this will be called again
            // once focus is regained
            ViewModel viewModel = DataContext as ViewModel;
            if (viewModel != null && this.IsKeyboardFocusWithin)
            {
                viewModel.DoIdle();
            }
        }

        private void ViewModelChanged(object sender, EventArgs e)
        {
            // this gets called when the view model is updated because the Xml Document was updated
            // since we don't get individual PropertyChanged events, just re-set the DataContext
            ViewModel viewModel = DataContext as ViewModel;
            DataContext = null; // first, set to null so that we see the change and rebind
            DataContext = viewModel;
        }

        private void tvRobot_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ViewModel viewModel = DataContext as ViewModel;
            viewModel.SelectedElement = e.NewValue as IProvider;
        }

        private void tbEditor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItemText = ((KeyValuePair<string, string>)(tbEditor.SelectedCells?.First().Item)).Key;
            if ("" == selectedItemText)
                return;

            //open up the child selected if it is a child
            var parent = (DataContext as ViewModel)?.SelectedElement as ICompoundProvider;
            if (null != parent)
            {
                var children = parent.Children;
                if (null != children)
                {
                    foreach (var child in children)
                    {
                        if (null == child)
                        {
                            continue;
                        }
                        if (child.Name == selectedItemText)
                        {
                            SelectChild(child.Name);
                            return;
                        }
                    }
                }
            }

            //edit it if it is not a child
            var selectedItemProvider = (DataContext as ViewModel)?.SelectedElement as IProvider;
            if(null != selectedItemProvider)
            {

            }
        }
        public static int GetRowIndex(DataGridCell dataGridCell)
        {
            // Use reflection to get DataGridCell.RowDataItem property value.
            PropertyInfo rowDataItemProperty = dataGridCell.GetType().GetProperty("RowDataItem", BindingFlags.Instance | BindingFlags.NonPublic);

            DataGrid dataGrid = GetDataGridFromChild(dataGridCell);

            return dataGrid.Items.IndexOf(rowDataItemProperty.GetValue(dataGridCell, null));
        }
        public static DataGrid GetDataGridFromChild(DependencyObject dataGridPart)
        {
            if (VisualTreeHelper.GetParent(dataGridPart) == null)
            {
                throw new NullReferenceException("Control is null.");
            }
            if (VisualTreeHelper.GetParent(dataGridPart) is DataGrid)
            {
                return (DataGrid)VisualTreeHelper.GetParent(dataGridPart);
            }
            else
            {
                return GetDataGridFromChild(VisualTreeHelper.GetParent(dataGridPart));
            }
        }

        private void SelectChild(string childName)
        {
            var children = (tvRobot.SelectedItem as TreeViewItem)?.Items;
            if (null == children)
                return;

            foreach(var item in children)
            {
                var treeViewItem = item as TreeViewItem;
                if (null == treeViewItem)
                    continue;
                if((string)treeViewItem.Header == childName)
                {
                    treeViewItem.IsSelected = true;
                }
            }
        }
    }
}
