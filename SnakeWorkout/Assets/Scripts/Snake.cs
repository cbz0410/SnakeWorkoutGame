using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour {
    
    public Transform segmentPrefab;
    public int initialSize = 6;
    
    private Vector2 _direction;
    private List<Transform> _segments = new List<Transform>();

    private void Start() {
        ResetState();
    }
    
    private void Update() {
        if((Input.GetKeyDown(KeyCode.UpArrow)  || Input.GetKeyDown(KeyCode.W)) && _direction != Vector2.down) {
            _direction = Vector2.up;
        } else if((Input.GetKeyDown(KeyCode.DownArrow)  || Input.GetKeyDown(KeyCode.S)) && _direction != Vector2.up) {
            _direction = Vector2.down;
        } else if((Input.GetKeyDown(KeyCode.LeftArrow)  || Input.GetKeyDown(KeyCode.A)) && _direction != Vector2.right) {
            _direction = Vector2.left;
        } else if((Input.GetKeyDown(KeyCode.RightArrow)  || Input.GetKeyDown(KeyCode.D)) && _direction != Vector2.left) {
            _direction = Vector2.right;
        }

        if(_segments.Count == 1) {
            WinGame();
        }
    }

    private void FixedUpdate() {
        for(int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector2(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y
        );
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Obstacle") {
            ResetState();
        } else if(collider.tag == "Food") {
            Workout();
        }
    }

    private void Workout() {
        Destroy(_segments[_segments.Count - 1].gameObject);
        _segments.RemoveAt(_segments.Count - 1);
    }

    private void Grow() {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    private void ResetState() {
        _direction = Vector2.right;
        transform.position = Vector3.zero;

        for (int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        for(int i = 0; i < initialSize - 1; i++) {
            Grow();
        }
    }

    private void WinGame() {
        _direction = Vector2.zero;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
