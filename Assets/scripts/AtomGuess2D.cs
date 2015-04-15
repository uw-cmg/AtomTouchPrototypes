using UnityEngine;
using System.Collections;

//for prototype #8: guess where atoms are at the start of
//the next time period

//Atoms with AtomGuess2D are just copy of atoms 
//that the physics engine calculates, but do NOT
//move and serve only as references of the old
//positions of the atoms

public class AtomGuess2D : Atom2D {
	//the model of this copy
	public Atom2D original;
	void Start () {	
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
