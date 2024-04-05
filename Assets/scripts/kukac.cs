using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kukac : MonoBehaviour
{
    //Ez kell ahhoz, hogy felismerje a cherry prefabot a kukac scriptje:
    public GameObject cherryPrototype;
    Animator anim;
    //Ebbe kell elmenteni az egér pozícióját:
    Vector3 mousePos;

    //Állapotgép változó:
    int s = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (s == 0) //Kattintásra várunk, idle állapot
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !anim.GetBool("anim_start"))
            {
                anim.SetBool("anim_start", true);

                //Így kapjuk meg a kurzor helyét:
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Ezt le kell nullázni, mert amúgy -10 lesz:
                mousePos.z = 0;
                //Most le kell klónozni a cherry-t:
                GameObject cherryClone = Instantiate(cherryPrototype);
                //Így tesszük a kurzor helyére a klónt:
                cherryClone.transform.position = mousePos;

                //Átmegyünk az s=1-be:
                s = 1;
            }


        }
        else if (s == 1) // Vízszintesen mozog
        {
            int direction = (int)Mathf.Sign(mousePos.x - transform.position.x);

            transform.Translate(direction * 1.0f * Vector3.right * Time.deltaTime);

            if (Mathf.Abs(mousePos.x - transform.position.x) < 0.05f)
            {
                int turnDir = (int)Mathf.Sign(mousePos.y - transform.position.y);
                transform.Rotate(turnDir * 90 * Vector3.forward);
                s = 2;
            }
        }
        else if (s == 2) // Vertikálisan mozog
        {
            transform.Translate(1.0f * Vector3.right * Time.deltaTime);
        }



        else if (Input.GetKeyDown(KeyCode.Mouse0) && anim.GetBool("anim_start"))
        {
            anim.SetBool("anim_start", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "food")
        {
            anim.SetBool("anim_start", false);
            transform.localEulerAngles = Vector3.zero;
            Destroy(collision.gameObject);

            s = 0;
        }
    }
}
