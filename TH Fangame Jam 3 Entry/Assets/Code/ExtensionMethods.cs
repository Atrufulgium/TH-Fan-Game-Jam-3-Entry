using UnityEngine;

public static class Extensionmethods {
    /// <summary>
    /// Returns the angle between the x-axis and a to b in rad.
    /// </summary>
    public static float AngleTo(this Vector2 a, Vector2 b) {
        return Mathf.Atan2(b.y - a.y, b.x - a.x);
    }
    /// <summary>
    /// Returns the angle between the x-axis and a to b in rad.
    /// Ignores the z-axis.
    /// </summary>
    public static float AngleTo(this Vector3 a, Vector2 b) {
        return Mathf.Atan2(b.y - a.y, b.x - a.x);
    }
    /// <summary>
    /// Returns the angle between the x-axis and a to b in rad.
    /// Ignores the z-axis.
    /// </summary>
    public static float AngleTo(this Vector3 a, Vector3 b) {
        return Mathf.Atan2(b.y - a.y, b.x - a.x);
    }
}