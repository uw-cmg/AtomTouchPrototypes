using UnityEngine;
using System.Collections;

public class AtomSpawner : MonoBehaviour {
	public float spawnFrequency; //per second
	public Vector2 xMinYmin;
	public Vector2 xMaxYmax;
	public float maxForceDegree = 30.0f; //in degrees
	public GameObject clPrefab;
	// Use this for initialization
	void Awake(){
		spawnFrequency = 5.0f;
	}
	void Start () {
		xMinYmin = Camera.main.ViewportToWorldPoint(Vector2.zero);
		xMaxYmax = Camera.main.ViewportToWorldPoint(new Vector2(1.0f,1.0f));
		StartCoroutine(SpawnAtoms());
	}
	IEnumerator SpawnAtoms(){
		while(Time.timeScale > 0){
			yield return new WaitForSeconds(spawnFrequency);
			//spawn
			float edgeRnd = Random.Range(0.0f, 4.0f);

			float spawnX = Random.Range(xMinYmin.x, xMaxYmax.x);
			float spawnY = Random.Range(xMinYmin.y, xMaxYmax.y);

			//force: max angle: 30
			Vector2 force = new Vector2(1.0f, 0.0f);


			if(edgeRnd < 1.0f){
				//left vertical
				spawnX = xMinYmin.x;
				float forceAngleRnd = Random.Range(-maxForceDegree,maxForceDegree) 
				* Mathf.Deg2Rad;
				force.y = Mathf.Sin(forceAngleRnd) * force.x;
			}else if(edgeRnd < 2.0f){
				//top horizontal
				spawnY = xMaxYmax.y;
				float forceAngleRnd = Random.Range(-maxForceDegree + 180, 360-maxForceDegree) 
					* Mathf.Deg2Rad;
				force.y = Mathf.Sin(forceAngleRnd) * force.x;
			}else if(edgeRnd < 3.0f){
				//right vertical
				spawnX = xMaxYmax.x;
				float forceAngleRnd = Random.Range(180-maxForceDegree,180+maxForceDegree) 
					* Mathf.Deg2Rad;
				force.y = Mathf.Sin(forceAngleRnd) * force.x;
			}else if(edgeRnd < 4.0f){
				//bottom horizontal
				spawnY = xMinYmin.y;
				float forceAngleRnd = Random.Range(maxForceDegree,180-maxForceDegree) 
				* Mathf.Deg2Rad;
				force.y = Mathf.Sin(forceAngleRnd) * force.x;
			}
			force.Normalize();
			float forceMag = Random.Range(1.0f,5.0f);
			Spawn(clPrefab, new Vector2(spawnX, spawnY), forceMag * force);

		}
		
	}
	public void Spawn(GameObject prefab, Vector2 spawnPos, Vector2 vel){
		
		Quaternion rotation = Quaternion.Euler(0, 0, 0);
		GameObject atom = Instantiate(prefab, spawnPos, rotation) as GameObject;
		atom.GetComponent<Rigidbody2D>().velocity = vel;
		atom.GetComponent<Rigidbody2D>().isKinematic = false;
		AtomPhysics2D.self.Ions.Add(atom);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
