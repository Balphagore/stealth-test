namespace StealthTest.Characters
{
    using System;
    using UnityEngine;
    using Zenject;

    public class CharacterControllersFactory
    {
        [Inject]
        private PlayerInputSystem playerInputSystem;
        [Inject]
        private CharacterModelsFactory characterModelsFactory;

        public void CreateCharacter(
            CharactersSystem characterSystem,
            ICharacterData characterData,
            Transform spawnPosition,
            Transform parent,
            bool isPlayerCharacter,
            Action<CharacterController> callback
            )
        {
            if (isPlayerCharacter)
            {
                ((PlayerCharacterData)characterData).CharacterPrefab.InstantiateAsync(
                    spawnPosition.position,
                    spawnPosition.rotation,
                    parent
                    ).Completed += operation =>
                        {
                            callback?.Invoke(new PlayerCharacterController(
                                characterSystem,
                                operation.Result.GetComponent<ICharacterView>(),
                                characterModelsFactory,
                                characterData,
                                true,
                                playerInputSystem
                                ));
                        };
            }
            else
            {
                ((EnemyCharacterData)characterData).CharacterPrefab.InstantiateAsync(
                    spawnPosition.position,
                    spawnPosition.rotation,
                    parent
                    ).Completed += operation =>
                        {
                            callback?.Invoke(new EnemyCharacterController(
                            characterSystem,
                            operation.Result.GetComponent<ICharacterView>(),
                            characterModelsFactory,
                            characterData,
                            false
                            ));
                        };
            }
        }
    }
}
