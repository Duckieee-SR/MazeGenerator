using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum WallState
{
    UP = 1,
    DOWN = 2,
    LEFT = 4,
    RIGHT = 8,
    VISITED = 128,
}

public struct Position
{
    public int x;
    public int y;
}

public struct NextCell
{
    public Position posNextCell;
    public WallState sharedWall;
}
public static class Maze_Generator2
{
    private static WallState GetOppositeWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.UP:
                return WallState.DOWN;
            case WallState.DOWN:
                return WallState.UP;
            case WallState.LEFT:
                return WallState.RIGHT;
            case WallState.RIGHT:
                return WallState.LEFT;
            default:
                return WallState.UP;
        }
    }

    private static WallState[,] RecursiveBacktracking(WallState[,] maze, int width, int height)
    {
        var rng = new System.Random();
        var position = new Position { x = rng.Next(0, width), y = rng.Next(0, height) };
        var stack = new Stack<Position>();

        maze[position.x, position.y] |= WallState.VISITED;                      // |= adds the bits associated with visited flag to the cell
        stack.Push(position); 
        while (stack.Count > 0)
        {
            var current = stack.Pop();                                          //Start at current cell position
            var nextCell = GetUnvisitedCells(current, maze, width, height);     //Get all unvisited cells around current cell

            if (nextCell.Count > 0)
            {
                stack.Push(current);

                var randIndex = rng.Next(0, nextCell.Count);                    //Pick a random unvisited cell
                var randNextCell = nextCell[randIndex];                         //Get the random unvisited cell
                var nextPosition = randNextCell.posNextCell;                    //Get the position of the random unvisited cell

                maze[current.x, current.y] &= ~randNextCell.sharedWall;         //&= removes the bits associated with the wall and ~ flips the bits
                maze[nextPosition.x, nextPosition.y] &= ~GetOppositeWall(randNextCell.sharedWall);
                maze[nextPosition.x, nextPosition.y] |= WallState.VISITED;
                stack.Push(nextPosition);                                       //Push the random unvisited cell to the stack and repeat
            }
        }
        return maze;
    }
    private static List<NextCell> GetUnvisitedCells(Position pos, WallState[,] maze, int width, int height)
    {
        var list = new List<NextCell>();
        
        if (pos.x > 0)  //left
        {
            if (!maze[pos.x - 1, pos.y].HasFlag(WallState.VISITED))
            {
                list.Add(new NextCell { posNextCell = new Position { x = pos.x - 1, y = pos.y }, sharedWall = WallState.LEFT });
            }
        }
        if (pos.x < width - 1)  //right
        {
            if (!maze[pos.x + 1, pos.y].HasFlag(WallState.VISITED))
            {
                list.Add(new NextCell { posNextCell = new Position { x = pos.x + 1, y = pos.y }, sharedWall = WallState.RIGHT });
            }
        }
        if (pos.y > 0)  //down
        {
            if (!maze[pos.x, pos.y - 1].HasFlag(WallState.VISITED))
            {
                list.Add(new NextCell { posNextCell = new Position { x = pos.x, y = pos.y - 1 }, sharedWall = WallState.DOWN });
            }
        }
        if (pos.y < height - 1)  //up
        {
            if (!maze[pos.x, pos.y + 1].HasFlag(WallState.VISITED))
            {
                list.Add(new NextCell { posNextCell = new Position { x = pos.x, y = pos.y + 1 }, sharedWall = WallState.UP });
            }
        }
        return list;
    }
    public static WallState[,] Generate(int width, int height)
    {
        WallState[,] maze = new WallState[width, height];
        WallState initialState = WallState.UP | WallState.DOWN | WallState.LEFT | WallState.RIGHT;
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = initialState;
            }
        }
        return RecursiveBacktracking(maze, width, height);
    }
}
