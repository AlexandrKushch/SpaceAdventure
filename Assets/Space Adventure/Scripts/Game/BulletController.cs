using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    void Update() {
        if (this.transform.position.y > 5) {
            Destroy(this.gameObject);
        }
    }
}
