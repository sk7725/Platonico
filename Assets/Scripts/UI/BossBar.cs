using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBar : MonoBehaviour {
    private const float HP_DELTA = 0.08f;

    public float width = 970f;
    public RectTransform hpBar, hpBarSub;
    public float deltaHp;
    private bool init = false;

    void Start() {
        SetBar(hpBar, 0);
        SetBar(hpBarSub, 0);
        //todo remove
        Init();
    }

    void Update() {
        if (!init) return;
        float hp = Mathf.Max(0, BossControl.health);

        if (Mathf.Abs(deltaHp - hp) < 0.01f) deltaHp = hp;
        else deltaHp = lerpDelta(deltaHp, hp, hp >= deltaHp ? HP_DELTA * 2f : HP_DELTA);
        if (deltaHp <= hp) {
            //healing
            SetBar(hpBarSub, hp / BossControl.MAX_HP);
            SetBar(hpBar, deltaHp / BossControl.MAX_HP);
        }
        else {
            //taken damage
            SetBar(hpBarSub, deltaHp / BossControl.MAX_HP);
            SetBar(hpBar, hp / BossControl.MAX_HP);
        }
    }

    public void Init() {
        FillBar(2f);
    }

    public void SetBar(RectTransform r, float f) {
        r.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, f * width);
    }

    float lerpDelta(float fromValue, float toValue, float progress) {
        return fromValue + (toValue - fromValue) * Mathf.Clamp01(progress * Time.deltaTime * 60f);
    }

    IEnumerator FillBar(float duration) {
        float time = 0;
        while (time < duration) {
            time += Time.deltaTime;
            float f = time / duration;
            transform.localScale = Vector3.one * f;
            yield return null;
        }
        transform.localScale = Vector3.one;

        time = 0f;
        while (time < duration) {
            time += Time.deltaTime;
            float f = time / duration;
            SetBar(hpBar, f);
            yield return null;
        }

        SetBar(hpBar, 1f);
        SetBar(hpBarSub, 1f);
        init = true;
    }
}
