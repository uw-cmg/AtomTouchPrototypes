using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class ReactionManager : MonoBehaviour {
	public GameObject[] reactionList;
	public Dictionary<string, Reaction> reactions;
	public Reaction prefab;
	void Awake(){
		//find all reactions 
		reactionList = GameObject.FindGameObjectsWithTag("Reaction");
		reactions = new Dictionary<string, Reaction>();
		//hash
		foreach(GameObject g in reactionList){
			Debug.Log(g.GetComponent<Reaction>().Hash());
			reactions.Add(g.GetComponent<Reaction>().Hash(), g.GetComponent<Reaction>());
		}
		
	}
	public Reaction FindReaction(string key){
		if(reactions.ContainsKey(key)){
			return (Reaction)reactions[key];
		}
		return null;
	}
	public Reaction CreateReactionWithIngredients(
		ref List<Compound> ingredients, 
		ref List<Condition> conds){

		List<Compound> sortedIngredients = ingredients.OrderBy(o=>o.name).ToList();
		List<Condition> sortedConds = conds.OrderBy(o=>o.type).ToList();
		Reaction r = Instantiate(prefab) as Reaction;
		r.ingredients = sortedIngredients;
		r.conditions = sortedConds;
		return r;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
