using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController controller;

    public float speed = 3f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float health = 0.0f;
    private float minHealth = 3.0f;
    public Camera cam;
    private bool onCombat = false;
    public bool isDeath = false;
    RaycastHit hit;

    public Image[] GUIs;
    public Image bloodOverlay;

    public AudioSource audio;
    public AudioClip footstep;
    public AudioClip pain;
    public BGMScript bgm;

    Vector3 velocity;
    bool isGround;

    void Start()
    {
        GUIs = FindObjectsOfType<Image>();
        for(int i = 0; i < GUIs.Length; i++){
            if(GUIs[i].name == "BloodOverlay"){
                bloodOverlay = GUIs[i];
            }
        }

        audio = GetComponent<AudioSource>();
        bgm = GameObject.Find("BackgroundMusic").GetComponent<BGMScript>();
    }
    // Update is called once per frame
    void Update()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGround && velocity.y < 0){
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(isGround && controller.velocity.magnitude > 0f && !audio.isPlaying && !isDeath){
            audio.PlayOneShot(footstep, Random.Range(0.81f, 1));
        }
        if(isDeath == false){
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
        }

        bloodOverlay.color = new Color(bloodOverlay.color.r, bloodOverlay.color.g, bloodOverlay.color.b, (float)0.25f * health);

        if(onCombat == false){
            if(health > 0 && isDeath == false){
                health -= Time.deltaTime;
            }
            else if(health <= 0 && isDeath == false){
                health = 0;
            }
        }
        if(health > minHealth){
            triggerDeath();
            StartCoroutine("GoToGameOver");
        }

        if(isDeath == true){
            bgm.fadeOut();
        }
        musicToggle();
    }

    public void SetHealth(int dmg){
        if(!isDeath){
            audio.PlayOneShot(pain, 0.5f);
        }
        bgm.toggleSong("chaseFast");
        health += dmg;
        onCombat = true;
        StopCoroutine("CombatCheck");
        StartCoroutine("CombatCheck");
        StartCoroutine(Shake(.15f, .4f));
    }

    IEnumerator Shake(float duration, float magnitude){
        Vector3 originalPos = cam.transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration){
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cam.transform.localPosition = new Vector3(x,y,originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cam.transform.localPosition = originalPos;
    }

    public void triggerDeath(){
        isDeath = true;
    }

    void musicToggle(){
        if(health == 0){
            bgm.toggleSong("ingame");
        }

        else if(health >= 1 && health < 2){
            bgm.toggleSong("chase");
        }

        else if(health >= 2){
            bgm.toggleSong("chaseFast");
        }
    }

    IEnumerator CombatCheck(){
        yield return new WaitForSeconds(10.0f);
        onCombat = false;
    }

    IEnumerator GoToGameOver(){
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Game over", LoadSceneMode.Single);
    }
}
