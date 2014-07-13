--じゃあ、これは相手に10のTrueダメージを与える効果にしよう
function hpdamage1(atk_nowhp, atk_maxhp, atk_atkp, atk_defp, def_nowhp, def_maxhp, def_atkp, def_defp)
	def_nowhp = def_nowhp - 10
	if def_nowhp <= 0 then
		def_nowhp = 0
	end

	--とりあえず、攻撃者と防御者の現在HPを返す
	return atk_nowhp, def_nowhp
end

--これは、自分の残りHPの20％のTrueダメージを与えるようにしよう
--こんな感じで、それこそ一個一個好きな情報を渡せば好きに処理できる
function hpdamage2(atk_nowhp, atk_maxhp, atk_atkp, atk_defp, def_nowhp, def_maxhp, def_atkp, def_defp)
	--単純に、防御側防御力-攻撃側攻撃力の値を、防御側の現在HPから引く
	damage =  atk_nowhp / 5;
	if damage <= 0 then
		damage = 0
	end

	def_nowhp = def_nowhp - damage
	if def_nowhp <= 0 then
		def_nowhp = 0
	end

	--とりあえず、攻撃者と防御者の現在HPを返す
	return atk_nowhp, def_nowhp
end

--検証が終わったので、ここから↓は機能していない
function mpdamage1(atk_nowhp, atk_maxhp, atk_atkp, atk_defp, def_nowhp, def_maxhp, def_atkp, def_defp)
	--単純に、防御側防御力-攻撃側攻撃力の値を、防御側の現在HPから引く
	damage =  atk_atkp - def_defp
	if damage <= 0 then
		damage = 0
	end

	def_nowhp = def_nowhp - damage
	if def_nowhp <= 0 then
		def_nowhp = 0
	end

	--とりあえず、攻撃者と防御者の現在HPを返す
	return atk_nowhp, def_nowhp
end

function mpdamage2(atk_nowhp, atk_maxhp, atk_atkp, atk_defp, def_nowhp, def_maxhp, def_atkp, def_defp)
	--単純に、防御側防御力-攻撃側攻撃力の値を、防御側の現在HPから引く
	damage =  atk_atkp - def_defp
	if damage <= 0 then
		damage = 0
	end

	def_nowhp = def_nowhp - damage
	if def_nowhp <= 0 then
		def_nowhp = 0
	end

	--とりあえず、攻撃者と防御者の現在HPを返す
	return atk_nowhp, def_nowhp
end
