using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : CivilianState {

	GameObject target = null;
	private List<GameObject> waypoints = new List<GameObject>();

	public WanderState(StateMachine sm, Civilian c) : base(sm, c)  {

    }

    public override void Awake() {
        //Debug.Log("Entró a WanderState");
        base.Awake();
		setWaypoints();
    }

    public override void Execute() {
		//Debug.Log("Execute WanderState");
        base.Execute();

		if (!target || (target.transform.position - civilian.transform.position).magnitude < 2) {
			target = getRandomWaypoint();
		}
		
		Vector3 endPosition = target.transform.position - civilian.transform.position;
		civilian.transform.forward = Vector3.Lerp(civilian.transform.forward, endPosition, civilian.rotationSpeed * Time.deltaTime);
		civilian.transform.position += civilian.transform.forward * civilian.speed * Time.deltaTime;
    }

    public override void Sleep() {
        base.Sleep();
        //Debug.Log("Salió de WanderState");
    }

	void setWaypoints(){
		if (waypoints.Count == 0) {
			var items = GameObject.FindGameObjectsWithTag("waypoint");

			foreach (var item in items) {
				waypoints.Add(item);
			}
		}
	}

	GameObject getRandomWaypoint(){
		int rand = Random.Range(0, waypoints.Count);
		return waypoints[rand];
	}
}
