using System;
using Codebase.UI.Menus.Settings.Field;
using TMPro;
using UnityEngine;

namespace Codebase.UI.Menus.Settings
{
    public class FieldSettingsElement : MonoBehaviour
    {
        public enum FieldType
        {
            Random = 0,
            Predefined = 1
        }

        [Header("Components")]
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private RandomFieldSettingsElement _randomFieldSettings;
        [SerializeField] private PredefinedFieldSettingsElement _predefinedFieldElement;

        [Header("Settings")]
        [SerializeField] private FieldType _fieldType = FieldType.Random;

        public FieldType Type => _fieldType;

        public RandomFieldSettingsElement RandomFieldSettings => _randomFieldSettings;
        public PredefinedFieldSettingsElement PredefinedFieldSettings => _predefinedFieldElement;

        private void Start()
        {
            _dropdown.onValueChanged.AddListener(OnDropdownChange);
        }

        private void OnDropdownChange(int value)
        {
            var type = (FieldType) value;
            _fieldType = type;

            _randomFieldSettings.gameObject.SetActive(type == FieldType.Random);
            _predefinedFieldElement.gameObject.SetActive(type == FieldType.Predefined);
        }
    }
}