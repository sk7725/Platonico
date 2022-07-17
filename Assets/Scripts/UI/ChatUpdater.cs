using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatUpdater : MonoBehaviour {
    public string[] chats;
    public Action onEnd;

    public GameObject textbox;
    public Text text;
    private bool active, outputting;
    private int chatid, letterid;

    void Start() {
        textbox.SetActive(false);
    }

    void Update() {
        if (!active) return;

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.C)) {
            if (outputting) {
                StopAllCoroutines();
                outputting = false;
                text.text = chats[chatid];
            }
            else {
                Next();
            }
        }
    }

    public void Set(string[] chats, Action onEnd) {
        this.chats = chats;
        this.onEnd = onEnd;
        outputting = false;
        letterid = 0;
        textbox.SetActive(true);
        active = true;
        chatid = -1;
        Next();
    }

    public void Next() {
        chatid++;
        if(chatid >= chats.Length) {
            onEnd();
            active = false;
            textbox.SetActive(false);
            return;
        }

        letterid = 0;
        text.text = "";
        outputting = true;
        StartCoroutine(Letters());
    }

    IEnumerator Letters() {
        for (; letterid < chats[chatid].Length; letterid++) {
            text.text += chats[chatid][letterid];
            yield return null;
        }
        outputting = false;
    }
}
