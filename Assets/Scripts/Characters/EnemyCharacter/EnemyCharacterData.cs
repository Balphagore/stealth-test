namespace StealthTest.Characters
{
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    [CreateAssetMenu(fileName = "EnemyCharacterData", menuName = "Game Data/Enemy Character Data")]
    public class EnemyCharacterData : ScriptableObject, ICharacterData
    {
        public AssetReference CharacterPrefab;
        public float FireCooldown = 1f;
    }
}
