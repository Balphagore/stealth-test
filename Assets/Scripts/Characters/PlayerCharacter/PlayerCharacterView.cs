namespace StealthTest.Characters
{
    using UniRx;
    using UnityEngine;

    public class PlayerCharacterView : MonoBehaviour, ICharacterView
    {
        [SerializeField]
        private Rigidbody2D characterRigidbody;
        [SerializeField]
        private Transform characterTransform;
        [SerializeField]
        private NoiseCheck noiseCheck;

        private PlayerCharacterController playerCharacterController;
        private Vector2 movementVector;
        private CompositeDisposable disposables = new();

        public void Initialize(CharacterController characterController)
        {
            playerCharacterController = (PlayerCharacterController)characterController;
            ((PlayerCharacterController)characterController).MovementValue.Subscribe(value =>
            {
                movementVector = value;
            }).AddTo(disposables);

            playerCharacterController.SwitchCrouchCommand.Subscribe(_ => { noiseCheck.Switch(); }).AddTo(disposables);

            characterController.GetCharacterTransformEvent += OnGetCharacterTransformEvent;
        }

        public Transform GetCharacterTransformFromView()
        {
            return characterTransform;
        }

        public void KillCharacter()
        {
            Observable.Timer(System.TimeSpan.FromSeconds(0.5f))
                .Subscribe(_ =>
                {
                    characterTransform.position = Vector3.zero;
                })
                .AddTo(disposables);
        }

        public void SpottingPlayer(EnemyCharacterView enemy)
        {
            enemy.PlayerSpotted(this);
        }

        private void FixedUpdate()
        {
            characterRigidbody.velocity = transform.TransformDirection(movementVector * playerCharacterController.GetCharacterSpeed());
        }

        private Transform OnGetCharacterTransformEvent()
        {
            return characterTransform;
        }

        private void OnDisable()
        {
            disposables.Dispose();
            playerCharacterController.GetCharacterTransformEvent -= OnGetCharacterTransformEvent;
        }
    }
}
