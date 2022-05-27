using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // this = 내 자신
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateAmmoTEXT(int magAmmo, int ammoRemain)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
