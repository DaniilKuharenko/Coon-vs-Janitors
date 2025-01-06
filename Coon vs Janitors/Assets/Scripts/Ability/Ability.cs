using Ability.Entities;
using System;
using UnityEngine;

namespace Raccons_House_Games
{
    public class Ability
    {
        public event Action<float, float> EventChangeCooldownTimer;

        public string Title { get; private set; }
        public string Description { get; private set; }
        public Sprite DisplayImage { get; private set; }

        public EAbilityStatus Status { get; private set; }
        

        public void SetDescription(string title, string description, Sprite displayImage)
        {
            Title = title;
            Description = description;
            DisplayImage = displayImage;
        }

        public void ChangeStatus(EAbilityStatus status) => Status = status;
        public virtual void Added(Actor owner){}
        public virtual void StartCast(){}
        public virtual bool CheckCondition(Actor owner, Actor target, Vector3 location = default) => false;
        public virtual void ApplyCast() { }
        public virtual void EventTick(float deltaTick) { }
        public virtual void CancelCast() { }

        public virtual void Remove(Actor owner) { }
    }
}
