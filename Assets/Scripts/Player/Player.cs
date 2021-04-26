using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityAction<int> ChangeCoinCount;
    private int _coins;

    public void ChangeCoins()
    {
        _coins++;
        ChangeCoinCount?.Invoke(_coins);
    }
}
