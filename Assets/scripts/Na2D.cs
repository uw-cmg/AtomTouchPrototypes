using UnityEngine;
using System.Collections;

public class Na2D : Atom2D{
	public override void Awake(){
		base.Awake();
		SetUp("Na", 1, 102f);
	}
}
