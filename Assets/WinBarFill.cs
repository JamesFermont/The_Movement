using UnityEngine;
using UnityEngine.UI;

public class WinBarFill : MonoBehaviour {
    public Image image;

    private void OnEnable() {
        InfluenceHandler.EmitWinPercent += SetFill;
        image.fillAmount = 0f;
    }

    private void SetFill(float fill) {
        image.fillAmount = fill;
    }

    private void OnDisable() {
        InfluenceHandler.EmitWinPercent -= SetFill;
    }
}
