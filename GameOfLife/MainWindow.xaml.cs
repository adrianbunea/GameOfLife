using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GameOfLife
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new ViewModel();

            for (int i = 0; i < 100; i++)
            {
                vm.Cells.Add(new Cell { CurrentState = CellState.Dead });
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
                
                vm.ActivateTimer();
                button.Content = "Stop";
            }
            else
            {
                vm.StopTimer();
                button.Content = "Start";
            }
        }
    }
}
