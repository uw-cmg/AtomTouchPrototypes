using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Reactor : Facility {
	public List<Collector> collectors;
	public Dictionary<string,Compound> compounds; 
	public List<Condition> conditions;
	public Reaction currentReaction;
	public Dictionary<string, Compound> products;
	public int state;
	public enum State{
		started,
		paused,
		stopped,
		empty
	};
	public void StartReaction(Reaction r){
		state = (int)State.started;
		currentReaction = r;
		foreach(Compound c in currentReaction.products){
			Compound product = CreateCompoundCopy(c);
			products.Add(product.name, product);
		}
	}
	public void StopReaction(){
		state = (int)State.stopped;
	}
	public Compound CreateCompoundCopy(Compound compoundModel){
		Compound newCompound = Instantiate(compoundModel) as Compound;
		newCompound.amount = 0.0f;
		return newCompound;
	}
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
		products = new Dictionary<string,Compound>();
		state = (int)State.empty;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(state == (int)State.started){
			if(currentReaction == null){
				Debug.Log("started reaction but reaction is null!");
				return;
			}
			foreach(Compound c in currentReaction.ingredients){
				Compound actualIngredient = compounds[c.name];
				if(actualIngredient.amount <= 0
					|| actualIngredient.amount < c.amount * currentReaction.rate * Time.deltaTime){
					//stop reaction
					foreach(Compound p in products.Values){
						Debug.Log(p.name + ": " + p.amount);
					}
					return;
				}
				actualIngredient.amount -= c.amount * currentReaction.rate * Time.deltaTime;
			}
			foreach(Compound p in currentReaction.products){
				Compound actualProduct = products[p.name];
				actualProduct.amount += p.amount * currentReaction.rate * Time.deltaTime;
			}
		}
	}
}
