using UnityEngine;

/// <summary>
/// Script to handle sound of the game
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource fxSource;                            //reference to audiosource which we will use for FX
    public AudioClip gameOverFx, gameCompleteFx, shotFx;    //fx audio clips

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Method which plays the required sound fx
    /// </summary>
    public void PlayFx(FxTypes fxTypes)
    {
        switch (fxTypes)                                //switch case is used to run respective logic for respective FxType
        {
            case FxTypes.GAMEOVERFX:                    //if its GAMEOVER
                fxSource.PlayOneShot(gameOverFx);       //play GAMEOVER fx
                break;
            case FxTypes.GAMECOMPLETEFX:                //if its GAMEWIN
                fxSource.PlayOneShot(gameCompleteFx);   //play GAMEWIN fx
                break;
            case FxTypes.SHOTFX:
                fxSource.PlayOneShot(shotFx);
                break;
        }

    }
}

/// <summary>
/// Enum to differ fx types, you can add as many fx types as possible
/// </summary>
public enum FxTypes
{
    GAMEOVERFX, 
    GAMECOMPLETEFX, 
    SHOTFX
}
