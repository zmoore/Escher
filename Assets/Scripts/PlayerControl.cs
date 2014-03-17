using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float walkSpeed;
	public float jumpStrength;
	public float distToGround;
	private Gravity playerGravity;
	public Transform laser;
	public bool shooting;

	public AudioClip laserSound;
	public AudioClip walkSound;


	Vector3 gravityDirection;
	Vector3 movement = new Vector3(0,0,0);
	Quaternion bodyRotation;
	// Use this for initialization
	void Start () {
		playerGravity = this.transform.GetComponent < Gravity> ();

	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetMouseButtonDown (0)) {
			audio.PlayOneShot(laserSound);
			GameObject.Instantiate(laser);
		}

		movement = new Vector3 (0, 0, 0);

		bodyRotation = transform.Find("PlayerModel").Find("Body").rotation;

		if (Input.GetKey("d")&& isGrounded()&&Mathf.Abs(this.rigidbody.velocity.x)<8f&&Mathf.Abs(this.rigidbody.velocity.y)<8f&&Mathf.Abs(this.rigidbody.velocity.z)<8f) {
			this.rigidbody.velocity += (bodyRotation*Vector3.forward*walkSpeed)*Time.deltaTime;
		}
		else if(Input.GetKey("d")&& !isGrounded()) {
			this.rigidbody.velocity += (bodyRotation*Vector3.forward*walkSpeed/15f)*Time.deltaTime;
		}

		if (Input.GetKey("e")&& isGrounded()&&Mathf.Abs(this.rigidbody.velocity.x)<8f&&Mathf.Abs(this.rigidbody.velocity.y)<8f&&Mathf.Abs(this.rigidbody.velocity.z)<8f) {	
			this.rigidbody.velocity += (bodyRotation*Vector3.back*walkSpeed)*Time.deltaTime;
		}
		else if(Input.GetKey("e")&& !isGrounded()) {
			this.rigidbody.velocity += (bodyRotation*Vector3.back*walkSpeed/15f)*Time.deltaTime;
		}

		if (Input.GetKey("s")&& isGrounded()&&Mathf.Abs(this.rigidbody.velocity.x)<8f&&Mathf.Abs(this.rigidbody.velocity.y)<8f&&Mathf.Abs(this.rigidbody.velocity.z)<8f) {	
			this.rigidbody.velocity += (bodyRotation*Vector3.left*walkSpeed)*Time.deltaTime;
		}
		else if(Input.GetKey("s")&& !isGrounded()) {
			this.rigidbody.velocity += (bodyRotation*Vector3.left*walkSpeed/15f)*Time.deltaTime;
		}

		if (Input.GetKey("f")&& isGrounded()&&Mathf.Abs(this.rigidbody.velocity.x)<8f&&Mathf.Abs(this.rigidbody.velocity.y)<8f&&Mathf.Abs(this.rigidbody.velocity.z)<8f) {	
			this.rigidbody.velocity += (bodyRotation*Vector3.right*walkSpeed)*Time.deltaTime;
		}
		else if(Input.GetKey("f")&& !isGrounded()) {
			this.rigidbody.velocity += (bodyRotation*Vector3.right*walkSpeed/15f)*Time.deltaTime;
		}

		if (Input.GetKey("space")&& isGrounded()) {
			gravityDirection = transform.GetComponent<Gravity>().gravityDirection;
			this.rigidbody.velocity += jumpStrength*(-gravityDirection)*Time.deltaTime;
		}



		if (Input.GetKeyDown(KeyCode.LeftControl)) {

			Vector3 rotation = transform.Find("Camera").forward;

			Vector3.Normalize(rotation);

			if(((rotation.x<=(Mathf.Sqrt(2f)/2f))&&(rotation.x>=-(Mathf.Sqrt(2f)/2f))) && ((rotation.z<=1f)&&(rotation.z>=(Mathf.Sqrt(2f)/2f))) && (Mathf.Abs (rotation.x)>Mathf.Abs(rotation.y) || Mathf.Abs(rotation.z)>Mathf.Abs(rotation.y))) {
				Debug.Log ("Forward");
				playerGravity.gravityDirection = Vector3.forward;
				playerGravity.rotationFromGravity = Quaternion.Euler(Vector3.Cross(playerGravity.gravityDirection,Vector3.up)*90);
			}

			else if( ((rotation.x<=(Mathf.Sqrt(2f)/2f))&&(rotation.x>=-(Mathf.Sqrt(2f)/2f))) && ((rotation.z<=-Mathf.Sqrt(2f)/2f))&&(rotation.z>=(-1f)) && (Mathf.Abs (rotation.x)>Mathf.Abs(rotation.y) || Mathf.Abs(rotation.z)>Mathf.Abs(rotation.y))) {
				Debug.Log ("Back");
				playerGravity.gravityDirection = Vector3.back;
				playerGravity.rotationFromGravity = Quaternion.Euler(Vector3.Cross(playerGravity.gravityDirection,Vector3.up)*90);
			}

			else if( ((rotation.x<=-(Mathf.Sqrt(2f)/2f))&&(rotation.x>=-1f))  && (((rotation.z<=(Mathf.Sqrt(2f)/2f))&&(rotation.z>=-(Mathf.Sqrt(2f)/2f)))) && (Mathf.Abs (rotation.x)>Mathf.Abs(rotation.y) || Mathf.Abs(rotation.z)>Mathf.Abs(rotation.y))) {
				Debug.Log ("Left");
				playerGravity.gravityDirection = Vector3.left;
				playerGravity.rotationFromGravity = Quaternion.Euler(Vector3.Cross(playerGravity.gravityDirection,Vector3.up)*90);
			}

			else if(  ((rotation.x<=1f)&&(rotation.x>=(Mathf.Sqrt(2f)/2f))) && (((rotation.z<=(Mathf.Sqrt(2f)/2f))&&(rotation.z>=-(Mathf.Sqrt(2f)/2f))))&& (Mathf.Abs (rotation.x)>Mathf.Abs(rotation.y) || Mathf.Abs(rotation.z)>Mathf.Abs(rotation.y))) {
				Debug.Log ("Right");
				playerGravity.gravityDirection = Vector3.right;
				playerGravity.rotationFromGravity = Quaternion.Euler(Vector3.Cross(playerGravity.gravityDirection,Vector3.up)*90);
			}

			else if( rotation.y > Mathf.Sqrt(2f)/2f){
				Debug.Log("Up");
				playerGravity.gravityDirection = Vector3.up;
				playerGravity.rotationFromGravity = Quaternion.Euler(Vector3.Cross(playerGravity.gravityDirection,Vector3.back)*180);
			}

			else if(rotation.y < -Mathf.Sqrt(2f)/2f) {
				Debug.Log("Down");
				playerGravity.gravityDirection = Vector3.down;
				playerGravity.rotationFromGravity = Quaternion.Euler(Vector3.Cross(playerGravity.gravityDirection,Vector3.zero));

			}




		}


	}

	public bool isGrounded() {
		gravityDirection = transform.GetComponent<Gravity>().gravityDirection;

		if (gravityDirection == Vector3.down) {
			distToGround = collider.bounds.extents.y;
		}
		else if(gravityDirection == Vector3.forward) {
			distToGround = collider.bounds.extents.z;
		}

		return Physics.Raycast(transform.position, gravityDirection, distToGround + 0.1f);
	}
}
