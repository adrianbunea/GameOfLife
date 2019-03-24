﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new ViewModel();

            for (int i = 0; i < 100; i++)
            {
                vm.Cells.Add(new Cell { State = i % 29 == 0 ? CellState.Alive : CellState.Dead });
            }

            //vm.Cells[3].State = CellState.Alive;
            //vm.Cells[2].State = CellState.Alive;
            vm.Cells[12].State = CellState.Alive;
            vm.Cells[13].State = CellState.Alive;
            vm.Cells[14].State = CellState.Alive;

            vm.BindAllNeighbors();
            vm.Cells[0].ActivateTimer();
            DataContext = vm;
        }

        private void StartClick(object sender, System.EventArgs e)
        {
        }
    }
}
