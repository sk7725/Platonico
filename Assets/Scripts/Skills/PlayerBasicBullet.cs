using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicBullet : MonoBehaviour {
    public float speed = 3f, lifetime = 3f, damage = 3f;
    public GameObject hitFx;
    private float time = 0f;

    public Rigidbody rigid;

    void Start() {
        time = 0f;
    }

    void Update() {
        time += Time.deltaTime;
        rigid.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        if (time > lifetime) Hit();
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log(collision);
        if (!collision.collider.CompareTag("Boss")) return;
        BossControl.health -= damage;
        Debug.Log(BossControl.health);
        Hit();
    }

    public void Hit() {
        if(hitFx != null) {
            Instantiate(hitFx, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
