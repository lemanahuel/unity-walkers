using UnityEngine;
using System.Collections;

public class Flee : MonoBehaviour {

	public GameObject target;
	public float speed;
	public float rotationSpeed;

	private Vector3 _dirToGo;

	void Update(){
		_dirToGo = -(target.transform.position - transform.position);
		transform.forward = Vector3.Lerp (transform.forward, _dirToGo, rotationSpeed * Time.deltaTime);
		transform.position += transform.forward * speed * Time.deltaTime;
	}
}
