 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthBar : MonoBehaviour
{
    [SerializeField]Transform m_fillBar;      
    
    public void SetBarSize(float fillValue)
    {
        if (fillValue < 0 )
        {
            fillValue = 0;
        }

        m_fillBar.localScale = new Vector3(fillValue, 1f);
    }
}
