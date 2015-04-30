using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {
	public static UIControl self;
	public GameObject endGamePanel;
	public GameObject tryAgainBtn;
	public GameObject nextLevelBtn;
	public GameObject atomMenuPanel;

	[HideInInspector]public Button cuBtn;
	[HideInInspector]public Button naBtn;
	[HideInInspector]public Button clBtn;

	public Text endGameText;
	public Text timerText;
	void Awake(){
		self = this;
		endGamePanel.SetActive(false);
		//find buttons
		Transform atomMenuPanelTransform = atomMenuPanel.transform;
		cuBtn = atomMenuPanelTransform.Find("Cu").gameObject.GetComponent<Button>();
		naBtn = atomMenuPanelTransform.Find("Na").gameObject.GetComponent<Button>();
		clBtn = atomMenuPanelTransform.Find("Cl").gameObject.GetComponent<Button>();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnHoverConnectionEntry(MonsterAtomConnection mac){
		mac.ShowPath();
		//mac.start.GetComponent<SpriteRenderer>().color = Color.white;
		//mac.end.GetComponent<SpriteRenderer>().color = Color.white;
	}
	public void OnLeaveConnectionEntry(MonsterAtomConnection mac){
		mac.HidePath();
		//mac.start.GetComponent<SpriteRenderer>().color = mac.start.normalColor;
		//mac.end.GetComponent<SpriteRenderer>().color = mac.end.normalColor;
	}
	public void UpdateTimer(float timeRemaining){
		timerText.text = Mathf.Max(0.0f, timeRemaining) + "";
	}

	public void OnAddAtom(GameObject prefab){
		//fade that atom button
		//DisableAtomBtn(prefab.GetComponent<Atom2D>());
		GameControl2D.self.CreateAtom(prefab);
	}
	public void EnableAtomBtns(bool enable = true){
		cuBtn.interactable = enable;
		naBtn.interactable = enable;
		clBtn.interactable = enable;
	}
	public void OnClickAtomBtn(){
		EnableAtomBtns(false);
	}
	public void EndGame(bool win){
		if(win){
			endGameText.text = "You Won nyo >w<!";
			tryAgainBtn.SetActive(false);
			nextLevelBtn.SetActive(true);
		}else{
			endGameText.text = "You Lost nio QvQ!";
			tryAgainBtn.SetActive(true);
			nextLevelBtn.SetActive(false);
		}
		
		endGamePanel.SetActive(true);
		Time.timeScale = 0;
	}
	public void OnClickQuit(){
		Application.Quit();
	}
	public void OnClickTryAgain(){
		Application.LoadLevel("main");
	}

}
