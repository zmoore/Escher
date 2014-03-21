using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {

	private Vector3 _gravityDirection = Vector3.down;
	public Vector3 gravityDirection{
		get{return _gravityDirection;}
		set{
			originalRotation = transform.rotation;
			Transform clone = (Transform)Instantiate(transform);
			clone.Rotate(Vector3.Cross(_gravityDirection,value),Vector3.Angle(-_gravityDirection,value));
			targetRotation = clone.rotation;
			Destroy(clone.gameObject);
			turned = 0;
			_gravityDirection = value;
		}
	}
	Vector3 rotation;
	float gravityForce = 10.0f;
	float gravityAcceleration = 0.0f;
	float distToGround=0.0f;

	private Transform body;
	private Quaternion originalRotation;
	private Quaternion targetRotation;

	public float turnTime = 5f;
	private float turned = 0;
	
	// Use this for initialization
	void Start () {
		body = transform.FindChild("PlayerModel").FindChild("Body");
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(transform.position,_gravityDirection*50);

		if (isGrounded ()) {
			gravityAcceleration = gravityForce;
		}
		else {
			gravityAcceleration += gravityForce*Time.deltaTime;
		}

		this.rigidbody.velocity += gravityAcceleration * _gravityDirection * Time.deltaTime;

		if(turned<turnTime)
			transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, turned += Time.deltaTime);
		
	}

	public bool isGrounded() {
		
		if (_gravityDirection == Vector3.down) {
			distToGround = collider.bounds.extents.y;
		}
		else if(_gravityDirection == Vector3.forward) {
			distToGround = collider.bounds.extents.z;
		}
		
		return Physics.Raycast(transform.position, _gravityDirection, distToGround + 0.1f);
	}

}