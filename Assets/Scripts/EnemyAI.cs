using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class EnemyAI : MonoBehaviour
{
	Animator anim;
	public GameObject[] waypoints;
	private float speed = 3.0f;
	public float sightDistance = 0.01f;
	public GameObject player;
	Vector3 direction;
	float angle;
	private int i = 0;

	private bool onCooldown = false;
	private bool isSeePlayer = false;

	public float gravity = -9.81f;
	public CharacterController controller;
	public Transform groundCheck;
	public float groundDistance = 0.4f;
    public LayerMask groundMask;
	Vector3 velocity;
    bool isGround;
    bool isMoving = true;
	RaycastHit hit;

	private AudioSource audio;
    public AudioClip footstep;
    public AudioClip scream;
    public HitTriggerSound audioAttack;

	public GUIManager guiManager;

	public float fieldOfViewAngle = 180f;
    // Start is called before the first frame update
    void Start()
    {
    	anim = GetComponent<Animator>();
    	guiManager = GameObject.Find("Manager").GetComponent<GUIManager>();
    	player = GameObject.Find("Player");
    	groundCheck = transform.Find("GroundCheck").transform;

    	controller = gameObject.GetComponent<CharacterController>();
    	audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    	isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    	velocity.y += gravity * Time.deltaTime;
    	controller.Move(velocity * Time.deltaTime);

    	if(isGround && velocity.y < 0){
            velocity.y = -2f;
        }

        if(anim.GetBool("isMoving") && !audio.isPlaying){
        	audio.PlayOneShot(footstep, 1);
        }

    	StartCoroutine("patrol");
    	if(gameObject.transform.position.y <= 0){
    		gameObject.transform.position = waypoints[i].transform.position;
    	}

    }

    IEnumerator patrol(){
    	direction = player.transform.position - gameObject.transform.position;
    	angle = Vector3.Angle(direction, this.transform.forward);
    	if(gameObject.transform.position != waypoints[i].transform.position && isMoving == true && isSeePlayer == false){
            StartCoroutine("patrolDebug");
    		speed = 3.0f;
    		anim.SetBool("isMoving", true);
    		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, waypoints[i].transform.position, speed * Time.deltaTime);
    		Vector3 rotateDirection = Vector3.RotateTowards(gameObject.transform.forward, new Vector3(waypoints[i].transform.position.x, 0, waypoints[i].transform.position.z) 
    			- new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), 30.0f * Time.deltaTime, 0.0f);
    		gameObject.transform.rotation = Quaternion.LookRotation(rotateDirection);
    	}
    	if(gameObject.transform.position == waypoints[i].transform.position && isSeePlayer == false){
            StopCoroutine("patrolDebug");
    		speed = 3.0f;
    		isMoving = false;
            audio.PlayOneShot(scream, 1);
    		anim.SetBool("isMoving", false);
    		if(i == waypoints.Length - 1) i = 0;
    		else i++;
    		yield return new WaitForSeconds(3.9f);
    		isMoving = true;
    	}
    	float distanceFromPlayer = (Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position));
    	if(angle < fieldOfViewAngle * 0.5f){
    		if(Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, this.transform.position.z), 
    		direction.normalized, out hit, sightDistance)){
    			if(hit.collider.tag == "Player" && distanceFromPlayer <= 8.0f){
    				isSeePlayer = true;
                    StopCoroutine("patrolDebug");
    				if(onCooldown == false){
    					speed = 3.45f;
    				}
    				if(onCooldown == true){
    					speed = 0.2f;
    				}
    				if(guiManager.GetEnemyCheck()){
    					guiManager.SetText("Hide in the room, Fighting is impossible.");
                		guiManager.SetEnemyCheck(false);
    				}
    				gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.deltaTime);
    				Vector3 rotateDirection = Vector3.RotateTowards(gameObject.transform.forward, new Vector3(player.transform.position.x, 0, player.transform.position.z) 
    				- new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), 30.0f * Time.deltaTime, 0.0f);
    				gameObject.transform.rotation = Quaternion.LookRotation(rotateDirection);
    			}
    			if(hit.collider.tag == "Player" && distanceFromPlayer <= 2.0f && onCooldown == false){
    				StartCoroutine(attackDelay());
    				anim.SetTrigger("Attack");
    				audioAttack.Play();
    			}
    			if(hit.collider.tag != "Player" && distanceFromPlayer >= 3.0f){
    				isSeePlayer = false;
    			}
    		}
    	}
    }

    IEnumerator attackDelay(){
    	onCooldown = true;
    	player.GetComponent<PlayerMovement>().SetHealth(1);
    	speed = 0;
    	yield return new WaitForSeconds(2.25f);
    	onCooldown = false;
    	speed = 3.45f;
        isSeePlayer = false;
        StopCoroutine("patrolDebug");
    }

    IEnumerator patrolDebug(){
        yield return new WaitForSeconds(15.0f);
        if(gameObject.transform.position != waypoints[i].transform.position && isMoving == true && isSeePlayer == false){
            gameObject.transform.position = waypoints[i].transform.position;
        }
    }
}
