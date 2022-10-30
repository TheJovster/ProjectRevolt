using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRevolt.Stats 
{
    public interface IModifierProvider
{
        IEnumerable<float> GetAdditiveModifier(Stat stat);
        
}
}
