using UnityEngine;

namespace RPG {

    // 戦闘用のキャラクタークラス（Battler）
    public class Battler {
        
        private readonly Character _character;
        

        public int CurrentHP { get; private set; }
        
        public int CurrentSP { get; private set; }


        public int AttackPower => _character.Attack;
        
        public int DefensePower => _character.Defense;


        /// ----------------------------------------------------------------------------
        // Public Method

        public Battler(Character character) {
            this._character = character;
            CurrentHP = character.MaxHP;
            CurrentSP = character.MaxSP;
        }

        // ダメージ計算
        public void TakeDamage(int damage) {
            CurrentHP -= damage;
            if (CurrentHP < 0)
                CurrentHP = 0;

            Debug.Log($"{_character.Name} took {damage} damage! Remaining HP: {CurrentHP}");
        }

        // 対象に攻撃を行う
        public void AttackTarget(Battler target) {
            int damage = CalculateDamage(target);
            target.TakeDamage(damage);
        }

        // ダメージ計算式
        private int CalculateDamage(Battler target) {
            int baseDamage = AttackPower - target.DefensePower;
            return Mathf.Max(1, baseDamage); // 最低でも1ダメージを与える
        }
    }
}
