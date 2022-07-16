using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlatonic", menuName = "Platonics/Platonic", order = 1)]
public class Platonic : ScriptableObject {
    public enum MoveType {
        SLIDE,
        ROLL,
        HOVER
    }
    public MoveType moveType = MoveType.SLIDE;
    public GameObject prefab;
    public int jumps = 1;
    public float speedMultiplier = 1f;

    public virtual void Shift() {

    }

    public virtual void ShiftDown() {

    }

    public virtual void ShiftUp() {

    }
}
