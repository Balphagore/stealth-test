namespace StealthTest.Characters
{
    using UnityEngine;

    public class EnemyCharacterView : MonoBehaviour, ICharacterView
    {
        [SerializeField]
        private Rigidbody2D characterRigidbody;
        [SerializeField]
        private Transform characterTransform;
        [SerializeField]
        private Laser laser;

        private EnemyCharacterController enemyCharacterController;

        public void Initialize(CharacterController characterController)
        {
            enemyCharacterController = (EnemyCharacterController)characterController;
        }

        public void PlayerSpotted(ICharacterView spottedPlayer)
        {
            if (enemyCharacterController.TryFire())
            {
                laser.Fire(characterTransform, spottedPlayer.GetCharacterTransformFromView());
                spottedPlayer.KillCharacter();

                Vector3 targetDirection = spottedPlayer.GetCharacterTransformFromView().position - characterTransform.position;
                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
                characterTransform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
            }
        }

        public Transform GetCharacterTransformFromView()
        {
            return characterTransform;
        }

        private void FixedUpdate()
        {
            characterRigidbody.velocity = Vector2.zero;
        }

        public void KillCharacter()
        {

        }
    }
}
