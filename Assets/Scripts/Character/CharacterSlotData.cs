using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterSlotData
{
    public GameObject objectPrediction;
    public GameObject characterPrefab;

    public CharacterSlotData(GameObject objectPrediction, GameObject characterPrefab)
    {
        this.objectPrediction = objectPrediction;
        this.characterPrefab = characterPrefab;
    }
    public CharacterSlotData() { }
}