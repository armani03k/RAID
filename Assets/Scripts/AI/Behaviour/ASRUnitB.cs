using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitPatrolPts : System.Object {
    public bool FaceRight;
    public Transform Point;
}

public class ASRUnitB : EnemyUnit {

    private UnitPatrolPts currentTarget;
    private bool faceRight = true;
    public List<UnitPatrolPts> Points;

    protected override void Start() {
        base.Start();
        UnitPatrolPts temp = Points[0];
        currentTarget = Points[0];
        Points.Add(temp);
        Points.Remove(Points[0]);
    }

    protected override void Update() {
        base.Update();
    }

    protected override void OnIdle() {
        if (delayTimer < IdleDelay) delayTimer += Time.deltaTime;

        if (delayTimer >= IdleDelay) {
            NextPoint();
            delayTimer = 0;
            Checkflip();
            ActiveState = UnitStates.Patrol;
        }
    }

    protected override void OnPatrol() {
        if (Vector3.Distance(new Vector3(this.transform.position.x, 0, this.transform.position.z), new Vector3(currentTarget.Point.position.x, 0, currentTarget.Point.position.z)) > 0.5f) {
            var distance = currentTarget.Point.position - transform.position;

            transform.position += new Vector3(distance.x, 0, distance.z) * MoveSpd * Time.deltaTime;
        } else ActiveState = UnitStates.Idle;
    }

    void NextPoint() {
        UnitPatrolPts temp = currentTarget;
        currentTarget = Points[0]; //next point
        Points.Remove(Points[0]); //pop from list
        Points.Add(temp); //add previous pos to list
    }

    void Checkflip() {
        if (faceRight != currentTarget.FaceRight) {
            Flip();
            faceRight = currentTarget.FaceRight;
        }
    }


}
