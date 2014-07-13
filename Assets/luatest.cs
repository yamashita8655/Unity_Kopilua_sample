using UnityEngine;
using System.Collections;
using KopiLua;

public class luatest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		test1 ();
		test2 ();
		test3 ();
		test4 ();
		test5 ();
	}

	void test1()
	{
		// C++でいうLuaState作成
		LuaState lstate = Lua.LuaOpen ();

		// LoadDoFile的なやつ
		int res = Lua.LuaLLoadFile (lstate, "C:/takuya/unity/luatest/Assets/load_lua.lua");
		// C++はマクロでこれ読んでkるけど、C#はこれを自前で呼び出す必要あり
		Lua.LuaPCall(lstate, 0, Lua.LUA_MULTRET, 0);

		// 各数値取得
		int num = Lua.LuaGetTop (lstate);
		Lua.LuaGetGlobal(lstate, "windowWidth");
		Lua.LuaGetGlobal(lstate, "windowHeight");
		Lua.LuaGetGlobal(lstate, "windowName");
		Lua.LuaGetGlobal(lstate, "testboolean");
		printStack(lstate);

		Lua.LuaClose (lstate);
	}

	void test2()
	{
		LuaState lstate = Lua.LuaOpen ();
		int res = Lua.LuaLLoadFile (lstate, "C:/takuya/unity/luatest/Assets/function_lua.lua");
		Lua.LuaPCall(lstate, 0, Lua.LUA_MULTRET, 0);

		// Luaで定義した関数をスタックに積む。Luaは関数も変数のひとつに過ぎないらしい
		Lua.LuaGetGlobal(lstate, "calc");
		
		// 関数に指定する引数をスタックに積む
		Lua.LuaPushNumber(lstate, 100);
		Lua.LuaPushNumber(lstate, 200);

		// Lua関数を実行
		//if(Lua.LuaPCall(lstate, 2, 4, 0))
		//{
		//}

		res = Lua.LuaPCall (lstate, 2, 4, 0);

		// 戻り値がスタックに積まれているらしいので、取得
		double add_res = Lua.LuaToNumber(lstate, 1);
		double sub_res = Lua.LuaToNumber(lstate, 2);
		double mult_res = Lua.LuaToNumber(lstate, 3);
		double dev_res = Lua.LuaToNumber(lstate, 4);

		printStack(lstate);

		Lua.LuaClose (lstate);
	}

	// コルーチンテスト
	LuaState cotest_State;
	LuaState co;
	void test3()
	{
		cotest_State = Lua.LuaOpen ();
		Lua.LuaOpenBase(cotest_State);
		int res = Lua.LuaLLoadFile (cotest_State, "C:/takuya/unity/luatest/Assets/coroutine.lua");
		Lua.LuaPCall(cotest_State, 0, Lua.LUA_MULTRET, 0);

		co = Lua.LuaNewThread(cotest_State);
		Lua.LuaGetGlobal(co, "step");

//		printStack(co);

//		res = Lua.LuaResume (co, 0);

		//Lua.LuaClose (cotest_State);
		//printStack(cotest_State);
	}

	// 逆呼び出しテスト
	void test4()
	{
		LuaState lstate = Lua.LuaOpen ();
		Lua.LuaRegister (lstate, "UnityFunction", UnityFunction);
		int res = Lua.LuaLLoadFile (lstate, "C:/takuya/unity/luatest/Assets/UnityFunction.lua");
		Lua.LuaPCall(lstate, 0, Lua.LUA_MULTRET, 0);
	}

	
	void printStack(LuaState L)
	{
		int num = Lua.LuaGetTop (L);
		if(num==0)
		{
			return;
		}
		
		for(int i = num; i >= 1; i--)
		{
			int type = Lua.LuaType(L, i);

			switch(type) {
			case Lua.LUA_TNIL:
				break;
			case Lua.LUA_TBOOLEAN:
				int res_b = Lua.LuaToBoolean(L, i);
				Debug.Log ("LUA_TBOOLEAN : " + res_b);
				break;
			case Lua.LUA_TLIGHTUSERDATA:
				break;
			case Lua.LUA_TNUMBER:
				double res_d = Lua.LuaToNumber(L, i);
				Debug.Log ("LUA_TNUMBER : " + res_d);
				break;
			case Lua.LUA_TSTRING:
				CharPtr res_s = Lua.LuaToString(L, i);
				Debug.Log ("LUA_TSTRING : " + res_s);
				break;
			case Lua.LUA_TTABLE:
				break;
			case Lua.LUA_TFUNCTION:
				break;
			case Lua.LUA_TUSERDATA:
				break;
			case Lua.LUA_TTHREAD:
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("click");


			if(cotest_State != null)
			{
				if(Lua.LuaResume(co, 0) != 0)
				{
					printStack(co);
				}
				else
				{
					Lua.LuaClose (cotest_State);
					cotest_State = null;
				}
			}
		}
	}

	void test5()
	{
		//LuaState lstate = Lua.LuaOpen ();
		//// 関数に指定する引数をスタックに積む
		//Chara data = new Chara ();
		//data.mNowHp = 100;
		//data.mMaxHp = 100;
		//data.mNowMp = 100;
		//data.mMaxMp = 100;
		//data.mAttackPoint = 10;
		//data.mDefencePoint = 5;

		//object obj = data;

		//int res = Lua.LuaLLoadFile (lstate, "C:/takuya/unity/luatest/Assets/luafunction/itemeffect.lua");
		//Lua.LuaPCall(lstate, 0, Lua.LUA_MULTRET, 0);

		//// Luaで定義した関数をスタックに積む。Luaは関数も変数のひとつに過ぎないらしい
		//Lua.LuaGetGlobal(lstate, "battle");

		//LuaTag tag = new LuaTag ();
		//tag.Tag = 1;
		//// 関数に指定する引数をスタックに積む
		//Lua.LuaPushLightUserData(lstate, tag);



		//// Lua関数を実行
		////if(Lua.LuaPCall(lstate, 2, 4, 0))
		////{
		////}

		//printStack(lstate);
		//res = Lua.LuaPCall (lstate, 1, 6, 0);

		//// 戻り値がスタックに積まれているらしいので、取得
		///*int nowhp = Lua.LuaToInteger(lstate, 1);
		//int maxhp = Lua.LuaToInteger(lstate, 2);
		//int nowmp = Lua.LuaToInteger(lstate, 3);
		//int maxmp = Lua.LuaToInteger(lstate, 4);
		//int atk = Lua.LuaToInteger(lstate, 5);
		//int def = Lua.LuaToInteger(lstate, 6);

		//printStack(lstate);*/

		//Lua.LuaClose (lstate);
	}

	int UnityFunction(LuaState L)
	{
		Debug.Log ("UnityFunction");
		return 0;
	}

}
