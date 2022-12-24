using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName ="ScriptebleObject/Attack", order =1)]
public class Attack : ScriptableObject
{

    public int Damage;
    public int Cost;
    public Sprite spriteImage;

    public string Name;
    public string Description;

}
