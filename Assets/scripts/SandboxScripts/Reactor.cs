using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Reactor : Facility {
	public List<Collector> collectors;
	public Dictionary<string,Compound> compounds; 
	public List<Condition> conditions;

	public Reaction CreateReactionWithIngredients(GameObject prefab){

		List<Compound> sortedIngredients = new List<Compound>(compounds.Values);

		sortedIngredients = sortedIngredients.OrderBy(o=>o.name).ToList();
		List<Condition> sortedConds = conditions.OrderBy(o=>o.type).ToList();
		GameObject newReactionObj = Instantiate(prefab) as GameObject;
		Reaction r = newReactionObj.GetComponent<Reaction>();
		r.ingredients = sortedIngredients;
		r.conditions = sortedConds;
		return r;
	}
	void Awake(){
		collectors = new List<Collector>();
		compounds = new Dictionary<string,Compound>();
		conditions = new List<Condition>();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
