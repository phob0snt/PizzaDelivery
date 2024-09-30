using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _FPSText;
    private float deltaTime = 0;

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        _FPSText.text = $"FPS: {(int)(1 / deltaTime)}";
    }
}
