﻿using Battleships.Model;

namespace Battleships.Tests.Helpers
{
    public static class Points
    {
        public static Point OutOfBounds = new(-1, -1);
        public static Point SecondRow = new(0, 1);
        public static Point TopLeft = new(0, 0);
    }
}
