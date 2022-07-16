using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    private bool m_living = true;

    public bool living
    {
        get { return m_living; }
        set { m_living = value; }
    }
}
