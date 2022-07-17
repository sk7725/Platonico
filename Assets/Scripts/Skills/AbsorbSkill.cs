using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/AbsorbSkill", order = 12)]
public class AbsorbSkill : Skill{
    public GameObject shield;
    public float shieldDuration = 4f;

    public override void Use(PlayerControl pcon) {
        pcon.StartCoroutine(ShieldE(pcon));
    }

    IEnumerator ShieldE(PlayerControl pcon) {
        PlayerControl.damageMultiplier = -0.5f;
        GameObject s = Instantiate(shield, Vector3.zero, Quaternion.identity);
        s.transform.localScale = Vector3.zero;
        s.transform.SetParent(pcon.transform, false);

        for(int i=1; i<=15; i++) {
            s.transform.localScale = Vector3.one * (i / 15f) * 2;
            yield return null;
        }

        yield return new WaitForSeconds(shieldDuration);

        for (int i = 1; i <= 15; i++) {
            s.transform.localScale = Vector3.one * (1f - i / 15f) * 2;
            yield return null;
        }
        Destroy(s);

        PlayerControl.damageMultiplier = 1f;
    }
}
