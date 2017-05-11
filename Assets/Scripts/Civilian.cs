using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour {
	GameObject target;
	public static List<GameObject> walkers = new List<GameObject>();
	public static List<GameObject> civilians = new List<GameObject>();
	List<GameObject> waypoints = new List<GameObject>();
	float maxSpeed = 6;
	float speed = 4;
	float rotationSpeed = 0.5f;
	float amountOfPushs = 10;

	StateMachine _sm;

	// Use this for initialization
	void Start () {
		_sm = new StateMachine();
		_sm.AddState(new WanderState(_sm, this));

		this.setWaypoints();
		this.speed = this.setRandomSpeed ();
		Walker.civilians.Add(gameObject);
		Civilian.civilians.Add(gameObject);
		//this.setWalkers();
	}
		
	// Update is called once per frame
	void Update () {
		GameObject waypoint = null;
		GameObject closestWalker = null;
		GameObject closestCivilian = null;

		if (!target || (target.transform.position - transform.position).magnitude < 2) {
			target = getRandomWaypoint();
		}

		foreach (var walker in walkers) {
			if (Vector3.Distance (walker.transform.position, transform.position) < 20) {
				closestWalker = walker;
				//closestCivilian = this.GetClosestCivilian();
				if (Vector3.Distance (closestWalker.transform.position, transform.position) < 10) {
					this.Push(closestWalker);
				}
			}
		}

//		 if (closestWalker) {
//		 	foreach (var civilian in civilians) {
//		 		if (Vector3.Distance (civilian.transform.position, transform.position) < 2 && 
//		 			Vector3.Distance (closestWalker.transform.position, civilian.transform.position) < 2 && 
//		 			Vector3.Distance (closestWalker.transform.position, transform.position) < 2) {
//		 			closestsCivilians.Add(civilian);
//		 		}
//		 	}
//		 }

		// 	target = waypoint;

		// 	if (closestsCivilians.Count > 1) {
		// 		this.KillState (closestWalker);
		// 		this.WanderState();
		// 	} else if (closestWalker) {
		// 		target = closestWalker;
		// 		this.FleeState ();
		// 	} else {
		// 		this.WanderState();
		// 	}
		// } else {
		// 	this.WanderState();
		// }

		if (closestWalker) {
			target = closestWalker;
			this.Flee();
		} else {
			this.Wander();
            //_sm.SetState<WanderState>();
		}
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
		int canKick = Random.Range(0, 1);

		Debug.Log (this.amountOfPushs);
		if (canKick != 0 && this.amountOfPushs > 0) {
			walker.transform.forward = Vector3.Lerp (walker.transform.forward, -(target.transform.position - walker.transform.position), Time.deltaTime);
			walker.transform.position += walker.transform.forward * 2 * Time.deltaTime;
			--this.amountOfPushs;
		}
		//walkers.Remove(walker);
		//Destroy(walker.gameObject);
	}

	void Wander() {
		// if (!target) {
		// 	target = getRandomWaypoint ();
		// }
		this.Move(target.transform.position - transform.position, speed);
	}

	void Flee()	{
		this.Move(-(target.transform.position - transform.position), speed + 1);
	}

	void Move(Vector3 endPosition, float currentSpeed){
		transform.forward = Vector3.Lerp(transform.forward, endPosition, rotationSpeed * Time.deltaTime);
		transform.position += transform.forward * currentSpeed * Time.deltaTime;
	}

	float setRandomSpeed(){
		return Random.Range(speed, maxSpeed);
	}

	GameObject GetClosestCivilian () {
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
}
