using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTileScript : MonoBehaviour
{
    public float crackSpeed = 1f;
    public Sprite[] crackSprites;
    public SpriteRenderer crackRenderer;

    public GameObject tile;
    public GameObject crack;
    public GameObject debris;
    public GameObject death;

    // Start is called before the first frame update
    void Start()
    {
        tile = transform.Find("Tile").gameObject;
        crack = transform.Find("Crack").gameObject;
        debris = transform.Find("Debris").gameObject;
        death = transform.Find("DeathTile").gameObject;

        crackRenderer = crack.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float crackCounter = 0f;

    void OnTriggerExit2D(Collider2D other) {
        if(crackCounter >= 0.5f) {
            Break();
        }

        crackCounter = 0f;
        crackRenderer.sprite = null;
    }

    void OnTriggerStay2D(Collider2D other) {
        if( !(other.gameObject.name == "Player" && !other.gameObject.GetComponent<PlayerScript>().isJumping) ) {
            return;
        }

        if(crackCounter >= 1f) {
            // break tile
            Break();
            // send player to their death
            StartCoroutine( other.gameObject.GetComponent<PlayerScript>().FallToDeath() );
            // disable this script
            this.enabled = false;
        } else {
            crackCounter += crackSpeed * Time.fixedDeltaTime;
            crackRenderer.sprite = crackSprites[Mathf.FloorToInt( (crackSprites.Length * crackCounter) % crackSprites.Length )];
        }
    }

    public void Break() {
        tile.SetActive(false);
        crack.SetActive(false);
        debris.SetActive(true);
        death.SetActive(true);
    }
}
