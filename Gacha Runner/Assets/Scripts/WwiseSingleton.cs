public class WwiseSingleton : Singleton<WwiseSingleton>
{
    public void SetState(string state, string value)
    {
        AkSoundEngine.SetState(state, value);
    }
}
