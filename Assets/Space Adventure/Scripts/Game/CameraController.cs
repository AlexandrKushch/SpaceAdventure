using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private int score;
    private GameObject backgorund;
    private Sprite[] backgrounds;

    private static int lvl;

    void Start() {
        score = Highscore.GetAmount();
        backgorund = GameObject.Find("Background");
        backgrounds = Resources.LoadAll<Sprite>("Backgrounds/");
    }

    // Smooth camera that follows a player.
    void Update()
    {
        // Limits camera movement for a certain range.
        if(target.position.x < 0 && transform.position.x > -0.25f || target.position.x > 0 && transform.position.x < 0.25f || target.position.x == 0)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z);
        }

        if (score > 40) {
            backgorund.GetComponent<SpriteRenderer>().sprite = backgrounds[0];
            lvl = 5;
        }
        else if (score > 30) {
            backgorund.GetComponent<SpriteRenderer>().sprite = backgrounds[2];
            lvl = 4;
        } else if (score > 20) {
            backgorund.GetComponent<SpriteRenderer>().sprite = backgrounds[3];
            lvl = 3;
        } else if (score > 10) {
            backgorund.GetComponent<SpriteRenderer>().sprite = backgrounds[1];
            lvl = 2;
        } else {
            backgorund.GetComponent<SpriteRenderer>().sprite = backgrounds[0];
            lvl = 1;
        }
    }

    public static int getLvl() {
        return lvl;
    }
}
