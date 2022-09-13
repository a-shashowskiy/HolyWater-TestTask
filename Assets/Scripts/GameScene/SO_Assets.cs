using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.GameScene
{
    [CreateAssetMenu(fileName = "SO",menuName ="AssetsBoundle",order =0)]
    public class SO_Assets : ScriptableObject
    {
        public AssetBundle loadedAsset;
    }
}