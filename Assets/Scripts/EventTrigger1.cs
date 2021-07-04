using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger1 : MonoBehaviour
{
	public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown(){
    	if(checkDistance()){
    		target.gameObject.SetActive(true);
    	}
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
}
