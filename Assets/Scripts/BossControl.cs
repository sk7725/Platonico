using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour {
    public const float MAX_HP = 100f;
    public const int ATTACKS = 3;
    public static float health = 100f;

    public int attack = 0;
    public bool attacking = false;
    public GameObject[] waypoints;
    public GameObject centerWaypoint, bulletHelper;

    public GameObject cBullet, laser;
    private GameObject currentLaser;

    public PlayerControl pcon;

    void Start() {
        health = MAX_HP;
        pcon = FindObjectOfType<PlayerControl>();
    }

    void Update() {
        if (!attacking) {
            attack = Random.Range(0, ATTACKS);
            attacking = true;
            switch (attack) {
                case 2:
                    MoveToRandom(4f);
                    StartCoroutine(BulletRingRain(cBullet, 4f, 0.5f, 20));
                    break;
                case 1:
                    MoveToRandom(3f);
                    StartCoroutine(BulletRain(cBullet, 3f, 0.1f));
                    break;
                case 0:
                    StartCoroutine(LaserFollowPlayer(6f));
                    break;
                default:
                    attacking = false;
                    break;
            }
        }
    }

    IEnumerator MoveAround() {
        Debug.Log("Move");
        int n = Random.Range(4, 7);
        for (int i = 0; i < n; i++) {
            float d = Random.Range(0.2f, 0.7f);
            yield return new WaitForSeconds(d);
            StartCoroutine(MoveTo(waypoints[Random.Range(0, waypoints.Length)].transform.position, 0.4f));
            yield return new WaitForSeconds(0.4f);
        }

        attacking = false;
    }

    void MoveToRandom(float duration) {
        int id = Random.Range(0, waypoints.Length);
        Vector3 way = waypoints[id].transform.position;
        if (Vector3.Distance(way, pcon.transform.position) < 8f) {
            way = waypoints[(id + 1) % waypoints.Length].transform.position;
        }

        StartCoroutine(MoveTo(way, duration));
    }

    IEnumerator MoveTo(Vector3 tpos, float duration) {
        Vector3 pos = transform.position;
        float time = 0f;
        while (time < duration) {
            time += Time.deltaTime;
            float f = time / duration;
            transform.position = Vector3.Lerp(pos, tpos, f);
            yield return null;
        }
        transform.position = tpos;
    }

    IEnumerator BulletRain(GameObject bullet, float duration, float interval) {
        float time = 0f;
        while (time < duration) {
            yield return new WaitForSeconds(interval);
            time += interval;
            Bullet(bullet);
        }
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        attacking = false;
    }

    IEnumerator BulletRingRain(GameObject bullet, float duration, float interval, int n) {
        float time = 0f;
        while (time < duration) {
            yield return new WaitForSeconds(interval);
            time += interval;
            bulletHelper.transform.forward = (pcon.transform.position - transform.position);
            bulletHelper.transform.rotation *= Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            RingShot(bullet, n);
        }
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        attacking = false;
    }

    void RingShot(GameObject bullet, int n) {
        for (int i = 0; i < n; i++) {
            //Vector3 qe = transform.rotation.eulerAngles;
            bulletHelper.transform.rotation *= Quaternion.Euler(0, 360f / n, 0);
            Instantiate(bullet, transform.position, bulletHelper.transform.rotation);
        }
    }

    void Bullet(GameObject bullet) {
        Vector3 p = (pcon.transform.position - transform.position).normalized * 2f;
        Quaternion dir = Quaternion.LookRotation(p, Vector3.up);
        //if (shootFx != null) Instantiate(shootFx, p + pcon.transform.position, dir);
        Instantiate(bullet, p + transform.position, dir);
    }

    IEnumerator LaserFollowPlayer(float duration) {
        StartCoroutine(MoveTo(centerWaypoint.transform.position, 1f));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(StartLaser(0.5f));
        yield return new WaitForSeconds(0.5f);
        //start laser
        float time = 0f;
        while (time < duration) {
            time += Time.deltaTime;
            bulletHelper.transform.forward = (pcon.transform.position - transform.position);
            currentLaser.transform.rotation = Quaternion.Lerp(currentLaser.transform.rotation, bulletHelper.transform.rotation, Time.deltaTime * 1f);
            yield return null;
        }

        StartCoroutine(EndLaser(0.5f));
        yield return new WaitForSeconds(0.7f);
        attacking = false;
    }

    IEnumerator StartLaser(float duration) {
        currentLaser = Instantiate(laser);
        currentLaser.transform.position = transform.position;
        currentLaser.transform.forward = Vector3.down;
        Transform core = currentLaser.transform.GetChild(0);
        core.localScale = new Vector3(0, 1, 0);

        float time = 0f;
        while (time < duration) {
            time += Time.deltaTime;
            float f = time / duration;
            core.localScale = new Vector3(f, 1, f);
            yield return null;
        }
        core.localScale = Vector3.one;
    }

    IEnumerator EndLaser(float duration) {
        Transform core = currentLaser.transform.GetChild(0);
        float time = 0f;
        while (time < duration) {
            time += Time.deltaTime;
            float f = time / duration;
            core.localScale = new Vector3(1 - f, 1, 1 - f);
            yield return null;
        }
        Destroy(currentLaser);
        currentLaser = null;
    }
}