using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainUIHandlerScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenControlsMenu()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }
    public void CloseControlsMenu()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
