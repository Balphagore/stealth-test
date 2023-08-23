namespace StealthTest.Characters
{
    using UniRx;
    using UnityEngine;
    using Zenject;

    public class CharactersSystem : MonoBehaviour
    {
        [Inject]
        private CharacterControllersFactory characterControllersFactory;

        [SerializeField]
        private PlayerCharacterData playerCharacterData;
        [SerializeField]
        private EnemyCharacterData enemyCharacterData;

        [SerializeField]
        private Transform playerSpawnPosition;
        [SerializeField]
        private Transform enemySpawnPosition;

        public ReactiveCommand<Transform> PlayerSpawnCommand = new();
        public ReactiveCommand DisposeCommand = new();

        [Inject]
        public void Construct()
        {
            characterControllersFactory.CreateCharacter(
                this,
                playerCharacterData,
                playerSpawnPosition,
                transform,
                true,
                parameter => OnCallback(parameter, true)
                );

            characterControllersFactory.CreateCharacter(
                this,
                enemyCharacterData,
                enemySpawnPosition,
                transform,
                false,
                parameter => OnCallback(parameter, false)
                );
        }

        private void OnCallback(CharacterController characterController, bool isPlayerCharacter)
        {
            if (isPlayerCharacter)
            {
                PlayerSpawnCommand.Execute(characterController.GetCharacterTransformFromController());
            }
        }
    }
}
