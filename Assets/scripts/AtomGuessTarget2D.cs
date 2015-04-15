using UnityEngine;
using System.Collections;

//prototype #8
//atoms to be guessed, not rendered, but physics calculated
public class AtomGuessTarget2D : AtomGuess2D {
	public GameObject predictedAtom;
	public GameObject renderedAtom;
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().enabled = false;
		renderedAtom.GetComponent<Renderer>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
