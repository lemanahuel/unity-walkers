using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour {
	GameObject target;
	public static List<GameObject> walkers = new List<GameObject>();
	List<GameObject> waypoints = new List<GameObject>();
	public float speed = 10;
    float rotationSpeed = 0.25f;

	// Use this for initialization
	void Start () {
		Walker.civilians.Add(gameObject);
		this.setWalkers();
		this.setWaypoints();
	}
		
	// Update is called once per frame
	void Update () {
		if (!target || (target.transform.position - transform.position).magnitude < 2) {
			target = getRandomWaypoint();
		}

		GameObject walker = null;
		foreach (var item in walkers) {
			if (Vector3.Distance (item.transform.position, transform.position) < 20) {
				walker = item;
			}
		}

		if (walker) {
			target = walker;
			Flee();
		} else {
			Seek();
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

    void Seek() {
		this.Move(target.transform.position - transform.position);
	}

	void Flee()	{
		this.Move(-(target.transform.position - transform.position));
	}

	void Move(Vector3 endPosition){
		transform.forward = Vector3.Lerp(transform.forward, endPosition, rotationSpeed * Time.deltaTime);
		transform.position += transform.forward * speed * Time.deltaTime;
	}

}
