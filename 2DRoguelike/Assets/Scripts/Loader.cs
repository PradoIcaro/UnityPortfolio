using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private GameObject m_gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        if (global::GameManager.instance == null)
        {
            Instantiate(m_gameManager);
        }
    }
}
