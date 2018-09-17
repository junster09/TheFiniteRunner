using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aThennascript : MonoBehaviour {
	[SerializeField] private RawImage redBlock;
	public float atenasped = 7f; //base speed god moves
	private Rigidbody2D thisrb;
	private GameObject Player;
	private Rigidbody2D PlayerRb;
	[SerializeField] private float DistanceBetweenPlayer;
    [SerializeField] private float MaximumDistanceFromPlayer;
	private Vector3 RespawnPoint;
	private Transform PlayerPoint;

	// Use this for initialization
	void Awake () {
		thisrb = GetComponent<Rigidbody2D> (); //give this rb to itself
		Player = GameObject.Find ("Player");//finds the player
		PlayerRb = Player.GetComponent<Rigidbody2D> ();//gives PlayerRb the player's rb
		PlayerPoint = Player.GetComponent<Transform> ();//gets the point where the player is.... for some reason
		RespawnPoint = new Vector3 ((PlayerPoint.position.x - 50f), PlayerPoint.position.y, -2f);//when the player respawns, always spawn a certain distance behind hin
		gameObject.transform.SetPositionAndRotation (RespawnPoint,Quaternion.identity); //actual respawning
		atenasped = Player.GetComponent<PlatformerCharacter2D> ().m_MaxSpeed;//scale godspeed


	}

	// Update is called once per frame
	void FixedUpdate () {
		DistanceBetweenPlayer = Mathf.Abs((Mathf.Abs(thisrb.position.x) - Mathf.Abs(PlayerRb.position.x)));//get an absolute value for the distance between the player and god
		if (PlayerRb.position.x >= 0f) {
			thisrb.MovePosition(new Vector2((thisrb.position.x + atenasped * Time.deltaTime),PlayerRb.position.y));
		}
        /*
        //do the hud color thingy
		if (DistanceBetweenPlayer <= 1f && PlayerRb.position.x >= 0f) {
			redBlock.color = new Color(1,0,0,DistanceBetweenPlayer);
		}else if (DistanceBetweenPlayer >= 1f && PlayerRb.position.x >= 0f) {
			redBlock.color = new Color(1,0,0,0);
		}
        */      

        //if player is "not moving" make godspeed REALLY fast
		if (PlayerRb.velocity.x <= 11.9f) {
			thisrb.MovePosition(new Vector2((thisrb.position.x + ((atenasped*20) * Time.deltaTime)),PlayerRb.position.y));
		}
        //if player is too far make god a certain distance away
		if (DistanceBetweenPlayer >= MaximumDistanceFromPlayer) {
			thisrb.MovePosition(new Vector2(PlayerRb.position.x - MaximumDistanceFromPlayer,PlayerRb.position.y));
		}
	}
}
