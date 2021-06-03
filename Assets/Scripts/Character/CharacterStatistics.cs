using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatistics
{
    private string name = "Empty";
    private int hp = 100;
    private float speed = 8.0f;
    private int damage = 10;
    private float attackDistance = 2.0f;
    private float attackSpeed = 1.0f;
    private float deathSpeed = 1.0f;
    public CharacterStatistics(string name, int hp, float speed, int damage, float attackDistance)
    {
        this.name = name;
        this.hp = hp;
        this.speed = speed;
        this.damage = damage;
        this.attackDistance = attackDistance;
    }

    public int Hp 
    { 
        get => hp;
        set
        {
            hp = value;
        }
    }

    public float Speed { get => speed; set => speed = value; }
    public int Damage { get => damage; set => damage = value; }
    public float AttackDistance { get => attackDistance; set => attackDistance = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float DeathSpeed { get => deathSpeed; set => deathSpeed = value; }
    public string Name { get => name; }
}
