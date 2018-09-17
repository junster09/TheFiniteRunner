using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this is vewy important try not to touch this
public class TheSingleton{

	private static TheSingleton instance;//I made a singleton
	private TheSingleton(){}

	public static TheSingleton Instance{
		get{
			if (instance == null) {
				instance = new TheSingleton ();
			}
			return instance;
		}

	}

	private Vector2 RespawnPoint = new Vector2 (-35.529f,0.843f);//original spawn
	private int DeathCount = 0;//death counter
	public Vector2 GetRespawnPoint(){
		return RespawnPoint;
	}
	public void SetRespawnPoint(Vector2 thing){
		RespawnPoint = thing;
	}

	private float PlayerSpeed = 12f;
	public float GetPlayerSpeed(){
		return PlayerSpeed;
	}
	public void SetPlayerSpeed(float thing){
		PlayerSpeed = thing;
	}
	public int GetDeathCount(){
		return DeathCount;
	}
	public void SetDeathCount(int thing){
		DeathCount = thing;
	}

}
