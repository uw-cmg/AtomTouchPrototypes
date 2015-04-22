using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Reaction : MonoBehaviour {
	public List<Compound> ingredients; //must be ordered
	public List<Condition> conditions; //must be ordered
	public List<Compound> products;

	public string Hash(){
		string hash = "";
		foreach(Compound c in ingredients){
			hash += c.name + "+";
		}
		hash += "-";
		foreach(Condition c in conditions){
			hash += c.type +"-";
		}
		return hash;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
