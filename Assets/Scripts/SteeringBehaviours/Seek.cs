using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seek : MonoBehaviour {

	public GameObject target;
	private float speed = 7;
	private float rotationSpeed = 1;
	private Vector3 _dirToGo;
	List<GameObject> waypoints = new List<GameObject>();

	void Start () {
		this.setWaypoints();
	}

	void Update()	{
		if (!target) {
			target = getRandomWaypoint();
		}

		_dirToGo = target.transform.position - transform.position;
		transform.forward = Vector3.Lerp (transform.forward, _dirToGo, rotationSpeed * Time.deltaTime);
		transform.position += transform.forward * speed * Time.deltaTime;
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
}
