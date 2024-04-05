using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("anim_start"))
        {
            anim.SetBool("anim_start", true);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool("anim_start"))
        {
            anim.SetBool("anim_start", false);
        }
    }
}
