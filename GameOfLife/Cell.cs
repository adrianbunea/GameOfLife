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

        //public int X
        //{
        //    get => x;
        //    set
        //    {
        //        if (x < 0) { throw new Exception("X coordinate lower than 0!"); }
        //        else { x = value; }
        //    }
        //}

        //public int Y
        //{
        //    get => y;
        //    set
        //    {
        //        if (y < 0) { throw new Exception("Y coordinate lower than 0!"); }
        //        else { y = value; }
        //    }
        //}

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
        private static Timer Timer = new Timer(500);

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (action == false)
            {
                //Timer.Enabled = false;
                CalculateNextState();
                //Timer.Enabled = true;
            }
            else
            {
                //Timer.Enabled = false;
                Update();
                //Timer.Enabled = true;
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool action;

        private Cell[] neighbors;
        //public Cell Neighbor(Direction direction)
        //{
        //    Cell neighbor = new Cell
        //    {
        //        State = neighbors[(int)direction].State
        //    };
        //    return neighbor;
        //}

        //public void SetNeighbor(Direction direction)
        //{

        //}

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

        private CellState state;
        public CellState State
        {
            get { return state; }
            set
            {
                state = value;
                NotifyPropertyChanged("State");
                NotifyPropertyChanged("Color");
            }
        }

        private CellState nextState;

        public Color Color
        {
            get { return state == CellState.Dead ? Colors.Black : Colors.White; }
        }

        private void CalculateNextState()
        {
            System.Console.WriteLine("Calcul");
            int counter = 0;
            foreach (Cell neighbor in Neighbors)
            {
                if (neighbor != null)
                {
                    if (neighbor.State == CellState.Alive)
                    {
                        counter++;
                    }
                }
            }
            if (State == CellState.Alive && counter < 2)
            {
                nextState = CellState.Dead;
            }
            if (State == CellState.Alive && counter > 3)
            {
                nextState = CellState.Dead;
            }
            if (State == CellState.Alive && (counter == 2 || counter == 3))
            {
                nextState = CellState.Alive;
            }
            if (State == CellState.Dead && counter == 3)
            {
                nextState = CellState.Alive;
            }

            action = true;
        }

        private void Update()
        {
            State = nextState;
            action = false;
        }

        public Cell()
        {
            action = false;
            State = CellState.Dead;
            neighbors = new Cell[8];

            Timer.Elapsed += OnTimedEvent;
            Timer.Enabled = false;
            Timer.AutoReset = true;
        }

        public Cell(CellState state) : this()
        {
            State = state;
        }

        public void ActivateTimer()
        {
            Timer.Enabled = true;
        }
    }

    public class ViewModel
    {
        public ObservableCollection<Cell> Cells { get; } = new ObservableCollection<Cell>();

        private int FindCellIndex(int x, int y)
        {
            int gridSize = (int)Math.Sqrt(Cells.Count);
            if (x < 0 || y < 0 || x >= gridSize || y >= gridSize)
            {
                return -1;
            }
            int index = y * gridSize + x;
            return index;
        }

        private Coordinate FindCellCoordinates(Cell cell)
        {
            Coordinate coordinate = new Coordinate();
            int gridSize = (int)Math.Sqrt(Cells.Count); //Trebuie sa am mereu valori intregi, deci numarul de celule e mereu patrat perfect
            coordinate.X = Cells.IndexOf(cell) % gridSize;
            coordinate.Y = Cells.IndexOf(cell) / gridSize;
            return coordinate;
        }

        private void BindCellNeighbors(Cell cell)
        {
            Coordinate[] directions = new Coordinate[8]
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
            Coordinate cellCoordinate = FindCellCoordinates(cell);
     
            Cell[] neighbors = new Cell[8];

            for (int i = 0; i < 8; i++)
            {
                int neighborX = cellCoordinate.X + directions[i].X;
                int neighborY = cellCoordinate.Y + directions[i].Y;
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
    }
}
