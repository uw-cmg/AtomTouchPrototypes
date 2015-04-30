using UnityEngine;
using System.Collections;

public class AtomStaticData : MonoBehaviour {
	public static int CuStartStock;
	public static int NaStartStock;
	public static int ClStartStock;

	public static int CuRemainingStock;
	public static int NaRemainingStock;
	public static int ClRemainingStock;
	void Awake(){
		CuStartStock = 10;
		NaStartStock = 15;
		ClStartStock = 20;
		CuRemainingStock = CuStartStock;
		NaRemainingStock = NaStartStock;
		ClRemainingStock = ClStartStock;
	}
}
