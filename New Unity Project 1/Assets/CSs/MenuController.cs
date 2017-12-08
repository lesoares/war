using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public List<CanvasGroup> canvasGroup;
    public GameObject credits;
    public GameObject instructions;
    // Use this for initialization
    void Start () {
               
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartGame  ()
    {
        Debug.Log("Jogar");
        Permanente.Resete();
        //Application.LoadLevel(1);
        SceneManager.LoadScene(1);
    }

    void OpenCredits()
    {
        Debug.Log("Créditos");
        SendMessage("ChangeToCredits");
    }

    void HowToPlay()
    {
        Debug.Log("Como Jogar");
        SendMessage("ChangeToInstructions");

    }

    void ExitGame()
	{
	    #if UNITY_EDITOR
	            UnityEditor.EditorApplication.isPlaying = false;
	    #elif UNITY_WEBPLAYER
	                Application.OpenURL(webplayerQuitURL);
	    #else
	                Application.Quit();
	    #endif
	}

    void ChangeToCredits()
    {
        var cG = credits.GetComponent<CanvasGroup>() as CanvasGroup;
        cG.alpha = cG.alpha == 1? 0 : 1;
        var iG = instructions.GetComponent<CanvasGroup>() as CanvasGroup;
        iG.alpha = 0;
    }

    void ChangeToInstructions()
    {
        var iG = instructions.GetComponent<CanvasGroup>() as CanvasGroup;
        iG.alpha = iG.alpha == 1 ? 0 : 1;
        var cG = credits.GetComponent<CanvasGroup>() as CanvasGroup;
        cG.alpha = 0;
    }

    void ChangeToMenu()
    {
        foreach (var item in canvasGroup)
        {
            item.alpha = 1;
            item.blocksRaycasts = true;
        }

        var cG = credits.GetComponent<CanvasGroup>() as CanvasGroup;
        cG.alpha = 0;
        cG.blocksRaycasts = false;
        var sR = credits.GetComponent<SpriteRenderer>() as SpriteRenderer;
        sR.enabled = false;

    }
}
