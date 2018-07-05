using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vectors : MonoBehaviour {
	public Camera cam;
	public GameObject testObject;
	Transform testObjTransform;

	void Start () {

		testObjTransform = testObject.transform;
		
		if (cam) {
			Debug.Log(cam.pixelWidth);
			Debug.Log(cam.pixelHeight);
		} else {
			Debug.LogError("no camera");
		}

	}
	
	void Update () {
		Vector3 screenPos = cam.WorldToScreenPoint(testObjTransform.position);

		Debug.DrawLine(Vector3.zero, new Vector3(testObjTransform.position.x, testObjTransform.position.y, testObjTransform.position.z), Color.white);
		
		Debug.Log(screenPos.x + " " + screenPos.y);
		
	}
}
