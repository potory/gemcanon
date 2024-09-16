using TMPro;
using UnityEngine;

namespace Codebase.UI.Menus.Settings.Turns
{
    public class LimitedTurnsSettingsElement : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;

        public int TurnsCount => int.Parse(_inputField.text);
    }
}