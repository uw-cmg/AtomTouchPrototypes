using UnityEngine;
using System.Collections;

public class K2D : Atom2D {
	public override void Awake(){
		base.Awake();
		//data: http://en.wikipedia.org/wiki/Ionic_radius
		radius = 138f;
		SetUp();
	}
	// Use this for initialization
	void Start () {
		charge = 1;
		if(UIControl.self != null){
			btn = UIControl.self.kBtn;
		}
		
	}
	// Update is called once per frame
	void Update () {
	
	}
}
