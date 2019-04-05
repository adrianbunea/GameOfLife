using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Timers;

namespace GameOfLife
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Cell> Cells { get; } = new ObservableCollection<Cell>();

        private int gridRows;
        public int GridRows
        {
            get
            {
                return gridRows;
            }

            set
            {
                gridRows = value;
                NotifyPropertyChanged("GridRows");
            }
        }

        private int gridColumns;
        public int GridColumns
        {
            get
            {
                return gridColumns;
            }

            set
            {
                gridColumns = value;
                NotifyPropertyChanged("GridColumns");
            }
        }

        public int CellCount => gridRows * gridColumns;

        private static Timer Timer = new Timer
        {
            Interval = 250,
            AutoReset = true,
            Enabled = false
        };

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            foreach (Cell cell in Cells)
            {
                cell.CalculateNextState();
            }
            
            foreach (Cell cell in Cells)
            {
                cell.Update();
            }
        }

        public void ActivateTimer()
        {
            Timer.Enabled = true;
        }

        public void StopTimer()
        {
            Timer.Enabled = false;
        }

        public double Interval
        {
            get
            {
                return 1000 / Timer.Interval;
            }
            set
            {
                Timer.Interval = 1000 / value;
                NotifyPropertyChanged("Interval");
            }
        }

        private int FindCellIndex(int x, int y)
        {
            if (x < 0 || y < 0 || x >= gridColumns || y >= gridRows)
            {
                return -1;
            }
            int index = y * gridColumns + x;
            return index;
        }

        private Coordinate FindCellCoordinates(Cell cell)
        {
            Coordinate coordinate = new Coordinate
            {
                X = Cells.IndexOf(cell) % GridRows,
                Y = Cells.IndexOf(cell) / GridColumns
            };
            return coordinate;
        }

        private void BindCellNeighbors(Cell cell)
        {
            Coordinate cellCoordinate = FindCellCoordinates(cell);

            Cell[] neighbors = new Cell[8];

            for (int i = 0; i < 8; i++)
            {
                int neighborX = cellCoordinate.X + Coordinate.Directions[i].X;
                int neighborY = cellCoordinate.Y + Coordinate.Directions[i].Y;
                Cell neighbor = new Cell();
                int index = FindCellIndex(neighborX, neighborY);
                if (index < 0)
                {
                    neighbor = null;
                }
                else
                {
                    neighbor = Cells[index];
                }
                neighbors[i] = neighbor;
            }

            cell.Neighbors = neighbors;
        }

        public void BindAllNeighbors()
        {
            foreach (Cell cell in Cells)
            {
                BindCellNeighbors(cell);
            }
        }

        public ViewModel()
        {
            Timer.Elapsed += OnTimedEvent;
            GridRows = 16;
            GridColumns = 16;
        }
    }
}