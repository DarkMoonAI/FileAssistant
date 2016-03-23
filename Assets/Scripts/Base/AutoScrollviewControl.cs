using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AutoScrollviewControl : MonoBehaviour {
	public ScrollRect parentScroll;
	public GameObject itemForScroll_prefab;				//item 预置
	private List<GameObject>  itemsList;				//item 容器
	public int itemCount;								//item 数量
	public float itemSpace;								//item 间隔
	private bool isHorizontal=false;					//水平还是竖直
	private float itemWidth;							//item 宽度
	private float itemHeight;							//item 高度
	private GameObject m_item;
	private RectTransform rectTrans;
	// Use this for initialization
	void Start () {
		itemWidth = itemForScroll_prefab.GetComponent<RectTransform> ().sizeDelta.x;
		itemHeight = itemForScroll_prefab.GetComponent<RectTransform> ().sizeDelta.y;
		itemsList = new List<GameObject> ();
		rectTrans = gameObject.GetComponent<RectTransform> ();
		getDirection ();
		InitItem ();
	}
	#region item
	/// <summary>
	/// Inits the item.
	/// </summary>
	public void InitItem(){
		if (!isHorizontal) {
			InitVerItem ();
		} else {
			InitHorItem ();
		}

	}
	void InitHorItem(){
		rectTrans.localPosition = new Vector3 (0, rectTrans.localPosition.y, rectTrans.localPosition.z);
		rectTrans.sizeDelta = new Vector2 (getContentHorSize(),0);
		for (int i = 0; i < itemCount; i++) {
			m_item = Instantiate (itemForScroll_prefab);
			m_item.transform.SetParent(this.gameObject.transform);
			m_item.GetComponent<RectTransform> ().sizeDelta= new Vector2(m_item.GetComponent<RectTransform> ().sizeDelta.x,0);
			itemHeight = m_item.GetComponent<RectTransform> ().rect.height;
			m_item.GetComponent<RectTransform> ().localPosition = new Vector3 (i * (itemWidth+ itemSpace)+ 0.5f * itemWidth,-0.5f * itemHeight,0);
			m_item.GetComponent<UIImageForScroll> ().flag = i;
			itemsList.Add (m_item);
		}
	}
	void InitVerItem(){
		rectTrans.sizeDelta = new Vector2 (0,getContentVerSize());
		for (int i = 0; i < itemCount; i++) {
			m_item = Instantiate (itemForScroll_prefab);
			m_item.transform.SetParent(this.gameObject.transform);
			m_item.GetComponent<RectTransform> ().sizeDelta= new Vector2(-20,m_item.GetComponent<RectTransform> ().sizeDelta.y);
			itemWidth = m_item.GetComponent<RectTransform> ().rect.width;
			m_item.GetComponent<RectTransform> ().localPosition = new Vector3 (0.5f * itemWidth, -i * (itemHeight + itemSpace) - 0.5f * itemHeight, 0);
			m_item.GetComponent<UIImageForScroll> ().flag = i;
			itemsList.Add (m_item);
		}
	}
	/// <summary>
	/// Deinits the item.
	/// </summary>
	public void DeinitItem(){
		foreach (GameObject obj in itemsList) {
			Destroy (obj);
		}
		itemsList.Clear ();
	}
	/// <summary>
	/// Updates the item.
	/// </summary>
	public void UpdateItem(int itemCount){
		this.itemCount = itemCount;
		UpdateItem ();
	}
	public void UpdateItem(){
		DeinitItem ();
		InitItem ();
	}
	#endregion 

	#region scroll
	/// <summary>
	/// Gets the Horizontal size of the content.
	/// </summary>
	/// <returns>The content size.</returns>
	float getContentHorSize(){
		return itemCount * (itemWidth + itemSpace);
	}
	/// <summary>
	/// Gets the ver vertical size of the content.
	/// </summary>
	/// <returns>The content size.</returns>
	float getContentVerSize(){
		return itemCount * (itemHeight + itemSpace);
	}
	/// <summary>
	/// Gets the direction.
	/// 注意：父scrollview不可以同时启用水平和竖直
	/// </summary>
	public void getDirection(){
		if (parentScroll.horizontal && parentScroll.vertical) {
			parentScroll.vertical = false;
			isHorizontal = false;
		} else {
			isHorizontal = parentScroll.horizontal;
		}
	}
	#endregion 
}
