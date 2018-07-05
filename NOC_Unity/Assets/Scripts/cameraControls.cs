using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControls : MonoBehaviour {
	
	public Camera cam;
	public float speed = 0.3f;
	Vector3 easeVector = new Vector3(1.0f, 1.0f, 1.0f);

	Vector3 translation = new Vector3(0.0f, 0.0f, 0.0f);


	Transform camTransform;

	void Start () {
		camTransform = cam.GetComponent<Transform>();
	}
	
	void Update () {
		setTranslation();
		moveCam();
	}

	void setTranslation() {

		float easeValue = 0.5f;
		
		if (Input.GetKey(KeyCode.W)) {
			translation.y = speed;
		} else if (Input.GetKeyUp(KeyCode.W)) {
			easeVector.y = easeValue;
		}

		if (Input.GetKey(KeyCode.S)) {
			translation.y = -speed;
		} else if (Input.GetKeyUp(KeyCode.S)) {
			easeVector.y = easeValue;
		}

		if (Input.GetKey(KeyCode.A)) {
			translation.x = -speed;
		} else if (Input.GetKeyUp(KeyCode.A)) {
			easeVector.x = easeValue;
		}

		if (Input.GetKey(KeyCode.D)) {
			translation.x = speed;
		} else if (Input.GetKeyUp(KeyCode.D)) {
			easeVector.x = easeValue;
		}

		if (Input.GetKey(KeyCode.O)) {
			translation.z = -speed;
		} else if (Input.GetKeyUp(KeyCode.O)) {
			easeVector.z = easeValue;
		}

		if (Input.GetKey(KeyCode.P)) {
			translation.z = speed;
		} else if (Input.GetKeyUp(KeyCode.P)) {
			easeVector.z = easeValue;
		}
	}

	void moveCam() {
		camTransform.Translate(translation);
		translation.Scale(easeVector);
	}
}
