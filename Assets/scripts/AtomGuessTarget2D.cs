using UnityEngine;
using System.Collections;

//prototype #8
//atoms to be guessed, not rendered, but physics calculated
public class AtomGuessTarget2D : AtomGuess2D {
	public GameObject expectedNextPeriod;
	public AtomGuess2D renderedAtom;
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
