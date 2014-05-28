using UnityEngine;
using System.Collections;
using Pathfinding;

public class Enemy : MonoBehaviour {

	private Seeker seeker;
	private CharacterController controller;

	public GameObject target = null;

	private Path path = null;
	public int currentWaypoint = 0;
	public float nextWaypointDistance = 0.8f;
	
	public float speed = 100;

	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker> ();
		controller = GetComponent<CharacterController>();
		seeker.StartPath (transform.position, target.transform.position, OnPathComplete);
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
		if (path == null) {
			//We have no path to move after yet
			return;
		}
		
		if (currentWaypoint >= path.vectorPath.Count) {
			Debug.Log ("End Of Path Reached");
			return;
		}
		
		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		controller.SimpleMove (dir);

		transform.LookAt (new Vector3(path.vectorPath [currentWaypoint].x,this.transform.position.y,path.vectorPath[currentWaypoint].z));
		
		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}
	
	public void OnPathComplete (Path p) {
		if(!p.error){
			path = p;
			currentWaypoint = 0;
		}
		Debug.Log (p.vectorPath.Count);
	}
	
}