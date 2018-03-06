using UnityEngine;

namespace Battlerock
{
    public enum LEVEL
    {
        PongLauncher,
        PongGame
    }

    [CreateAssetMenu(menuName = "ScriptableObjects/NetworkSettings")]
    public class NetworkSettings : SingletonScriptableObject<NetworkSettings>
    {
        [Tooltip("This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).")]
        public string gameVersion = "1";

        [Tooltip("The maximum number of players per room")]
        public byte maxPlayersPerRoom = 4;

        [Tooltip("List of Levels for loading.")]
        public LEVEL level = LEVEL.PongLauncher;

    }
}
