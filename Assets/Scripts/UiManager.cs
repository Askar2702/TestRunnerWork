using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    public static UiManager instance { get; private set; }
    public event Action StartPlay;

    #region Timer
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private float _timer;
    #endregion

    #region Road Length
    [SerializeField] private Slider _roadLength;
    [SerializeField] private float _posStart;
    [SerializeField] private float _posFinish;
    #endregion

    #region Coins
    [SerializeField] private TextMeshProUGUI _coinsText;
    public int Coin { get; private set; }
    public int BuyoutPrice { get; private set; }
    #endregion

    #region Selection Bar
    [SerializeField] private RectTransform _panelSelect;
    [SerializeField] private Button _pay;
    #endregion

    [SerializeField] private PlayerController _player;
    private void Awake()
    {
        if (!instance) instance = this;
        _pay.onClick.AddListener(() => PayOff());
    }
    private void Start()
    {
        StartCoroutine(StartGame());
        BuyoutPrice = 5;
    }
    private IEnumerator StartGame()
    {
        while (_timer > 0)
        {
            _timer -= Time.deltaTime;
            _timerText.text = _timer.ToString("f0");
            if (_timer <= 0) 
            {
                StartPlay?.Invoke();
                _timerText.enabled = false;
            }
            
            yield return new WaitForSeconds(0.001f);
        }

        _roadLength.minValue = _posStart;
        _roadLength.maxValue = _posFinish;

        while (_roadLength.value != 170)
        {
            _roadLength.value = _player.GetPosPlayer().z;
            yield return new WaitForSeconds(0.001f);
        }
    }

    public void AddCoin(int value)
    {
        Coin += value;
        _coinsText.text = Coin.ToString();
    }

    public void ShowPanelSelect()
    {
        _panelSelect.DOScale(new Vector3(1f, 1f , 1f), 1.5f).SetEase(Ease.OutBounce);
    }

    public void ClosePanelSelect()
    {
        _panelSelect.DOScale(new Vector2(0f, 0f), 1.5f);
        StartPlay?.Invoke();
    }
    private void PayOff()
    {
        if (Coin < BuyoutPrice) return;
        AddCoin(-BuyoutPrice);
        ClosePanelSelect();
    }
}
