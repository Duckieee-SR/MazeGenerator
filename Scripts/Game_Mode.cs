using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game_Mode : MonoBehaviour
{
    [SerializeField] private GameObject playerPref;
    [SerializeField] private GameObject tutorialPref;
    public void StartGame(int width, int height)
    {
        Instantiate(tutorialPref, transform);
        var playerPos = Instantiate(playerPref, transform);
        playerPos.transform.position = new Vector3(0, 0, 0);
    }
}
