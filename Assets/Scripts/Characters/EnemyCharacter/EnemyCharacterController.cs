namespace StealthTest.Characters
{
    using System;
    using UniRx;

    public class EnemyCharacterController : CharacterController
    {
        public Func<bool> TryFireFunc;
        public ReactiveCommand DisposeCommand = new();

        private CompositeDisposable disposables = new();

        public EnemyCharacterController(
            CharactersSystem characterSystem,
            ICharacterView characterView,
            CharacterModelsFactory characterModelsFactory,
            ICharacterData characterData,
            bool isPlayerCharacter
            ) : base(characterView, characterModelsFactory, characterData, isPlayerCharacter)
        {
            characterSystem.DisposeCommand.Subscribe(_ => Dispose()).AddTo(disposables);
        }

        public bool TryFire()
        {
            return TryFireFunc.Invoke() ? true : false;
        }

        public void Dispose()
        {
            DisposeCommand.Execute();
            disposables.Dispose();
        }
    }
}
