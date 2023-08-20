using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator anim;
    private float time = 0;
    private float delayTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //anim.SetTrigger("Jump");
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if(time > delayTime){
    //         anim.SetTrigger("Jump");
    //         time += Time.deltaTime;
    //         time = 0;
    //     }
    // }
}
