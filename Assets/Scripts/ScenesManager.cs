using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name != "PlayZone"){
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goToStart(){
    	SceneManager.LoadScene("Start", LoadSceneMode.Single);
    }

    public void goToPrologue(){
    	SceneManager.LoadScene("Prologue", LoadSceneMode.Single);
    }

    public void goToPlay(){
    	SceneManager.LoadScene("PlayZone", LoadSceneMode.Single);
    }

    public void goToDeathScene(){
    	SceneManager.LoadScene("Game Over", LoadSceneMode.Single);
    }

    public void goToEpilogue(){
    	SceneManager.LoadScene("Epilogue", LoadSceneMode.Single);
    }

    public void goToEnding(){
    	SceneManager.LoadScene("Ending", LoadSceneMode.Single);
    }

    public void quit(){
    	Application.Quit();
    }
}
