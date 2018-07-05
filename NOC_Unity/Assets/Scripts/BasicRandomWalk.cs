using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRandomWalk : MonoBehaviour {

	Transform thisTransform;
	Vector3 newTranslation;
	public float speed = 0.1f;

	void Start () {
		thisTransform = GetComponent<Transform>();
		newTranslation = new Vector3(0,0,0);
	}
	
	void Update () {
		Move();
	}
	void Move() {
		newTranslation.x = Random.Range(-speed,speed);
		newTranslation.y = Random.Range(-speed,speed);
		thisTransform.Translate(newTranslation);

	}
}
