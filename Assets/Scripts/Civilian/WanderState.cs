using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : CivilianState {

	private List<GameObject> waypoints = new List<GameObject>();
	private float _rotationSpeed = 0.5f;
	private float _speed = 4;

	public WanderState(StateMachine sm, Civilian c) : base(sm, c)  {
    }

    public override void Awake()
    {
        Debug.Log("Entró a Run");
        base.Awake();
    }

    public override void Execute()
    {
        base.Execute();
		GameObject target = getRandomWaypoint ();
		//civilian.transform.position += civilian.transform.forward * _speed * Time.deltaTime;
		Vector3 endPosition = target.transform.position - civilian.transform.position;
		civilian.transform.forward = Vector3.Lerp(civilian.transform.forward, endPosition, _rotationSpeed * Time.deltaTime);
		civilian.transform.position += civilian.transform.forward * _speed * Time.deltaTime;
		//civilian.transform.position += civilian.transform.forward * _speed * Time.deltaTime;
    }

    public override void Sleep()
    {
        base.Sleep();
        Debug.Log("Salió de Run");
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
