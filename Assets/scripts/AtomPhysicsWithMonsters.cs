using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AtomPhysicsWithMonsters : MonoBehaviour {
	public static AtomPhysicsWithMonsters self;
	public List<GameObject> Ions;
	public GameObject[] monsterAnchorAtoms;
	void Awake(){
		self = this;
		Application.targetFrameRate = 150;
		GameObject[] loadedAtoms = GameObject.FindGameObjectsWithTag("Atom");
		monsterAnchorAtoms = GameObject.FindGameObjectsWithTag("MonsterAnchor");
		foreach(GameObject g in loadedAtoms){
			Ions.Add(g);
		}
		foreach(GameObject g in monsterAnchorAtoms){
			Ions.Add(g);
		}
	}
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		Physics2D.IgnoreLayerCollision(
			LayerMask.NameToLayer("AtomDestroyer"),
			LayerMask.NameToLayer("AtomAttracter")
		);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		for(int i=0; i < Ions.Count;i++){
			if(Ions[i] == null){
				Ions.Remove(Ions[i]);
				continue;
			}
			Atom2D atom = Ions[i].GetComponent<Atom2D>();
			atom.totalForce = Vector2.zero;
			atom.Kick(); 
			//check if atoms out of viewport, if so, destroy

			bool withinViewport = atom.WithinViewport();
			if(!withinViewport){
				//Destroy(atom.gameObject);
				Ions.Remove(atom.gameObject);
				
				Destroy(atom.gameObject);
			}
		}
		Atom2D curr;
		Atom2D other;
		for(int i=0; i < Ions.Count;i++){
			Rigidbody2D rb = Ions[i].GetComponent<Rigidbody2D>();
			if(rb.velocity.magnitude < 1f){
				rb.gameObject.GetComponent<Atom2D>().Kick();
			}
		}
		for(int i=0; i < Ions.Count;i++){
			curr = Ions[i].GetComponent<Atom2D>();
			Rigidbody2D currRb = Ions[i].GetComponent<Rigidbody2D>();

			for(int j=i+1; j < Ions.Count;j++){
				other = Ions[j].GetComponent<Atom2D>();

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
			//currRb.velocity = curr.vel;
			currRb.velocity = Vector3.zero;
			if(currRb.gameObject.tag == "AnchorAtom" || currRb.gameObject.tag == "MonsterAnchor"){
				
			}else{

				currRb.AddForce(curr.totalForce);
			}
				
		}
	}
}
