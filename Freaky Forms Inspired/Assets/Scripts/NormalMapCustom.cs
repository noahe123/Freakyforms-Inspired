/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class NormalMapCustom : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void SetNormalMap()
    {
        material.EnableKeyword("_NORMALMAP");

        //fetch name of normal map
        string normal = "Images/Body Parts/Normals" + "/" + spriteRenderer.sprite.name;

        //Set the Normal map using the Texture
        spriteRenderer.material.SetTexture("_BumpMap", Resources.Load(normal) as Texture2D);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/