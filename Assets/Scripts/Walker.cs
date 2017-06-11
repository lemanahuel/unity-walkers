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
	public float rotationSpeed = 1f;
	Vector3 endPosition;
	public float thrust;
	public Rigidbody rb;

	StateMachine _sm;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		this.speed = this.setRandomSpeed ();
		Civilian.walkers.Add(gameObject);
		//this.setCivilians();
		this.setWaypoints();

		_sm = new StateMachine();
		_sm.AddState(new SeekState(_sm, this));
	}
	
	void Update () {
		if (!target) {
			_sm.SetState<SeekState>();
		}
		_sm.Update();
	}

	void FixedUpdate(){
		rb.AddForce(0,0,0);
	}

	public GameObject GetClosestCivilian (GameObject target) {
		if (target) {
			InfectState(target);
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
		if (civilian.CompareTag("civilian") && (civilian.transform.position - transform.position).magnitude < 2) {
			Instantiate(newWalkerPrefab, civilian.transform.position, Quaternion.identity);
			civilians.Remove(civilian);
			Destroy(civilian.gameObject);
		}
	}

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
