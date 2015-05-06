using UnityEngine;
using System.Collections;

public class N2D : Atom2D {
	public override void Awake(){
		base.Awake();
		//data: http://en.wikipedia.org/wiki/Ionic_radius
		SetUp("N",-3,146f);
	}
}
