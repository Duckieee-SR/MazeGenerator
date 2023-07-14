using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject clearText;
    public TMPro.TMP_InputField widthInput;
    public TMPro.TMP_InputField heightInput;
    private GameObject[] maze = null;
    private GameObject mazeGenerator = null;
    private int width;
    private int height;


    private void Start() 
    {
        mazeGenerator = GameObject.Find("Maze Generator");
    }
    
    private void Update()
    {
        maze = GameObject.FindGameObjectsWithTag("Maze");
    }

    //Garrantees the maze is cleared before generating a new one
    //(had a problem with destroying the maze and rebuilding with the same button press)
    public void ButtonPress()
    {
        Debug.Log("maze.Length = " + maze.Length);
        if (0 == maze.Length)
        {
            GenerateUI();
        }
        else
        {
            clearText.SetActive(true);
        }
    }

    public void RecursiveAlgoButton()
    {
        Debug.Log("maze.Length = " + maze.Length);
        if (0 == maze.Length)
        {
            GenerateRecursiveUI();
        }
        else
        {
            clearText.SetActive(true);
        }
    }

    //Destroys the maze
    public void DestroyMaze()
    {
        clearText.SetActive(false);
        for (int i = 0; i < maze.Length; i++)
        {
            Destroy(maze[i]);
        }
    }

    //Calls on the maze generator 1 only if the input fields inputs are valid
    private void GenerateUI()
    {
        width = int.Parse(widthInput.text);
        height = int.Parse(heightInput.text);
        if (width >= 10 && width <= 250 && height >= 10 && height <= 250)
        {
            mazeGenerator.GetComponent<Maze_Generator>().GenerateMaze(width, height);
        }
    }

    //Calls on the maze generator 2 only if the input fields inputs are valid
    private void GenerateRecursiveUI()
    {
        width = int.Parse(widthInput.text);
        height = int.Parse(heightInput.text);
        if (width >= 10 && width <=250 && height >= 10 && height <= 250)
        {
            Maze_Generator2.Generate(width, height);
            mazeGenerator.GetComponent<Maze_Renderer>().StartGenerating(width, height);
        }
    }

    //Destroys the tutorial window when the button is pressed
    public void DestroyTutorial()
    {
        GameObject[] tutorial = GameObject.FindGameObjectsWithTag("Tutorial");

        for (int i = 0; i < tutorial.Length; i++)
        {
            Destroy(tutorial[i]);
        }
    }
    //Starts the Game Mode where the player can move around
    public void GameMode()
    {
        width = int.Parse(widthInput.text);
        height = int.Parse(heightInput.text);
        var gameButton = GameObject.FindGameObjectWithTag("Game Button");

        if (maze.Length != 0)
        {
            Destroy(gameButton);
            GameObject.Find("Game Mode").GetComponent<Game_Mode>().StartGame(width, height);
        }
    }

    /*
        * The following code is used to hide the UI through an inactive button
        * It is not used in the final version of the game but may be usefull in the future
        

    public void HideUI()
    {
        GameObject[] hideUI = GameObject.FindGameObjectsWithTag("UI");
        for (int i = 0; i < hideUI.Length; i++)
        {
            hideUI[i].SetActive(false);
        }
    }
    */
}
