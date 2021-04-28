using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate bool Skill();
public delegate bool OvertimeSkill(ref float time);
public delegate void CallbackIndex();
public enum SkillType
{
    ClearMissiles, SpeedBoost, Invulnerable, QuickDraw, CoinGrab, Upwarp
}

public class ActiveAbilities : MonoBehaviour
{
    private GameManager gm;
    private Character character;

    //[SerializeField]
    private SkillType skillType;

    [SerializeField]
    private Button skillButton = null;
    private Slider cooldownSlider;
    private Text skillText;

    private ActiveSkill skill;

    // Start is called before the first frame update
    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        character = gameObject.GetComponent<Character>();
        skillText = skillButton.transform.GetChild(0).GetComponent<Text>();
        cooldownSlider = skillButton.transform.GetChild(1).GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Do cooldown if there is any
        if (skill.InCooldown)
        {
            skill.DoCooldown(Time.deltaTime);
            cooldownSlider.value = skill.CurrCooldown / skill.MaxCooldown;
        }

        if (skill.Active)
        {
            skill.DoOvertimeActive();
        }
    }

    /// <summary>
    /// Activates skill if possible
    /// </summary>
    public void ActivateSkill()
    {
        if (!skill.InCooldown)
        {
            skill.Activate();

            //Check if skill activated successfully
            if (skill.InCooldown)
            {
                //Change button to darken
                ColorBlock colors = skillButton.colors;
                colors.normalColor = new Color32(150, 150, 150, 255);
                colors.highlightedColor = new Color32(150, 150, 150, 255);
                colors.selectedColor = new Color32(150, 150, 150, 255);
                skillButton.colors = colors;
                cooldownSlider.gameObject.SetActive(true);
                cooldownSlider.value = 0;
            }
        }
    }

    /// <summary>
    /// Resets button's color after cooldown ends
    /// </summary>
    private void ResetButton()
    {
        //Change button to normal
        ColorBlock colors = cooldownSlider.colors;
        colors.normalColor = new Color32(255, 255, 255, 255);
        colors.highlightedColor = new Color32(255, 255, 255, 255);
        skillButton.colors = colors;
        cooldownSlider.gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets active skill to specified slot
    /// </summary>
    /// <param name="i">Index of skill</param>
    /// <param name="type">Type of skill</param>
    public void SetActiveSkill(SkillType type)
    {
        skill = GetActiveSkill(type);
        if (skill != null)
        {
            skillText.text = skill.Name;
            skill.OnCooldownEnd += ResetButton;
        }
        else
        {
            skillText.text = "None";
            Debug.LogError("No active skill of that type found.");
        }
        cooldownSlider.gameObject.SetActive(false);
    }

    /// <summary>
    /// Gets active skill based on type
    /// </summary>
    /// <param name="type">Type of skill</param>
    /// <returns>Active skill</returns>
    private ActiveSkill GetActiveSkill(SkillType type)
    {
        switch (type)
        {
            case SkillType.ClearMissiles:
                return new ActiveSkill("Clear Missiles", ClearMissiles, 15.0f);
            case SkillType.SpeedBoost:
                return new ActiveSkill("Speed Boost", SpeedBoost, 20.0f);
            case SkillType.Invulnerable:
                return new ActiveSkill("Invulnerable!", Invulnerability, 25.0f);
            case SkillType.QuickDraw:
                return new ActiveSkill("Quick Draw", QuickDraw, 30.0f);
            case SkillType.CoinGrab:
                return new ActiveSkill("Coin Grab", CoinGrab, 15.0f);
            case SkillType.Upwarp:
                return new ActiveSkill("Upwarp", Upwarp, 10.0f);
        }
        return null;
    }

    #region Skills

    /// <summary>
    /// Skill that clears all missiles on the screen
    /// </summary>
    /// <returns></returns>
    private bool ClearMissiles()
    {
        GameObject[] missiles = GameObject.FindGameObjectsWithTag("Missile");
        foreach (GameObject missile in missiles)
            Destroy(missile);
        return true;
    }

    /// <summary>
    /// Gives the player a 25% speed boost for 5 seconds
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private bool SpeedBoost(ref float time)
    {
        if(time == 0)
        {
            character.SpeedBoost = 1.25f;
        }

        if(time >= 5.0f)
        {
            character.SpeedBoost = 1.0f;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Makes player invulnerable for 5 seconds
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private bool Invulnerability(ref float time)
    {
        if (time == 0)
        {
            character.InvulnerabilityBoost = true;
        }

        if (time >= 5.0f)
        {
            character.InvulnerabilityBoost = false;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Decrease cooldown of rect drawing for 10 seconds
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private bool QuickDraw(ref float time)
    {
        if (time == 0)
            character.SetRectCooldown(0.0f);

        if (time >= 10.0f)
        {
            character.ResetRectCooldown();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Collects all coins on screen
    /// </summary>
    /// <returns></returns>
    private bool CoinGrab()
    {
        gm.CollectAllCoins();
        return true;
    }

    /// <summary>
    /// Warps player to top of screen
    /// </summary>
    /// <returns></returns>
    private bool Upwarp()
    {
        character.transform.position = new Vector3(character.transform.position.x, 4.0f, character.transform.position.z);
        return true;
    }

    #endregion
}

public class ActiveSkill
{
    protected Skill _skill;
    protected OvertimeSkill _timeSkill;
    protected float _maxCooldown;
    protected float _currCooldown;
    protected string _name;

    public float MaxCooldown => _maxCooldown;
    public float CurrCooldown => _currCooldown;
    public bool Active => _active;
    public string Name => _name;

    public event CallbackIndex OnCooldownEnd;

    //Over time active skill variables
    protected bool _instant;
    protected bool _active;
    protected float _currActiveTime;

    public ActiveSkill(string name, Skill skill, float cooldown)
    {
        _skill = skill;
        _maxCooldown = cooldown;
        _currCooldown = 0;
        _instant = true;
        _name = name;
    }

    public ActiveSkill(string name, OvertimeSkill skill, float cooldown)
    {
        _timeSkill = skill;
        _maxCooldown = cooldown;
        _currCooldown = 0;
        _instant = false;
        _currActiveTime = 0;
        _name = name;
    }

    /// <summary>
    /// Returns if the skill is in cooldown
    /// </summary>
    public bool InCooldown
    {
        get { return _currCooldown > 0; }
    }

    /// <summary>
    /// Decreases the skills cooldown
    /// </summary>
    /// <param name="time">Time to decrement skill</param>
    public void DoCooldown(float time)
    {
        if (_currCooldown > 0)
        {
            _currCooldown -= time;
            if (_currCooldown <= 0)
            {
                OnCooldownEnd?.Invoke();
                _currCooldown = 0;
            }
        }
    }

    /// <summary>
    /// Activates active skill
    /// </summary>
    public void Activate()
    {
        //If skill is not in cooldown, activate skill
        if (!InCooldown)
        {
            //If skill is instant
            if (_instant)
            {
                if (_skill())
                {
                    //Set cooldown
                    _currCooldown = _maxCooldown;
                }
            }
            //If skill is overtime
            else
            {
                _active = true;
                _currActiveTime = 0;
                _currCooldown = _maxCooldown;
            }
        }
    }

    /// <summary>
    /// Executes an overtime active skill
    /// </summary>
    public void DoOvertimeActive()
    {
        //Do active skill until time is up
        if (_timeSkill(ref _currActiveTime))
        {
            //Turn off overtime active skill
            _active = false;
            _currActiveTime = 0;
        }
        else
        {
            _currActiveTime += Time.deltaTime;
        }
    }
}
