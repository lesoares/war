using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Vector3 defaultCameraPosition = new Vector3(202, -99.4f, 155.6f);
    public int defaultCameraSize = 8;
    public bool isZoomActivated = false;
    public Camera camera;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Debug.Log("Mouse");
        
        var mouse = Input.mousePosition;
        Debug.Log(mouse);
        if (!isZoomActivated)
        {
            camera.orthographicSize = 2;
            transform.position = new Vector3 (Camera.main.ScreenToWorldPoint(mouse).x, Camera.main.ScreenToWorldPoint(mouse).y, transform.position.z);
            isZoomActivated = true;
        }
        else
        {
            camera.orthographicSize = defaultCameraSize;
            //transform.position = defaultCameraPosition;
            isZoomActivated = false;
        }
    }
}
