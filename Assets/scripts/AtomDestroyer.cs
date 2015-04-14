using UnityEngine;
using System.Collections;

public class AtomDestroyer : MonoBehaviour {
	public static AtomDestroyer self; 
	void Awake(){
		self = this;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.name != "CuUserControlled"){
			//eat that
			AtomPhysics2D.self.Ions.Remove(other.gameObject);
			Destroy(other.gameObject);
		}
	}
}
