using UnityEngine;
using System.Collections;

//for prototype #8: guess where atoms are at the start of
//the next time period

//all normal atoms that are always rendered
public class AtomGuess2D : MonoBehaviour{
	//the model of this copy
	
	public int charge;
	public Vector2 totalForce = Vector2.zero;
	public CircleCollider2D cc;
	public GameObject renderedObj;

	void Start () {	
		GetComponent<Renderer>().enabled = false;
		renderedObj.transform.position = transform.position;
		renderedObj.GetComponent<Renderer>().enabled = true;	
	}
	
	public bool WithinViewport(){
		if(cc == null){
			//Debug.Log("cc is null");
			return true;
		}
		Vector2 rightMostPoint = transform.position + new Vector3(-cc.radius,0, 0);
		Vector2 leftMostPoint = transform.position + new Vector3(cc.radius,0, 0);
		Vector2 topPoint = transform.position + new Vector3(0, -cc.radius, 0);
		Vector2 bottomPoint = transform.position + new Vector3(0, cc.radius, 0);
	
		//right bound
		Vector2 viewportRightPoint = Camera.main.WorldToViewportPoint(leftMostPoint);
		//left bound
		Vector2 viewportLeftPoint = Camera.main.WorldToViewportPoint(rightMostPoint);
		//top bound
		Vector2 viewportTopPoint = Camera.main.WorldToViewportPoint(bottomPoint);
		//bottom boud
		Vector2 viewportBottomPoint = Camera.main.WorldToViewportPoint(topPoint);

		if(viewportRightPoint.x < -0.1f || viewportRightPoint.x > 1.1f){
//			Debug.Log(viewportRightPoint);
			return false;
		}
		if(viewportLeftPoint.x < -0.1f || viewportLeftPoint.x > 1.1f){
	//		Debug.Log(viewportLeftPoint);
			return false;
		}
		if(viewportTopPoint.y < -0.1f || viewportTopPoint.y > 1.1f){
			return false;
		}
		if(viewportBottomPoint.y < -0.1f || viewportBottomPoint.y > 1.1f){
			return false;
		}
		return true;
	}
}
