namespace Battleships.Model
{
    public readonly record struct Point(int x, int y)
    {
        public int X => x;
        public int Y => y;

        public override string ToString() => $"{x}:{y}";
    }
}
