namespace matchPuzzle.MVCS.model.level
{
    public struct Point
    {
        public readonly int x;
        public readonly int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object other)
        {
            if (other is Point) {
                Point casted = (Point) other;
                return x == casted.x && y == casted.y;
            }
            else {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        public string ToString()
        {
            return base.ToString() + string.Format("[ x = {0}, y = {1} ]", x, y);
        }
    }
}
