using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    public Image[] hoverGUI;
    public Image hover;
    public Sprite pickupGUI;
    public string keyName;
    public TextMeshProUGUI info;
    public GUIManager guiManager;

    public AudioSource audio;
    public AudioClip pickup;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    	hoverGUI = FindObjectsOfType<Image>();
        guiManager = GameObject.Find("Manager").GetComponent<GUIManager>();
    	for(int i = 0; i < hoverGUI.Length; i++){
    		if(hoverGUI[i].name == "InteractableGui"){
    			hover = hoverGUI[i];
    		}
    	}
        pickupGUI = Resources.Load<Sprite>("Sprite Assets/Icon/OpenIcon") as Sprite;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter(){
    	if(checkDistance()){
            if(guiManager.GetKeyCheck()){
                guiManager.SetText("Keys are an item that used to unlock a LOCKED door. You must find out which key is used for which door.");
                guiManager.SetKeyCheck(false);
            }
            hover.sprite = pickupGUI;
    		hover.enabled = true;
    	}
    }

    private void OnMouseExit(){
    	hover.enabled = false;
    }

    private void OnMouseDown(){
    	if(checkDistance()){
            audio.PlayOneShot(pickup, 1f);
            info.text = "I got the " + keyName + '.';
    		hover.enabled = false;
            info.gameObject.SetActive(true);
            gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(delay());
            
    	}
    }
    private void OnMouseUp(){
    	hover.enabled = false;
    }

    private bool checkDistance(){
    	float distance = (Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position));
    	if(distance <= 1.5f){
    		return true;
    	}
    	else{
    		return false;
    	}
    }

    IEnumerator delay(){
        yield return new WaitForSeconds(3.0f);
        info.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
