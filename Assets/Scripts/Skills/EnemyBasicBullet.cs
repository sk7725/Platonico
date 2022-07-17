using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicBullet : MonoBehaviour {
    public float speed = 3f, lifetime = 3f, damage = 3f;
    public GameObject hitFx;
    private float time = 0f;

    public Rigidbody rigid;
    public AudioClip hitSound;
    public AudioSource audioSource;

    void Start() {
        time = 0f;
    }

    void Update() {
        time += Time.deltaTime;
        rigid.MovePosition(transform.position + transform.forward * speed / 60f);
        if (time > lifetime) Hit();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Player")) {
            PlayerControl.health -= damage * PlayerControl.damageMultiplier;
            Debug.Log("P" + PlayerControl.health);
            Hit();
            if (hitSound != null) {
                audioSource.enabled = true;
                audioSource.PlayOneShot(hitSound);
            }
        }
        else if (collision.collider.CompareTag("Terrain")) {
            Hit();
        }
    }

    public void Hit() {
        if (hitFx != null) {
            Instantiate(hitFx, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
