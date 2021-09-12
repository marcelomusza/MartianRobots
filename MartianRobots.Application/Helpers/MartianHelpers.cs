using MartianRobots.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Application.Helpers
{
    public static class MartianHelpers
    {
        public static GridCell[,] InitializeMarsField(GridCell[,] grid)
        {
            //Helper Method to Initialize each Cell in the Grid
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int column = 0; column < grid.GetLength(1); column++)
                {                    
                    grid[row, column] = new GridCell();
                }
            }

            return grid;
        }
    }
}
