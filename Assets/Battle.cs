using UnityEngine;
using System.Collections;

// Luaを使わないで、アイテム実装したらどうなるか
// 大変だということがわかったので、中途半端になっている
public class Battle : MonoBehaviour {

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

		NormalAttack(MyPlyaer, EnemyPlyaer);
		UseItem(MyPlyaer, EnemyPlyaer, "1");
	}

	// 通常攻撃の場合
	// ダメージ計算式は、以下の仕様ということになっている
	// 自分の攻撃力-相手の防御力＝ダメージ
	
	void NormalAttack(Chara attaker, Chara defender)
	{
		int damage = CaleNormalAttackDamage(attaker, defender);
		defender.mNowHp -= damage;
		if(defender.mNowHp <= 0)
		{
			Debug.Log("dead");
		}
	}


	// ダメージ計算ロジック：通常攻撃
	int CaleNormalAttackDamage(Chara attaker, Chara defender)
	{
		int damage = defender.mDefencePoint - attaker.mAttackPoint;
		if(damage < 0)
		{
			damage = 0;
		}

		return damage;
	}
	
	// アイテム使用の関数
	void UseItem(Chara attaker, Chara defender, string itemId)
	{
		ItemManager.ItemData idata = ItemManager.Instance.GetItemData(itemId);
		CheckItemEffect(attaker, defender, idata);
	}

	// アイテム効果判別
	void CheckItemEffect(Chara attaker, Chara defender, ItemManager.ItemData data)
	{
		// このやり方だと、ここの条件式がどんどん増えていく気がするね
		if(data.effectType == "HP_DAMAGE")
		{
			// さらにアイテムのダメージに関するルールをここで個別設定することになる
			// とりあえず、このダメージは防御力を無視するダメージということにする
			int damage = data.effectValue;
			defender.mNowHp -= damage;
			if(defender.mNowHp < 0)
			{
				defender.mNowHp = 0;
			}
		}
		else if(data.effectType == "MP_DAMAGE")
		{
			// もう実装する気が失せた･･･ｗ
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
