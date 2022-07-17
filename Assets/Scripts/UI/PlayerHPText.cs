using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPText : MonoBehaviour {
    public string[] hakjum;
    public Text text;

    void Update() {
        if(PlayerControl.health <= 0) {
            text.text = "ÇÐÁ¡: F";
        }
        else {
            text.text = hakjum[Mathf.Clamp((int)(hakjum.Length * (1f - PlayerControl.health / PlayerControl.MAX_HP)), 0, hakjum.Length - 1)];
        }
    }
}
