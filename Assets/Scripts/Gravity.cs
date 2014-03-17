using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {
	
	public Vector3 gravityDirection;
	Vector3 rotation;
	float gravityForce = 10.0f;
	float gravityAcceleration = 0.0f;
	float distToGround=0.0f;
	
	public Quaternion rotationFromGravity;
	private Vector3 _direction;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (isGrounded ()) {
			gravityAcceleration = gravityForce;
		}
		else {
			gravityAcceleration += gravityForce*Time.deltaTime;
		}

		this.rigidbody.velocity += gravityAcceleration * gravityDirection * Time.deltaTime;

		transform.rotation = Quaternion.Slerp(transform.rotation, rotationFromGravity, Time.deltaTime * 4f);		
		
	}

	public bool isGrounded() {
		
		if (gravityDirection == Vector3.down) {
			distToGround = collider.bounds.extents.y;
		}
		else if(gravityDirection == Vector3.forward) {
			distToGround = collider.bounds.extents.z;
		}
		
		return Physics.Raycast(transform.position, gravityDirection, distToGround + 0.1f);
	}

}