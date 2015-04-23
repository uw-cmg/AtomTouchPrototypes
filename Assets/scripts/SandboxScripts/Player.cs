using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {
	public static Player self;
	public GameObject holding;//selected a facility in the menu
	public GameObject selectedReactor;
	public List<RaycastResult> raycastResults;
	void Awake(){
		self = this;
	}
	// Use this for initialization
	void Start () {
		holding = null;
		raycastResults = new List<RaycastResult>();
	}
	
	// Update is called once per frame
	void Update () {
		if(holding != null){
			//is reactor
			if(holding.GetComponent<Reactor>() != null){
				//handle mouse lick
				//need to click in empty space
				HandleAddingFacility(null);
			}else if(holding.GetComponent<Collector>() != null){
				//need to click on a reactor
				//HandleAddingFacility()
			}
		}else{
			//get mouse click position if hits reactor, hightlight it
			if(Input.GetMouseButtonDown(0)){
				Vector3 mousePos = Input.mousePosition;
				mousePos.z += 5.0f;
				Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
				//raycast
				RaycastHit2D hit = Physics2D.Raycast(
					worldPos, 
					Vector3.forward,
					10.0f,
					LayerMask.GetMask("Reactor")
					);
				bool mouseClickUI = EventSystem.current.IsPointerOverGameObject();

				if(hit != null && hit.collider != null){
				
					selectedReactor = hit.collider.gameObject;
					//highlight reactor
					hit.collider.gameObject.GetComponent<SpriteRenderer>().color
						= new Color(193f/255f, 88f/255f, 1.0f, 1.0f);
					
					
				}else{
					if(mouseClickUI){
						Debug.Log("on ui");
					}else{
						if(selectedReactor != null){
							selectedReactor.GetComponent<SpriteRenderer>().color = Color.white;
						}
						selectedReactor = null;
					}
					

				}
			}
		}
	}

	void HandleAddingFacility(GameObject raycastObject){

	}
}
