using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : CivilianState {

	GameObject target = null;
	public static List<GameObject> walkers = new List<GameObject>();

	public FleeState(StateMachine sm, Civilian c) : base(sm, c)  {

    }

    public override void Awake() {
        //Debug.Log("Entró a FleeState");
        base.Awake();
		setWalkers();
    }

    public override void Execute() {
		//Debug.Log("Execute FleeState");
        base.Execute();
		setWalkers();
		GameObject closestWalker = null;

		foreach (var walker in walkers) {
			if (Vector3.Distance (walker.transform.position, civilian.transform.position) < 20) {
				closestWalker = walker;
			}
		}

		if (closestWalker) { 
			Vector3 endPosition = -(closestWalker.transform.position - civilian.transform.position);
			civilian.transform.forward = Vector3.Lerp(civilian.transform.forward, endPosition, civilian.rotationSpeed * Time.deltaTime);
			civilian.transform.position += civilian.transform.forward * civilian.speed * Time.deltaTime;
		}
    }

    public override void Sleep() {
        base.Sleep();
        //Debug.Log("Salió de FleeState");
    }

	void setWalkers(){
		walkers = new List<GameObject>();
		var items = GameObject.FindGameObjectsWithTag("walker");

		foreach (var item in items) {
			walkers.Add(item);
		}
	}
}
