using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeGravity : MonoBehaviour {

	public GameObject planet;       // 引力の発生する星
	public float accelerationScale; // 加速度の大きさ

	void FixedUpdate () {
		// 星に向かう向きの取得
		var direction = planet.transform.position - this.transform.position;
		direction.Normalize();
	
		// 加速度与える
		this.gameObject.GetComponent<Rigidbody>().AddForce(accelerationScale * direction, ForceMode.Acceleration);

	}

	// Use this for initialization
	void Start () {
		Physics.gravity = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
