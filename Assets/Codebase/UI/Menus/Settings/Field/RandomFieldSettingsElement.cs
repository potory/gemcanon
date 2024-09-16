using TMPro;
using UnityEngine;

namespace Codebase.UI.Menus.Settings.Field
{
    public class RandomFieldSettingsElement : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _widthInput;
        [SerializeField] private TMP_InputField _heightInput;

        public Vector2Int Size => new(
            int.Parse(_widthInput.text),
            int.Parse(_heightInput.text)
            );
    }
}