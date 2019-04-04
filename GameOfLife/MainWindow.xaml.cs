using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;
using System.IO;

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

        private void PatternSelect(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            string patternName = menuItem.Header.ToString();
            string path = String.Format("..\\..\\Patterns\\{0}.txt", patternName);
            

            string[] lines = File.ReadAllLines(path);

            int rows = lines.Length;
            int columns = lines[0].Length;

            if (rows > columns)
            {
                int difference = rows - columns;
                string filler = new string('.', difference);
                
                for (int i = 0; i < rows; i++)
                {
                    lines[i] += filler;
                }
            }

            if (rows < columns)
            {
                int difference = columns - rows;

                List<string> temp = lines.ToList<string>();
                for (int i = 0; i < difference; i++)
                {
                    temp.Add(new string('.', columns));
                }

                lines = temp.ToArray<string>();
            }

            List<char> cellStates = new List<char>();
            foreach (string line in lines)
            {
                for (int i = 0; i < line.Length; i++) 
                {
                    cellStates.Add(line[i]);
                }
            }

            ViewModel vm = DataContext as ViewModel;
            vm.GridColumns = (int)Math.Sqrt(cellStates.Count);
            vm.GridRows = (int)Math.Sqrt(cellStates.Count);

            vm.Cells.Clear();
            for (int i = 0; i < vm.CellCount; i++)
            {
                vm.Cells.Add(new Cell { CurrentState = cellStates[i] == 'O' ? CellState.Alive : CellState.Dead });
            }

            vm.BindAllNeighbors();
        }

        private void RandomPatternSelect(object sender, RoutedEventArgs e)
        {
            ViewModel vm = DataContext as ViewModel;

            Random random = new Random();
            vm.Cells.Clear();
            for (int i = 0; i < vm.CellCount; i++)
            {
                vm.Cells.Add(new Cell { CurrentState = (CellState)random.Next(0, 2) });
            }

            vm.BindAllNeighbors();
        }
    }
}
