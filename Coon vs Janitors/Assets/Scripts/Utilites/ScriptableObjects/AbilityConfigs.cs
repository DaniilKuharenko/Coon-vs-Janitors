using UnityEngine;

namespace Raccons_House_Games
{
    public class AbilityConfigs : ScriptableObject
    {
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite DisplayImage { get; private set; }
        [field: SerializeField] public float DurationTime { get; private set; }
        [field: SerializeField] public float RadiusView { get; private set; }

        public virtual AbilityBuilder GetBuilder() => new AbilityBuilder(this);

    }
}