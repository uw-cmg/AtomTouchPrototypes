using UnityEngine;
using System.Collections;

public class Cu2D : Atom2D {
	public override void Awake(){
		base.Awake();
		//data: http://en.wikipedia.org/wiki/Ionic_radius
		radius = 73f;
		SetUp();
	}
	// Use this for initialization
	void Start () {
		charge = 2;
		if(UIControl.self != null){
			btn = UIControl.self.cuBtn;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
