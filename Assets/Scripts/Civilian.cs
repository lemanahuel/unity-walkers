using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour {
	GameObject target;
	public static List<GameObject> walkers = new List<GameObject>();
	public static List<GameObject> civilians = new List<GameObject>();
	List<GameObject> waypoints = new List<GameObject>();
	float maxSpeed = 7;
	float speed = 4;
	float rotationSpeed = 1;

	// Use this for initialization
	void Start () {
		this.setWaypoints();
		this.speed = this.setRandomSpeed ();
		Walker.civilians.Add(gameObject);
		Civilian.civilians.Add(gameObject);
		//this.setWalkers();
	}
		
	// Update is called once per frame
	void Update () {
		var waypoint = getRandomWaypoint();

		if (!target || (target.transform.position - transform.position).magnitude < 2) {
			target = waypoint;
		}

		GameObject closestWalker = null;
		List<GameObject> closestsCivilians = new List<GameObject>();

		foreach (var walker in walkers) {
			if (Vector3.Distance (walker.transform.position, transform.position) < 15) {
				closestWalker = walker;
			}
		}

//		if (closestWalker) {
//			foreach (var civilian in civilians) {
//				if (Vector3.Distance (civilian.transform.position, transform.position) < 2 && 
//					Vector3.Distance (closestWalker.transform.position, civilian.transform.position) < 2 && 
//					Vector3.Distance (closestWalker.transform.position, transform.position) < 2) {
//					closestsCivilians.Add(civilian);
//				}
//			}
//
//			target = waypoint;
//
//			if (closestsCivilians.Count > 1) {
//				this.KillState (closestWalker);
//				this.WanderState();
//			} else if (closestWalker) {
//				target = closestWalker;
//				this.FleeState ();
//			} else {
//				this.WanderState();
//			}
//		} else {
//			this.WanderState();
//		}

		if (closestWalker) {
			target = closestWalker;
			this.FleeState ();
		} else {
			this.WanderState();
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

	void KillState(GameObject walker){
		walkers.Remove(walker);
		Destroy(walker.gameObject);
	}

	void WanderState() {
		this.Move(target.transform.position - transform.position, speed);
	}

	void FleeState()	{
		this.Move(-(target.transform.position - transform.position), speed + 3);
	}

	void Move(Vector3 endPosition, float currentSpeed){
		transform.forward = Vector3.Lerp(transform.forward, endPosition, rotationSpeed * Time.deltaTime);
		transform.position += transform.forward * currentSpeed * Time.deltaTime;
	}

	float setRandomSpeed(){
		return Random.Range(speed, maxSpeed);
	}

}
