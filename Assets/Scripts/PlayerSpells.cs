using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpells : MonoBehaviour
{
    public Spell[] allSpells;
    static Spell[] spells;
    public PlayerCharacter player;

    GameObject LearnSpellUI;

    public void Initialize()
    {
        spells = new Spell[4] { null, null, null, null };


        //test initialize spells
        for(int i = 0; i < 3; i++)
            LearnSpell(allSpells[i]);
    }

    public void LearnSpell(Spell newSpell)
    {
        bool anyNull = false;
        for(int i = 0; i < spells.Length; i++)
            if(spells[i] == null)
            {
                anyNull = true;
                SetSpell(i, newSpell);
                break;
            }
        if(!anyNull)
            LearnSpellUI.SetActive(true);
    }

    void SetSpell(int i, Spell newSpell)
    {
        spells[i] = newSpell;
    }

    public Spell GetSpell(int i)
    {
        return spells[i];
    }


}
