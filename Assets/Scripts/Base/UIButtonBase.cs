using UnityEngine;
using System.Collections;

public class UIButtonBase : MonoBehaviour {
	public int flag;
	public voidDelegate onClickDelegate;
	public void OnClick(){
		if (onClickDelegate != null)
			this.onClickDelegate ();
	}
}
