using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Game_Tutorial : MonoBehaviour
{
    //Destroys Game Mode tutorial
    public void DestroyGameTutorial()
    {
        GameObject[] gameTutorial = GameObject.FindGameObjectsWithTag("Game Tutorial");

        for (int i = 0; i < gameTutorial.Length; i++)
        {
            Destroy(gameTutorial[i]);
        }
    }
}
