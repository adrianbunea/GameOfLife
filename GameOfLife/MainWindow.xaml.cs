using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace GameOfLife
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new ViewModel();

            for (int i = 0; i < vm.CellCount; i++)
            {
                vm.Cells.Add(new Cell());
            }

            vm.BindAllNeighbors();
            DataContext = vm;
        }

        private void ChangeCellState(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGridCell dataGridCell = sender as DataGridCell;
            Cell cell = (Cell)dataGridCell.DataContext;
            cell.CurrentState = (CellState)(((int)cell.CurrentState+1)%2);
        }

        private void StartSimulation(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            //ObservableCollection<Cell> Cells = CellsPanel.ItemsSource as ObservableCollection<Cell>;
            ViewModel vm = DataContext as ViewModel;

            if ((string)button.Content == "Start")
            {
                RowsUpDown.IsEnabled = false;
                ColumnsUpDown.IsEnabled = false;
                vm.ActivateTimer();
                button.Content = "Stop";
            }
            else
            {
                RowsUpDown.IsEnabled = true;
                ColumnsUpDown.IsEnabled = true;
                vm.StopTimer();
                button.Content = "Start";
            }
        }

        private void GridSizeChange(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ViewModel vm = DataContext as ViewModel;    

            vm.Cells.Clear();
            for (int i = 0; i < vm.CellCount; i++)
            {
                vm.Cells.Add(new Cell());
            }

            vm.BindAllNeighbors();
        }
    }
}
