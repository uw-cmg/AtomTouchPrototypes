using UnityEngine;
using System.Collections;

public class K2D : Atom2D {
	public override void Awake(){
		base.Awake();
		//data: http://en.wikipedia.org/wiki/Ionic_radius
		SetUp("K",1,138f);
	}
}
