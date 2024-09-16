using System;
using Codebase.Data;
using Codebase.Infrastructure.Game;
using Codebase.Infrastructure.Game.States;
using Codebase.UI.Menus.About;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.UI.Menus
{
    public class AboutMenu : MonoBehaviour
    {
        [Header("Buttons")] 
        
        [SerializeField] private Button _mainMenuButton;
        
        [Header("Socials")]
        [SerializeField] private SocialElement _socialElementTemplate;
        [SerializeField] private SocialData[] _socialData;

        private GameStateMachine _gameStateMachine;
        
        [Inject]
        private void Construct(GameStateMachine stateMachine)
        {
            _gameStateMachine = stateMachine;
        }

        private void Start()
        {
            _mainMenuButton.onClick.AddListener(() => _gameStateMachine.Enter<MainMenuState>());
    
            foreach (var data in _socialData)
            {
                var element = Instantiate(_socialElementTemplate, 
                    _socialElementTemplate.transform.parent);
                
                element.SetIcon(data.Icon);
                element.Clicked += () => Application.OpenURL(data.Url);
                
                element.gameObject.SetActive(true);
            }
        }
    }
}