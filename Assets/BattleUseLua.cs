using UnityEngine;
using System.Collections;
using KopiLua;

public class BattleUseLua : MonoBehaviour {

	Chara MyPlyaer = new Chara();
	Chara EnemyPlyaer = new Chara();
	
	// Use this for initialization
	void Start () {
		MyPlyaer.mNowHp = 100;
		MyPlyaer.mMaxHp = 100;
		MyPlyaer.mNowMp = 100;
		MyPlyaer.mMaxMp = 100;
		MyPlyaer.mAttackPoint = 10;
		MyPlyaer.mDefencePoint = 5;
		
		EnemyPlyaer.mNowHp = 100;
		EnemyPlyaer.mMaxHp = 100;
		EnemyPlyaer.mNowMp = 100;
		EnemyPlyaer.mMaxMp = 100;
		EnemyPlyaer.mAttackPoint = 10;
		EnemyPlyaer.mDefencePoint = 5;

		NormalAttack_Lua(MyPlyaer, EnemyPlyaer);
		UseItemLua(MyPlyaer, EnemyPlyaer, "1");
		UseItemLua(MyPlyaer, EnemyPlyaer, "2");
	}


	void NormalAttack_Lua(Chara attaker, Chara defender)
	{
		LuaState lstate = Lua.LuaOpen ();
		int res = Lua.LuaLLoadFile (lstate, "C:/takuya/unity/luatest/Assets/luafunction/normalattack.lua");
		Lua.LuaPCall(lstate, 0, Lua.LUA_MULTRET, 0);

		// Luaで定義した関数をスタックに積む。Luaは関数も変数のひとつに過ぎないらしい
		Lua.LuaGetGlobal(lstate, "normalattack");
		
		// Lua側に、プレイヤーの情報（状態）を渡す
		// これを渡せば、詳細はLua側でやってくれる
		// ほしいのは、各プレイヤーの状態（＝残りHP）さえわかればいいので
		// その過程を知る必要がなくなる
		Lua.LuaPushInteger(lstate, attaker.mNowHp);
		Lua.LuaPushInteger(lstate, attaker.mMaxHp);
		Lua.LuaPushInteger(lstate, attaker.mAttackPoint);
		Lua.LuaPushInteger(lstate, attaker.mDefencePoint);
		Lua.LuaPushInteger(lstate, defender.mNowHp);
		Lua.LuaPushInteger(lstate, defender.mMaxHp);
		Lua.LuaPushInteger(lstate, defender.mAttackPoint);
		Lua.LuaPushInteger(lstate, defender.mDefencePoint);

		// Lua関数を実行
		//if(Lua.LuaPCall(lstate, 2, 4, 0))
		//{
		//}

		res = Lua.LuaPCall (lstate, 8, 2, 0);

		// 戻り値がスタックに積まれているらしいので、取得
		int attaker_nowhp = Lua.LuaToInteger(lstate, 1);
		int defender_nowhp = Lua.LuaToInteger(lstate, 2);

		defender.mNowHp = defender_nowhp;

		Lua.LuaClose (lstate);
	}
	
	void UseItemLua(Chara attaker, Chara defender, string itemid)
	{
		LuaState lstate = Lua.LuaOpen ();

		ItemLuaManager.ItemLuaData itemdata = ItemLuaManager.Instance.GetItemData(itemid);

		string luapath = System.String.Format("C:/takuya/unity/luatest/Assets/luafunction/{0}.lua", itemdata.luaFileName);

		int res = Lua.LuaLLoadFile (lstate, luapath);
		Lua.LuaPCall(lstate, 0, Lua.LUA_MULTRET, 0);

		// Luaで定義した関数をスタックに積む。Luaは関数も変数のひとつに過ぎないらしい
		Lua.LuaGetGlobal(lstate, itemdata.luaFunctionName);
		
		// 関数に指定する引数をスタックに積む
		Lua.LuaPushInteger(lstate, attaker.mNowHp);
		Lua.LuaPushInteger(lstate, attaker.mMaxHp);
		Lua.LuaPushInteger(lstate, attaker.mAttackPoint);
		Lua.LuaPushInteger(lstate, attaker.mDefencePoint);
		Lua.LuaPushInteger(lstate, defender.mNowHp);
		Lua.LuaPushInteger(lstate, defender.mMaxHp);
		Lua.LuaPushInteger(lstate, defender.mAttackPoint);
		Lua.LuaPushInteger(lstate, defender.mDefencePoint);

		// Lua関数を実行
		//if(Lua.LuaPCall(lstate, 2, 4, 0))
		//{
		//}

		res = Lua.LuaPCall (lstate, 8, 2, 0);

		// 戻り値がスタックに積まれているらしいので、取得
		int attaker_nowhp = Lua.LuaToInteger(lstate, 1);
		int defender_nowhp = Lua.LuaToInteger(lstate, 2);

		defender.mNowHp = defender_nowhp;
		
		Lua.LuaClose (lstate);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
