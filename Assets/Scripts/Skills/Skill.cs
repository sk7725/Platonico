using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/Skill", order = 10)]
public class Skill : ScriptableObject {
    public KeyCode key;
    public float cooldown = 15f;
    public Texture2D icon;
    public virtual void Use(PlayerControl pcon) {

    }
}
