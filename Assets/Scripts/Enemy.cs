using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event EventHandler OnAttack;
    private MotherTree motherTree;
    public float attackDamage;
    public float coolDownTime;
    private float timePasted;
    private bool ableToAttack;
    // Start is called before the first frame update
    void Start()
    {
        motherTree = GameObject.Find("Mother Tree").GetComponent<MotherTree>();
        ableToAttack = false;
        timePasted = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
            motherTree.TakeDamage(attackDamage);
            OnAttack?.Invoke(this, EventArgs.Empty);
            ableToAttack = false;
            timePasted = 0;
        }
    }

}
