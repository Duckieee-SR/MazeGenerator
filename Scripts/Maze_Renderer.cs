using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze_Renderer : MonoBehaviour
{
    [SerializeField] private GameObject wallPref = null;
    [SerializeField] private GameObject startPref = null;
    [SerializeField] private GameObject endPref = null;
    [SerializeField] private GameObject floorPref = null;
    [SerializeField] private float size = 1f;
    [SerializeField] private bool existsFloor = true;

    public void StartGenerating(int width, int height)
    {
        var maze = Maze_Generator2.Generate(width, height);
        Draw(maze, width, height);
        CreateFoor(width, height);
    }

    //Instantiates necessary walls on the position they are called
    private void Draw(WallState[,] maze, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cell = maze[x, y];
                var pos = new Vector3(-width/2 + x, -height/2 + y, 0);

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPref, transform);
                    topWall.transform.position = pos + new Vector3(0, size/2, 0);
                }
                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPref, transform);
                    leftWall.transform.position = pos + new Vector3(-size / 2, 0, 0);
                    leftWall.transform.eulerAngles = new Vector3(0, 0, 90);
                }
                if(x == width - 1 )
                {
                    if(cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPref, transform);
                        rightWall.transform.position = pos + new Vector3(size / 2, 0, 0);
                        rightWall.transform.eulerAngles = new Vector3(0, 0, 90);
                    }
                }
                if (y == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPref, transform);
                        bottomWall.transform.position = pos + new Vector3(0, -size / 2, 0);
                    }
                }
            }
        }
        
    }

    //Generates the floor, the starting and the ending cells of the maze
    public void CreateFoor(int width, int height)
    {
        var endPos = new Vector3(Random.Range(0, width - 1),Random.Range(0, height - 1),0);
        var startPos = new Vector3(Random.Range(0, width - 1), height - 1, 0);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (startPos.x == x && startPos.y == y)
                {
                   var startCell = Instantiate(startPref, transform);
                   startCell.transform.position = new Vector3(-width / 2 + x, -height / 2 + y, 0);
                }
                else if (endPos.x == x && endPos.y == y)
                {
                    var endCell = Instantiate(endPref, transform);
                    endCell.transform.position = new Vector3(-width / 2 + x, -height / 2 + y, 0);
                }
                else if (existsFloor == true)
                {
                    var floorCell = Instantiate(floorPref, transform);
                    floorCell.transform.position = new Vector3(-width / 2 + x, -height / 2 + y, 0);
                }
            }
        }
    }
}
