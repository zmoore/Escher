using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float walkSpeed;
	public float jumpStrength;
	public float distToGround;
	public Transform laser;
	public bool shooting;

	public AudioClip laserSound;
	public AudioClip walkSound;
	
	private Gravity playerGravity;
	private Transform playerCamera;

	Vector3 gravityDirection;
	Quaternion bodyRotation;
	// Use this for initialization
	void Start () {
		playerGravity = this.transform.GetComponent <Gravity> ();
		playerCamera = transform.Find("Camera");
	}
	
	// Update is called once per frame
	void Update () {
		gravityDirection = playerGravity.gravityDirection;

		if (Input.GetMouseButtonDown (0)) {
			audio.PlayOneShot(laserSound);
			GameObject.Instantiate(laser);
		}

		bodyRotation = transform.Find("PlayerModel").Find("Body").rotation;

		float speed;

		if (isGrounded()){
			speed = Time.deltaTime*walkSpeed;
			
			this.rigidbody.velocity += Input.GetAxis("Jump")*jumpStrength*(-gravityDirection);
		}else{
			speed = Time.deltaTime*walkSpeed/15f;
		}

		rigidbody.velocity +=
			Input.GetAxis("Horizontal") * speed * playerCamera.right + //Left/Right
			Input.GetAxis("Vertical") * speed * playerCamera.forward; //Forward/backward

		if (Input.GetKeyDown(KeyCode.LeftControl)) {

			Vector3 facing = transform.Find("Camera").forward;

			float
				absX = Mathf.Abs (facing.x),
				absY = Mathf.Abs (facing.y),
				absZ = Mathf.Abs (facing.z);

			//Debug.Log("X: " + absX + "         Y:" + absY + "           Z:" + absZ);

			if(absX>absY&&absX>absZ){
				playerGravity.gravityDirection = Mathf.Sign(facing.x)*Vector3.right;
			}else if(absY>absX&&absY>absZ){
				playerGravity.gravityDirection = Mathf.Sign(facing.y)*Vector3.up;
			}else{
				playerGravity.gravityDirection = Mathf.Sign(facing.z)*Vector3.forward;
			}
			//Debug.Log(playerGravity.gravityDirection);

		}


	}

	public bool isGrounded() {
		return playerGravity.isGrounded();
		/*
		//done once at the begining of Update()
		//gravityDirection = transform.GetComponent<Gravity>().gravityDirection;

		if (gravityDirection == Vector3.down) {
			distToGround = collider.bounds.extents.y;
		}
		else if(gravityDirection == Vector3.forward) {
			distToGround = collider.bounds.extents.z;
		}

		return Physics.Raycast(transform.position, gravityDirection, distToGround + 0.1f);*/
	}
}
