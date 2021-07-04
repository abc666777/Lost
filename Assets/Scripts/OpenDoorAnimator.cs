using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenDoorAnimator : MonoBehaviour
{
	Animator anim;
	public Image[] hoverGUI;
    public Image hover;
    public Sprite openGUI;
    public TextMeshProUGUI tutorial;

    public AudioSource audio;
    public AudioClip open;
    public AudioClip close;
    // Start is called before the first frame update
    void Start()
    {
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
            if(!anim.GetBool("isOpen")){audio.PlayOneShot(open, 0.7f);}
            else{audio.PlayOneShot(close, 0.7f);}
    		anim.SetBool("isOpen", !anim.GetBool("isOpen"));
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
}
