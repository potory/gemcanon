using Codebase.Logic.Gameplay.Field.Tags;
using Codebase.Logic.Gameplay.Shooting.Tags;
using UnityEngine;
using Zenject;

namespace Codebase.Logic.Gameplay.Installers
{
    public class TagsInstaller : MonoInstaller
    {
        [SerializeField] private LoadedBubbleParentTag _loadedBubbleParent;
        [SerializeField] private FieldTargetParentTag _fieldTargetParent;
        
        public override void InstallBindings()
        {
            Container.Bind<LoadedBubbleParentTag>().FromInstance(_loadedBubbleParent).AsSingle();
            Container.Bind<FieldTargetParentTag>().FromInstance(_fieldTargetParent).AsSingle();
        }
    }
}