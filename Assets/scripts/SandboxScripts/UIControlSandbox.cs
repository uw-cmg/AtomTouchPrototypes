using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIControlSandbox : MonoBehaviour {
	public GameObject[] ingredientsInputField;
	public GameObject emptyCompoundPrefab;
	// Use this for initialization
	void Start () {
		ingredientsInputField = GameObject.FindGameObjectsWithTag("IngredientsInputField");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickReactor(GameObject prefab){
		Player.self.holding = prefab;
	}
	public void OnClickIngredient(Compound c){
		InputField inputField = c.GetComponentInChildren<InputField>();
		Text amountText = inputField.GetComponentInChildren<Text>();
		if(c.amount >= 1.0f){
			c.amount -= 1.0f;
			inputField.GetComponent<InputFieldScript>().amountStored += 1.0f;
			amountText.text = inputField.GetComponent<InputFieldScript>().amountStored.ToString("0.0");
			Text btnText = c.gameObject.transform.Find("Text").GetComponent<Text>();
			btnText.text = c.name + "   (" + c.amount.ToString("0.0") + ")";
		}
	}
	public void OnClickAddIngredients(){
		if(Player.self.selectedReactor == null){
			Debug.Log("NO reactor selected");
			return;
		}
		Debug.Log("adding ingredients");
		Reactor reactor = Player.self.selectedReactor.GetComponent<Reactor>();
		foreach(GameObject g in ingredientsInputField){
			InputFieldScript ifs = g.GetComponent<InputFieldScript>();
			if(reactor.compounds.ContainsKey(ifs.name)){
				Compound c = reactor.compounds[ifs.name];
				c.amount += ifs.amountStored;
				reactor.compounds.Add(ifs.name, c);
			}else{
				//create compound
				GameObject newCompound = Instantiate(emptyCompoundPrefab) as GameObject;
				Compound c = newCompound.GetComponent<Compound>();
				c.name = ifs.name;
				c.amount = ifs.amountStored;
				reactor.compounds.Add(c.name, c);
			}
		}
		foreach(KeyValuePair<string, Compound> pair in reactor.compounds){
			Debug.Log(pair.Key + ": " + pair.Value.amount);
		}
	}
	public void OnClickAddCondition(Condition cond){
		if(Player.self.selectedReactor == null){
			Debug.Log("No reactor selected");
			return;
		}
		Reactor reactor = Player.self.selectedReactor.GetComponent<Reactor>();
		foreach(Condition c in reactor.conditions){
			if(c.type == cond.type){
				return;
			}
		}
		GameObject condition = Instantiate(cond.gameObject) as GameObject;
		condition.transform.parent = Player.self.selectedReactor.transform;
		condition.transform.localPosition = condition.GetComponent<Condition>().relativePosToReactor;
		reactor.conditions.Add(cond);

	}
}
