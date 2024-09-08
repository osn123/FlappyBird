using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerater : MonoBehaviour
{
    #region Pipe
    [Header("土管 上")]
    [SerializeField] GameObject pipeTop;

    [Header("土管 下")]
    [SerializeField] GameObject pipeBottom;

    [Header("土管 上角度")]
    [SerializeField] float pipeAngle = 180f;

    [Header("隙間")]
    [SerializeField] float pipeGap = 5f;

    [Header("土管下限 上")]
    [SerializeField] float topLowerLimit = 1f;

    #endregion

    #region PipeGenerate
    [Header("初回生成間隔")]
    [SerializeField] float genFirstTime = 1f;

    [Header("生成間隔")]
    [SerializeField] float genNextTime = 1f;

    #endregion

    #region ScoreCollider
    [Header("スコアコライダー")]
    [SerializeField] GameObject scoreCollider;

    #endregion

    #region Item
    [Header("回復アイテム")]
    [SerializeField] GameObject healItem;
    [Header("上限値")]
    [SerializeField] float genItemUpperLimit = 1f;
    [Header("下限値")]
    [SerializeField] float genItemLowerLimit = 1f;
    [Header("出現位置")]
    [SerializeField] float genItemHorizontalGap = 1f;
    #endregion

    #region Internal
    Vector2 screenMin, screenMax;
    #endregion
    void Start()
    {
        GetScreenSize();
        InvokeRepeating(nameof(ObjectGenerate), genFirstTime, genNextTime);
    }
    void Update()
    {

    }
    void GetScreenSize()
    {
        screenMin = Camera.main.ViewportToWorldPoint(Vector2.zero);
        screenMax = Camera.main.ViewportToWorldPoint(Vector2.one);
    }
    void ObjectGenerate()
    {
        float _generatePoint = Random.Range(screenMax.y, topLowerLimit);
        float _generateItemPoint = Random.Range(screenMin.y + genItemLowerLimit,
            screenMax.y - genItemUpperLimit);

        Instantiate(pipeTop,
            new Vector2(transform.position.x, _generatePoint),
            Quaternion.Euler(Variables.zero, Variables.zero, pipeAngle));

        Instantiate(pipeBottom,
            new Vector2(transform.position.x, _generatePoint - pipeGap),
            Quaternion.identity);

        Instantiate(scoreCollider,
            new Vector2(transform.position.x, transform.position.y),
            Quaternion.identity);

        Instantiate(healItem,
    new Vector2(transform.position.x + genItemHorizontalGap, _generateItemPoint),
            Quaternion.identity);
    }
}
