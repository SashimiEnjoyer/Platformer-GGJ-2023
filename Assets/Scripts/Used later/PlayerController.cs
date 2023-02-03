using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController2D controller;
    private Rigidbody2D rb;
    public Animator anim;
    public float speed = 40f;
    float dirX = 0f;
    private  Vector3 localScale;
    //Transform camera;
    bool jump = false;
    public static bool GameIsFreeze = false;
    public bool isPause;
    static AudioSource asrc;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        asrc = GetComponent<AudioSource>();
        localScale = transform.localScale;
        //camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPause){
            AudioListener.pause = false;
            dirX = Input.GetAxisRaw("Horizontal") * speed;
            anim.SetFloat("Speed", Mathf.Abs(dirX));
            if(!asrc.isPlaying)
                asrc.Play();
            if(dirX == 0){
                asrc.Stop();
            }
            if(Input.GetButtonDown("Jump")){
                asrc.Stop();
                jump = true;
                anim.SetBool("isJumping", true);
            }
            // var cameraPos = transform.position;
            // cameraPos.x += 4f;
            // cameraPos.y += 1f;
            // cameraPos.z += -10f;
            // camera.position = Vector3.Lerp(
            //     camera.position,
            //     cameraPos,
            //     0.8f* speed *Time.deltaTime
            // );
        }else{
            AudioListener.pause = true;
        }
        if(HealthManager.healthAmmount <=0){
            gameOver();
            isPause = true;
            AudioListener.pause = false;
            asrc.Stop();
        }
    }
    public void startGame(){
        Time.timeScale = 1f;
        GameIsFreeze = false;
    }

    public void gameOver(){
        Time.timeScale = 0f;
        GameIsFreeze = true;
    }
    public void OnLanding(){
        anim.SetBool("isJumping", false);
    }
    private void FixedUpdate()
    {
        controller.Move(dirX * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
