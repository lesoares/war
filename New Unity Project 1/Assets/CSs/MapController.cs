using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Vector3 defaultCameraPosition = new Vector3(202, -99.4f, 155.6f);
    public int defaultCameraSize = 350;
    public static bool isZoomActivated = false;
    public GameObject cameraGO;
    PolygonCollider2D[] cp;
    // Use this for initialization
    void Start()
    {
        //pego todos os colliders dos filhos do objeto
        cp = this.GetComponentsInChildren<PolygonCollider2D>();
        //seto todos para false de modo que possa clicar no mapa sem clicar em nenhum território específico
        foreach (var item in cp)
        {
            item.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isZoomActivated)
            {
                SendMessage("OnMouseDown");
            }
        }

    }

    public void resetCamera()
    {
        var camera = cameraGO.GetComponent<Camera>() as Camera;
        camera.orthographicSize = defaultCameraSize;
        cameraGO.transform.position = defaultCameraPosition;
        isZoomActivated = false;
    }

    /// <summary>
    /// No evento de clique do mouse, verifico se o zoom já está ativo ou não
    /// e caso não esteja, aplico o zoom e religo os colliders dos filhos do meu objeto. Depois
    /// reposiciono a camera e mudo seu FOV.
    /// Caso esteja, desligo os colliders e volto a camera ao original
    /// </summary>
    void OnMouseDown()
    {
        Debug.Log("Mouse");
        var camera = cameraGO.GetComponent<Camera>() as Camera;
        var mouse = Input.mousePosition;
        Debug.Log(mouse);

        if (!isZoomActivated)
        {
            
            cameraGO.transform.position = new Vector3 (Camera.main.ScreenToWorldPoint(mouse).x,
                Camera.main.ScreenToWorldPoint(mouse).y,
                cameraGO.transform.position.z);
            isZoomActivated = true;
            camera.orthographicSize = 80;

            this.GetComponent<BoxCollider2D>().enabled = false;

            foreach (var item in cp)
            {
                item.enabled = true;
            }
        }
        else
        {
            foreach (var item in cp)
            {
                item.enabled = false;
            }

            this.GetComponent<BoxCollider2D>().enabled = true;

            resetCamera();
        }
    }
}
