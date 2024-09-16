using System;
using UnityEngine;

namespace Codebase.Data
{
    [Serializable]
    public class SocialData
    {
        [SerializeField] private string _url;
        [SerializeField] private Sprite _icon;

        public string Url => _url;
        public Sprite Icon => _icon;
    }
}