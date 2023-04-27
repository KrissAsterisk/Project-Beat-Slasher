using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{   
    private Animator animator;
    public GameObject sword;
    public KeyCode slashKey = KeyCode.Space;
    private bool canSlash = true;

    // Start is called before the first frame update
    void Start()
    {
        // Find the sword GameObject as a child of the player GameObject
        animator = sword.GetComponent<Animator>();
    }

    // Update is called once per frame  
    void Update()
    {
    
        if (canSlash && Input.GetKeyDown(slashKey))
        {
            canSlash = false;
            animator.SetTrigger("SlashTrigger");
        }
    
    }

    // Used by animation event to end the slash
    public void OnSlashAnimationEnd()
    { 

        canSlash = true;
        Debug.Log("Animation Reset!");
    }
}