using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour {
	GameObject target;
	public static List<GameObject> walkers = new List<GameObject>();
	public static List<GameObject> civilians = new List<GameObject>();
	List<GameObject> waypoints = new List<GameObject>();
	float maxSpeed = 7;
	public float speed = 4;
	public float rotationSpeed = 1f;
	float amountOfPushs = 10;

	public float thrust;
	public Rigidbody rb;

	StateMachine _sm;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		this.setWaypoints();
		this.speed = this.setRandomSpeed();
		Walker.civilians.Add(gameObject);
		Civilian.civilians.Add(gameObject);
		//this.setWalkers();

		_sm = new StateMachine();
		_sm.AddState(new WanderState(_sm, this));
		_sm.AddState(new FleeState(_sm, this));
	}

	void FixedUpdate(){
			rb.AddForce(0,0,0);
	}
		
	// Update is called once per frame
	void Update () {

		GameObject waypoint = null;
		GameObject closestWalker = null;
		GameObject closestCivilian = null;

		if (!target || Vector3.Distance(target.transform.position, transform.position) < 2) {
			target = getRandomWaypoint();
		}

		foreach (var walker in walkers) {
			if (Vector3.Distance (walker.transform.position, transform.position) < 20) {
				closestWalker = walker;
				//closestCivilian = this.GetClosestCivilian();
			}
			if (Vector3.Distance (walker.transform.position, transform.position) < 10) {
				//this.Push(walker);
			}
		}

		if (closestWalker) {
			//target = closestWalker;
      _sm.SetState<FleeState>();
		} else {
      _sm.SetState<WanderState>();
		}
		_sm.Update();
	}

	void setWalkers(){
		var items = GameObject.FindGameObjectsWithTag("walker");

		foreach (var item in items) {
			walkers.Add(item);
		}
	}

	void setWaypoints(){
		var items = GameObject.FindGameObjectsWithTag("waypoint");

		foreach (var item in items) {
			waypoints.Add(item);
		}
	}

	GameObject getRandomWaypoint(){
		int rand = Random.Range(0, waypoints.Count);
		return waypoints[rand];
	}

	void Push(GameObject walker){
		int canKick = Random.Range(0, 2);

		if (canKick != 0 && this.amountOfPushs > 0) {
			walker.transform.forward = Vector3.Lerp (walker.transform.forward, -(target.transform.position - walker.transform.position), Time.deltaTime);
			Vector3 finalMove = walker.transform.forward * 2 * Time.deltaTime;
			//finalMove.z = 0;
			walker.transform.position += finalMove;
			--this.amountOfPushs;
		}
		//walkers.Remove(walker);
		//Destroy(walker.gameObject);
	}

	float setRandomSpeed(){
		return Random.Range(this.speed, this.maxSpeed);
	}

	// GameObject GetClosestCivilian () {
	// 	GameObject closest = null;
	// 	float closestDistanceSqr = Mathf.Infinity;
	// 	Vector3 currentPosition = transform.position;

	// 	foreach(GameObject civilian in civilians) {
	// 		Vector3 directionToClosest = civilian.transform.position - currentPosition;
	// 		float dSqrToClosest = directionToClosest.sqrMagnitude;

	// 		if(dSqrToClosest < closestDistanceSqr) {
	// 			closestDistanceSqr = dSqrToClosest;
	// 			closest = civilian;
	// 		}
	// 	}

	// 	return closest;
	// }
}
