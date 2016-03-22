using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.IO;
public delegate void voidDelegate();
public class CommonTool : MonoBehaviour {
	private static CommonTool m_instance;
	public static CommonTool Instance{
		get{return m_instance;}
	}
	void Awake(){
		m_instance = this;
	}
	#region zhuanma
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
	#endregion 

	#region file
	/// <summary>  
	/// 获取所有文件  
	/// </summary>  
	/// <returns></returns>  
	public Hashtable getAllFies(string dir)  
	{  
		Hashtable FilesList = new Hashtable();  
		DirectoryInfo fileDire = new DirectoryInfo(dir);  
		if (!fileDire.Exists)  
		{  
			throw new System.IO.FileNotFoundException("目录:" + fileDire.FullName + "没有找到!");  
		}  

		this.getAllDirFiles(fileDire, FilesList);  
		this.getAllDirsFiles(fileDire.GetDirectories(), FilesList);  
		return FilesList;  
	}
	/// <summary>
	/// 获取一个文件夹下的所有的文件
	/// 不包含子文件夹
	/// </summary>
	public Hashtable getAllDirsFiles(string dir){
		Hashtable FilesList = new Hashtable();  
		DirectoryInfo fileDire = new DirectoryInfo(dir);
		if (!fileDire.Exists)  
		{  
			throw new System.IO.FileNotFoundException("目录:" + fileDire.FullName + "没有找到!");  
		}  

		this.getAllDirFiles(fileDire, FilesList); 
		return FilesList;
	}
	/// <summary>
	/// 获取一个文件夹下的所有的文件夹数组
	/// 不包含子文件夹
	/// </summary>
	public DirectoryInfo[] getAllDirs(string dir){
		DirectoryInfo fileDire = new DirectoryInfo(dir);
		if (!fileDire.Exists)  
		{  
			throw new System.IO.FileNotFoundException("目录:" + fileDire.FullName + "没有找到!");  
		}  
		return fileDire.GetDirectories();
	}
	/// <summary>  
	/// 获取一个文件夹下的所有文件夹里的文件  
	/// </summary>  
	/// <param name="dirs"></param>  
	/// <param name="filesList"></param>  
	private void getAllDirsFiles(DirectoryInfo[] dirs, Hashtable filesList)  
	{  
		foreach (DirectoryInfo dir in dirs)  
		{  
			foreach (FileInfo file in dir.GetFiles("*.*"))  
			{  
				filesList.Add(file.FullName, file.LastWriteTime);  
			}  
			this.getAllDirsFiles(dir.GetDirectories(), filesList);  
		}  
	}  
	/// <summary>  
	/// 获取一个文件夹下的文件 
	/// 不包含子文件夹 
	/// </summary>  
	/// <param name="strDirName">目录名称</param>  
	/// <param name="filesList">文件列表HastTable</param>  
	private void getAllDirFiles(DirectoryInfo dir, Hashtable filesList)  
	{  
		foreach (FileInfo file in dir.GetFiles("*.*"))  
		{  
			filesList.Add(file.FullName, file.LastWriteTime);  
		}  
	}  
	#endregion 
}
