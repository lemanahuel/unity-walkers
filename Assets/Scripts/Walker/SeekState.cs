using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekState : WalkerState {

	GameObject target = null;
	public static List<GameObject> civilians = new List<GameObject>();

	public SeekState(StateMachine sm, Walker w) : base(sm, w)  {

    }

    public override void Awake() {
        //Debug.Log("Entró a FleeState");
        base.Awake();
		setCivilians();
    }

    public override void Execute() {
		//Debug.Log("Execute FleeState");
        base.Execute();
		
		if (!target || (target.transform.position - walker.transform.position).magnitude < 2) {
			//target = GetRandomCivilian();
			target = GetClosestCivilian();
		}

		if (!target) {
			walker.speed = 3;
			target = walker.getRandomWaypoint();
		}

		walker.transform.forward = Vector3.Lerp(walker.transform.forward, target.transform.position - walker.transform.position, walker.rotationSpeed * Time.deltaTime);
		walker.transform.position += walker.transform.forward * walker.speed * Time.deltaTime;
    }

    public override void Sleep() {
        base.Sleep();
        //Debug.Log("Salió de FleeState");
    }

	void setCivilians(){
		civilians = new List<GameObject>();
		var items = GameObject.FindGameObjectsWithTag("civilian");

		foreach (var item in items) {
			civilians.Add(item);
		}
	}

	GameObject GetRandomCivilian() {
		if(target) {
			walker.InfectState(target);
		}

		int rand = Random.Range(0, civilians.Count);
		return civilians[rand];
	}

	GameObject GetClosestCivilian () {
		if (target) {
			walker.InfectState(target);
		}

		GameObject closest = null;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = walker.transform.position;

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
