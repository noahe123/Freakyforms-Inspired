using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DropShadow : MonoBehaviour
{
    public Vector2 ShadowOffset;
    public Material ShadowMaterial;

    SpriteRenderer spriteRenderer;
    GameObject shadowGameobject;

    void Start()
    {
        //destroy old shadow if it exists
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        spriteRenderer = GetComponent<SpriteRenderer>();

        //create a new gameobject to be used as drop shadow
        shadowGameobject = new GameObject("Shadow");

        //make object child
        shadowGameobject.transform.parent = transform;

        //set shadow scale
        shadowGameobject.transform.localScale = Vector3.one;


        //create a new SpriteRenderer for Shadow gameobject
        SpriteRenderer shadowSpriteRenderer = shadowGameobject.AddComponent<SpriteRenderer>();

        //set the shadow gameobject's sprite to the original sprite
        shadowSpriteRenderer.sprite = spriteRenderer.sprite;
        //set the shadow gameobject's material to the shadow material we created
        shadowSpriteRenderer.material = ShadowMaterial;

        //update the sorting layer of the shadow to always lie behind the sprite
        shadowSpriteRenderer.sortingLayerName = spriteRenderer.sortingLayerName;
        shadowSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;

        //set the shadow strength
        shadowSpriteRenderer.color = new Color(0, 0, 0, .4f);



    }
  

    void LateUpdate()
    {
        //update the position and rotation of the sprite's shadow with moving sprite
        shadowGameobject.transform.position = transform.position + (Vector3)ShadowOffset;
        shadowGameobject.transform.rotation = transform.rotation;
    }
}