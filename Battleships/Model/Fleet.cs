namespace Battleships.Model
{
    public abstract class Fleet : List<ShipLayout>
    {
    }

    public class Flotilla : Fleet
    {
        public Flotilla()
        {
            Add(new Battleship());
            Add(new Destroyer());
        }
    }

    public class Squadron : Fleet
    {
        public Squadron()
        {
            Add(new Battleship());
            Add(new Destroyer());
            Add(new Destroyer());
        }
    }

    public class Armada : Fleet
    {
        public Armada()
        {
            Add(new Battleship());
            Add(new Battleship());
            Add(new Destroyer());
            Add(new Destroyer());
            Add(new Destroyer());
        }
    }
}
