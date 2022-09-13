using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.GameScene
{
    public class BoundlessLoader
    {
        private AssetBundle _localAssetBundle;
        private static BoundlessLoader _loader;
        private BoundlessLoader() { }
        public static BoundlessLoader GetLoader() { return _loader = _loader ?? new BoundlessLoader(); }
        public string BoundlessFolder { get; set; } = "AssetsBundles\\Android";
        public GameObject GetGameObject(string boundleName, string assetName)
        {
            _localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.dataPath, $"AssetsBundles\\Android\\{boundleName}"));
            if (_localAssetBundle != null)
            {
                GameObject a = UnityEngine.Object.Instantiate(_localAssetBundle.LoadAsset<GameObject>(assetName));
                return a;

            }
            Debug.LogError("Failed to load AssetBoundle");
            return null;
        }


        public void Dispose()
        {
            _localAssetBundle.Unload(false);
        }
    }
}