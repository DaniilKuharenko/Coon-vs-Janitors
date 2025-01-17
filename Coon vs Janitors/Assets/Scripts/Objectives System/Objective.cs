using UnityEngine;

namespace Raccons_House_Games
{
    public abstract class Objective : MonoBehaviour, IObjective
    {
        [SerializeField] private string _title;
        [SerializeField] private string _description;

        public string Title => _title;
        public string Description => _description;
        public bool IsCompleted { get; private set; }

        protected virtual void Start()
        {
            ObjectivesManager.Instance.AddObjective(this);
        }

        protected virtual void Update()
        {
            CheckCompleted();
        }

        private void CheckCompleted()
        {
            if(IsCompleted)
            {
                return;
            }

            if(IsObjectiveCompleted())
            {
                IsCompleted = true;
                ObjectivesManager.Instance.SetObjectiveCompleted(this);
            }
        }

        protected abstract bool IsObjectiveCompleted();
    }
}
