using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour {
	GameObject target;
	public GameObject newWalkerPrefab;
	public static List<GameObject> civilians = new List<GameObject>();
	public float speed = 10;
	float rotationSpeed = 0.25f;
	Vector3 endPosition;

	// Use this for initialization
	void Start () {
		Civilian.walkers.Add(gameObject);
		this.setCivilians();
	}
	
	// Update is called once per frame
	void Update () {
		if (!target || (target.transform.position - transform.position).magnitude < 3) {
			target = GetRandomCivilian();
		}

		Seek();
	}

	void setCivilians(){
		var items = GameObject.FindGameObjectsWithTag("civilian");

		foreach (var item in items) {
			civilians.Add(item);
		}
	}

	GameObject GetRandomCivilian() {
		if(target) {
			Infect();
		}

		int rand = Random.Range(0, civilians.Count);
		return civilians[rand];
	}

	void Infect(){
		Instantiate(newWalkerPrefab, target.transform.position, Quaternion.identity);
		civilians.Remove(target);
		Destroy(target.gameObject);
	}

	void Seek() {
		this.Move(target.transform.position - transform.position);
	}

	void Move(Vector3 endPosition){
		transform.forward = Vector3.Lerp(transform.forward, endPosition, rotationSpeed * Time.deltaTime);
		transform.position += transform.forward * speed * Time.deltaTime;
	}
}
