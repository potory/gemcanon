using TMPro;
using UnityEngine;

namespace Codebase.Infrastructure.Validators
{
    public class PlayerNameValidator : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        
        private void OnEnable() => _inputField.onValidateInput += OnValidateInput;
        private void OnDisable() => _inputField.onValidateInput -= OnValidateInput;

        private static char OnValidateInput(string text, int charindex, char addedchar)
        {
            if (char.IsLetterOrDigit(addedchar))
                return addedchar;

            return '\0';
        }
    }
}