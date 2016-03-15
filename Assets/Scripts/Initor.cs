using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Initor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SceneManager.LoadSceneAsync ("FileTest");
		//Application.LoadLevelAsync ("FileTest");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
