 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthBar : MonoBehaviour
{
    [SerializeField]Transform fillBar;      
    
    public void SetBarSize(float fillValue)
    {
        if (fillValue < 0 )
        {
            fillValue = 0;
        }

        fillBar.localScale = new Vector3(fillValue, 1f);
    }
}
