using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    private IEnemy _enemy;
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<IEnemy>();
        if (enemy is IEnemy)
        {
            _enemy = enemy;
            UiManager.instance.ShowPanelSelect();
            PlayerController.instance.StopRun();
        }
    }

    public void AttackEnemy()
    {
        if (_enemy == null) return;
        UiManager.instance.ClosePanelSelect();
        _enemy.TakeDamage();
        _enemy = null;
    }

    public void EnemyDisable()
    {
        if (_enemy == null || UiManager.instance.Coin < UiManager.instance.BuyoutPrice) return;
        _enemy.SelfDisable();
        _enemy = null;
    }
}
