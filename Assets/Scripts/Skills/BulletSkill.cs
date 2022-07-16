using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/BulletSkill", order = 11)]
public class BulletSkill : Skill {
    public GameObject bullet, shootFx;
    public int shots = 1;
    public float interval;

    public override void Use(PlayerControl pcon) {
        pcon.StartCoroutine(Shoot(pcon));
    }

    IEnumerator Shoot(PlayerControl pcon) {
        for (int i = 0; i < shots; i++) {
            Bullet(pcon);
            yield return new WaitForSeconds(interval);
        }
    }

    public void Bullet(PlayerControl pcon) {
        Vector3 p = (pcon.boss.transform.position - pcon.transform.position).normalized;
        Quaternion dir = Quaternion.LookRotation(p, Vector3.up);
        if (shootFx != null) Instantiate(shootFx, p + pcon.transform.position, dir);
        Instantiate(bullet, p + pcon.transform.position, dir);
    }
}
