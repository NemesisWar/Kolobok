using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using IJunior.TypedScenes;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _developers;

    public void LoadScene()
    {
        Game.Load();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Developers()
    {
        _developers.enabled = !_developers.enabled;
    }
}
