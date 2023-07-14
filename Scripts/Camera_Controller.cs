using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private float zoomMax = 300f;
    [SerializeField] private float zoomMin = 10f;
    [SerializeField] private float zoomFactor = 3f;
    private bool dragMoveActive = false;
    private Vector2 lastMousePosition;
    private float moveSpeed = 4f;
    private float dragSpeed = 2f;
    private float targetZoom;

    private void Update()
    {
        MoveCamera();
        ZoomCamera();
    }

    //Moves the camera with dragging the mouse after clicking the right mouse button
    private void MoveCamera()
    {
        var inputDir = new Vector2(0, 0);
        if (Input.GetMouseButtonDown(1))
        {
            dragMoveActive = true;
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            dragMoveActive = false;
        }
        if (dragMoveActive)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;
            inputDir.x = mouseMovementDelta.x * -dragSpeed;
            inputDir.y = mouseMovementDelta.y * -dragSpeed;
            lastMousePosition = Input.mousePosition;
        }

        Vector2 moveDir = transform.right * inputDir.x + transform.up * inputDir.y;
        cam.transform.position += (Vector3)moveDir * moveSpeed * Time.deltaTime;
    }

    //Zooms the camera with the mouse wheel
    private void ZoomCamera()
    {
        if (Input.mouseScrollDelta.y > 0) 
        {
            targetZoom -= zoomFactor; 
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetZoom += zoomFactor;
        }

        targetZoom = Mathf.Clamp(targetZoom, zoomMin, zoomMax);
        cam.GetComponent<Camera>().orthographicSize = targetZoom;
    }
}
