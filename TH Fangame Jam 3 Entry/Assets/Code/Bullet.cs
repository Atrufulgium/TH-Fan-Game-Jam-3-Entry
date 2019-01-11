using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    public float speed;
    public int maxLifetime = 600;

    Vector2 scaledDirection;

    int lifetime = 0;
    Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        ChangeSpeed(direction, speed);
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = tr.position;
        pos += scaledDirection * Time.deltaTime;
        tr.position = new Vector3(pos.x, pos.y, tr.position.z);
        lifetime++;
        if (lifetime > maxLifetime - 60) {
            tr.localScale = Vector3.one * (1 - (lifetime - maxLifetime + 60) / 60f);
        }
        if (lifetime > maxLifetime) {
            Destroy(gameObject);
        }
    }

    public void StartShrink() {
        lifetime = maxLifetime = 60;
    }

    // Move the bullet "direction" with a speed of "speed" tiles a second.
    public void ChangeSpeed(Vector2 direction, float speed) {
        scaledDirection = direction.normalized * speed;
    }

    public static void CreateBullet(Vector2 position, Vector2 direction, float speed, Color innerColor, Color outerColor) {
        GameObject createdObject = Instantiate((GameObject) Resources.Load("Prefabs/Bullet"));
        createdObject.transform.position = new Vector3(position.x, position.y, -1);
        Bullet bullet = createdObject.GetComponent<Bullet>();
        bullet.direction = direction;
        bullet.speed = speed;
        SpriteRenderer renderer = createdObject.GetComponent<SpriteRenderer>();
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(block);
        block.SetColor("_InnerColor", innerColor);
        block.SetColor("_OuterColor", outerColor);
        renderer.SetPropertyBlock(block);
    }

    public static void CreateBullet(Vector2 position, float angle, float speed, Color innerColor, Color outerColor) {
        CreateBullet(position, new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)), speed, innerColor, outerColor);
    }
}
