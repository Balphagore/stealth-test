namespace StealthTest.Characters
{
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    [CreateAssetMenu(fileName = "PlayerCharacterData", menuName = "Game Data/Player Character Data")]
    public class PlayerCharacterData : ScriptableObject, ICharacterData
    {
        public AssetReference CharacterPrefab;
        [Range(0f, 10f)]
        public float CharacterSpeed = 5f;
        [Range(0f, 1f)]
        public float CrouchSpeedMultiplier = 0.5f;
    }
}
