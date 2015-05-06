using UnityEngine;
using System.Collections;

public class Al2D : Atom2D {
	void Awake(){
		base.Awake();
		radius = 53.5f;
		SetUp();
	}
	// Use this for initialization
	void Start () {
		charge = 3;
		if(UIControl.self != null){
			btn = UIControl.self.alBtn;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
