using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockedDoor : MonoBehaviour
{
	Animator anim;
	public Image[] hoverGUI;
    public Image hover;
    public Sprite lockedGUI;
    public Sprite openGUI;
    public TextMeshProUGUI info;
    public GameObject key;
    public string keyName;
    private string[] arrayOfText;
    private bool isTextActive = false;
    private bool isLocked = true;

    public AudioSource audio;
    public AudioClip open;
    public AudioClip close;
    public AudioClip locked;
    public AudioClip unlocked;
    // Start is called before the first frame update
    void Start()
    {
        arrayOfText = new string[]{"The door is locked.", "I need a key to open it.", "I need to find a key first."};
        hoverGUI = FindObjectsOfType<Image>();
        for(int i = 0; i < hoverGUI.Length; i++){
    		if(hoverGUI[i].name == "InteractableGui"){
    			hover = hoverGUI[i];
    		}
    	}
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        openGUI = Resources.Load<Sprite>("Sprite Assets/Icon/OpenIcon") as Sprite;
        lockedGUI = Resources.Load<Sprite>("Sprite Assets/Icon/KeyIcon") as Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver(){
    	if(checkDistance()){
            if(isLocked){
                hover.sprite = lockedGUI;
            }
            else{
                hover.sprite = openGUI;
            }
            hover.enabled = true;
    	}
    }

    private void OnMouseExit(){
        hover.enabled = false;
    }

    private void OnMouseDown(){
    	if(checkDistance()){
            if(GameObject.Find(keyName) != null){ //LOCKED
                audio.PlayOneShot(locked, 0.7f);
                if(isTextActive == false){
                    isTextActive = true;
                    info.text = arrayOfText[Random.Range(0, 3)];
                    info.gameObject.SetActive(true);
                    StartCoroutine(delay());
                }
            }
            else{
                if(isLocked == true){ //HAVE KEY (UNLOCKED)
                    audio.PlayOneShot(unlocked, 0.7f);
                    hover.sprite = openGUI;
                    isLocked = false;
                    info.text = "The door is unlocked.";
                    info.gameObject.SetActive(true);
                    StartCoroutine(delay());
                }
                else{ //OPEN
                    if(!anim.GetBool("isOpen")){audio.PlayOneShot(open, 0.7f);}
                    else{audio.PlayOneShot(close, 0.7f);}
                    anim.SetBool("isOpen", !anim.GetBool("isOpen"));
                }
            }
    	}
    }

    private bool checkDistance(){
    	float distance = (Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position));
    	if(distance <= 3f){
    		return true;
    	}
    	else{
    		return false;
    	}
    }
    IEnumerator delay(){
        yield return new WaitForSeconds(3.0f);
        info.gameObject.SetActive(false);
        isTextActive = false;
    }
}
