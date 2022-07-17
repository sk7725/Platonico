using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneControl : MonoBehaviour {
    public BossBar bossBar;
    public Transform playerStart;

    public ChatUpdater chat;

    public PlayerControl pcon;
    public BossControl boss;

    public bool ended = false;

    public string[] start, win, lose;

    void Start() {
        pcon = FindObjectOfType<PlayerControl>();
        boss = FindObjectOfType<BossControl>();
        PlayerControl.cutscene = true;
        boss.enabled = false;
        ended = false;
        StartCoroutine(BossStartCutscene());
    }

    void Update() {
        if (ended) return;
        if (BossControl.health <= 0) {
            //todo
            EndBoss();
            chat.Set(win, () => {

            });
        }
        else if (PlayerControl.health <= 0) {
            //todo
            EndBoss();
            chat.Set(lose, () => {

            });
        }
    }

    public void EndBossStartCutscene() {
        PlayerControl.cutscene = false;
        boss.enabled = true;
        pcon.transform.position = playerStart.position;
        pcon.transform.rotation = playerStart.rotation;
    }
    public void EndBoss() {
        ended = true;
        PlayerControl.cutscene = true;
        boss.enabled = false;
        boss.StopAllCoroutines();
        if(boss.currentLaser != null) Destroy(boss.currentLaser);
        bossBar.End();
    }

    IEnumerator BossStartCutscene() {
        yield return new WaitForSeconds(1);
        //pcon walks in
        //dialogue start
        chat.Set(start, () => {
            //show boss bar
            bossBar.Init();
            EndBossStartCutscene();
        });
    }
}
