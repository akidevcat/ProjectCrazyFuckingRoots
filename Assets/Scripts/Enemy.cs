using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : DamageableEntity
{
    public event EventHandler OnAttack;
    public Animator Animator;
    private MotherTree motherTree;
    public float attackDamage;
    public float coolDownTime;
    private float timePasted;
    private bool ableToAttack;

    private Vector3 _lastPos;

    private EnemiesController _enemiesController;

    private void Awake()
    {
        _enemiesController = FindObjectOfType<EnemiesController>();
    }

    void Start()
    {
        motherTree = GameObject.Find("Mother Tree").GetComponent<MotherTree>();
        ableToAttack = false;
        timePasted = 0;

        _enemiesController.RegisterEnemy(this);
        OnBecomeDead += Die;
        OnStartDying += OnDying;
    }

    public void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Vector3.Distance(transform.position, _lastPos) > 0.001f)
        {
            Animator.SetBool("Run Forward", true);
        }
        else
        {
            Animator.SetBool("Run Forward", false);
        }
        _lastPos = transform.position;
        
        if (ableToAttack)
            return;

        timePasted += Time.deltaTime;

        if (timePasted >= coolDownTime)
            ableToAttack = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MotherTree") && ableToAttack)
        {
            Animator.SetTrigger("Stab Attack");
            motherTree.TakeDamage(attackDamage);
            OnAttack?.Invoke(this, EventArgs.Empty);
            ableToAttack = false;
            timePasted = 0;
        }
    }

    private void OnDying(object sender, EventArgs args)
    {
        GetComponent<NavMeshAgent>().enabled = false;
    }
    
    private void Die(object sender, EventArgs args)
    {
        _enemiesController.UnregisterEnemy(this);
        Destroy(gameObject);
    }

}
