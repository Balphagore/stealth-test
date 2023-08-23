namespace StealthTest
{
    using UnityEngine;
    using Zenject;

    using StealthTest.Characters;

    public class SceneIstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerInputSystem playerInputSystem;
        [SerializeField]
        private CharactersSystem charactersSystem;

        public override void InstallBindings()
        {
            Container.Bind<PlayerInputSystem>().FromInstance(playerInputSystem).AsSingle();
            Container.Bind<CharactersSystem>().FromInstance(charactersSystem).AsSingle();

            Container.Bind<CharacterControllersFactory>().FromNew().AsSingle();
            Container.Bind<CharacterModelsFactory>().FromNew().AsSingle();
        }
    }
}
