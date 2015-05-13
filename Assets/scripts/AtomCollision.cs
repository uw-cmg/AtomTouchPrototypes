using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AtomCollision : MonoBehaviour {
	public List<AtomCollision> neighbours;
	public int visitState;
	public enum DFSState{
		visited,
		unvisited,
		visiting
	};
	void Awake(){
		neighbours = new List<AtomCollision>();
	}
	void Start(){
		visitState = (int)DFSState.unvisited;
	}
	void OnCollisionEnter2D(Collision2D collision){
		if(Application.loadedLevelName != "ConnectMonsters")return;
		if(collision.gameObject.tag != "Atom"
			&& collision.gameObject.tag != "MonsterAnchor")return;
		
		if(this.enabled && !neighbours.Contains(collision.gameObject.GetComponent<AtomCollision>())){
			Debug.Log("entering");
			neighbours.Add(collision.gameObject.GetComponent<AtomCollision>());
		}
	}
	void OnCollisionExit2D(Collision2D collision){
		if(Application.loadedLevelName != "ConnectMonsters")return;
		if(collision.gameObject.tag != "Atom"
			&& collision.gameObject.tag != "MonsterAnchor")return;
		//if(this.enabled){
			Debug.Log("removing");
			neighbours.Remove(collision.gameObject.GetComponent<AtomCollision>());
		//}
		
	}
}
