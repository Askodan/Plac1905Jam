using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	// Use this for initialization
	public Player_controller player;
	public Slider HPSlider;
	public Text points;
	public Text ammo;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HPSlider.value = player.getHP();
		points.text = "Points: " + 100;
		ammo.text = 160 +"/"+ 200;
	}
}
