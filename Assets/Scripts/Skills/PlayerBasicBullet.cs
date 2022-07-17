using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicBullet : MonoBehaviour {
    public float speed = 3f, lifetime = 3f, damage = 3f;
    public GameObject hitFx, despawnFx;
    public AudioClip hitSound;
    public AudioSource audioSource;
    private float time = 0f;

    public Rigidbody rigid;

    void Start() {
        time = 0f;
    }

    void Update() {
        time += Time.deltaTime;
        rigid.MovePosition(transform.position + transform.forward * speed / 60f);
        if (time > lifetime) Despawn();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Boss")) {
            BossControl.health -= damage;
            Debug.Log("B" + BossControl.health);
            Hit();
        }
        else if (collision.collider.CompareTag("Terrain")) {
            Despawn();
        }
    }

    public void Hit() {
        if (hitFx != null) {
            Instantiate(hitFx, transform.position, transform.rotation);
        }
        if (hitSound != null) {
            //audioSource.PlayOneShot(hitSound);
            AudioSource.PlayClipAtPoint(hitSound, GameObject.FindGameObjectWithTag("Player").transform.position);
        }
        Destroy(gameObject);
    }

    public void Despawn() {
        if (despawnFx != null) {
            Instantiate(despawnFx, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}