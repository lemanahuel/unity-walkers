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
    float rotationSpeed = 0.25f;

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
		if (!target || (target.transform.position - transform.position).magnitude < 2) {
			target = getRandomWaypoint();
		}

		GameObject closestWalker = null;
		GameObject closestCivilian = null;

		foreach (var walker in walkers) {
			if (Vector3.Distance (walker.transform.position, transform.position) < 20) {
				closestWalker = walker;
			}
		}

//		if (closestWalker) {
//			foreach (var civilian in civilians) {
//				if (Vector3.Distance (civilian.transform.position, transform.position) < 2 && 
//					Vector3.Distance (closestWalker.transform.position, civilian.transform.position) < 2 && 
//					Vector3.Distance (closestWalker.transform.position, transform.position) < 2) {
//					closestCivilian = civilian;
//					//this.KillState(closestWalker, civilian);
//				}
//			}
//
//			if (closestWalker) {
//				target = closestWalker;
//				this.FleeState ();
//			} else {
//				this.WanderState();
//			}
//			target = closestWalker;
//			this.FleeState ();
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

	void KillState(GameObject walker, GameObject closestCivilian){
		//this.Move(walker.transform.position - transform.position);
		//this.Move(walker.transform.position - closestCivilian.transform.position);
		//walkers.Remove(walker);
		//Destroy(walker.gameObject);
	}

	void WanderState() {
		this.Move(target.transform.position - transform.position);
	}

	void FleeState()	{
		this.Move(-(target.transform.position - transform.position));
	}

	void Move(Vector3 endPosition){
		transform.forward = Vector3.Lerp(transform.forward, endPosition, Time.deltaTime);
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	float setRandomSpeed(){
		return Random.Range(speed, maxSpeed);
	}

}
