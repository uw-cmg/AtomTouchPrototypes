using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {
	public static UIControl self;
	public GameObject endGamePanel;
	public GameObject tryAgainBtn;
	public GameObject nextLevelBtn;
	public GameObject atomMenuPanel;
	public Text scoreText;
	public Text scoreTextInStat;// score in the end of game stat
	public Text numberOfConnections;//in end of game stat
	public Text numberOfUsedAtomsText;//in end of game stat

	public Text endGameText;
	public Text timerText;
	void Awake(){
		self = this;
		endGamePanel.SetActive(false);
	}
	// Use this for initialization
	void Start () {
		if(Application.loadedLevelName == "ConnectMonsters"){
			foreach(AtomUIData atomUIData in AtomStaticData.AtomDataMap.Values){
				atomUIData.btnText.text = atomUIData.atomName + " (" 
					+ atomUIData.remainingStock + ")";
			}
		}
	}
	//for connect monsters
	public void OnGameEnded(){
		//update game stats
		scoreTextInStat.text = GameControl2D.self.score.ToString("0.0");
		numberOfConnections.text 
			= MonsterAtomManager.self.totalConnections + "/" 
			+ MonsterAtomManager.self.atomConnections.Count; 
		numberOfUsedAtomsText.text = GameControl2D.self.totalAtomsUsed.ToString("0");

		endGamePanel.SetActive(true);
	}
	public void OnHoverConnectionEntry(MonsterAtomConnection mac){
		mac.ShowPath();
	}
	public void OnLeaveConnectionEntry(MonsterAtomConnection mac){
		mac.HidePath();
	}
	public void UpdateTimer(float timeRemaining){
		timerText.text = Mathf.Max(0.0f, timeRemaining).ToString("0.0");
	}

	public void OnAddAtom(GameObject prefab){
		//fade that atom button
		GameControl2D.self.CreateAtom(prefab);

	}
	public void EnableAtomBtns(bool enable = true){
		if(Application.loadedLevelName != "ConnectMonsters"){
			
			foreach(AtomUIData atomUIData in AtomStaticData.AtomDataMap.Values){
				atomUIData.btn.interactable = enable;
			}
			
		}else{
			if(GameControl2D.self.gameState == (int)GameControl2D.GameState.AddingAtom){
				
				foreach(AtomUIData atomUIData in AtomStaticData.AtomDataMap.Values){
					atomUIData.btn.interactable = false;
				}
				return;
			}
			foreach(AtomUIData atomUIData in AtomStaticData.AtomDataMap.Values){
				atomUIData.btn.interactable = (atomUIData.remainingStock > 0);
			}
		}
		
	}
	public void UpdateScore(){
		scoreText.text = GameControl2D.self.score.ToString("0.0");
	}
	public void UpdateAtomBtnWithStock(Atom2D newlyAddedAtom){
		if(Application.loadedLevelName == "ConnectMonsters"){
			AtomUIData atomUIData = AtomStaticData.AtomDataMap[newlyAddedAtom.name];
			atomUIData.btnText.text = newlyAddedAtom.name + " (" + 
				atomUIData.remainingStock + ")";
			if(atomUIData.remainingStock <= 0){
				atomUIData.btn.interactable = false;
			}

		}
	}
	public void OnClickAtomBtn(){
		EnableAtomBtns(false);
	}
	//for gooey
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
	}
	public void OnClickQuit(){
		Application.Quit();
	}
	public void OnClickTryAgain(){
		Application.LoadLevel("main");
	}

}
