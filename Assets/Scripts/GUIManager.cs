using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIManager : MonoBehaviour
{
	public static bool isInteractWithBatteryFirstTime = true;
	public static bool isInteractWithKeyFirstTime = true;
	public static bool isInteractWithEnemyFirstTime = true;
    public static bool isInteractWithPaperFirstTime = true;
	public TextMeshProUGUI tutorialText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text){
    	tutorialText.gameObject.SetActive(true);
    	tutorialText.text = text;
    	StartCoroutine(delay());
    }

	IEnumerator delay(){
		yield return new WaitForSeconds(8.5f); 
		tutorialText.gameObject.SetActive(false);
    }

    public bool GetBatteryCheck(){
    	return isInteractWithBatteryFirstTime;
    }

    public bool GetKeyCheck(){
    	return isInteractWithKeyFirstTime;
    }

    public bool GetEnemyCheck(){
    	return isInteractWithEnemyFirstTime;
    }

    public bool GetPaperCheck(){
        return isInteractWithPaperFirstTime;
    }

     public void SetBatteryCheck(bool param){
     	isInteractWithBatteryFirstTime = param;
    }

    public void SetKeyCheck(bool param){
    	isInteractWithKeyFirstTime = param;
    }

    public void SetEnemyCheck(bool param){
    	isInteractWithEnemyFirstTime = param;
    }

    public void SetPaperCheck(bool param){
        isInteractWithPaperFirstTime = param;
    }
}
