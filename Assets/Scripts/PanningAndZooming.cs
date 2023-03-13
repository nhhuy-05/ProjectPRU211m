using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanningAndZooming : MonoBehaviour
{
    public float minZoomLevel = 1f;
    public float zoomSpeed = 1f;
    public float panSpeed = 10f;
    public float borderSize = 10f;

    private float leftBoundary;
    private float rightBoundary;
    private float topBoundary;
    private float bottomBoundary;
    private float maxZoomLevel;

    void Start()
    {
        // Calculate the map boundaries
        float mapWidth = 200f; // replace with your map's width
        float mapHeight = 150f; // replace with your map's height
        leftBoundary = -mapWidth / 2f + borderSize;
        rightBoundary = mapWidth / 2f - borderSize;
        topBoundary = mapHeight / 2f - borderSize;
        bottomBoundary = -mapHeight / 2f + borderSize;

        // Calculate the maximum zoom level that would allow the camera to see the entire map
        maxZoomLevel = Mathf.Max(mapWidth / (2f * Camera.main.aspect), mapHeight / 2f);
    }

    void Update()
    {
        // Handle zooming
        float zoomInput = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - zoomInput, minZoomLevel, maxZoomLevel);

        // Handle panning
        float panInputX = Input.GetAxis("Horizontal") * panSpeed;
        float panInputY = Input.GetAxis("Vertical") * panSpeed;
        Vector3 cameraPos = transform.position;
        float cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;

        if (cameraPos.x + cameraHalfWidth > rightBoundary)
        {
            cameraPos.x = rightBoundary - cameraHalfWidth;
        }
        else if (cameraPos.x - cameraHalfWidth < leftBoundary)
        {
            cameraPos.x = leftBoundary + cameraHalfWidth;
        }

        if (cameraPos.y + Camera.main.orthographicSize > topBoundary)
        {
            cameraPos.y = topBoundary - Camera.main.orthographicSize;
        }
        else if (cameraPos.y - Camera.main.orthographicSize < bottomBoundary)
        {
            cameraPos.y = bottomBoundary + Camera.main.orthographicSize;
        }

        cameraPos += new Vector3(panInputX, panInputY, 0) * Time.deltaTime;
        transform.position = cameraPos;
    }
}
