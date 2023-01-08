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

            Debug.Log($"target index  {_context.CurrentCommand.TargetIndex} user idnex {_context.CurrentCommand.UserIndex}");

            foreach(var f in _context.Field)
            {
                Debug.Log(f?.ToString());
            }

            //for (int i = 0; i < _context.Field.Length; i++)
            //{
            //    if (_context.Field[i].Health <= 0)
            //    {
            //        Debug.Log($"DIE! On position {i}");
            //        _context.Field[i] = null;
            //    }
            //}
        }
    }
}