using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    [SerializeField] private Player _player;
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable()
    {
        _player.CoinCollected += OnCoinCollected;
    }

    private void OnDisable()
    {
        _player.CoinCollected -= OnCoinCollected;
    }

    private void OnCoinCollected(int score)
    {
        _text.text = score.ToString();
    }
}
