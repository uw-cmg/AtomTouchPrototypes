using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class ReactionManager : MonoBehaviour {
	public static ReactionManager self;
	public GameObject[] reactionList;
	public Dictionary<string, Reaction> reactions;
	public Reaction prefab;
	void Awake(){
		self = this;
		//find all reactions 
		reactionList = GameObject.FindGameObjectsWithTag("Reaction");
		reactions = new Dictionary<string, Reaction>();
		//hash
		foreach(GameObject g in reactionList){
			Reaction r = g.GetComponent<Reaction>();
			Debug.Log(r.Hash());
			reactions.Add(r.Hash(), r);
		}
		
	}
	
	
	public Reaction FindReaction(string key){
		if(reactions.ContainsKey(key)){
			return (Reaction)reactions[key];
		}
		return null;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
