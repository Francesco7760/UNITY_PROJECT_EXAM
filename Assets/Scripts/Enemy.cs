using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform playerTrasform;
    private Rigidbody2D rigidB;
    private Vector2 enemyDirection;
    private Animator animator;
    public float enemySight = 3f;
    public float enemySpeed = 1f;
    public int health;
    private bool isRunning=false;
    private bool canTurn=true;

    public float side =1;

    private void Awake() {
        rigidB = this.GetComponent<Rigidbody2D>();

        animator = this.GetComponent<Animator>();
    }
    void Start()
    {
        playerTrasform=GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTrasform != null){
            Vector2 direction = new Vector2(playerTrasform.position.x-transform.position.x, playerTrasform.position.y - transform.position.y);
            direction.Normalize();
            enemyDirection=direction;
            if (Vector2.Distance(playerTrasform.position,transform.position)<enemySight && (Mathf.Sign(playerTrasform.position.x-transform.position.x)==side)){
                turnEnemy();
            }
            
        }
    }
    private void FixedUpdate() {
        moveEnemy(enemyDirection);
    }
    void moveEnemy(Vector2 direction){

        if (Vector2.Distance(playerTrasform.position,transform.position)<enemySight && (Mathf.Sign(playerTrasform.position.x-transform.position.x)==side)){
            rigidB.MovePosition((Vector2)transform.position+(direction*enemySpeed*Time.deltaTime)+new Vector2(0, Mathf.Sin(Time.time*5)*0.003f));
            turnEnemy();
            //yTranslate= Mathf.Sin(Time.time);
            //transform.position = transform.position + new Vector2(0, Mathf.Sin(Time.time)*0.01f);
            //rigidB.MovePosition((Vector2)transform.position+(direction*enemySpeed*Time.deltaTime));
            
        }
        else{
            rigidB.MovePosition((Vector2)transform.position+new Vector2(0, Mathf.Sin(Time.time*5)*0.003f)+new Vector2(Mathf.Sin(Time.time)*0.02f, 0));
            if(Mathf.Sin(Time.time) * -side > 0){
                transform.localScale = new Vector2(-side*2,2);
                side=-side;
            }

        }

    }
    void turnEnemy(){
        if(playerTrasform.position.x-transform.position.x<0){
            transform.localScale = new Vector2(-2,2);
            side=-1;
        }
        else {
            transform.localScale = new Vector2(2,2);
            side=1;
        }
    }
    public void TakeDamage(int damage){
        if(!(Mathf.Sign(playerTrasform.position.x-transform.position.x)==side)){
            health -= damage*10;
            turnEnemy();
        }
        else{
            health -= damage;
        }
        health -= damage;
        
        Debug.Log("damageTaken!!!!!!!!!!");
    }
}
