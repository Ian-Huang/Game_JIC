﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Modify Date：2013-08-16
/// Author：Ian
/// Description：
///     角色的屬性資訊
///     0816：修改回血速率公式
/// </summary>
public class RolePropertyInfo : MonoBehaviour
{
    public GameDefinition.Role Role;    //角色名稱
    public float currentLife;           //當前生命值
    public float maxLife;               //最大生命值    
    public int damage;                  //攻擊傷害值
    public int defence;                 //防禦力

    public float cureRate;              //每秒回復生命速率    

    public bool isWeak { get; private set; }

    private SmoothMoves.BoneAnimation boneAnimation;

    // Use this for initialization
    void Start()
    {
        this.isWeak = false;

        //設定BoneAnimation
        this.boneAnimation = this.GetComponent<SmoothMoves.BoneAnimation>();

        //讀取系統儲存的角色屬性資料
        GameDefinition.RoleData getData = GameDefinition.RoleList.Find((GameDefinition.RoleData data) => { return data.RoleName == Role; });
        this.maxLife = getData.Life;
        this.currentLife = getData.Life;
        this.damage = getData.Damage;
        this.defence = getData.Defence;

        this.cureRate = this.maxLife / 300;

        InvokeRepeating("RestoreLifePersecond", 0.1f, 1);
    }

    /// <summary>
    /// 減少角色血量函式
    /// </summary>
    /// <param name="deLife">減少的數值</param>
    public void DecreaseLife(int deLife)
    {
        //扣除防禦力
        deLife -= this.defence;
        if (deLife <= 0)
            deLife = 1;

        if (!this.isWeak)
        {
            //角色當前未虛弱，扣角色的生命值
            this.currentLife -= deLife;
        }
        else
        {
            //角色當前虛弱，扣總士氣(未完成)
            GameManager.script.CurrentMorale -= deLife;
            if (GameManager.script.CurrentMorale <= 0)
                GameManager.script.CurrentMorale = 0;
        }

        if (!this.isWeak)
        {
            //當生命小於0
            if (this.currentLife <= 0)
            {
                this.currentLife = 0;
                this.isWeak = true;
                this.cureRate = this.maxLife / 15;
                //判斷當前背景移動狀況，如果無移動則使用"idleweak"
                if (BackgroundController.script.isRunning)
                    this.boneAnimation.Play("walkweak");
                else
                    this.boneAnimation.Play("idleweak");
            }
        }
    }

    /// <summary>
    /// 每秒固定回復生命
    /// </summary>
    void RestoreLifePersecond()
    {
        //未虛弱，回復速率正常
        if (!this.isWeak)
        {
            this.currentLife += this.cureRate;     //正常狀態：每秒回最大生命值的1/300
            if (this.currentLife >= this.maxLife)
                this.currentLife = this.maxLife;
        }
        else
        {
            this.currentLife += this.cureRate;      //虛弱狀態：每秒回最大生命值的1/15
            if (this.currentLife >= this.maxLife)
            {
                this.currentLife = this.maxLife;

                this.isWeak = false;
                this.cureRate = this.maxLife / 300;
            }
        }
    }
}
