using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }


    /*
     *  Movement
     */
    public float walkSpeed;
    public float stepSize = 1f;
    Vector2 movement;


    // Update is called once per frame
    void Update()
    {
        movement = GetMovement().normalized;
    }

    void FixedUpdate() {
        transform.position += new Vector3(movement.x, movement.y, 0f) * walkSpeed * Time.fixedDeltaTime;
    }

    protected Vector2 GetMovement() {
        Vector2 mov;

        bool d = Input.GetKey(KeyCode.DownArrow);
        bool u = Input.GetKey(KeyCode.UpArrow);
        bool l = Input.GetKey(KeyCode.LeftArrow);
        bool r = Input.GetKey(KeyCode.RightArrow);

        mov.x = (l && r ? 0 : (l ? -1 : (r ? 1 : 0)));
        mov.y = (d && u ? 0 : (d ? -1 : (u ? 1 : 0)));

        return mov;
    }
}
