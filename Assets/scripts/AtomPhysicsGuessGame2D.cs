using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AtomPhysicsGuessGame2D : MonoBehaviour {
	public static AtomPhysicsGuessGame2D self;
	public List<GameObject> Ions;

	void Awake(){
		self = this;
		Application.targetFrameRate = 150;
		GameObject[] loadedAtoms = GameObject.FindGameObjectsWithTag("Atom");
		foreach(GameObject g in loadedAtoms){
			Ions.Add(g);
		}
	}
	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!GameControlGuessAtoms2D.started )return;
		for(int i=0; i < Ions.Count;i++){
			if(Ions[i] == null){
				Ions.Remove(Ions[i]);
				continue;
			}
			AtomGuess2D atom = Ions[i].GetComponent<AtomGuess2D>();
			atom.totalForce = Vector2.zero;
			//atom.Kick(); 
			//check if atoms out of viewport, if so, destroy
			/*
			bool withinViewport = atom.WithinViewport();
			if(!withinViewport){
				//Destroy(atom.gameObject);
				//Ions.Remove(atom.gameObject);
				
				//Destroy(atom.gameObject);
			}
			*/
			
		}
		AtomGuess2D curr;
		AtomGuess2D other;
		for(int i=0; i < Ions.Count;i++){
			Rigidbody2D rb = Ions[i].GetComponent<Rigidbody2D>();
			if(rb.velocity.magnitude < 1f){
				//rb.gameObject.GetComponent<AtomGuess2D>().Kick();
			}
		}
		for(int i=0; i < Ions.Count;i++){
			curr = Ions[i].GetComponent<AtomGuess2D>();
			Rigidbody2D currRb = Ions[i].GetComponent<Rigidbody2D>();

			for(int j=i+1; j < Ions.Count;j++){
				other = Ions[j].GetComponent<AtomGuess2D>();

				float distance = Vector3.Distance(curr.gameObject.transform.position, 
				other.gameObject.transform.position);
				//repel
				//current to other
				Vector2 forceDireciton = curr.gameObject.transform.position - other.gameObject.transform.position;
				//attract
				if(curr.charge * other.charge < 0){
					forceDireciton *= -1;
				}
				float otherToCurr = 9 * Mathf.Pow(10, 9) * 1.602f *1.602f 
					* Mathf.Abs(other.charge) * Mathf.Abs(curr.charge) * Mathf.Pow(10,-8);
				float currToOther = otherToCurr;
				//Vector3 force = (currRb.mass * currRb.velocity - otherRb.mass * otherRb.velocity)/Time.deltaTime;
				forceDireciton.Normalize();

				curr.totalForce += forceDireciton * otherToCurr / distance / distance;
				//Debug.Log(otherRb.velocity);
				other.totalForce += -forceDireciton * currToOther / distance / distance; 
			}
			if(currRb.gameObject.tag == "Atom"){
				currRb.AddForce(curr.totalForce);
				Component[] rbs = currRb.GetComponentsInChildren<Rigidbody2D>();
				foreach(Rigidbody2D r in rbs){
					r.velocity = Vector2.zero;
				}
			}

				
		}
	}
}
