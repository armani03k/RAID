using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable/Transition")]
public class Transition : ScriptableObject {
    public Decision decision;
    public State trueState;
    public State falseState;
}
