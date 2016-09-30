// Summary:
//
// Created by: Julian Glass-Pilon

using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class IntRange 
{
    public int m_Min;
    public int m_Max;

    //Constructor
    public IntRange(int min, int max)
    {
        m_Min = min;
        m_Max = max;
    }

    /// <summary>
    /// Return a random value from within a range
    /// </summary>
    public int Random
    {
        get { return UnityEngine.Random.Range(m_Min, m_Max); }
    }
}
