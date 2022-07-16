using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillIconDisplay : MonoBehaviour {
    public Image icon, cooldown;
    public TextMeshProUGUI key;

    public Skill skill;
    public int id;
    private PlayerControl pcon;

    void Start() {

    }

    void Update() {
        if(skill != null) {
            cooldown.fillAmount = Mathf.Clamp01(pcon.cooldowns[id] / skill.cooldown);
        }
    }

    public void Set(PlayerControl pcon, Skill skill, int id) {
        this.skill = skill;
        this.id = id;
        this.pcon = pcon;
        icon.sprite = cooldown.sprite = skill.icon;
    }
}
