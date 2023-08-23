namespace StealthTest.Characters
{
    public class CharacterModelsFactory
    {
        public ICharacterModel CreateCharacterModel(bool isPlayerCharacter, ICharacterData characterData)
        {
            if (isPlayerCharacter)
            {
                return new PlayerCharacterModel(characterData);
            }
            else
            {
                return new EnemyCharacterModel(characterData);
            }
        }
    }
}
