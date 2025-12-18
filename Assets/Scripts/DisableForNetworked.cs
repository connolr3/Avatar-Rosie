using UnityEngine;

public class DisableForNetworked : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*Netcode works fine for multiplayer in VR, you just need to take care to disable some features of the player when it is Spawned in the scene, if it is not your player you should disable components like Canera, XRDirectInteractor, XRRayInteractor and so on.
in my case I created a script that tests if I am the owner of the player, if not I disable the following components.*/
    // https://discussions.unity.com/t/best-options-for-multiplayer-vr/911230/6

    public GameObject Cam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
