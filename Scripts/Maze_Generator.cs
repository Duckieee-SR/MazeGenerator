using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Maze_Generator : MonoBehaviour
{
    [SerializeField] private GameObject wallPref;
    [SerializeField] private GameObject startPref;
    [SerializeField] private GameObject endPref;
    [SerializeField] private GameObject floorPref;
    [SerializeField] private bool existsFloor = true;

    //Generates the floor, the starting and the ending cells of the maze
    public void GenerateMaze(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 && y == 0)
                {
                    var start = Instantiate(startPref, transform);
                    start.transform.position = new Vector3(x, y, 0);
                }
                else if (height % 2 == 0 && x == 0 && y == height - 1)
                {
                    var end = Instantiate(endPref, transform);
                    end.transform.position = new Vector3(0, height - 1, 0);
                }
                else if (height % 2 != 0 && x == width - 1 && y == height - 1)
                {
                    var end2 = Instantiate(endPref, transform);
                    end2.transform.position = new Vector3(width - 1, height - 1, 0);
                }
                else if (existsFloor == true)
                {
                    var floor = Instantiate(floorPref, transform);
                    floor.transform.position = new Vector3(x, y, 0);
                }
            }
        }
        GenerateOuterWalls(width, height);
        GenerateInnerWalls(width, height);
    }

    //Generates the outer walls of the maze
    public void GenerateOuterWalls(int width, int height)
    {
        
        for (int x = width - 1; x >= 0; x--)
        {
            if (x == width)
            {
                var horizWall = Instantiate(wallPref, transform);
                horizWall.transform.position = new Vector3(x, height - 0.5f, 0);
            }
            else if (x == 0)
            {
                var horizWall2 = Instantiate(wallPref, transform);
                horizWall2.transform.position = new Vector3(x, -0.5f, 0);
                var horizWall3 = Instantiate(wallPref, transform);
                horizWall3.transform.position = new Vector3(x, height - 0.5f, 0);
            }
            else
            {
                var horizmWall4 = Instantiate(wallPref, transform);
                horizmWall4.transform.position = new Vector3(x, height - 0.5f, 0);
                var horizWall5 = Instantiate(wallPref, transform);
                horizWall5.transform.position = new Vector3(x, -0.5f, 0);
            }
        }
        for (int y = height - 1; y >= 0; y--)
        {
            if (y == height)
            {
                var vertWall = Instantiate(wallPref, transform);
                vertWall.transform.position = new Vector3(width - 0.5f, y, 0);
                vertWall.transform.eulerAngles = new Vector3(0, 0, 90);
            }
            else if (y == 0)
            {
                var vertWall2 = Instantiate(wallPref, transform);
                vertWall2.transform.position = new Vector3(-0.5f, y, 0);
                vertWall2.transform.eulerAngles = new Vector3(0, 0, 90);
                var vertWall3 = Instantiate(wallPref, transform);
                vertWall3.transform.position = new Vector3(width - 0.5f, y, 0);
                vertWall3.transform.eulerAngles = new Vector3(0, 0, 90);
            }
            else
            {
                var vertWall4 = Instantiate(wallPref, transform);
                vertWall4.transform.position = new Vector3(width - 0.5f, y, 0);
                vertWall4.transform.eulerAngles = new Vector3(0, 0, 90);
                var vertWall5 = Instantiate(wallPref, transform);
                vertWall5.transform.position = new Vector3(-0.5f, y, 0);
                vertWall5.transform.eulerAngles = new Vector3(0, 0, 90);
            }
        }
    }

    //Generates the inner walls of the maze
    void GenerateInnerWalls(int width, int height)
    {
        for (int x = width - 2; x >= 0; x--)
        {
            for (int y = height - 1; y >= 0; y--)
            {
                if (y % 2 != 0)
                {
                    var innerWallOdd = Instantiate(wallPref, transform);
                    innerWallOdd.transform.position = new Vector3(x, y - 0.5f, 0);
                }
                else if (y % 2 == 0)
                {
                    var innerWallEven = Instantiate(wallPref, transform);
                    innerWallEven.transform.position = new Vector3(x + 1, -0.5f, 0);
                    var innerWallEven2 = Instantiate(wallPref, transform);
                    innerWallEven2.transform.position = new Vector3(x + 1, y - 0.5f, 0);
                }
            }
        }
    }
}
