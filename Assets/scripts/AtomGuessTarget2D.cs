using UnityEngine;
using System.Collections;

//prototype #8
//atoms to be guessed, not rendered, but physics calculated
public class AtomGuessTarget2D : AtomGuess2D {
	public GameObject predictedAtom;
	public GameObject renderedAtom;
	public GameObject renderedPrevious;
	public GameObject arrow;
	private GameObject arrowBody;
	private GameObject arrowHead;
	void Awake(){
		arrowBody = arrow.transform.Find("arrowbody").gameObject;
		arrowHead = arrow.transform.Find("arrowhead").gameObject;
		ShowArrow(false);
	}
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().enabled = false;
		renderedAtom.GetComponent<Renderer>().enabled = true;
		renderedPrevious.transform.position = transform.position;
		renderedPrevious.GetComponent<Renderer>().enabled = false;
	}
	public void ShowArrow(bool show = true){
		arrowBody.GetComponent<Renderer>().enabled = show;
		arrowHead.GetComponent<Renderer>().enabled = show;
	}
	//connect old position and current position with an arrow
	public void ConnectArrow(){

		Vector2 oldToCurrent = renderedAtom.transform.position
			-renderedPrevious.transform.position;
		float dist = oldToCurrent.magnitude;
		//scale body
		arrowBody.transform.localScale = new Vector3(
			dist - 2*cc.radius, 
			arrowBody.transform.localScale.y, 
			1f);
		
		//translate both
		arrow.transform.position = renderedPrevious.transform.position 
			+ Vector3.MoveTowards(renderedPrevious.transform.position,
			renderedAtom.transform.position, cc.radius/dist);
		
		//arrow head
		//translate head

		arrowHead.transform.localPosition 
			= Vector2.right * 
			arrowBody.transform.localScale.x;
		//no need to scale since arrow head's and body's y's always matchs

		//rotate both
		float rotAngle = GetAngleInWholeRange(Vector2.right, oldToCurrent);
		//float rotAngle = Vector2.Angle(Vector2.right, oldToCurrent);
		arrow.transform.rotation = Quaternion.Euler(0,0,rotAngle);
		//show arrow
		ShowArrow();
	}

	public float GetAngleInWholeRange(Vector2 from, Vector2 to){
		Vector3 from3 = new Vector3(from.x, from.y, 1f);

		Vector3 to3 = new Vector3(to.x, to.y, 1f);

		float dotProduct = Vector2.Dot(from, to);
		float cos = dotProduct / (from.magnitude * to.magnitude);
		float rotAngle = Mathf.Acos(cos) * Mathf.Rad2Deg;

		Vector3 crossProduct = Vector3.Cross(from3, to3);
		if(crossProduct.z > 0){
			//0 to 180
			//do nothing
		}else{
			//-180 to 0 
			rotAngle *= -1;
		}
		return rotAngle;
	}
	// Update is called once per frame
	void Update () {
	
	}

}
