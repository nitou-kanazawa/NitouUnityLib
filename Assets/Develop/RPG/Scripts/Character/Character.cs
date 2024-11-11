using UnityEngine;

namespace RPG
{
    // 基本ステータスを保持するキャラクタークラス
    public class Character {

        public string Name { get; private set; }

        public int MaxHP { get; private set; }
        public int MaxSP { get; private set; }
        
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int Speed { get; private set; }

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public Character(string name, int maxHP, int maxSP, int attack, int defense, int speed) {
            Name = name;
            MaxHP = maxHP;
            MaxSP = maxSP;
            Attack = attack;
            Defense = defense;
            Speed = speed;
        }
    }

}
