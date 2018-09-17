using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeScript : MonoBehaviour {
    //good ol fashioned initialization
	[SerializeField] private AudioSource BreakSound;
	[SerializeField] private GameObject exsplosion;
	[SerializeField] private BoxCollider2D thisCollider;
	private bool isDone = false;
	private float waitTimer = 1f;
	private float curTime = 0f;
	void Awake(){
		thisCollider = gameObject.GetComponent<BoxCollider2D>();
	}
	void OnCollisionEnter2D(Collision2D coll){//if a dashing player collides with this
		if (coll.gameObject.GetComponent<PowerUpsAndAbilities> ().isDashing) {
			exsplosion.SetActive (true);//make explosion
			BreakSound.Play ();
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			Destroy (thisCollider);//kms
			isDone = true;
		}
	}


	void Update(){
		if (isDone == true) {
			curTime = curTime + (1f * Time.deltaTime);
		}
		if (curTime >= waitTimer) {
			Destroy (gameObject);//delet this
		}
	}
}
