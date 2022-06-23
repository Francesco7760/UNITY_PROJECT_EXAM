using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombact : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public LayerMask enemyLayers;
    private InputController _inputController;
    void Awake(){
        _inputController = new InputController();
    }
    void Start(){
    }
    void OnEnable(){
        _inputController.Enable();
    }
    void OnDisable(){
        _inputController.Disable();
    }
    void Update()
    {
        _inputController.InputAction.Attack.performed += ctx => attack();
    }
    void attack(){
        float attackInput = _inputController.InputAction.Attack.ReadValue<float>();
        //attack animations
        animator.SetFloat("Attack", attackInput);
        // detect enemies to attack
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemy){
            Debug.Log("attaccato" + enemy.name);
            enemy.GetComponent<EnemyManager>().takeDamage(attackDamage);
        }
        //damage enemy
    }
}
