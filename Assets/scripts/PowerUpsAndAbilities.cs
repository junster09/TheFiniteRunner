using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PowerUpsAndAbilities : MonoBehaviour {
    //good ol fashioned initializing
    [SerializeField] private GameObject StorageGod;
    [SerializeField] private GameObject TheThing;
    [SerializeField] private AudioSource DashSound;

    [SerializeField] private Rigidbody2D ThisRigidbody;
    [SerializeField] private GameObject dustCloud;
    [SerializeField] private MeshRenderer dashtext;
    [SerializeField] private Text DeathCount;

    [SerializeField] private float dashCooldownTimer = 0f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private float inDashingTime = 0f;
    [SerializeField] private float maxDash = 0.02f;
    [SerializeField] private int DeathCountNumber;
    
	public bool isDashing = false;
	public float dashSpeed = 1000f;
    [SerializeField] private float DashBounceBackSpeed = 1f;
    private float curspeed;
	[SerializeField] private float accelerate;

	void Awake () {
		ThisRigidbody = gameObject.GetComponent<Rigidbody2D> ();//this RB is definetly this rb
		StorageGod = GameObject.Find ("StorageGod");//find the storage
		DeathCountNumber = TheSingleton.Instance.GetDeathCount ();//get teh times you have died from singleton
		DeathCount.text =  "" + DeathCountNumber;//text
        DashBounceBackSpeed = (dashSpeed * -1) - 100f;//setup the counter force so that player doesnt clip through shit wihile dashing

    }

	void Update(){
		if (Input.GetButtonDown ("Restart")) {//pressing "r" kms
			TheSingleton.Instance.SetDeathCount (DeathCountNumber + 1);
			SceneManager.LoadScene (0);
		}
		if (dashCooldownTimer <= 0f) {
			dashtext.enabled = true;
		} else {
			dashtext.enabled = false;
		}
	}
	
	void FixedUpdate () {
		if (Input.GetButtonDown ("Dashing") && dashCooldownTimer <= 0f) {
			isDashing = true;
			inDashingTime = maxDash;
			dashCooldownTimer = dashCooldown;
		}


		if (isDashing == true && inDashingTime > 0f) {
			Dash ();
			inDashingTime = inDashingTime - (1f * Time.deltaTime);
			dustCloud.SetActive (true);
		}
		if (inDashingTime <= 0f) {
			dustCloud.SetActive (false);
			isDashing = false;
		}

		if (dashCooldownTimer > 0f) {
			dashCooldownTimer = dashCooldownTimer - (1f * Time.deltaTime);
		}

		curspeed = gameObject.GetComponent<PlatformerCharacter2D> ().m_MaxSpeed;
		curspeed = curspeed + (accelerate * Time.deltaTime);
		gameObject.GetComponent<PlatformerCharacter2D> ().m_MaxSpeed = curspeed;

	}

	void Dash(){
		ThisRigidbody.AddForce (new Vector2 (dashSpeed, 0f));
		if (DashSound.isPlaying == false) {
			DashSound.Play ();
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		Debug.Log ("YOU CRASHED INTO A THING");
		if (coll.gameObject.tag == "THINGTHATKILLSYOU" && !isDashing) {
			Debug.LogWarning ("YOU CRASHED INTO A THING THAT SHOULD KILL YOU");
			TheSingleton.Instance.SetPlayerSpeed (gameObject.GetComponent<PlatformerCharacter2D> ().m_MaxSpeed);
			TheSingleton.Instance.SetDeathCount (DeathCountNumber + 1);
			SceneManager.LoadScene (0);
		}
		if (coll.gameObject.tag == "INSTADETH" ) {
			Debug.LogWarning ("YOU CRASHED INTO A THING THAT REALLY SHOULD KIEL YOU");
			TheSingleton.Instance.SetDeathCount (DeathCountNumber + 1);
			SceneManager.LoadScene (0);
		}
        if (isDashing == true && coll.gameObject.tag != "THINGTHATKILLSYOU") {
            ThisRigidbody.AddForce(new Vector2(DashBounceBackSpeed, (ThisRigidbody.velocity.y)*-1f));
            inDashingTime = 0f;
        }
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("YOU ENTERED A TRIGGER");
		
		if (other.gameObject.tag == "CheckPoint") {
			Debug.LogWarning ("THAT WAS A CHECKPOINT");
			TheSingleton.Instance.SetRespawnPoint(new Vector2(other.transform.position.x,other.transform.position.y));
		}
		if (other.gameObject.tag == "Ending") {
			TheThing.GetComponent<aThennascript> ().atenasped = 0f;
		}
	}

}
