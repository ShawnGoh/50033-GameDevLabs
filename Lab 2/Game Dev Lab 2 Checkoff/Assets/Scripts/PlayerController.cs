using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
 using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public float upSpeed;
    private Rigidbody2D marioBody;
    public float maxSpeed = 10;
    private bool onGroundState = false;
    private float moveHorizontal;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;



    public GameOverScreen GameOverScreen;

    private Animator marioAnimator;

    private AudioSource marioJumpAudio;


    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
	    Application.targetFrameRate =  30;
	    marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioJumpAudio = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetKeyDown("a") && faceRightState){
          Debug.Log("Left Key pressed");
          faceRightState = false;
          marioSprite.flipX = true;
          if (Mathf.Abs(marioBody.velocity.x) >  1.0) 
	        marioAnimator.SetTrigger("onSkid");
      }

      if (Input.GetKeyDown("d") && !faceRightState){
          Debug.Log("Right Key pressed");
          faceRightState = true;
          marioSprite.flipX = false;
          if (Mathf.Abs(marioBody.velocity.x) >  1.0) 
	        marioAnimator.SetTrigger("onSkid");
      }
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) {
            Debug.Log("On the Ground");
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }

        if (col.gameObject.CompareTag("Obstacles")) {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }
        
    } 



    void  FixedUpdate()
    {
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
            // stop
            Debug.Log("Directional Key Released");
            marioBody.velocity = Vector2.zero;
        }

    
        moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
                    marioBody.AddForce(movement * speed);
        }

        if (Input.GetKey("space") && onGroundState){
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            marioAnimator.SetBool("onGround", onGroundState);
        }

    }


  void OnTriggerEnter2D(Collider2D other)
  {
      if (other.gameObject.CompareTag("Enemy"))
      {
          Debug.Log("Collided with Gomba!");
          Time.timeScale = 0.0f;
      }
  }


    void  PlayJumpSound(){
        marioJumpAudio.PlayOneShot(marioJumpAudio.clip);
    }

    
}
