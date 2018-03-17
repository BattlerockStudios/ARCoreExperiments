using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Battlerock
{
    public class CreateScriptableObject
    {
#if UNITY_EDITOR
        [MenuItem("Assets/Create/NetworkSettings")]
        public static NetworkSettings CreateSaveData()
        {
            NetworkSettings asset = ScriptableObject.CreateInstance<NetworkSettings>();

            AssetDatabase.CreateAsset(asset, "Assets/" + asset.GetType().ToString() + ".asset");
            AssetDatabase.SaveAssets();
            return asset;
        }
#endif
    }
}