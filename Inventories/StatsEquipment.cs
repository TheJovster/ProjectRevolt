using GameDevTV.Inventories;
using ProjectRevolt.Stats;
using System.Collections.Generic;

namespace ProjectRevolt.Inventories
{
    public class StatsEquipment : Equipment, IModifierProvider
    {
        IEnumerable<float> IModifierProvider.GetAdditiveModifiers(Stat stat)
        {
            foreach (var slot in GetAllPopulatedSlots())
            {
                var item = GetItemInSlot(slot) as IModifierProvider;
                if (item == null) continue;

                foreach(float modifier in item.GetAdditiveModifiers(stat)) 
                {
                    yield return modifier;
                }
            }
        }

        IEnumerable<float> IModifierProvider.GetPercentageModifiers(Stat stat) 
        {
            foreach (var slot in GetAllPopulatedSlots())
            {
                var item = GetItemInSlot(slot) as IModifierProvider;
                if (item == null) continue;

                foreach (float modifier in item.GetAdditiveModifiers(stat))
                {
                    yield return modifier;
                }
            }
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            throw new System.NotImplementedException();
        }
    }
}
