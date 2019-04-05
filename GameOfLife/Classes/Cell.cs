using System.Windows.Media;
using System.ComponentModel;

namespace GameOfLife
{
    public enum CellState
    {
        Dead, Alive
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
}
