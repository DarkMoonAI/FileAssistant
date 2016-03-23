using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Btn_SceneChange : MonoBehaviour {
	public enum LoadInfoType
	{
		STRING,
		INT
	}
	public LoadInfoType m_loadInfoType;
	public int LoadSceneNumer;
	public string LoadSceneName;
	public void OnClick(){
		if (m_loadInfoType == LoadInfoType.INT) {
			SceneManager.LoadSceneAsync (LoadSceneNumer);
		} else {
			SceneManager.LoadSceneAsync (LoadSceneName);
		}
	}
}
