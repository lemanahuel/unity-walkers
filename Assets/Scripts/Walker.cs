using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Walker : MonoBehaviour {
	GameObject target;
	public GameObject newWalkerPrefab;
	public static List<GameObject> civilians = new List<GameObject>();
	List<GameObject> waypoints = new List<GameObject>();
	public float maxSpeed = 15;
	public float speed = 7;
	public float rotationSpeed = 1;
	Vector3 endPosition;

	StateMachine _sm;

	// Use this for initialization
	void Start () {
		this.speed = this.setRandomSpeed ();
		Civilian.walkers.Add(gameObject);
		//this.setCivilians();
		this.setWaypoints();

		_sm = new StateMachine();
		_sm.AddState(new SeekState(_sm, this));
	}
	
	// Update is called once per frame
	void Update () {
		// if (!target || (target.transform.position - transform.position).magnitude < 2) {
		// 	//target = GetRandomCivilian();
		// 	target = GetClosestCivilian();
		// }

		//SeekState();
		_sm.SetState<SeekState>();
		_sm.Update();
	}

	void setCivilians(){
		var items = GameObject.FindGameObjectsWithTag("civilian");

		foreach (var item in items) {
			civilians.Add(item);
		}
	}

	GameObject GetRandomCivilian() {
		if(target) {
			this.InfectState(target);
		}

		int rand = Random.Range(0, civilians.Count);
		return civilians[rand];
	}

	GameObject GetClosestCivilian () {
		if(target) {
			this.InfectState(target);
		}

		GameObject closest = null;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = transform.position;

		foreach(GameObject civilian in civilians) {
			Vector3 directionToClosest = civilian.transform.position - currentPosition;
			float dSqrToClosest = directionToClosest.sqrMagnitude;

			if(dSqrToClosest < closestDistanceSqr) {
				closestDistanceSqr = dSqrToClosest;
				closest = civilian;
			}
		}

		return closest;
	}

	public void InfectState(GameObject civilian){
		if (civilian.CompareTag("civilian")) {
			Instantiate(newWalkerPrefab, civilian.transform.position, Quaternion.identity);
			civilians.Remove(civilian);
			Destroy(civilian.gameObject);
		}
	}

	// void SeekState() {
	// 	if (!target) {
	// 		this.speed = 3;
	// 		target = getRandomWaypoint();
	// 	}

	// 	this.Move (target.transform.position - transform.position);
	// }

	// void Move(Vector3 endPosition){
	// 	transform.forward = Vector3.Lerp(transform.forward, endPosition, rotationSpeed * Time.deltaTime);
	// 	transform.position += transform.forward * speed * Time.deltaTime;
	// }

	void SetSpeed(int value){
		this.speed = value;
	}

	void setWaypoints(){
		var items = GameObject.FindGameObjectsWithTag("waypoint");

		foreach (var item in items) {
			waypoints.Add(item);
		}
	}

	public GameObject getRandomWaypoint(){
		int rand = Random.Range(0, waypoints.Count);
		return waypoints[rand];
	}

	float setRandomSpeed(){
		return Random.Range(speed, maxSpeed);
	}
}
