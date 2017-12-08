using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.CSs;

public class MapDrag : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    public GameObject map;
    public Vector3 defaultCameraPosition = new Vector3(202, -99.4f, 155.6f);

    public float zoomSpeed = 1;
    public float targetOrtho;
    public float smoothSpeed = 2.0f;
    public float minOrtho = 1.0f;
    public float maxOrtho = 20.0f;
    public float zoom = 350;

    void Start()
    {
        targetOrtho = Camera.main.orthographicSize;
        //seto todos para false de modo que possa clicar no mapa sem clicar em nenhum território específico
        foreach (var item in map.GetComponentsInChildren<PolygonCollider2D>()) {
            item.enabled = true;
        }
    }

    public void resetCamera()
    {
        Camera.main.orthographicSize = maxOrtho;
        Camera.main.transform.position = defaultCameraPosition;
        targetOrtho = Camera.main.orthographicSize;
    }

    void Update()
    {
        /*
        if (MapController.isZoomActivated)
        {
            
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f) {
            targetOrtho -= scroll * zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
        }

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
        */
        if (Input.GetKeyDown(KeyCode.Escape)) {
            resetCamera();
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f) {
            targetOrtho -= scroll * zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
        }
        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(2)) {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(2)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(-pos.x * dragSpeed * Time.deltaTime,- pos.y * dragSpeed * Time.deltaTime, 0);

        map.transform.Translate(move, Space.World);

    }

    

}
