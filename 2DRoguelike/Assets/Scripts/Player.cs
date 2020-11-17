using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{
    [SerializeField] int m_baseAttackPower = 10;
    private readonly int m_wallDamage = 1;
    private int m_pointsPerFood = 10;
    private int m_pointsPerSoda = 20;
    private float m_restarLevelDelay = 1f;
    [SerializeField] private Text m_foodText;
    private readonly AudioClip m_moveSound1;
    private readonly AudioClip m_moveSound2;
    private readonly AudioClip m_eatSound1;
    private readonly AudioClip m_eatSound2;
    private readonly AudioClip m_drinkSound1;
    private readonly AudioClip m_drinkSound2;
    private readonly AudioClip m_gameOverSound;

    private Animator m_animator;
    private int m_food;
    private int m_currentPower;

    public int GetDamageDealt()
    {
        return CalculateAttackDamage();
    }
    protected override void Start()
    {
        m_animator = GetComponent<Animator>();
        m_food = GameManager.instance.playerFoodPoints;
        m_foodText.text = "Food: " + m_food;
        base.Start();
        m_currentPower = m_baseAttackPower;
    }

    private void OnDisable()
    {
        GameManager.instance.playerFoodPoints = m_food;
    }
 
    void Update()
    {
        if (!GameManager.instance.playersTurn)
        {
            return;
        }

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (horizontal != 0)
        {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove<Wall>(horizontal, vertical);
        }
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        m_food--;
        m_foodText.text = "Food: " + m_food;
        base.AttemptMove<T>(xDir, yDir);

        RaycastHit2D hit;
        if (Move(xDir, yDir, out hit))
        {
            SoundManager.Instance.RandomizeSfx(m_moveSound1, m_moveSound2);
        }
        CheckIfGameOver();
        GameManager.instance.playersTurn = false;
    }

    private void CheckIfGameOver()
    {
        if (m_food <= 0)
        {
            SoundManager.Instance.PlaySingle(m_gameOverSound);
            SoundManager.Instance.MusicSource.Stop();
            GameManager.instance.GameOver();
        }
        
    }
    public void LoseFood(int loss)
    {
        m_animator.SetTrigger("PlayerHit");
        m_food -= loss;
        m_foodText.text = "-" + loss + " Food: " + m_food;
        CheckIfGameOver();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", m_restarLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Food")
        {
            m_food += m_pointsPerFood;
            SoundManager.Instance.RandomizeSfx(m_eatSound1, m_eatSound2);
            m_foodText.text = "+" + m_pointsPerFood + " Food: " + m_food;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Soda")
        {
            m_food += m_pointsPerSoda;
            SoundManager.Instance.RandomizeSfx(m_drinkSound1, m_drinkSound2);
            m_foodText.text = "+" + m_pointsPerSoda + " Refreshing! " + m_food;
            other.gameObject.SetActive(false);
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(m_wallDamage);

        m_animator.SetTrigger("PlayerChop");
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       //Application.LoadLevel(Application.loadedLevel);
    }

    private int CalculateAttackDamage()
    {
        return m_currentPower;
    }
    
}
