﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Modify Date：2013-08-27
/// Author：Ian
/// Description：
///     事件控制器(用來偵測觸發事件)
///     0808新增：魔王警告點
///     0822新增：魔王為樹人長老時，抵達終點觸發魔王的登場動畫
///     0827修改：BackgroundController 修正狀態的方式
///     0827新增：介面變化
/// </summary>
public class EventController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EventTriggerType>().Type == GameDefinition.EventTriggerType.出怪點)
        {
            //觸發出怪事件，做以下處理
            other.GetComponent<EnemyCreatePoint>().CreateEnemy();
        }
        else if (other.GetComponent<EventTriggerType>().Type == GameDefinition.EventTriggerType.魔王警告點)
        {
            //魔王警告點，出現提示玩家警訊
            Instantiate(EffectCreator.script.魔王接近提示);
            EffectCreator.script.isBossUIEffectShow = true; //呼叫魔王血量介面
        }
        else if (other.GetComponent<EventTriggerType>().Type == GameDefinition.EventTriggerType.有魔王終點)
        {
            //抵達終點，做以下處理(停止背景移動、腳色切換Idle狀態)
            BackgroundController.script.SetRunBackgroundState(false);

            //因抵達終點有魔王，進入BOSS戰
            if (BossController_TreeElder.script)
            {
                GameManager.script.CurrentBossObject = BossController_TreeElder.script.gameObject; //紀錄當前魔王物件(使用絕技要暫停iTween)
                BossController_TreeElder.script.boneAnimation.Play("登場");
                BossController_TreeElder.script.currentBossAction = BossController_TreeElder.BossAction.閒置;
            }
        }
        else if (other.GetComponent<EventTriggerType>().Type == GameDefinition.EventTriggerType.無魔王終點)
        {
            //抵達終點，做以下處理(停止背景移動、腳色切換Idle狀態)
            BackgroundController.script.SetRunBackgroundState(false);

            //因抵達終點後無魔王，直接過關
            GameManager.script.RunLevelComplete();
        }
    }
}