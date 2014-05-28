﻿using UnityEngine;
using System.Collections;

public class RotatingGem : MonoBehaviour {

	public float rotationSpeed = 6;// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.transform.Rotate (
			Vector3.up, Time.fixedDeltaTime*rotationSpeed,Space.World);
	}
}
