namespace Battleships.Model
{
    public abstract class ShipLayout
    {
        public const int BattleshipLength = 5;
        public const int DestroyerLength = 4;

        public abstract int Length { get; }
    }

    public class Battleship : ShipLayout
    {
        public override int Length => BattleshipLength;
    }

    public class Destroyer : ShipLayout
    {
        public override int Length => DestroyerLength;
    }
}
