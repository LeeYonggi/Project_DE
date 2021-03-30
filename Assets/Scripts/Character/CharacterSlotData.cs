using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterSlotData
{
    public CharacterStatistics characterStatistics;

    public CharacterSlotData(CharacterStatistics characterStatistics)
    {
        this.characterStatistics = characterStatistics;
    }
    public CharacterSlotData() { }
}