using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourney
{
    static uint m_compteur;
    private string m_name;
    private LevelProfile[] m_levels;

    public Tourney(string name, LevelProfile[] levels)
    {
        m_name = name;
        m_levels = levels;
        m_compteur = ++m_compteur;
    }

    public string GetName()
    {
        return m_name;
    }

    public LevelProfile GetLevel(uint levelNb)
    {
        return m_levels[levelNb];
    }

    public int GetNbOfLevel()
    {
        return m_levels.Length;
    }


    
}
