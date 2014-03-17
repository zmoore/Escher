using UnityEngine;
using System.Collections;

public class LaserRenderer : MonoBehaviour {

	private LineRenderer lineRenderer;
	private float counter;
	private float distance;

	public Transform origin;
	public Vector3 destination;

	public float speed;

	// Use this for initialization
	void Start () {

		lineRenderer = this.GetComponent<LineRenderer>();

		lineRenderer.SetPosition(1,GameObject.Find ("Player").transform.Find ("Camera").transform.Find ("Weapon").transform.position);
		speed = 300f;
		counter = 0f;
		origin = GameObject.Find ("Player").transform.Find("Camera").transform.Find ("Weapon").transform;
		destination = GameObject.Find ("Player").transform.Find("Camera").transform.forward *10000f; 
		
		distance = Vector3.Distance (origin.position, destination);
		lineRenderer.SetPosition (0, origin.position);
		lineRenderer.SetWidth (0.25f, 0.25f);

	}

	// Update is called once per frame
	void Update () {

		if (counter < distance) {
			counter += 0.1f / speed;

			float x = Mathf.Lerp(0,distance,counter);

			Vector3 pointA = origin.position;
			Vector3 pointB = destination;

			Vector3 pointAlongLine = x*Vector3.Normalize(pointB-pointA) + pointA;

			lineRenderer.SetPosition(1,pointAlongLine);

		}
	
	}
}
