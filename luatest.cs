using UnityEngine;
using System.Collections;
using KopiLua;

public class luatest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Lua lua = new Lua ();
		LuaState lstate = Lua.LuaOpen ();
		//int res = Lua.LuaLLoadFile (lstate, "C:/takuya/unity/luatest/Assets/first_function.lua");
		int res = Lua.LuaLLoadFile (lstate, "C:/takuya/unity/luatest/Assets/load_lua.lua");
		Lua.LuaPCall(lstate, 0, Lua.LUA_MULTRET, 0);
		int num = Lua.LuaGetTop (lstate);
		Lua.LuaGetGlobal(lstate, "windowWidth");
		Lua.LuaGetGlobal(lstate, "windowHeight");
		Lua.LuaGetGlobal(lstate, "windowName");
		Lua.LuaGetGlobal(lstate, "testboolean");
		printStack(lstate);
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
				break;
			case Lua.LUA_TLIGHTUSERDATA:
				break;
			case Lua.LUA_TNUMBER:
				double res_d = Lua.LuaToNumber(L, i);
				break;
			case Lua.LUA_TSTRING:
				CharPtr res_s = Lua.LuaToString(L, i);
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
	
	}
}
