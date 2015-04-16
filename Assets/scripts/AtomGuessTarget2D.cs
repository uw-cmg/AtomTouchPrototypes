using UnityEngine;
using System.Collections;

//prototype #8
//atoms to be guessed, not rendered, but physics calculated
public class AtomGuessTarget2D : AtomGuess2D {
	public GameObject predictedAtom;
	public GameObject renderedAtom;
	public GameObject renderedPrevious;
	public GameObject arrow;
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().enabled = false;
		renderedAtom.GetComponent<Renderer>().enabled = true;
		renderedPrevious.transform.position = transform.position;
		renderedPrevious.GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
