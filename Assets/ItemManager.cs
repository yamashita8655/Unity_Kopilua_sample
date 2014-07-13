using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public sealed class ItemManager {

	private static ItemManager instance = new ItemManager();
	public static ItemManager Instance 
	{
		get { return instance; }
	}

	private ItemManager()
	{
	}

	public class ItemData
	{
		public string itemId;
		public string itemName;
		public string effectType;
		public int effectValue;
	}

	string dataPath = "Assets/resource/item.csv";

	Dictionary<string, ItemData> itemDataList = new Dictionary<string, ItemData>();

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
			ItemData data = new ItemData();
			string[] word = line.Split(chara);
			data.itemId = word[0];
			data.itemName = word[1];
			data.effectType = word[2];
			data.effectValue = int.Parse(word[3]);

			itemDataList.Add(data.itemId, data);
		}

	}
	
	public ItemData GetItemData(string itemId)
	{
		ItemData data = null;
		if(itemDataList.ContainsKey(itemId) == false)
		{
			return data;
		}

		data = itemDataList[itemId];
		return data;
	}
}
