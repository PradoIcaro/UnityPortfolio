using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private Sprite m_damageSprite;    
    [SerializeField]
    private  AudioClip m_chopSound1;
    [SerializeField]
    private  AudioClip m_chopSound2;

    
    private SpriteRenderer m_spriteRenderer;
    private int m_hp = 4;

    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DamageWall(int loss)
    {
        SoundManager.Instance.RandomizeSfx(m_chopSound1, m_chopSound2);
        m_spriteRenderer.sprite = m_damageSprite;
        m_hp -= loss;

        if (m_hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    
}
