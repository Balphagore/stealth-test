namespace StealthTest.Characters
{
    using System;
    using UnityEngine;

    public abstract class CharacterController
    {
        public Func<Transform> GetCharacterTransformEvent;

        protected CharacterController(
            ICharacterView characterView,
            CharacterModelsFactory characterModelsFactory,
            ICharacterData characterData,
            bool isPlayerCharacter
            )
        {
            characterView.Initialize(this);
            characterModelsFactory.CreateCharacterModel(isPlayerCharacter, characterData).Initialize(this);
        }

        public virtual Transform GetCharacterTransformFromController()
        {
            return GetCharacterTransformEvent?.Invoke();
        }
    }
}
