using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Battery : MonoBehaviour
{
    // Start is called before the first frame update
    public Flashlight flashlight;
    public GUIManager guiManager;
    public Image[] hoverGUI;
    public Image hover;
    public Sprite pickupGUI;
    public TextMeshProUGUI info;

    public AudioSource audio;
    public AudioClip pickup;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    	flashlight = GameObject.Find("Flashlight").GetComponent<Flashlight>();
    	guiManager = GameObject.Find("Manager").GetComponent<GUIManager>();
    	hoverGUI = FindObjectsOfType<Image>();
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
    		if(guiManager.GetBatteryCheck()){
    			guiManager.SetText("Batteries is an equipment use to power a flashlight. Be wary that when you load a new one, the old one will be depleted and gone forever.");
    			guiManager.SetBatteryCheck(false);
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
    		hover.enabled = false;
    		flashlight.AddBattery();
    		info.gameObject.SetActive(true);
    		info.text = "I got a battery.";
    		gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
    		StartCoroutine(delayForBattery());
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

    IEnumerator delayForBattery(){
        yield return new WaitForSeconds(3.0f);
        info.gameObject.SetActive(false);
        Destroy(transform.parent.gameObject);
    }
}
