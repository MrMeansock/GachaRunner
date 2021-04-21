[System.Serializable]
//Where all saved variables will go
public class SaveState
{
    public int currency;
    public int selectedCharacter;
    public CharacterBase[] characters;

    public SaveState(int currency, int selectedCharacter, CharacterBase[] characters)
    {
        this.currency = currency;
        this.selectedCharacter = selectedCharacter;
        this.characters = characters;
    }
}
