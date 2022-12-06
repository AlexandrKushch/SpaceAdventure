using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBullets : MonoBehaviour
{
    private float speed;
    public float timeS;
    private GameObject y_bullet;

    void Start()
    {
        speed = 0.25f;
        y_bullet = (GameObject) Resources.Load("Y Bullet");
        StartCoroutine(WaitShooting());
    }

    void Update()
    {
        transform.Rotate(0, 0, speed, Space.Self);
    }

    IEnumerator WaitShooting() {
        Shoot();
        yield return new WaitForSeconds(speed);

        StartCoroutine(WaitShooting());
    }

    void Shoot() {
        GameObject b = Instantiate(y_bullet, transform.position, transform.rotation);
        b.GetComponent<Rigidbody2D>().AddRelativeForce(b.transform.position * 100.0f);
    }

    public void SetCrashed() {
        Destroy(this.gameObject);
    }
}
