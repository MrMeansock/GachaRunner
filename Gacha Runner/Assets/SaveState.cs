[System.Serializable]
//Where all saved variables will go
public class SaveState
{
    public Character [] characters;

    public SaveState(Character[] characters)
    {
        this.characters = characters;
    }
}
