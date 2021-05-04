using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public event UnityAction<int> ChangeCoinCount;
    private int _coins;

    public void AddCoins()
    {
        _coins++;
        ChangeCoinCount?.Invoke(_coins);
    }
}
