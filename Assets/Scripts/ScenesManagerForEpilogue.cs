﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenesManagerForEpilogue: MonoBehaviour
{
	public ScenesManager sceneMGR;
    public GameObject faderObj;
	private Image fader;
    // Start is called before the first frame update
    void Start()
    {
    	fader = faderObj.GetComponent<Image>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
        	faderObj.gameObject.SetActive(true);
    		StartCoroutine("delay");
        }
    }

    IEnumerator delay(){
    	yield return new WaitForSeconds(1.0f);
    	while(true){
    		yield return new WaitForSeconds(0.05f);
    		fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, fader.color.a + Time.deltaTime);
    		if(fader.color.a >= 1){
    			yield return new WaitForSeconds(1.0f);
    			sceneMGR.goToEnding();
    		}
    	}
    }
}
