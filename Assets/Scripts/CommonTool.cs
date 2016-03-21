using UnityEngine;
using System.Collections;
using System.Text;

public class CommonTool : MonoBehaviour {
	private static CommonTool m_instance;
	public static CommonTool Instance{
		get{return m_instance;}
	}
	void Awake(){
		m_instance = this;
	}
	/// <summary>
	/// GB2312转换成UTF8
	/// </summary>
	/// <param name="text"></param>
	/// <returns></returns>
	public static string gb2312_utf8(string text)
	{
		//声明字符集   
		Encoding utf8, gb2312;
		//gb2312   
		gb2312 = Encoding.GetEncoding("gb2312");
		//utf8   
		utf8 = Encoding.GetEncoding("utf-8");
		byte[] gb;
		gb = gb2312.GetBytes(text);
		gb = Encoding.Convert(gb2312, utf8, gb);
		//返回转换后的字符   
		return utf8.GetString(gb);
	}

	/// <summary>
	/// UTF8转换成GB2312
	/// </summary>
	/// <param name="text"></param>
	/// <returns></returns>
	public static string utf8_gb2312(string text)
	{
		//声明字符集   
		Encoding utf8, gb2312;
		//utf8   
		utf8 = Encoding.GetEncoding("utf-8");
		//gb2312   
		gb2312 = Encoding.GetEncoding("gb2312");
		byte[] utf;
		utf = utf8.GetBytes(text);
		utf = Encoding.Convert(utf8, gb2312, utf);
		//返回转换后的字符   
		return gb2312.GetString(utf);
	}
}
