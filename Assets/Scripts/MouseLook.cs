using UnityEngine;
using System.Collections;



public class MouseLook : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	Transform theCamera, theHead, theBody;

	float rotationY = 0F;

	void Update ()
	{
		Screen.lockCursor = true;
		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = theCamera.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
			
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

			theCamera.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

			theHead.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
			theBody.localEulerAngles = new Vector3(0,rotationX, 0);

			//transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}

	}


	void Start () {
		theCamera = transform.Find("Camera");
		theHead = transform.Find ("PlayerModel").Find("Head");
		theBody = transform.Find("PlayerModel").Find("Body");
		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;
	}
}