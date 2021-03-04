[System.Serializable]
//Where all saved variables will go
public class SaveState
{
    public CharacterBase[] characters;

    public SaveState(CharacterBase[] characters)
    {
        this.characters = characters;
    }
}
