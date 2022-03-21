using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    private TextMeshProUGUI textBox;

    private void Awake()
    {
        textBox = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Player.OnHealthChanged.AddListener(UpdateText);
    }

    private void UpdateText(int health, int maxHealth)
    {
        textBox.text = $"Health: {health}/{maxHealth}";
    }
}
