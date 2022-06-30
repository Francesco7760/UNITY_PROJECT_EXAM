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
    public int money;
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
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Exit")     //se incontro l'EXIT, allora cambia livello (con un delay di 1 secondo)
        {
           Debug.Log("exit trigger");
           //SceneManager.LoadScene("SampleScene2");
        }
        else if (other.tag == "Food")    //se incontro il FOOD, aumenta la vita di 10
        {
            Debug.Log("food trigger");
            if(currentHealth + 10 < maxHealth){
                other.gameObject.SetActive(false);
                ScoreScript.instance.addPointFood();
                currentHealth += 10;
                SoundManagerScriptOther.PlaySound("clic");
            }
        }
        else if (other.tag == "Soda")    
        {
            Debug.Log("soda trigger");
            other.gameObject.SetActive(false);
            ScoreScript.instance.addPointSoda();
            SoundManagerScriptOther.PlaySound("clic");
            // update attack score
        }
        else if(other.tag == "Life"){ //se incontro la Life, aumenta la vita di 20
            if(currentHealth + 20 < maxHealth){
                other.gameObject.SetActive(false);
                currentHealth += 20;
                SoundManagerScriptOther.PlaySound("collecting");
            }
        }
        else if(other.tag == "Money"){
            money += 1;
            other.gameObject.SetActive(false);
            SoundManagerScriptOther.PlaySound("clic");
        }
    }                  

}
