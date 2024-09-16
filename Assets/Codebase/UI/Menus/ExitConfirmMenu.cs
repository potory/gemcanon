using System;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI.Menus
{
    public class ExitConfirmMenu : MonoBehaviour
    {
        [Header("Submenus")] 
        [SerializeField] private MainMenu _mainMenu;
        
        [Header("Buttons")]
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _cancelButton;

        private void Start()
        {
            _exitButton.onClick.AddListener(Application.Quit);
            _cancelButton.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                _mainMenu.gameObject.SetActive(true);
            });
        }
    }
}