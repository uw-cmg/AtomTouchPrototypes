using UnityEngine;
using System.Collections;

public class N2D : Atom2D {
	public override void Awake(){
		base.Awake();
		//data: http://en.wikipedia.org/wiki/Ionic_radius
		radius = 146f;
		SetUp();
	}
	// Use this for initialization
	void Start () {
		charge = -3;
		if(UIControl.self != null){
			btn = UIControl.self.nBtn;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
