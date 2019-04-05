namespace GameOfLife
{
    public enum Direction
    {
        NW, N, NE, E, SE, S, SW, W
    }

    struct Coordinate
    {
        public int X;
        public int Y;

        public static Coordinate[] Directions = new Coordinate[8]
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
            X = x;
            Y = y;
        }
    }

}
