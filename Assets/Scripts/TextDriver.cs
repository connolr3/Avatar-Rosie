using TMPro;
using UnityEngine;

public class TextDriver : MonoBehaviour
{
    public SkinnedMeshRenderer rend;
    public string blendshapeName;
    private int blendshapeIndex;
    private float value;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        // Get the index of the blendshape by name
        blendshapeIndex = rend.sharedMesh.GetBlendShapeIndex(blendshapeName);
        if (blendshapeIndex == -1)
        {
            Debug.LogError($"BlendShape '{blendshapeName}' not found!");
            return;
        }

        value = rend.GetBlendShapeWeight(blendshapeIndex);
        text.text = blendshapeName + " " + value.ToString("F2");
    }

    void Update()
    {
        if (blendshapeIndex == -1) return;

        value = rend.GetBlendShapeWeight(blendshapeIndex);
        text.text = blendshapeName + " " + value.ToString("F2");
    }
}
