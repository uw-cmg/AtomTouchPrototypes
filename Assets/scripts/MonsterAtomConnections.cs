using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterAtomConnection : MonoBehaviour {
	public MonsterAtom2D start;
	public MonsterAtom2D end;
	public Stack<Atom2D> path;
	// Use this for initialization
	void Start () {
		path = new Stack<Atom2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
