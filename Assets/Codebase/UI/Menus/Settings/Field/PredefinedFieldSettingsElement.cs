using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Codebase.UI.Menus.Settings.Field
{
    public class PredefinedFieldSettingsElement : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;

        public string FieldName => _dropdown.options[_dropdown.value].text;

        public void SetOptions(IEnumerable<string> options)
        {
            _dropdown.options.Clear();
            _dropdown.options.AddRange(options.Select(o => new TMP_Dropdown.OptionData(o)));
        }
    }
}