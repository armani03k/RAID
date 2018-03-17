using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilForm : Transformation {
    public Transform HostLocation;
    public Transform TPLocation;

    public override void UseGroundAbility() {
        HostLocation.position = TPLocation.position;
    }

    public override void UseAerialAbility() {
        HostLocation.position = TPLocation.position;
    }

}
