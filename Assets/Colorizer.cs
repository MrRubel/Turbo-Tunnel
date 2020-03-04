using UnityEngine;

public class Colorizer: MonoBehaviour {
    public ParticleSystem tunnelWall;
    public ParticleSystem cellBig;
    public ParticleSystem cellSmall;
    public ParticleSystem cellVessel;

    void Update() {
        Color color = new Color(
            Mathf.SmoothStep(1, 0.5f, Mathf.PingPong(Time.time / 10f, 1)),
            Mathf.SmoothStep(0, 1, Mathf.PingPong(Time.time / 15f, 1)),
            Mathf.SmoothStep(0, 1, Mathf.PingPong(Time.time / 5f, 1))
        );

        var tunnelWallMainSettings = tunnelWall.main;
        tunnelWallMainSettings.startColor = color;

        var cellBigMainSettings = cellBig.main;
        cellBigMainSettings.startColor = color;

        var cellSmallMainSettings = cellSmall.main;
        cellSmallMainSettings.startColor = color;

        var cellVesselMainSettings = cellVessel.main;
        cellVesselMainSettings.startColor = color;
    }
}