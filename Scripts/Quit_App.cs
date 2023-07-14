using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_App : MonoBehaviour
{
   public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Quit.Quitter();
        }
    }

}
public static class Quit
{
    public static void Quitter()
    {
    #if UNITY_STANDALONE
        Application.Quit();
    #endif
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
