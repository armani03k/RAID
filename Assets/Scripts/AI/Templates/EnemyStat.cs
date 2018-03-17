using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/New Enemy")]
public class EnemyStat : ScriptableObject {

    public string Name;
    public float HP;
    public float Dmg;
    public Color BaseSkin;
}
