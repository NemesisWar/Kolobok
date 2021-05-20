using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private int _coins;

    public event UnityAction<int> CoinCollected;

    public void AddCoins()
    {
        _coins++;
        CoinCollected?.Invoke(_coins);
    }
}
