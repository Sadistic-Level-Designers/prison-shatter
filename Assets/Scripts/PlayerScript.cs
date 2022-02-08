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
    Vector2Int _targetPos;


    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Debug.Log(input);

        if(input.sqrMagnitude > 0f) {
            Vector2Int currentPos = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));

            _targetPos.x = currentPos.x + Mathf.CeilToInt(input.x);
            _targetPos.y = currentPos.y + Mathf.CeilToInt(input.y);
        }
    }

    void FixedUpdate() {
        Vector3 target = new Vector3(_targetPos.x, _targetPos.y, 0f);
        transform.position = Vector3.MoveTowards(transform.position, target, walkSpeed * Time.fixedDeltaTime);
    }
}
