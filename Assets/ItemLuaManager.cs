using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public sealed class ItemLuaManager {

	private static ItemLuaManager instance = new ItemLuaManager();
	public static ItemLuaManager Instance 
	{
		get { return instance; }
	}

	private ItemLuaManager()
	{
	}

	public class ItemLuaData
	{
		public string itemId;
		public string itemName;
		public string luaFileName;
		public string luaFunctionName;
	}

	string dataPath = "Assets/resource/item_lua.csv";

	Dictionary<string, ItemLuaData> itemDataList = new Dictionary<string, ItemLuaData>();

	public void Load()
	{
		string line = "";
		StreamReader sr = new StreamReader(dataPath, Encoding.GetEncoding("Shift_JIS"));

		bool isFirst = true;
		while((line = sr.ReadLine()) != null)
		{
			if(isFirst == true)
			{
				isFirst = false;
				continue;
			}
			char[] chara = { ',' };
			ItemLuaData data = new ItemLuaData();
			string[] word = line.Split(chara);
			data.itemId = word[0];
			data.itemName = word[1];
			data.luaFileName = word[2];
			data.luaFunctionName = word[3];

			itemDataList.Add(data.itemId, data);
		}

	}
	
	public ItemLuaData GetItemData(string itemId)
	{
		ItemLuaData data = null;
		if(itemDataList.ContainsKey(itemId) == false)
		{
			return data;
		}

		data = itemDataList[itemId];
		return data;
	}
}
