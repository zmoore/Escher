using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Mathf.Abs (this.transform.position.x) > 200 || Mathf.Abs (this.transform.position.y) > 200 || Mathf.Abs(this.transform.position.z) > 200) {
			GameObject.Destroy(GameObject.Find("Laser"));
			this.transform.position =(new Vector3(0,5,0));
			this.rigidbody.velocity = Vector3.zero;
			this.transform.GetComponent<Gravity>().gravityDirection = Vector3.down;
			this.transform.GetComponent<Gravity>().rotationFromGravity = Quaternion.Euler(Vector3.Cross(this.transform.GetComponent<Gravity>().gravityDirection,Vector3.zero));
		}

	}
}
