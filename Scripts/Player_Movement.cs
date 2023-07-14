using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float speed = 5f;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)) //Up
        {
            player.transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S)) //Down
        {
            player.transform.position += new Vector3(0, -1, 0) * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D)) //Right
        {
            player.transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A)) //Left
        {
            player.transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
        }
    }

}
