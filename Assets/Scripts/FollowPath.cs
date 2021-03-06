﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour {

	public GameObject path;

	public Transform[] waypoints;

	public int currentWaypoint = 0;

	float speed = 5;

	public delegate void ReachedAction();
	public event ReachedAction OnDestinationReached;

	// Use this for initialization
	void Start () {
		List<Transform> w = new List<Transform> ();

		foreach (Transform waypoint in path.transform) {
			w.Add (waypoint);
		}

		waypoints = w.ToArray ();
	}
	
	// Update is called once per frame
	void Update () {

		if (currentWaypoint != (waypoints.Length - 1)) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, waypoints [currentWaypoint + 1].position, step);
		}
			
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "waypoint") {
			currentWaypoint++;

			if (currentWaypoint == waypoints.Length - 1) {
				if (OnDestinationReached != null) {
					OnDestinationReached ();
				}
			}
		}
	}
		
}
