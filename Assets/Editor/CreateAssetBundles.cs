using UnityEditor;

public class CreateAssetBundles
{
    [MenuItem ("Assets/Build AssetsBunles")]
    static public void BuildAssetsBundle()
    { 
        BuildPipeline.BuildAssetBundles("Assets/AssetsBundles/Android", BuildAssetBundleOptions.None, BuildTarget.Android);
    }
    
}
