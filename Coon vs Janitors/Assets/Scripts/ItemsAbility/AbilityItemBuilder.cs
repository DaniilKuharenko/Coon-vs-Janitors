namespace Raccons_House_Games
{
    public class AbilityItemBuilder
    {
        protected AbilityItemConfig _abilityItemConfig;
        protected AbilityItem _abilityItem;

        public AbilityItemBuilder(AbilityItemConfig abilityItemConfig)
        {
            _abilityItemConfig = abilityItemConfig;
        }

        public virtual void Make()
        {
            if(_abilityItem != null)
            {
                _abilityItem.SetDescription(_abilityItemConfig.Title, _abilityItemConfig.Description, _abilityItemConfig.DisplayIcon);
                _abilityItem.SetMaxDurability(_abilityItemConfig.MaxDurability);
                _abilityItem.ChangeStatus(EItemStatus.Ready);
            }
        }

        public virtual AbilityItem GetResult() => _abilityItem;
    }
}