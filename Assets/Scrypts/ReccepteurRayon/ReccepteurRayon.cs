using UnityEngine;

public class ReccepteurRayon : MonoBehaviour
{
    [SerializeField] ObjectToAlliment[] objectToAlliments;


    [SerializeField] public bool powered;
    [SerializeField] Material materialOFF;
    [SerializeField] Material materialON;

    private MeshRenderer renderer;
    RaycastHit hit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetPowered(bool isPowered)
    {
        powered = isPowered;
        if (powered)
        {
            renderer.material = materialON; // Change color to ON material when powered
            SoundLauncher.instance.PlayRecepteurPowerOn();
            for (int i = 0; i < objectToAlliments.Length; i++)
            {
                objectToAlliments[i].CheckAllimentation();
            }
        }
        else
        {
            renderer.material = materialOFF; // Change color to OFF material when not powered
            for (int i = 0; i < objectToAlliments.Length; i++)
            {
                objectToAlliments[i].CheckAllimentation();
            }
        }


    }

}
