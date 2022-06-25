using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    private InputController _inputActions;
    public float speed = 2;
    public int maxHealth = 100;
    public int currentHealth;
    Image healthBar;
    float barWidth, barHeight;
    float healthCurrentBar;
    void Awake(){
        _inputActions = new InputController();
    }
    void OnEnable(){
        _inputActions.Enable();
    }
    void OnDisable(){
        _inputActions.Disable();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
        currentHealth = maxHealth;
        barWidth =  healthBar.rectTransform.sizeDelta.x;
        barHeight =  healthBar.rectTransform.sizeDelta.y;

        healthCurrentBar = barWidth;
    }

    void Update()
    {
       //moveRightLeft();
       //moveUp();
       //moveDown();
       Move();
    }
    void Move(){

        Vector2 direction = new Vector2(_inputActions.InputAction.RightLeft.ReadValue<float>(),_inputActions.InputAction.Up.ReadValue<float>()-_inputActions.InputAction.Down.ReadValue<float>());
        if(!(direction==Vector2.zero))
        {
        animator.SetFloat("xDir", _inputActions.InputAction.RightLeft.ReadValue<float>());
        animator.SetFloat("yDir", _inputActions.InputAction.Up.ReadValue<float>()-_inputActions.InputAction.Down.ReadValue<float>());
        }
        animator.SetFloat("moveSpeed", direction.sqrMagnitude);
        
        float dash = _inputActions.InputAction.Dash.ReadValue<float>();
        if(dash > 0)
        {
            transform.Translate(Vector2.up * speed * 1.5f * Time.deltaTime * (_inputActions.InputAction.Up.ReadValue<float>()));
            transform.Translate(Vector2.down * speed * 1.5f * Time.deltaTime * (_inputActions.InputAction.Down.ReadValue<float>()));
            transform.Translate(Vector2.right * speed * 1.5f * Time.deltaTime * _inputActions.InputAction.RightLeft.ReadValue<float>());
        }
        else
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime * (_inputActions.InputAction.Up.ReadValue<float>()));
            transform.Translate(Vector2.down * speed * Time.deltaTime * (_inputActions.InputAction.Down.ReadValue<float>()));
            transform.Translate(Vector2.right * speed * Time.deltaTime * _inputActions.InputAction.RightLeft.ReadValue<float>());
        }
    }
    void moveRightLeft(){
        
        float moveRight = _inputActions.InputAction.RightLeft.ReadValue<float>();
        animator.SetFloat("PlayerRight", Mathf.Abs(moveRight));
        float dash = _inputActions.InputAction.Dash.ReadValue<float>();
        if(dash > 0){
            transform.Translate(Vector2.right * speed * 1.5f * Time.deltaTime * moveRight);
        }else{
            transform.Translate(Vector2.right * speed * Time.deltaTime * moveRight);
        }
        if(moveRight < 0){
            transform.localScale = new Vector2(-1,1);
        }else if(moveRight !=0){
            transform.localScale = new Vector2(1,1);
        }
    }

    void moveUp(){
        float moveUp = _inputActions.InputAction.Up.ReadValue<float>();
        animator.SetFloat("PlayerUp", Mathf.Abs(moveUp));
        float dash = _inputActions.InputAction.Dash.ReadValue<float>();
         if(dash > 0){
            transform.Translate(Vector2.up * speed * 1.5f * Time.deltaTime * moveUp);
        }else{
            transform.Translate(Vector2.up * speed * Time.deltaTime * moveUp);
        }
    }
    void moveDown(){
        float moveDown = _inputActions.InputAction.Down.ReadValue<float>();
        animator.SetFloat("PlayerDown", Mathf.Abs(moveDown));
        float dash = _inputActions.InputAction.Dash.ReadValue<float>();
         if(dash > 0){
            transform.Translate(Vector2.down * speed * 1.5f * Time.deltaTime * moveDown);
        }else{
            transform.Translate(Vector2.down * speed * Time.deltaTime * moveDown);
        }
    }
    public void takeDamage(int damage){
            currentHealth -= damage;
            //hurt aniation
            if(currentHealth <= 0){
                die();
                healthBar.rectTransform.sizeDelta = new Vector2(0, barHeight);
            }else{
                healthCurrentBar = (currentHealth * barWidth) / maxHealth;
                healthBar.rectTransform.sizeDelta = new Vector2(healthCurrentBar, barHeight);
            }
        }
    void die(){
            GetComponent<Collider2D>().enabled = false; 
            this.gameObject.SetActive(false);
                    }

}
