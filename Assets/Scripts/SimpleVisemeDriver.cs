using UnityEngine;

public class SimpleVisemeDriver : MonoBehaviour
{
    [Header("Avatar Renderer")]
    public SkinnedMeshRenderer skinnedMesh;

    [Header("Your 8 Blendshape Indices")]
    public int v_open;
    public int v_explosive;
    public int v_dental_lip;
    public int v_tight_o;
    public int v_tight;
    public int v_wide;
    public int v_afficate;
    public int v_lip_opep;

    private OVRFaceExpressions face;

    void Start()
    {
       // face = OVRFaceExpressions.Instance;

        if (face == null)
            Debug.LogError("No OVRFaceExpressions instance found in scene!");

        // Make sure visemes are enabled
       // OVRPlugin.FaceTracking.EnableVisemes = true;
       // OVRPlugin.FaceTracking.EnableFace = true;
    }

    void Update()
    {
        if (face == null || !face.AreVisemesValid)
            return;

        // Short helper
        float V(OVRFaceExpressions.FaceViseme v) => face.GetViseme(v);

        // Grab the official Oculus visemes
        float v_PP = V(OVRFaceExpressions.FaceViseme.PP);   // p/b/m
        float v_FF = V(OVRFaceExpressions.FaceViseme.FF);   // f/v
        float v_TH = V(OVRFaceExpressions.FaceViseme.TH);
        float v_DD = V(OVRFaceExpressions.FaceViseme.DD);
        float v_CH = V(OVRFaceExpressions.FaceViseme.CH);
        float v_SS = V(OVRFaceExpressions.FaceViseme.SS);
        float v_NN = V(OVRFaceExpressions.FaceViseme.NN);
        float v_RR = V(OVRFaceExpressions.FaceViseme.RR);
        float v_AA = V(OVRFaceExpressions.FaceViseme.AA);   // open vowels
        float v_E = V(OVRFaceExpressions.FaceViseme.E);
        float v_IH = V(OVRFaceExpressions.FaceViseme.IH);
        float v_OH = V(OVRFaceExpressions.FaceViseme.OH);
        float v_OU = V(OVRFaceExpressions.FaceViseme.OU);

        //MAP OFFICIAL VISEMES to cc YOUR 8 SHAPES

        // 1. Open vowel -> v_open
        float out_open = Mathf.Max(v_AA, v_RR * 0.3f, v_IH * 0.2f);

        // 2. Explosive (p/b/m/t/d/k/g) -> v_explosive
        float out_explosive = Mathf.Max(v_PP, v_DD * 0.5f, v_CH * 0.4f);

        // 3. Dental / lip (f/v) -> v_dental_lip
        float out_dental = v_FF;

        // 4. Tight O (oh/oo) -> v_tight_o
        float out_tight_o = Mathf.Max(v_OH, v_OU);

        // 5. Tight (I/E vowel) -> v_tight
        float out_tight = Mathf.Max(v_E, v_IH);

        // 6. Wide (AE/EH/AY) -> v_wide
        float out_wide = Mathf.Max(v_E * 0.6f, v_AA * 0.2f);

        // 7. Affricate (ch/j/sh) -> v_afficate
        float out_afficate = Mathf.Max(v_CH, v_SS * 0.3f);

        // 8. Lip-open (neutral / L / R) -> v_lip_opep
        float out_lip = Mathf.Max(v_NN * 0.4f, v_RR, v_SS * 0.2f);

        // APPLY TO AVATAR BLENDSHAPES
        Apply(v_open, out_open);
        Apply(v_explosive, out_explosive);
        Apply(v_dental_lip, out_dental);
        Apply(v_tight_o, out_tight_o);
        Apply(v_tight, out_tight);
        Apply(v_wide, out_wide);
        Apply(v_afficate, out_afficate);
        Apply(v_lip_opep, out_lip);
    }

    void Apply(int index, float value)
    {
        if (index >= 0)
            skinnedMesh.SetBlendShapeWeight(index, value * 100f);
    }
}
