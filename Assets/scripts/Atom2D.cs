using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Atom2D : MonoBehaviour {
	public static Atom2D self;
	public int charge;
	public Button btn; //corresponding add atom btn
	public Vector2 totalForce = Vector2.zero;
	public CircleCollider2D cc;
	public bool visited;
	public List<Atom2D> neighbours;
	//for connect monsters game
	public Color normalColor;
	public Color pathColor;
	public GameObject pathHighlighter;
	// Use this for initialization
	void Awake(){
		self = this;
		visited = false;
		if(Application.loadedLevelName == "ConnectMonsters"){
			neighbours = new List<Atom2D>();
			//pathHighlighter = transform.Find("PathHighlighter").gameObject;
		}

		//by default, path color is white
		pathColor = Color.white;
	}
	void Start () {
		if(Application.loadedLevelName == "ConnectMonsters"){
			pathHighlighter = transform.Find("PathHighlighter").gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D collision){
		if(Application.loadedLevelName != "ConnectMonsters")return;
		if(collision.gameObject.tag != "Atom"
			&& collision.gameObject.tag != "MonsterAnchor")return;
		neighbours.Add(collision.gameObject.GetComponent<Atom2D>());
	}
	void OnCollisionExit2D(Collision2D collision){
		if(Application.loadedLevelName != "ConnectMonsters")return;
		if(collision.gameObject.tag != "Atom"
			&& collision.gameObject.tag != "MonsterAnchor")return;
		neighbours.Remove(collision.gameObject.GetComponent<Atom2D>());
	}
	//gives a random vel
	public void Kick(){
		float lo = -15.50f;
		float hi = 15.50f;
		float x = UnityEngine.Random.Range(lo, hi);
		float y = UnityEngine.Random.Range(lo, hi);
		GetComponent<Rigidbody2D>().velocity = new Vector2(x,y);
	}
	//used for destroying atoms beyond viewport
	//if it's the target atom, lose game
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
