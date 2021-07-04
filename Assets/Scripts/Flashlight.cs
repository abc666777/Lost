using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{

	private bool flashlightCon;
	public GameObject lightObj;
	private float maxEnergy = 50;
	private int battery;
	private float currentEnergy;

	public TextMeshProUGUI energyCountGUI;
	public TextMeshProUGUI batteryCountGUI;
	public TextMeshProUGUI tutorialGUI;

    public AudioSource audio;
    public AudioClip _switch;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

        currentEnergy = maxEnergy;
        battery = 1;
        tutorialGUI.gameObject.SetActive(true);
        tutorialGUI.text = "Press W A S D to walk\nPress M1 to interact with object\nPress F to turn on/off\nPress R to load a new battery\nthis will deplete the old battery that stored inside and replace with a new one.";
        StartCoroutine(delay());
    }

    // Update is called once per frame
    void Update()
    {
        energyCountGUI.text = (Mathf.RoundToInt((currentEnergy * 100)/maxEnergy)) + "%";
        batteryCountGUI.text = battery + "";
        if(Input.GetKeyDown(KeyCode.F)){
            audio.PlayOneShot(_switch, 0.3f);
            flashlightCon = !flashlightCon;
        }

        if(Input.GetKeyDown(KeyCode.R) && battery != 0){
        	currentEnergy = maxEnergy;
        	battery -= 1;
        }

        if(flashlightCon){

        	if(currentEnergy <= 0){
        		currentEnergy = 0;
        		lightObj.SetActive(false);
        	}

        	if(currentEnergy > 0){
        		lightObj.SetActive(true);
        		currentEnergy -= 0.5f * Time.deltaTime;
        	}
        }
        else{
        	lightObj.SetActive(false);
        }
    }
    public void AddBattery(){
    	battery += 1;
    }

    IEnumerator delay(){
        yield return new WaitForSeconds(8.5f);
        tutorialGUI.gameObject.SetActive(false);
    }
}
