using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    private const int MAXHEALTH = 50;
    private int currentHealth;
    public int playerDamage;
    [SerializeField] HealthBar healthBar; 
    private Animator animator;
    private Transform target;
    private bool skipMove;
    public AudioClip enemyAttack1;
    public AudioClip enemyAttack2;

    public bool IsAlive => currentHealth > 0;

    protected override void Start()
    {
        GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = MAXHEALTH;
        base.Start();
        
    }
        
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        if (skipMove)
        {
            skipMove = false;
            return;
        }

        base.AttemptMove<T>(xDir, yDir);
        skipMove = true;
    }

    public void MoveEnemy()
    {
        int xDir = 0 ;
        int yDir = 0 ;

        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
        {
            yDir = target.position.y > transform.position.y ? 1 : -1;
        }
        else
        {
            xDir = target.position.x > transform.position.x ? 1 : -1;
        }

        AttemptMove<Player>(xDir, yDir);
    }

    protected override void OnCantMove<T>(T component)
    {
        Player hitPlayer = component as Player;

        animator.SetTrigger("EnemyAttack");
        TakeDamage(playerDamage);
        hitPlayer.LoseFood(playerDamage);
        SoundManager.instance.RandomizeSfx(enemyAttack1, enemyAttack2);
    }

    protected void TakeDamage(int value)
    {
        currentHealth-= value;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        float normalizedHealth =  ( (float) currentHealth / (float) MAXHEALTH );
        healthBar.SetBarSize(normalizedHealth);
    }
}
