using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    private const int MAXHEALTH = 50;
    private int m_currentHealth;
    
    private Transform m_target;
    private bool m_skipMove;
    [SerializeField]
    public int PlayerDamage;
    [SerializeField] HealthBar m_healthBar; 
    private Animator m_animator;
    [SerializeField]
    private AudioClip m_enemyAttack1;
    [SerializeField]
    private AudioClip m_enemyAttack2;

    public Enemy(int playerDamage)
    {
        PlayerDamage = playerDamage;
    }

    public bool IsAlive => m_currentHealth > 0;

    protected override void Start()
    {
        GameManager.instance.AddEnemyToList(this);
        m_animator = GetComponent<Animator>();
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
        m_currentHealth = MAXHEALTH;
        base.Start();
        
    }
        
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        if (m_skipMove)
        {
            m_skipMove = false;
            return;
        }

        base.AttemptMove<T>(xDir, yDir);
        m_skipMove = true;
    }

    public void MoveEnemy()
    {
        int xDir = 0 ;
        int yDir = 0 ;

        if (Mathf.Abs(m_target.position.x - transform.position.x) < float.Epsilon)
        {
            yDir = m_target.position.y > transform.position.y ? 1 : -1;
        }
        else
        {
            xDir = m_target.position.x > transform.position.x ? 1 : -1;
        }

        AttemptMove<Player>(xDir, yDir);
    }

    protected override void OnCantMove<T>(T component)
    {
        Player hitPlayer = component as Player;

        m_animator.SetTrigger("EnemyAttack");
        TakeDamage(hitPlayer.GetDamageDealt());
        hitPlayer.LoseFood(PlayerDamage);
        SoundManager.Instance.RandomizeSfx(m_enemyAttack1, m_enemyAttack2);
    }

    protected void TakeDamage(int value)
    {
        m_currentHealth-= value;
        if (m_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        float normalizedHealth =  ( (float) m_currentHealth / (float) MAXHEALTH );
        m_healthBar.SetBarSize(normalizedHealth);
    }
}
