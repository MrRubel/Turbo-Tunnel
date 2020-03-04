using UnityEngine;

public class Mover: MonoBehaviour {
    public float rangeX = 2;
    public float rangeY = 1.5f;
    public float timeDelimiterX = 4f;
    public float timeDelimiterY = 3f;

    void Update() {
        transform.position = new Vector3(
            Mathf.SmoothStep(-rangeX, rangeX, Mathf.PingPong(Time.time / timeDelimiterX, 1)),
            Mathf.SmoothStep(-rangeY, rangeY, Mathf.PingPong(Time.time / timeDelimiterY, 1)),
            0
        );
    }
}