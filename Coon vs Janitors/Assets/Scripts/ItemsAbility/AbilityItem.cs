using Raccons_House_Games.Actor;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Raccons_House_Games
{
    public class AbilityItem
    {
        public event Action<float, float> EventChangeDurability;

        public string Name { get; private set; }
        public string Description { get; private set; }
        public Sprite DisplayIcon { get; private set; }

        public float MaxDurability { get; private set; }
        public float CurrentDurability { get; private set; }
        public bool IsPassive { get; private set; }

        public Button UseButton { get; private set; }
        public EItemStatus Status { get; private set; }

        public void SetDescription(string name, string description, Sprite displayIcon)
        {
            Name = name;
            Description = description;
            DisplayIcon = displayIcon;
        }

        public void SetButton(Button button) => UseButton = button;
        public void SetMaxDurability(float maxDurability)
        {
            MaxDurability = maxDurability;
            CurrentDurability = maxDurability;
        }

        public void ChangeDurability(float amount)
        {
            CurrentDurability = Mathf.Clamp(CurrentDurability + amount, 0.0f, MaxDurability);
            EventChangeDurability?.Invoke(CurrentDurability, MaxDurability);
        }

        public void ChangeStatus(EItemStatus status) => Status = status;

        public virtual void OnEquip(Player owner) { }
        public virtual void OnUse() { }
        public virtual bool CheckCondition(Player owner, Vector3 location = default) => true; // The CheckCondition method allows defining conditions under which an item can be used.
        public virtual void ApplyEffect() { }
        public virtual void EventTick(float deltaTick) { }
        public virtual void OnUnequip(Player owner) { }
    }
}

/* 
This adds flexibility and extendability to the architecture.
Purpose of CheckCondition:
    Usage Conditions:
        This method checks if an item can be used at the current moment. Examples include:
        - Verifying if the player/Actor - newermind has enough resources (like mana or energy).
        - Checking if the player/Actor - newermind is within a specific area or near a certain object.

Example Usage:

public override bool CheckCondition(Player owner, Vector3 location = default)
{
    // Example: Check if there is enough mana to use the item
    if (owner.Mana >= 20)
    {
        return true;
    }
    Debug.Log("Not enough mana to use this item.");
    return false;
}
*/