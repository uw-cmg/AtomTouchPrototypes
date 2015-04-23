using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIControlSandbox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
}
