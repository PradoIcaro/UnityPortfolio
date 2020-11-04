using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthBar : MonoBehaviour
{
    [SerializeField]Transform fillBar;      
    
    public void SetBarSize(float fillValue)
    {
        fillBar.localScale = new Vector3(fillValue, 1f);
    }
    
}
