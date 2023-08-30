using System;

namespace ZAMB.Utilities
{
    /// <summary>
    /// Use this class to create a trigger functionality: the delegate is only executed if the bool value changes.
    /// </summary>
    public class BoolChangeTrigger
    {
        public bool Value { get; private set; } = false;

        public event Action<bool> OnValueChange = delegate { };

        public void Subscribe(Action<bool> newAction) => OnValueChange += newAction;
        public void Unsubscribe(Action<bool> newAction) => OnValueChange -= newAction;

        /// <summary>
        /// Check if the new value is different from previous. If it is, executes the Action.
        /// </summary>
        public void CheckChanges(bool newValue)
        {
            if(Value != newValue)
            {
                OnValueChange(newValue);
                Value = newValue;
            }
        }
    }
}
