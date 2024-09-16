using System;
using TMPro;
using UnityEngine;

namespace Codebase.UI.Menus.Settings
{
    public class WinConditionSettingsElement : MonoBehaviour
    {
        public enum WinConditionType
        {
            TopThirtyPercent,
            Complete
        }

        [SerializeField] private TMP_Dropdown _dropdown;
        
        public WinConditionType Type { get; private set; }

        private void Start()
        {
            _dropdown.onValueChanged.AddListener(OnValueChange);
        }

        private void OnValueChange(int index) => Type = (WinConditionType)index;
    }
}