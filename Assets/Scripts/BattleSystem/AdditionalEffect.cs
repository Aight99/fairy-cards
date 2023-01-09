using System;

namespace BattleSystem
{
    public enum EffectType
    {
        Damage,
        Heal,
        Shield,
        Recoil,
        Vampire,
        Silence, 
        HalfLife,
        Move,
        Endurance,
        ManaGain,
        CardGain,
    }
    
    [Serializable]
    public struct AdditionalEffect
    {
        public EffectType EffectType;
        public int EffectParameter;
        public bool IsSelfTarget;
        public bool IsAfterAttack;
    }
}