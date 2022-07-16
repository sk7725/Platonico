using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour {
    public const float MAX_HP = 100f;
    public static float health = 100f;

    void Start() {
        health = MAX_HP;
    }

    void Update() {

    }
}
