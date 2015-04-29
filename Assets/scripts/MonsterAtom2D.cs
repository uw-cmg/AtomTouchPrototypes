using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//for cosmos riff
public class MonsterAtom2D : Atom2D {
	
	void Awake(){
		
	}
	// Use this for initialization
	void Start () {
		if(charge == 0){
			charge = 6;
		}
		GetComponent<SpriteRenderer>().color = normalColor;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
