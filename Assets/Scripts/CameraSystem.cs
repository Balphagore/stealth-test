namespace StealthTest
{
    using UnityEngine;
    using Cinemachine;
    using UniRx;
    using Zenject;

    using StealthTest.Characters;

    public class CameraSystem : MonoBehaviour
    {
        [Inject]
        private CharactersSystem characterSystem;

        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        private void OnEnable()
        {
            characterSystem.PlayerSpawnCommand.Subscribe(OnPlayerSpawn);
        }

        private void OnPlayerSpawn(Transform playerTransform)
        {
            virtualCamera.Follow = playerTransform;
        }
    }
}
