using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	[HideInInspector]
	public PlayerResults playerRes;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider col){
		if (col.tag == "Ptoszek") {
			playerRes.ptoszekShoted++;
			playerRes.points+=GameMaster.Instance.pointsForPtoszek;
		}
		if (col.tag == "Bobieslaw") {
			playerRes.bobieslawShoted++;
			playerRes.points+=GameMaster.Instance.pointsForBobieslaw;
		}
		if (col.tag == "Player") {
			playerRes.playersShoted++;
			playerRes.points+=GameMaster.Instance.pointsForPlayer;

		}

	}
}
