using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
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
    	StartCoroutine("delay");
    	if(fader.color.a <= 0){
    		StopCoroutine("delay");
    		Destroy(faderObj);
    	}
    }

    IEnumerator delay(){
    	yield return new WaitForSeconds(1.0f);
    	fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, fader.color.a - Time.deltaTime);
    }
}
