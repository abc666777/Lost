using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Paper : MonoBehaviour
{
	public Image[] hoverGUI;
    public Image hover;
    public Image showImage;
    public Sprite pickupGUI;
    public TextMeshProUGUI info;
    public GUIManager guiManager;
    public GameObject player;

   	public AudioSource audio;
    public AudioClip pickup;
    public AudioClip putdown;
    // Start is called before the first frame update
    void Start()
    {
    	player = GameObject.Find("Player");
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
    	if((Input.GetMouseButtonDown(1)) && showImage.enabled == true){
    		HideImage();
    	}
    }

    private void OnMouseEnter(){
    	if(checkDistance()){
            if(guiManager.GetPaperCheck()){
                guiManager.SetText("Notes are paper containing clue that scattered around the apartment.\nPress M1 to pick up the note.\nPress M2 to put down the note.");
                guiManager.SetPaperCheck(false);
            }
            hover.sprite = pickupGUI;
    		hover.enabled = true;
    	}
    }

    private void OnMouseExit(){
    	hover.enabled = false;
    }

    private void OnMouseDown(){
    	if(showImage.enabled == false){
    		if(checkDistance()){
    			ShowImage();
    		}
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

    private void ShowImage(){
    	showImage.enabled = true;
    	audio.PlayOneShot(pickup);
    	player.GetComponent<PlayerMovement>().enabled = false;
    }

    private void HideImage(){
    	showImage.enabled = false;
    	audio.PlayOneShot(putdown);
    	player.GetComponent<PlayerMovement>().enabled = true;
    }
}
