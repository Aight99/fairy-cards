using System;

namespace BattleSystem
{
    public enum EffectType
    {
        Damage,
        Heal,
        Shield,
        Vampire,
        Silence, 
        HalfLife,
        Move,
        Endurance,
        ManaGain,
        CardGain,
        FreeAttack,
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