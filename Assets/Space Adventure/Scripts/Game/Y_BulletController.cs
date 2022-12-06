using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_BulletController : MonoBehaviour
{
    private bool crashed;
    private bool destroyed;
    private SpinBullets sb;

    void Start()
    {
        crashed = false;
        destroyed = false;
    }

    void Update() {
        if (this.transform.position.x > 5 || this.transform.position.x < -5 || this.transform.position.y < -5) {
            Destroy(this.gameObject);
        }

        if (destroyed) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);

        // Check if did not crashed already.
        if(!crashed)
        {
            // Checks if obstacle collides with one of the player colliders.
            if(col.name == "Front" || col.name == "Back")
            {
                // Open game over window.
                GameOver.instance.Crashed();
                // Set crashed value to true so that this function would not be called again.
                crashed = true;
                try {
                    sb = GameObject.Find("CenterOfYBullets(Clone)").GetComponent<SpinBullets>();
                    sb.SetCrashed();
                } catch (System.Exception) {
                    Debug.Log("Err");
                }
                
                Highscore.SetAmount(Highscore.GetAmount() - 10 >= 0 ? Highscore.GetAmount() - 10 : 0);
            }

            if (col.tag == "Bullet") {
                Destroy(col.gameObject);
                destroyed = true;
            }
        }
    }
}
