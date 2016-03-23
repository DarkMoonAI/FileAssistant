using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Initor : MonoBehaviour {

	void Awake(){
		Screen.SetResolution (776, 485, false);
	}
	// Use this for initialization
	void Start () {
		//SceneManager.LoadSceneAsync ("FileTest");
		SceneManager.LoadSceneAsync ("SceneChange");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
