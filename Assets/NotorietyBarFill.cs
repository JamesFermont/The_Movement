using UnityEngine;
using UnityEngine.UI;

public class NotorietyBarFill : MonoBehaviour
{
    public Image image;

    private void OnEnable() {
        InfluenceHandler.EmitNotorietyPercent += SetFill;
    }

    private void SetFill(float fill) {
        image.fillAmount = fill;
    }

    private void OnDisable() {
        InfluenceHandler.EmitNotorietyPercent -= SetFill;
    }
}
