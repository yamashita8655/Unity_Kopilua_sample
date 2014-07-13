using UnityEngine;
using System.Collections;

public class main : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ItemManager.Instance.Load ();
		ItemLuaManager.Instance.Load ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
