using UnityEngine;
using System.Collections;

public class ShieldballBehaviour : MonoBehaviour {

	GameObject center;

	// Use this for initialization
	void Start () {
		center = GameObject.Find ("Center");
	}

	// Update is called once per frame
	void Update () {
		transform.RotateAround(center.transform.position, -transform.up, Time.deltaTime * 180f);
	}
}
