using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dream Roamer/Create Dialogue Asset", order = 0)]
public class Dialogue : ScriptableObject
{
    public string actorName;
    public Sprite actorPortrait;

    public string[] textBlocks;



}
