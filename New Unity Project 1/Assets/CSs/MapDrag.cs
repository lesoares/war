using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.CSs;

public class MapDrag : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    public GameObject map;
    public static bool isDragging = false;

    void Update()
    {
        if (MapController.isZoomActivated)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(0))    return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x * dragSpeed * Time.deltaTime, pos.y * dragSpeed * Time.deltaTime, 0);

            transform.Translate(move, Space.World);
        }
        
    }


}
