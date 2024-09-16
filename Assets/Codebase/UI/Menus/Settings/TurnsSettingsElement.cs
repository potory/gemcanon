using System;
using Codebase.UI.Menus.Settings.Turns;
using TMPro;
using UnityEngine;

namespace Codebase.UI.Menus.Settings
{
    public class TurnsSettingsElement : MonoBehaviour
    {
        public enum TurnsType
        {
            Limited = 0,
            Unlimited = 1
        }
        
        [Header("Components")]
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private LimitedTurnsSettingsElement _limitedTurnsSettings;

        [Header("Settings")]
        [SerializeField] private TurnsType _turnsType = TurnsType.Limited;

        public TurnsType Type => _turnsType;
        public LimitedTurnsSettingsElement LimitedTurnsSettings => _limitedTurnsSettings;

        private void Start()
        {
            _dropdown.onValueChanged.AddListener(OnDropdownChange);
        }

        private void OnDropdownChange(int value)
        {
            var type = (TurnsType) value;
            _turnsType = type;

            _limitedTurnsSettings.gameObject.SetActive(type == TurnsType.Limited);
        }
    }
}