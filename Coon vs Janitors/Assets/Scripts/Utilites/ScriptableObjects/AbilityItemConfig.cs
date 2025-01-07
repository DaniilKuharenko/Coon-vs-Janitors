using UnityEngine;

namespace Raccons_House_Games
{
    [CreateAssetMenu(menuName = "CoonGame/Items/ItemConfig", fileName = "NewItemConfig")]
    public class AbilityItemConfig : ScriptableObject
    {
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite DisplayIcon { get; private set; }
        [field: SerializeField] public float MaxDurability { get; private set; }

        public virtual AbilityItemBuilder GetBuilder() => new AbilityItemBuilder(this);
    }
}