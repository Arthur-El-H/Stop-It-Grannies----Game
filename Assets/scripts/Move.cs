using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 1.5f;
    InputManager inputManager;
    private Animator animator;


    void Start()
    {
        this.inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    bool up ; public void stopUp() { up = false; }
    bool down ; public void stopDown() { down = false; }
    bool left ; public void stopLeft() { left = false; }
    bool right ; public void stopRight() { right = false; }

    public void moveUp()
    {
        transform.position = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + Vector2.up, speed * Time.deltaTime);
        if (!(right || left)) { down = false; animator.Play("player_Up"); up = true; }
        //rb.AddForce(Vector3.up);
    }
    public void moveDown()
    {
        transform.position = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + Vector2.down, speed * Time.deltaTime);
        if (!(right || left)) { up = false; animator.Play("player_Down"); down = true; }
        //rb.AddForce(Vector3.down);
    }
    public void moveLeft()
    {
        transform.position = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + Vector2.left, speed * Time.deltaTime);
        if (!(up || down)) { right = false; animator.Play("player_Left"); left = true; }
        //rb.AddForce(Vector3.left);
    }
    public void moveRight()
    {
        transform.position = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + Vector2.right, speed * Time.deltaTime);
        if (!(up || down)) { left = false; animator.Play("player_Right"); right = true; }
        //rb.AddForce(Vector3.right);
    }

    public void stand()
    {
        animator.Play("stand");
    }
}
