namespace StealthTest.Characters
{
    using System;
    using UnityEngine;
    using UniRx;

    public class PlayerCharacterController : CharacterController
    {
        private Subject<Vector2> movementSubject = new();

        public IObservable<Vector2> MovementValue => movementSubject;
        public Func<float> GetCharacterSpeedEvent;
        public ReactiveCommand SwitchCrouchCommand = new();
        public ReactiveCommand DisposeCommand = new();

        private CompositeDisposable disposables = new();

        public PlayerCharacterController(
            CharactersSystem characterSystem,
            ICharacterView characterView,
            CharacterModelsFactory characterModelsFactory,
            ICharacterData characterData,
            bool isPlayerCharacter,
            PlayerInputSystem playerInputSystem
            ) : base(characterView, characterModelsFactory, characterData, isPlayerCharacter)
        {
            characterSystem.DisposeCommand.Subscribe(_ => Dispose()).AddTo(disposables);

            playerInputSystem.MovementValue.Subscribe(value =>
                {
                    movementSubject.OnNext(value);
                }).AddTo(disposables);

            playerInputSystem.SwitchCrouch.Subscribe(_ =>
            {
                SwitchCrouchCommand.Execute();
            }).AddTo(disposables);
        }

        public float GetCharacterSpeed()
        {
            return GetCharacterSpeedEvent.Invoke();
        }

        public void Dispose()
        {
            DisposeCommand.Execute();
            disposables.Dispose();
        }
    }
}
