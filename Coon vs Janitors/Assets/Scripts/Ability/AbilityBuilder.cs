namespace Raccons_House_Games
{
    public class AbilityBuilder
    {
        private AbilityConfigs _config;
        protected Ability _ability;

        public AbilityBuilder(AbilityConfigs config)
        {
            _config = config;
        }

        public virtual void Make()
        {
            if(_ability != null)
            {
                _ability.SetDescription(_config.Title, _config.Description, _config.DisplayImage);
            }
        }

        public virtual Ability GetResult() => _ability;
    }
}