using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit
{
    protected int posX, posY;
    protected int health;
    protected int maxHealth;
    protected int speed;
    protected int attack, attackRange;
    protected Faction factionType;
    protected bool isAttacking;

    public Unit(int x, int y, int hp, int sp, int att, int attRange, Faction faction, bool isAtt)
    {
        posX = x;
        posY = y;
        health = hp;
        speed = sp;
        attack = att;
        attackRange = attRange;
        factionType = faction;
        isAttacking = isAtt;

        maxHealth = hp;
    }

    public abstract void Move(int type);

    public abstract void Combat(int type);

    public abstract void CheckAttackRange(List<Unit> uni, List<Building> build);

    public abstract Unit ClosestEnemy();

    public abstract bool Death();
}
