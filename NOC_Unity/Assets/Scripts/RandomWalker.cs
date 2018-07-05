using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalker : MonoBehaviour {


	public float speed;
	Transform thisTransform;
	Vector3 newTranslation;
	public GameObject clone;
	public Color kolor;

	float randomSeed;
	float randomSeedB;

	List<GameObject> clone_instances;
	List<Vector2> startingPositions;
	

	void Start () {
		thisTransform = GetComponent<Transform>();
		newTranslation = new Vector3(0,0,0);

		randomSeed = Random.Range(-10000, 10000);
		randomSeedB = Random.Range(-10000, 10000);

		kolor.r = Random.Range(0.0f, 1.0f);
		kolor.g = Random.Range(0.0f, 1.0f);
		kolor.b = Random.Range(0.0f, 1.0f);

		clone_instances = DistributeGrid();
		startingPositions = getStartingPositions(clone_instances);
	}
	void Update () {

		AnimNoiseB(clone_instances, startingPositions);
		Display();
	}

	void AnimNoise(List<GameObject> instance_list) {
		Vector3 newScale = new Vector3(2.0f,2.0f, 2.0f);
		float noiseScale = 0.25f;
		float seed = Time.time;

		for (int i = 0; i < instance_list.Count; i++) {
			GameObject instance = instance_list[i];
			float instanceX = instance_list[i].transform.position.x;
			float instanceY = instance_list[i].transform.position.y;

			newScale.x = Map(Mathf.PerlinNoise((instanceX * noiseScale) + seed, (instanceY * noiseScale) + seed), 0, 1, 0.2f, 1.5f);
			newScale.y = Map(Mathf.PerlinNoise((instanceX * noiseScale) + seed, (instanceY * noiseScale) + seed), 0, 1, 0.2f, 1.5f);
			instance.transform.localScale = newScale;

			newTranslation.x = Map(Mathf.PerlinNoise((instanceX * noiseScale) + seed, (instanceY * noiseScale) + seed), 0, 1, -0.03f, 0.03f);
			newTranslation.y = Map(Mathf.PerlinNoise((instanceX * noiseScale) + seed, (instanceY * noiseScale) + seed), 0, 1, -0.03f, 0.03f);
			instance.transform.Translate(newTranslation);
		}
	}

	void AnimNoiseB(List<GameObject> instance_list, List<Vector2> startingPositions) {

		Vector3 newScale = new Vector3(2.0f,2.0f, 2.0f);
		Vector3 worldPos = new Vector3(2.0f,2.0f, 2.0f);
		Quaternion newRotation = new Quaternion();

		float noiseScale = 0.25f;
		float seed = Time.time;

		for (int i = 0; i < instance_list.Count; i++) {
			newScale.x = Map(Mathf.PerlinNoise((instance_list[i].transform.position.x * noiseScale) + seed, (instance_list[i].transform.position.y * noiseScale) + seed), 0, 1, 0.2f, 1.5f);
			newScale.y = Map(Mathf.PerlinNoise((instance_list[i].transform.position.x * noiseScale) + seed, (instance_list[i].transform.position.y * noiseScale) + seed), 0, 1, 0.2f, 1.5f);
			clone_instances[i].transform.localScale = newScale;

			newTranslation.x = Map(Mathf.PerlinNoise((instance_list[i].transform.position.x * noiseScale) + seed, (instance_list[i].transform.position.y * noiseScale) + seed), 0, 1, -0.03f, 0.03f);
			newTranslation.y = Map(Mathf.PerlinNoise((instance_list[i].transform.position.x * noiseScale) + seed, (instance_list[i].transform.position.y * noiseScale) + seed), 0, 1, -0.03f, 0.03f);

			worldPos.x = startingPositions[i].x + Map(Mathf.PerlinNoise((instance_list[i].transform.position.x * noiseScale) + seed, (instance_list[i].transform.position.y * noiseScale) + seed), 0, 1, -1.0f, 1.0f);
			worldPos.y = startingPositions[i].y + Map(Mathf.PerlinNoise((instance_list[i].transform.position.x * noiseScale) + seed, (instance_list[i].transform.position.y * noiseScale) + seed), 0, 1, -1.0f, 1.0f);

			instance_list[i].transform.SetPositionAndRotation(worldPos, newRotation);
			Move(instance_list[i]);
		}
	}
	List<Vector2> getStartingPositions(List<GameObject> instance_list){

		List<Vector2> startingPositions = new List<Vector2>();

		for (int j = 0; j < instance_list.Count; j++) {
			Vector2 instanceStartPos = new Vector2();
			instanceStartPos.x = instance_list[j].transform.position.x;
			instanceStartPos.y = instance_list[j].transform.position.y;

			startingPositions.Add(instanceStartPos);
		}
		return startingPositions;
	}

	void Move(GameObject clone) {
		newTranslation.x = Random.Range(-speed,speed);
		newTranslation.y = Random.Range(-speed,speed);
		clone.transform.Translate(newTranslation);

	}

	Vector3 Go(string direction) {
		//UP, DOWN, LEFT, RIGHT
		
		switch (direction)
		{
			case "UP":
				return new Vector3(0.0f,speed,0.0f);
			case "DOWN":
				return new Vector3(0.0f,-speed,0.0f);
			case "LEFT":
				return new Vector3(-speed,0.0f,0.0f);
			case "RIGHT":
				return new Vector3(speed,0.0f,0.0f);	
			default:
				return new Vector3(0.0f,0.0f,0.0f);
		}
	}

	void MoveB() {
		int choice;
		choice = Mathf.RoundToInt(Random.Range(0,4));

		if (choice == 0) {
			newTranslation = Go("UP");
		}
		if (choice == 1) {
			newTranslation = Go("DOWN");
		}
		if (choice == 2) {
			newTranslation = Go("RIGHT");
		}
		if (choice == 3) {
			newTranslation = Go("LEFT");
		}
		thisTransform.Translate(newTranslation);
	}

	void MoveC() {
		float floatChoice = Random.Range(0.0f, 1.0f);

		if (floatChoice < 0.4f)      { newTranslation = Go("UP"); } 
		else if (floatChoice < 0.6f) { newTranslation = Go("DOWN"); } 
		else if (floatChoice < 0.8f) { newTranslation = Go("LEFT"); } 
		else 						 { newTranslation = Go("RIGHT"); }

		thisTransform.Translate(newTranslation);

	}

	float Map(float src, float srcMin, float srcMax, float targetMin, float targetMax) {
		return targetMin + ((src - srcMin) * (targetMax - targetMin)) / (srcMax - srcMin);
	}

	void Distribute() {
		Vector3 normalPosition = new Vector3(0.0f, 0.0f, 0.0f);
		normalPosition.x = transform.position.x + RandomFromDistribution.RandomNormalDistribution(0.0f,1.0f);
		normalPosition.y = transform.position.y + RandomFromDistribution.RandomNormalDistribution(0.0f,1.0f);

		Instantiate(clone, normalPosition, transform.rotation);
	}

	List<GameObject> DistributeGrid(){
		Vector3 gridPos = new Vector3();
		Vector3 newScale = new Vector3(2.0f,2.0f, 2.0f);
		float noiseScale = 0.5f;
		float seed = Random.Range(0, 10000);
		List<GameObject> clones = new List<GameObject>();
		
		for (float x = 0.0f; x < 15.0f; x+=0.25f) {	
			for (float y = 0.0f; y < 15.0f; y+=0.25f) {	
				gridPos.x = x;
				gridPos.y = y;
				gridPos.z = 0;

				clones.Add(Instantiate(clone, gridPos, transform.rotation));
			}
		}
		for (int i = 0; i < clones.Count; i++) {
			newScale.x = Map(Mathf.PerlinNoise((clones[i].transform.position.x * noiseScale) + seed, (clones[i].transform.position.y * noiseScale) + seed), 0, 1, 0.2f, 1.5f);
			newScale.y = Map(Mathf.PerlinNoise((clones[i].transform.position.x * noiseScale) + seed, (clones[i].transform.position.y * noiseScale) + seed), 0, 1, 0.2f, 1.5f);
			clones[i].transform.localScale = newScale;
		}

		return clones;
	}

	void Display() {
		SpriteRenderer sprite = clone.GetComponent<SpriteRenderer>();
		sprite.color = kolor;
	}
}
