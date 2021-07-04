using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeadEndDoor : MonoBehaviour
{
	Animator anim;
	public Image[] hoverGUI;
    public Image hover;
    public Sprite openGUI;
    public TextMeshProUGUI info;
    private string[] arrayOfText;
    private bool isTextActive = false;

    public AudioSource audio;
    public AudioClip locked;
    // Start is called before the first frame update
    void Start()
    {
        arrayOfText = new string[]{"The lock is jammed,", "The lock is broken,", "The doorknob is broken,"};
        hoverGUI = FindObjectsOfType<Image>();
        for(int i = 0; i < hoverGUI.Length; i++){
    		if(hoverGUI[i].name == "InteractableGui"){
    			hover = hoverGUI[i];
    		}
    	}
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        openGUI = Resources.Load<Sprite>("Sprite Assets/Icon/OpenIcon") as Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver(){
    	if(checkDistance()){
            hover.sprite = openGUI;
    		hover.enabled = true;
    	}
    }

    private void OnMouseExit(){
    	hover.enabled = false;
    }

    private void OnMouseDown(){
    	if(checkDistance()){
            audio.PlayOneShot(locked, 0.7f);
            if(isTextActive == false){
                isTextActive = true;
                info.text = arrayOfText[Random.Range(0, 3)] + " can't open it.";
                info.gameObject.SetActive(true);
                StartCoroutine(delay());
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
