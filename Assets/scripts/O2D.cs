﻿using UnityEngine;
using System.Collections;

public class O2D : Atom2D{
	public override void Awake(){
		base.Awake();
		SetUp("O",-2,140f);
	}
}
