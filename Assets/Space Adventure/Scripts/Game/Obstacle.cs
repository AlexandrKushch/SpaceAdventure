using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float speed;
    private bool crashed;
    private bool destroyed;
    private AudioSource audioSource;
    private SpinBullets sb;

    void Start()
    {
        // Randomly set the rotation speed.
        speed = Random.Range(-1.0f, 1.0f);
        audioSource = this.GetComponent<AudioSource>();
        crashed = false;
        destroyed = false;
    }

    void Update()
    {
        // Rotate obstacle.
        transform.Rotate(0, 0, speed, Space.Self); 

        if (destroyed) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Check if did not crashed already.
        if(!crashed)
        {
            // Checks if obstacle collides with one of the player colliders.
            if(col.name == "Front" || col.name == "Back" || col.name == "Y_Bullet")
            {
                // Play crash sound.
                audioSource.Play();
                // Reset player speed.
                speed = 0;
                // Open game over window.
                GameOver.instance.Crashed();
                
                Highscore.SetAmount(Highscore.GetAmount() - 10 >= 0 ? Highscore.GetAmount() - 10 : 0);
                // Set crashed value to true so that this function would not be called again.
                crashed = true;
                try {
                    sb = GameObject.Find("CenterOfYBullets(Clone)").GetComponent<SpinBullets>();
                    sb.SetCrashed();
                } catch (System.Exception) {
                    Debug.Log("Err");
                }
                
            }

            if (col.tag == "Bullet") {
                Destroy(col.gameObject);
                destroyed = true;

                if (this.name.Contains("1")) {
                    Highscore.SetAmount(Highscore.GetAmount() + 2);
                } else if (this.name.Contains("2")) {
                    Highscore.SetAmount(Highscore.GetAmount() + 4);
                } else if (this.name.Contains("3")) {
                    Highscore.SetAmount(Highscore.GetAmount() + 6);
                } else if (this.name.Contains("4")) {
                    Highscore.SetAmount(Highscore.GetAmount() + 8);
                }
            }
        }
    }
}
