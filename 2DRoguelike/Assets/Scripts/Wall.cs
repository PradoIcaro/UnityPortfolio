using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    public Sprite DamageSprite;
    public int Hp = 4;
    private readonly AudioClip m_chopSound1;
    private readonly AudioClip m_chopSound2;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DamageWall(int loss)
    {
        SoundManager.Instance.RandomizeSfx(m_chopSound1, m_chopSound2);
        spriteRenderer.sprite = DamageSprite;
        Hp -= loss;

        if (Hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    
}
