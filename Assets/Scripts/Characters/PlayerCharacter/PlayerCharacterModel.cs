namespace StealthTest.Characters
{
    using UniRx;

    public class PlayerCharacterModel : ICharacterModel
    {
        private PlayerCharacterController playerCharacterController;
        private float characterSpeed;
        private float crouchSpeedMultiplier;
        private bool isCrouch;

        private CompositeDisposable disposables = new();

        public PlayerCharacterModel(ICharacterData characterData)
        {
            PlayerCharacterData playerCharacterData = (PlayerCharacterData)characterData;
            characterSpeed = playerCharacterData.CharacterSpeed;
            crouchSpeedMultiplier = playerCharacterData.CrouchSpeedMultiplier;
        }

        public void Initialize(CharacterController characterController)
        {
            playerCharacterController = (PlayerCharacterController)characterController;
            playerCharacterController.GetCharacterSpeedEvent += OnGetCharacterSpeedEvent;
            playerCharacterController.SwitchCrouchCommand.Subscribe(_ => { isCrouch = !isCrouch; }).AddTo(disposables);
            playerCharacterController.DisposeCommand.Subscribe(_ => Dispose()).AddTo(disposables);
        }

        private float OnGetCharacterSpeedEvent()
        {
            return isCrouch ? characterSpeed * crouchSpeedMultiplier : characterSpeed;
        }

        public void Dispose()
        {
            disposables.Dispose();
            playerCharacterController.GetCharacterSpeedEvent -= OnGetCharacterSpeedEvent;
        }
    }
}
