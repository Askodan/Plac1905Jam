using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour {

	public Animator animator;
	public float speedForward = 16.0f;
	public float speedBack = 8.0f;
	public float speedSides = 8.0f;
	public float rotationSpeedHor = 200.0f;
	public float rotationSpeedVer = 100.0f;

	private float maxHP = 100.0f;
	public float HP = 100.0f;

	public GameObject gun;
	private Vector3 motion;
	private float maxYSpeed = -40.0f;
	private float currentYSpeed;
	private bool canJump;
	public float cantJumpTime = 3;
	public bool isAlive = true;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		currentYSpeed = maxYSpeed;
		canJump = true;
		HP = maxHP;
		isAlive = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		WSADcontrol ();
		mouseControl();
	}
	void WSADcontrol ()
	{
		CharacterController controller = GetComponent<CharacterController> ();
		///keyboard
		if (Input.GetKeyDown (KeyCode.R)) {reload();}
		if (Input.GetKeyDown (KeyCode.F)) {pickWeapon();}
		if (controller.isGrounded)
			if (Input.GetKeyDown (KeyCode.Space)) {jump();}

		///movement
		float x = Input.GetAxis ("Horizontal");
		float z = Input.GetAxis ("Vertical");
		//for jump
		currentYSpeed -= 15.0f*Time.deltaTime;
		if(currentYSpeed<maxYSpeed)
			currentYSpeed = maxYSpeed;

		motion = Vector3.Normalize (new Vector3 (x, 0.0f, z));
		if(motion.magnitude > 0)
			animator.SetBool("Walking",true);
		else
			animator.SetBool("Walking",false);
		if (z > 0)
			motion.z *= speedForward;
		else
			motion.z *= speedBack;
		motion.x *= speedSides;
		motion.y = currentYSpeed;
		
		motion = this.transform.rotation * motion;
		controller.Move (motion * Time.deltaTime);
		//print(motion);
	}
	void mouseControl ()
	{
		///buttons
		if (Input.GetMouseButtonDown (0)) {
			shoot ();
		}
		///rotating
		if (Input.GetAxis ("Mouse X") < 0) {
			//print("Left");
			this.transform.RotateAround(this.transform.position, new Vector3(0,1,0),-rotationSpeedHor*Time.deltaTime);
		}else if (Input.GetAxis ("Mouse X") > 0) {
			//print("Right");
			this.transform.RotateAround(this.transform.position, new Vector3(0,1,0),+rotationSpeedHor*Time.deltaTime);
		}
		Transform gunTransform = gun.GetComponent<Transform>();
		//print(gunTransform.localRotation.eulerAngles.x);
		if (Input.GetAxis ("Mouse Y") > 0) {
			//print("UP");
			gunTransform.Rotate(-rotationSpeedVer*Time.deltaTime, 0, 0,Space.Self);
			if(gunTransform.localRotation.eulerAngles.x > 180 && gunTransform.localRotation.eulerAngles.x < 315)
				gunTransform.Rotate(+rotationSpeedVer*Time.deltaTime, 0, 0,Space.Self);
		}else if (Input.GetAxis ("Mouse Y") < 0) {
			//print("Down");
			gunTransform.Rotate(+rotationSpeedVer*Time.deltaTime, 0, 0,Space.Self);
			if(gunTransform.localRotation.eulerAngles.x < 180 && gunTransform.localRotation.eulerAngles.x > 45)
				gunTransform.Rotate(-rotationSpeedVer*Time.deltaTime, 0, 0,Space.Self);
		}
	}
	void shoot()
	{
		//print("Shoot");
	}
	void reload()
	{
		//print("Reload");
	}
	void pickWeapon()
	{
		//print("Drop");
		animator.SetTrigger("Picking");
	}
	void jump ()
	{	//print("jump");
		if (canJump) {
			currentYSpeed = 15.0f;
			StartCoroutine (jumpTime ());
			animator.SetTrigger("Jumping");
		}
	}
	IEnumerator jumpTime()
	{
		canJump = false;
		yield return new WaitForSeconds(cantJumpTime);
		canJump = true;
	}


	public void takeDMG (float dmg)
	{
		HP -= dmg;
		if (HP <= 0) {
			HP = 0;
			isAlive = false;
		}
	}
	public float getHP()
	{	return HP;

	}
}
