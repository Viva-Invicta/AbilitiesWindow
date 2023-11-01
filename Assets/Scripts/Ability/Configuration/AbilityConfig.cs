using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AbilitiesWindow.AbilityUI;

namespace AbilitiesWindow
{
    [CreateAssetMenu(fileName = nameof(AbilityConfig), menuName = "Abilities/Config")]
    public class AbilityConfig : ScriptableObject
    {
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public AbilityUIView ViewPrefab { get; private set; }

        [SerializeField] private AbilityConfig[] descendants;

        public IEnumerable<string> DescendantsIds => descendants.Select(descendant => descendant.Id);
        public IEnumerable<AbilityConfig> DescendantsConfigs => descendants;
    }
}