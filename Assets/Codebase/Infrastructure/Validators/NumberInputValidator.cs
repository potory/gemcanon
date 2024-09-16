using System;
using TMPro;
using UnityEngine;

namespace Codebase.Infrastructure.Validators
{
    public class NumberInputValidator : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        
        [SerializeField] private int _minInput = 1;
        [SerializeField] private int _maxInput = 8;

        private void OnEnable()
        {
            _inputField.onValidateInput += OnValidateInput;
            _inputField.onEndEdit.AddListener(OnEndEdit);
        }

        private void OnDisable()
        {
            _inputField.onValidateInput -= OnValidateInput;
            _inputField.onEndEdit.RemoveListener(OnEndEdit);
        }

        private void OnEndEdit(string text)
        {
            int number = 0;
            
            if (IsEmpty(text) || !IsValidNumber(text, out number) || number < _minInput) 
                _inputField.text = _minInput.ToString();

            if (number > _maxInput) 
                _inputField.text = _maxInput.ToString();
        }

        private static bool IsValidNumber(string text, out int result) => 
            int.TryParse(text, out result);

        private static bool IsEmpty(string text) => 
            string.IsNullOrEmpty(text);

        private static char OnValidateInput(string text, int charindex, char addedchar)
        {
            if (addedchar > 47 && addedchar < 58)
                return addedchar;
            
            return '\0';
        }
    }
}