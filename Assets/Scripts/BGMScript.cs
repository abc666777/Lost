using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMScript : MonoBehaviour
{
	private static BGMScript instance;
	public AudioSource audio;
	public AudioClip menuSong;
	public AudioClip ingameSong;
	public AudioClip chaseSong;
  public AudioClip chaseSongFaster;
	//private bool inChase = false;
    // Start is called before the first frame update
    void Awake()
    {
    	audio = GetComponent<AudioSource>();
    	if(instance != null){
    		Destroy(gameObject);
    	}
    	else{
        	DontDestroyOnLoad(transform.gameObject);
        	instance = this;
        }
    }

    void Start(){
    }

    // Update is called once per frame
    void Update()
    {
    	if(SceneManager.GetActiveScene().name == "Prologue" && Input.GetMouseButtonDown(0)){
    		StartCoroutine("delay");
      }
      if(SceneManager.GetActiveScene().name != "PlayZone"){
        Cursor.visible = true;
      }
      if (Input.GetKeyDown(KeyCode.Escape)){
        Application.Quit();
      }
    }

    public void toggleSong(string song){
    	if(song == "menu" && audio.clip != menuSong){
    		audio.clip = menuSong;
        audio.Play();
    	}
    	else if(song == "ingame" && audio.clip != ingameSong){
    		audio.clip = ingameSong;
        audio.Play();
    	}
    	else if(song == "chase" && (audio.clip != chaseSong)){
    		audio.clip = chaseSong;
        audio.Play();
    	}
      else if(song =="chaseFast" && audio.clip != chaseSongFaster){
        audio.clip = chaseSongFaster;
        audio.Play();
      }
    }

    public void fadeOut(){
      StartCoroutine("delay");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
    	if(scene.name == "Start"){
        StopCoroutine("delay");
    		audio.volume = 1.0f;
    		audio.clip = menuSong;
        audio.Play();
    	}
      
      if(scene.name == "PlayZone"){
         Cursor.visible = false;
         StopCoroutine("delay");
         audio.volume = 1.0f;
         audio.clip = ingameSong;
         audio.Play();
         }
    }

    private void OnSceneUnloaded(Scene current)
    {

    }


    void OnEnable(){
         SceneManager.sceneLoaded += OnSceneLoaded;
         if(SceneManager.GetActiveScene().name == "PlayZone" || SceneManager.GetActiveScene().name == "Start"){
         	StopCoroutine("delay");
         }
  }

  	void OnDisable(){
  		SceneManager.sceneLoaded -= OnSceneLoaded;
  	}

    IEnumerator delay(){
    	while(true){
    		yield return new WaitForSeconds(1.0f);
    		audio.volume -= Time.deltaTime * 10;
        print(audio.volume);
    	}
    }

}
