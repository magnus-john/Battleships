﻿namespace Battleships.Model
{
    public readonly struct Point(int x, int y)
    {
        public int X => x;
        public int Y => y;
    }
}
