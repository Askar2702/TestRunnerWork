using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Change Level
    [SerializeField] private Color[] _colors;
    [SerializeField] private MeshRenderer _roadMat;
    #endregion

    [SerializeField] private float _finishPosZ;
    #region UI
    [SerializeField] private RectTransform _panelFinish;
    [SerializeField] private Button _restartGame;
    [SerializeField] private Button _exitGame;
    #endregion

    private void Awake()
    {
        _restartGame.onClick.AddListener(() => RestartGame());
        _exitGame.onClick.AddListener(() => ExitGame());
    }

    private void Start()
    {
        StartCoroutine(CheckPlayerPos());
        _roadMat.material.color = _colors[Random.Range(0, _colors.Length)];
    }
    private void ShowPanelFinish()
    {
        _panelFinish.DOAnchorPos(Vector2.zero, 2f).SetEase(Ease.OutBounce);
        PlayerController.instance.StopRun();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator CheckPlayerPos()
    {
        yield return new WaitUntil(() => PlayerController.instance.GetPosPlayer().z >= _finishPosZ);
        ShowPanelFinish();
    }

   
}
