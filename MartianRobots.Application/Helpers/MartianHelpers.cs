using MartianRobots.Application.DTOs;
using MartianRobots.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Application.Helpers
{
    public static class MartianHelpers
    {
        public static GridCellDTO[,] InitializeMarsField(GridCellDTO[,] grid)
        {
            //Helper Method to Initialize each Cell in the Grid
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int column = 0; column < grid.GetLength(1); column++)
                {                    
                    grid[row, column] = new GridCellDTO();
                }
            }

            return grid;
        }

        public static bool PositionIsEqualTo(Position previousPos, Position nextPos)
        {
            return (previousPos.X == nextPos.X && previousPos.Y == nextPos.Y);
        }
    }
}
