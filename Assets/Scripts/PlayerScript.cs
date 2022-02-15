using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript i;
    public static bool isHurt;

    // Start is called before the first frame update
    void Start()
    {
        if(!i) {
            i = this;
        } else {
            throw new System.Exception("Singleton override");
        }

        renderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();

         if(SceneManager.GetActiveScene().name == "scene1") {
             isHurt = false;
             renderer.transform.Find("Blood").gameObject.SetActive(false);
         }
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
        if(!isJumping) {
            movement = GetMovement().normalized;
            UpdateSprites(movement);
        }

        bool pressed = Input.GetKeyDown(KeyCode.A);
        if(!isJumping && pressed) {
            StartCoroutine( Jump(movement) );
        }
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

        // hurt logic
        if(PlayerScript.isHurt) {
            renderer.transform.Find("Blood").gameObject.SetActive(true);
            int hurtCounter = Mathf.RoundToInt(Time.time * 6f) % 4;
            // Debug.Log("Hurt! " + hurtCounter);
            switch(hurtCounter) {
                case 0:
                case 1:
                case 2:
                    return Vector2.zero;

                case 3:
                    return mov;
            }
        }

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

    [Header("Jumping")]
    public float jumpDuration = 1f;
    public float jumpHeight = 4f;
    public bool isJumping = false;

    private static Vector2 HALF = new Vector2(0.5f, 0.5f);

    protected IEnumerator Jump(Vector2 mov) {
        float height = -renderer.gameObject.transform.localPosition.z;

        float jumpCounter = 0f;
        float jumpStart = height;
        float jumpEnd = jumpStart;

        Vector3 playerStart = transform.position;
        Vector3 playerEnd = mov.sqrMagnitude > 0f ? new Vector3(Mathf.RoundToInt(playerStart.x + mov.x * 2f), Mathf.RoundToInt(playerStart.y + mov.y * 2f), 0) : playerStart;
        Vector3 spriteStart = renderer.transform.localPosition;

        // raycast to check for walls
        Debug.DrawLine((Vector2)playerStart + HALF, (Vector2)playerEnd + HALF, Color.red, 0.5f, false);
        Vector2 walkDirection = playerEnd - playerStart;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)playerStart + HALF, walkDirection.normalized, walkDirection.magnitude, LayerMask.GetMask("Default"));
        Debug.Log(hit.collider.gameObject.name);

        if(!!hit && !!hit.collider && !!hit.collider.gameObject && hit.collider.gameObject.tag != "DeathTile") {
            playerEnd = (Vector2)playerStart + (hit.point - (Vector2)playerStart) / 2;
        }
        // yield return null;

        // start jump
        isJumping = true;
        GetComponent<Rigidbody2D>().isKinematic = true;

        while(jumpCounter < 1f) {
            yield return new WaitForEndOfFrame();
            jumpCounter += Time.deltaTime / jumpDuration;

            height = Mathf.Lerp(jumpStart, jumpEnd, jumpCounter) + Mathf.Sin(jumpCounter * Mathf.PI) * jumpHeight;

            
            transform.position = Vector3.Lerp(playerStart, playerEnd, jumpCounter);
            renderer.transform.localPosition = new Vector3(spriteStart.x, spriteStart.y, -height);

        }

        // end jump

        transform.position = playerEnd;
        renderer.transform.localPosition = new Vector3(spriteStart.x, spriteStart.y, -jumpEnd);
        isJumping = false;
        GetComponent<Rigidbody2D>().isKinematic = false;
    }

    

    public IEnumerator FallToDeath() {
        float time = 3f;
        this.enabled = false;
        renderer.sprite = death;

        while(time > 0f) {
            transform.position += new Vector3(0,0, 0.125f) * Time.deltaTime;

            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        if(SceneManager.GetActiveScene().name == "scene4b") {
            SceneManager.LoadScene( "scene4a" );
        } else {
            SceneManager.LoadScene( "scene1" );
        }


        yield return null;
    }
}
