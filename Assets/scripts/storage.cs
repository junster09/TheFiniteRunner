using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storage : MonoBehaviour {
	[SerializeField] private GameObject OtherStorageGod;
	[SerializeField] private GameObject Player;
	public Vector2 RespawnPoint = new Vector2 (-35.529f,0.843f);//first spawn

	// Use this for initialization
	void Awake () {
		GameObject[] Copies = GameObject.FindGameObjectsWithTag ("Storage");//wtf is this even for


		Player = GameObject.Find ("Player");
		Player.GetComponent<PlatformerCharacter2D> ().RespawnPoint = RespawnPoint;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
