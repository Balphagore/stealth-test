namespace StealthTest.Characters
{
    using UnityEngine;

    public interface ICharacterView
    {
        void Initialize(CharacterController characterController);

        Transform GetCharacterTransformFromView();

        void KillCharacter();
    }
}
