using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Stats
{
    public float m_Health;
    public float m_MaxHealth;
    public float m_AttackDamage;
}

public class BossAI : MonoBehaviour, IDamagable
{
    public List<AudioClip> WinLines;
    public List<AudioClip> TauntLines;

    public AudioSource TauntSource;
    public AudioSource WinSource;
    public PlayerStats Player;
    public BossDamager m_BossDamager;
    public EnemyStat m_EnemyStat;
    public bool m_Invulnerable;
    public float m_AttackTransitionTime;
    public float m_InvulnerabilityTime;
    public float m_FlipValue;
    public List<AttackPattern> m_attackPatterns;
    public AttackPattern m_currentAttack;


    Animator m_animator;
    Rigidbody2D m_rigidbody2D;
    
    int m_attackIndex;
    float m_attackTransitionTimer;
    float m_health;
    float m_invulnerabilityTimer;
    float m_invulColor;
    bool m_taunt;

    [Space(25)]
    public float BarFillSpd;

    //barfill
    private Color fullCol;
    private Color lowCol;
    private float baseT = 0;

    [Header("UI")]
    public Image HPImg;

    public float m_flipValue;
    //public Stats m_Stat;
    // Use this for initialization
    void Start () {
        LevelEventHandler.StartListening("GameOver", PlayWin);
        LevelEventHandler.StartListening("Taunt", PlayTaunt);
        m_animator = GetComponent<Animator>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_attackPatterns.AddRange(GetComponents<AttackPattern>());
        m_health = m_EnemyStat.HP;
        m_invulColor = GetComponent<SpriteRenderer>().color.r;
        m_animator.SetBool("Taunt", true);
        fullCol = Color.green;
        lowCol = Color.red;
    }

    public float AveHP
    {
        get
        {
            return m_health / m_EnemyStat.MaxHP;
        }
    }

    public float Health
    {
        get { return m_health; }
    }

    public bool InvulnerabilityOff()
    {
        m_invulnerabilityTimer += Time.deltaTime;
        return (m_invulnerabilityTimer > m_InvulnerabilityTime);
    }

    //Reference to Current Units Attack Pattern. Used to manipulate outside of script such as State transitions.
    public AttackPattern CurrentAttackPattern
    {
        get { return m_currentAttack; }
        set { m_currentAttack = value; }
    }

    public Rigidbody2D GetRigidBody
    {
        get { return m_rigidbody2D; }
    }

    //Used to set parameters inside animator outside of this script.
    public Animator GetAnimator
    {
        get { return m_animator; }
    }

    //Reference to current index.
    public int AttackIndex
    {
        get { return m_attackIndex; }
        set { m_attackIndex = value; }
    }

    // Update is called once per frame
    void Update () {
        UIUpdate();
        if (m_currentAttack != null && m_currentAttack.IsFinished)
        {
            m_currentAttack.EndAttack();
            m_currentAttack = null;
        }
        if (!m_taunt && Player.Dead)
        {
            if (m_currentAttack != null)
                CurrentAttackPattern.EndAttack();
            m_currentAttack = null;
            m_taunt = true;
            m_animator.SetBool("Taunt", true);
        }

        if (Player.Dead)
            return;

        if (m_currentAttack == null && m_EnemyStat.HP > 0)
            m_attackTransitionTimer += Time.deltaTime;

        if (m_attackTransitionTimer > m_AttackTransitionTime && m_attackPatterns.Count != 0 && m_currentAttack == null)
        {
            m_animator.SetBool("Taunt", false);
            m_currentAttack = m_attackPatterns[m_attackIndex];
            StartCoroutine(m_currentAttack.Attack());

            //Increment Attack index without going over the number of attack patterns detected.
            m_attackIndex = (m_attackIndex + 1) % m_attackPatterns.Count;
            m_attackTransitionTimer = 0;
        }

        if (m_health <= 0)
            Die();

        if (!m_Invulnerable)
            return;

        InvulnerableFeedback();
        if (InvulnerabilityOff())
        {
            m_Invulnerable = false;
            m_invulnerabilityTimer = 0;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
	}

    public void ResetDamager()
    {
        m_BossDamager.SetDamage(m_EnemyStat.Dmg);
    }

    void InvulnerableFeedback()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 0.2f));
    }

    public void TakeDamage(float damage)
    {
        if (m_Invulnerable) return;
        if (m_health - damage >= 0) m_health -= damage;
        if (m_health - damage < 0) m_health = 0;
        m_Invulnerable = true;
        m_animator.SetTrigger("Damaged");
        if (m_currentAttack != null && damage > 10 && m_currentAttack.IsDisruptable())
            m_currentAttack.StopAttack();
        
    }

    public virtual void Flip()
    {
        var temp = transform.localScale;
        temp.x = m_flipValue;
        transform.localScale = temp;

    }


    void PlayTaunt()
    {
        int index = Random.Range(0, TauntLines.Count);
        TauntSource.clip = TauntLines[index];
        TauntSource.Play();
    }

    void PlayWin()
    {
        int index = Random.Range(0, WinLines.Count);
        WinSource.clip = WinLines[index];
        WinSource.Play();
    }

    //To be called upon unit death.
    public void Die()
    {
        if (m_currentAttack != null)
            m_currentAttack.StopAttack();
        m_currentAttack = null;
        m_attackPatterns.Clear();
        Destroy(gameObject);
        LevelEventHandler.TriggerEvent("Victory");
    }
    public void UIUpdate()
    {
        StartCoroutine(UIAnimScale(5));

        //change color
        if (HPImg == null)
            return;

        HPImg.color = Color.Lerp(lowCol, fullCol, AveHP);
    }

    IEnumerator UIAnimScale(float t)
    {
        if (HPImg == null)
            yield return null;

        while (baseT < t)
        {
            baseT += Time.deltaTime;
            HPImg.fillAmount = Mathf.Lerp(HPImg.fillAmount, AveHP, BarFillSpd * Time.deltaTime);
            yield return null;
        }

        baseT = 0;
    }

}
