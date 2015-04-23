using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Reactor : Facility {
	public List<Collector> collectors;
	public Dictionary<string,Compound> compounds; 
	void Awake(){
		collectors = new List<Collector>();
		compounds = new Dictionary<string,Compound>();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
