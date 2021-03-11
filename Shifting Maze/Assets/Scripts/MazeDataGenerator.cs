using System.Collections.Generic;
using UnityEngine;

public class MazeDataGenerator
{
    // Will be used by the data generation algorithm to determine whether a space is empty.
    // It is assigned a default value in the class constructor, but it's made public so that
    // other code can tune the generated maze.
    public float placementThreshold;    // chance of empty space

    public MazeDataGenerator()
    {
        placementThreshold = .1f;
    }

    public int[,] FromDimensions(int sizeRows, int sizeCols)
    {
        int[,] maze = new int[sizeRows, sizeCols];
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                // Check if the current cell is on the boundaries of the array. If so, assign 1 for wall. 
                if (i == 0 || j == 0 || i == rMax || j == cMax)
                {
                    maze[i, j] = 1;
                }

                // Checks if the coordinate is evenly divisable by 2 in order to operate on every other cell.
                else if (i % 2 == 0 && j % 2 == 0)
                {
                    // Checks against the placementThreshold value to randomly skip this cell and continue to the next cell.
                    if (Random.value > placementThreshold)
                    {
                        // Assigns 1 to current and randomly selected adjacent cell.
                        maze[i, j] = 1;

                        int a = Random.value < .5 ? 0 : (Random.value < .5 ? -1 : 1);
                        int b = a != 0 ? 0 : (Random.value < .5 ? -1 : 1);
                        maze[i + a, j + b] = 1;
                    }
                }
            }
        }
        return maze;
    }
}
