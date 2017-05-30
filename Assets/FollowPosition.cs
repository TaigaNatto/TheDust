using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour {

	public GameObject followObject;//追う対象
	public GameObject ground;//基準とする大地

	Vector3 startPos;
	float startDistance;

	Vector3 resultPos;

	void FixedUpdate(){

//		var direction =this.transform.position - ground.transform.position;
//		float distance = startDistance - Vector3.Distance (this.transform.position, ground.transform.position);
//		direction.Set (direction.x*distance,direction.y*distance,direction.z*distance);
//
//		//追従
//		this.transform.position = startPos + followObject.transform.position+direction;

		//targetの方に少しずつ向きが変わる
		this.transform.LookAt(followObject.transform);

		print (startPos + "+" + startDistance);
	}

	// Use this for initialization
	void Start () {
		startPos = this.transform.position-followObject.transform.position;
		startDistance = Vector3.Distance (this.transform.position, ground.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
