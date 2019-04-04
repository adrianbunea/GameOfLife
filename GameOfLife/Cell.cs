using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Input;

namespace GameOfLife
{
    public enum CellState
    {
        Dead, Alive
    }

    public enum Direction
    {
        NW, N, NE, E, SE, S, SW, W
    }

    struct Coordinate
    {
        public int X;
        public int Y;

        public static Coordinate[] directions = new Coordinate[8]
        {
            new Coordinate(-1, -1), // NW
            new Coordinate( 0, -1), // N
            new Coordinate(+1, -1), // NE
            new Coordinate(+1,  0), // E
            new Coordinate(+1, +1), // SE
            new Coordinate( 0, +1), // S
            new Coordinate(-1, +1), // SW
            new Coordinate(-1,  0)  // W
        };

        public Coordinate(int x, int y)
        {
            //if (x < 0) { throw new Exception("X coordinate lower than 0!"); }
            //else { this.x = x; }

            //if (y < 0) { throw new Exception("Y coordinate lower than 0!"); }
            //else { this.y = y; }
            X = x;
            Y = y;
        }
    }

    public class Cell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Cell[] neighbors;

        public Cell[] Neighbors
        {
            get
            {
                Cell[] neighborsCopy = new Cell[8];
                neighbors.CopyTo(neighborsCopy, 0);
                return neighborsCopy;
            }
            set => neighbors = value;
        }

        private CellState currentState;
        public CellState CurrentState
        {
            get { return currentState; }
            set
            {
                currentState = value;
                NotifyPropertyChanged("CurrentState");
                NotifyPropertyChanged("Color");
            }
        }

        private CellState nextState;

        public Color Color
        {
            get { return currentState == CellState.Dead ? Colors.Black : Colors.White; }
        }

        public void CalculateNextState()
        {
            int counter = 0;
            foreach (Cell neighbor in Neighbors)
            {
                if (neighbor != null)
                {
                    if (neighbor.currentState == CellState.Alive)
                    {
                        counter++;
                    }
                }
            }
            if (currentState == CellState.Alive && counter < 2)
            {
                nextState = CellState.Dead;
            }
            else if (currentState == CellState.Alive && counter > 3)
            {
                nextState = CellState.Dead;
            }
            else if (currentState == CellState.Alive && (counter == 2 || counter == 3))
            {
                nextState = CellState.Alive;
            }
            else if (currentState == CellState.Dead && counter == 3)
            {
                nextState = CellState.Alive;
            }
            else
            {
                nextState = CellState.Dead;
            }
            
        }

        public void Update()
        {
            CurrentState = nextState;
        }

        public Cell()
        {
            CurrentState = CellState.Dead;
            neighbors = new Cell[8];
        }

        public Cell(CellState state) : this()
        {
            CurrentState = state;
        } 
    }

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
            //Timer.Enabled = false;
            foreach (Cell cell in Cells)
            {
                cell.CalculateNextState();
            }
            //Timer.Enabled = true;

            //Timer.Enabled = false;
            foreach (Cell cell in Cells)
            {
                cell.Update();
            }
            //Timer.Enabled = true;
        }

        public void ActivateTimer()
        {
            Timer.Enabled = true;
        }

        public void StopTimer()
        {
            Timer.Enabled = false;
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
            Coordinate coordinate = new Coordinate();
            coordinate.X = Cells.IndexOf(cell) % GridRows;
            coordinate.Y = Cells.IndexOf(cell) / GridColumns;
            return coordinate;
        }

        private void BindCellNeighbors(Cell cell)
        { 
            Coordinate cellCoordinate = FindCellCoordinates(cell);
     
            Cell[] neighbors = new Cell[8];

            for (int i = 0; i < 8; i++)
            {
                int neighborX = cellCoordinate.X + Coordinate.directions[i].X;
                int neighborY = cellCoordinate.Y + Coordinate.directions[i].Y;
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
