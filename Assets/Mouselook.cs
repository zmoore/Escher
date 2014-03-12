using UnityEngine;
using System.Collections;

public class Mouselook : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.rigidbody.MovePosition(
			this.transform.position +
			(this.transform.forward * Input.GetAxis("Vertical") +
			this.transform.right * Input.GetAxis("Horizontal")) * .15f
		);
	}
}
