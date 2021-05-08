using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private int _coins;

    public event UnityAction<int> ChangeCoinCount;

    public void AddCoins()
    {
        _coins++;
        ChangeCoinCount?.Invoke(_coins);
    }
}
