using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Reactor : Facility {
	public List<Collector> collectors;
	void Awake(){
		collectors = new List<Collector>();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
