using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingTrigger : MonoBehaviour
{
	public PlayerMovement player;
	public GameObject faderObj;
	private Image fader;
	public ScenesManager sceneMGR;
    // Start is called before the first frame update
    void Start()
    {
        fader = faderObj.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.triggerDeath();
            faderObj.gameObject.SetActive(true);
            StartCoroutine("delay");
        }
    }

    IEnumerator delay(){
    	yield return new WaitForSeconds(3.0f);
    	while(true){
    		yield return new WaitForSeconds(0.01f);
    		fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, fader.color.a + Time.deltaTime);
    		if(fader.color.a >= 1){
    			yield return new WaitForSeconds(1.0f);
    			sceneMGR.goToEpilogue();
    		}
    	}
    }
}
