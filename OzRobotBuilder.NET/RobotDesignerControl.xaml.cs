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
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Contracts.Events;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
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
            /*AddSubsystemCommand = new DelegateCommand(OnAddSubsystem);
            AddEventHandlerCommand = new DelegateCommand(OnAddEventHandler);
            AddJoystickCommand = new DelegateCommand(OnAddJoystick);

            AddPWMOutputCommand = new DelegateCommand(OnAddPWMOutput);
            AddAnalogInputCommand = new DelegateCommand(OnAddAnalogInput);
            AddQuadEncoderCommand = new DelegateCommand(OnAddQuadEncoder);
            AddDigitalInputCommand = new DelegateCommand(OnAddDigitalInput);
            AddRelayOutputCommand = new DelegateCommand(OnAddRelayOutput);
            AddPWMOutputCommand = new DelegateCommand(OnAddCANTalon);*/

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
            viewModel.ViewModelChanged += new System.EventHandler(ViewModelChanged);
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        internal struct TestStruct
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
        private List<TestStruct> _tempList = new List<TestStruct>() { new TestStruct() { Key = "Hello", Value = "World" } };

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

        private void SetDesignerDirty()
        {
            if(DataContext is ViewModel)
            {
                (DataContext as ViewModel).DesignerDirty = true;
            }
        }

        #region Event Handlers
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
            if (null == DataContext)
                return;

            ViewModel viewModel = DataContext as ViewModel;
            viewModel.SelectedElement = e.NewValue as IProvider;
        }
        
        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedElement")
            {
                //var test = ((DataContext as ViewModel));
                //var test1 = test?.SelectedElement as ViewModelBase;
                //tbEditor.ItemsSource = (test.SelectedElement as ViewModelBase);                
                tbEditor.ItemsSource = ((DataContext as ViewModel)?.SelectedElement as ViewModelBase)?.GetEditableKeyValueList() ?? null;
                //UpdateDataGrid();
            }
        }

        private void tbEditor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            /*var selectedItemText = ((KeyValuePair<string, string>)(tbEditor.SelectedCells?.First().Item)).Key;
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

            }*/
        }

        private void tbEditor_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if(e.EditAction == DataGridEditAction.Commit)
            {
                // TODO: if there was no actual change, then DON"T do this
                SetDesignerDirty();
            }
        }

        private void tvRobot_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as ViewModel;
            if (null == viewModel)
                return;

            //open up the appropriate context menu
            ContextMenu cm = null;
            if (viewModel.SelectedElement is IRobotProvider || viewModel.SelectedElement == null)
            {
                cm = tvRobot.FindResource("cmRobot") as ContextMenu;
            }
            else if(viewModel.SelectedElement is IEnumerable<ISubsystemProvider>)
            {
                cm = tvRobot.FindResource("cmSubsystems") as ContextMenu;
            }
            else if (viewModel.SelectedElement is IEnumerable<IEventHandlerProvider>)
            {
                cm = tvRobot.FindResource("cmEventHandlers") as ContextMenu;
            }
            else if (viewModel.SelectedElement is IEnumerable<IJoystickProvider>)
            {
                cm = tvRobot.FindResource("cmJoysticks") as ContextMenu;
            }
            else if (viewModel.SelectedElement is ISubsystemProvider)
            {
                cm = tvRobot.FindResource("cmSubsystem") as ContextMenu;
            }
            else if(viewModel.SelectedElement is IEventHandlerProvider)
            {
            }
            else if(viewModel.SelectedElement is IJoystickProvider)
            {
            }
            else if(viewModel.SelectedElement is IRobotMapProvider)
            {
                cm = tvRobot.FindResource("cmRobotMap") as ContextMenu;
            }
            if (null == cm)
                return;
            cm.PlacementTarget = sender as TreeView;
            cm.IsOpen = true;
        }

        private void cmRobot_AddSubsystem(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is ViewModel))
                return;
            (DataContext as ViewModel)?.AddSubsystem(new Subsystem() { Name = "NewSubsystem" });
        }
        private void cmRobot_AddEventHandler(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is ViewModel))
                return;
            (DataContext as ViewModel)?.AddEventHandler(new MVVM.Models.EventHandler() { Name = "NewEventHandler" });
        }
        private void cmRobot_AddJoystick(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is ViewModel))
                return;
            (DataContext as ViewModel)?.AddJoystick(new Joystick() { Name = "NewJoystick" });
        }

        private void cm_AddRobotMapElement(object sender, RoutedEventArgs e)
        {
            if(!(DataContext is ViewModel))
                return;
            ((DataContext as ViewModel)?.SelectedElement as IRobotMapProvider)?.AddEntry("","");
        }

        private void cmSubsystem_AddPWMOutput(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is ViewModel))
                return;
            ((DataContext as ViewModel)?.SelectedElement as ISubsystemProvider)?.AddPWMOutput(new PWMOutput() { Name = "NewPWMOutput" });
        }

        private void cmSubsystem_AddAnalogInput(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is ViewModel))
                return;
            ((DataContext as ViewModel)?.SelectedElement as ISubsystemProvider)?.AddAnalogInput(new AnalogInput() { Name = "NewAnalogInput" });
        }

        private void cmSubsystem_AddDigitalInput(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is ViewModel))
                return;
            ((DataContext as ViewModel)?.SelectedElement as ISubsystemProvider)?.AddDigitalInput(new DigitalInput() { Name = "NewDigitalInput" });
        }

        private void cmSubsystem_AddQuadEncoder(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is ViewModel))
                return;
            ((DataContext as ViewModel)?.SelectedElement as ISubsystemProvider)?.AddQuadEncoder(new QuadEncoder() { Name = "NewQuadEncoder" });
        }

        private void cmSubsystem_AddRelayOutput(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is ViewModel))
                return;
            ((DataContext as ViewModel)?.SelectedElement as ISubsystemProvider)?.AddRelayOutput(new RelayOutput() { Name = "NewRelayOutput" });
        }

        private void cmSubsystem_AddCANTalon(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is ViewModel))
                return;
            ((DataContext as ViewModel)?.SelectedElement as ISubsystemProvider)?.AddCANTalon(new CANTalon() { Name = "NewCANTalon" });
        }
        #endregion

        #region Commands
        //public ICommand AddSubsystemCommand { get; }
        //public ICommand AddEventHandlerCommand { get; }
        //public ICommand AddJoystickCommand { get; }

        //public ICommand AddPWMOutputCommand { get; }
        //public ICommand AddAnalogInputCommand { get; }
        //public ICommand AddDigitalInputCommand { get; }
        //public ICommand AddQuadEncoderCommand { get; }
        //public ICommand AddRelayOutputCommand { get; }
        //public ICommand AddCANTalonCommand { get; }
        #endregion

        #region Command Methods
        private void OnAddSubsystem()
        {
        }
        private void OnAddEventHandler()
        {
        }
        private void OnAddJoystick()
        {
        }
        private void OnAddAnalogInput()
        {
        }
        private void OnAddCANTalon()
        {
        }
        private void OnAddDigitalInput()
        {
        }
        private void OnAddPWMOutput()
        {
        }
        private void OnAddQuadEncoder()
        {
        }
        private void OnAddRelayOutput()
        {
        }
        #endregion

    }

}
