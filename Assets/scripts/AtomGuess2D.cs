using UnityEngine;
using System.Collections;

//for prototype #8: guess where atoms are at the start of
//the next time period

//all normal atoms that are always rendered
public class AtomGuess2D : MonoBehaviour{
	//the model of this copy
	
	public int charge;
	public Vector2 totalForce = Vector2.zero;
	public CircleCollider2D cc;
	public GameObject renderedObj;

	void Start () {	
		GetComponent<Renderer>().enabled = false;
		renderedObj.transform.position = transform.position;
		renderedObj.GetComponent<Renderer>().enabled = true;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
