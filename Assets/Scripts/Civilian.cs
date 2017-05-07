using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour {
	GameObject target;
	List<GameObject> waypoints = new List<GameObject>();
	public float speed = 10;
    float rotationSpeed = 0.25f;
    Vector3 endPosition;

	// Use this for initialization
	void Start () {
		this.setWaypoints();
	}
		
	// Update is called once per frame
	void Update () {
		if (!target || (target.transform.position - transform.position).magnitude < 2) {
			target = getRandomWaypoint();
		}
		Seek();
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
		endPosition = target.transform.position - transform.position;
		transform.forward = Vector3.Lerp(transform.forward, endPosition, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

}
