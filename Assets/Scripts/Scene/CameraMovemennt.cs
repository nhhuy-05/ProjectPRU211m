using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMovemennt : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float zoomStep, minCamSize, maxCamSize;

    [SerializeField]
    private TilemapRenderer tilemapRenderer;

    private Vector3 dragOrigin;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;
    private Vector3 Origin = new Vector3(-7, -9, -10);

    private void Awake()
    {
        mapMinX = tilemapRenderer.transform.position.x - tilemapRenderer.bounds.size.x / 2f;
        mapMaxX = tilemapRenderer.transform.position.x + tilemapRenderer.bounds.size.x / 2f;

        mapMinY = tilemapRenderer.transform.position.y - tilemapRenderer.bounds.size.y / 2f;
        mapMaxY = tilemapRenderer.transform.position.y + tilemapRenderer.bounds.size.y / 2f;
    }


    private void Update()
    {
        if (cam.orthographicSize > 18.5)
        {
            cam.orthographicSize = 29.4f;
        }
        PanCamera();
    }

    private void PanCamera()
    {
        //save position of mouse in world space 
        if (cam.orthographicSize <= 18.5)
        {

            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
                cam.transform.position = ClampCamera(cam.transform.position + difference);
            }

        }
    }

    public void ZoomIn()
    {

        if (cam.orthographicSize == 29.4f)
        {
            zoomStep = 10.9f;
        }
        float newSize = cam.orthographicSize - zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
        cam.transform.position = ClampCamera(cam.transform.position);
        zoomStep = 1;
    }
    public void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoomStep;
        if (newSize>18.1)
        {
            cam.transform.position = Origin;
        }
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
        cam.transform.position = ClampCamera(cam.transform.position);

    }

    private Vector3 ClampCamera(Vector3 targetPositon)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;

        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPositon.x, minX, maxX);
        float newY = Mathf.Clamp(targetPositon.y, minY, maxY);

        return new Vector3(newX, newY, targetPositon.z);
    }
}
