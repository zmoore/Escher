using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {
	public Vector3 gravityDirection;
	
	// Update is called once per frame
	void Update () {
		this.rigidbody.velocity += gravityDirection*Time.deltaTime;
	}
}
