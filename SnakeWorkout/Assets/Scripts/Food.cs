using UnityEngine;

public class Food : MonoBehaviour {
    public BoxCollider2D boundaries;

    private void Start() {
        RandomizePosition();
    }

    private void RandomizePosition() {
        Bounds bounds = this.boundaries.bounds;

        this.transform.position = new Vector3(
            Mathf.Round(Random.Range(bounds.min.x, bounds.max.x)),
            Mathf.Round(Random.Range(bounds.min.y, bounds.max.y)),
            0f
        );
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player" || collision.tag == "Obstacle")
            RandomizePosition();
    }
}

