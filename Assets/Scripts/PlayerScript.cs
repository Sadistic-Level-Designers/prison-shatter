using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript i;

    // Start is called before the first frame update
    void Start()
    {
        if(!i) {
            i = this;
        } else {
            throw new System.Exception("Singleton override");
        }

        renderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    /*
     * Sprites
     */
    [Header("Sprites")]
    public Sprite[] walkLeft;
    public Sprite[] walkRight;
    public Sprite[] walkUp;
    public Sprite[] walkDown;
    public Sprite[] jumpLeft;
    public Sprite[] jumpRight;
    public Sprite[] jumpUp;
    public Sprite[] jumpDown;
    public Sprite death;

    [Header("Rendering")]
    public SpriteRenderer renderer;
    protected Sprite[] spriteSheet;
    public float spriteIndex;

    /*
     *  Movement
     */
    [Header("Movement")]
    public float walkSpeed;
    Vector2 movement;


    // Update is called once per frame
    void Update()
    {
        movement = GetMovement().normalized;
        UpdateSprites(movement);
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

    protected void UpdateSprites(Vector2 mov) {
                /* if(mov.y > 0) */ spriteSheet = walkUp;
                if(mov.y < 0) spriteSheet = walkDown;
        else    if(mov.x > 0) spriteSheet = walkRight;
        else    if(mov.x < 0) spriteSheet = walkLeft;

        if(mov.sqrMagnitude > 0) {
            spriteIndex = (spriteIndex + (spriteSheet.Length * Time.deltaTime)) % spriteSheet.Length;
        } else {
            spriteIndex = 0;
        }

        this.renderer.sprite = spriteSheet[Mathf.FloorToInt(spriteIndex)];
    }
}
