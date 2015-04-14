using UnityEngine;
using System.Collections;

public class Cl2D : Atom2D {

	// Use this for initialization
	void Start () {
		charge = -1;
		if(UIControl.self != null)
			btn = UIControl.self.clBtn;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
