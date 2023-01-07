using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem.Rules
{
    public class CreatureDieRule : IRule
    {
        private readonly Context _context;
        
        public CreatureDieRule(Context context)
        {
            _context = context;
        }
        
        public void ApplyRule()
        {
            CheckList(_context.Allies);
            CheckList(_context.Enemies);
        }

        private void CheckList(List<Creature> list)
        {
            var toRemove = new List<Creature>(5);
            foreach (var creature in list)
            {
                if (creature.Health <= 0)
                {
                    Debug.Log($"DIE! {creature}");
                    toRemove.Add(creature);
                }
            }
            foreach (var dead in toRemove)
            {
                list.Remove(dead);
            }
        }
    }
}