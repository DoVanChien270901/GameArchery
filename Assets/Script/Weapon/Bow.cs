using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [System.Serializable]
    public class BowSetting
    {
        [Header("Arrow Setting")]
        public float arrowCownt; //soluongten
        public Rigidbody arrowPrefab; //object arrow
        public Transform arrowPos; // vitriten
        public Transform arrowEquiParent;//vitricha
        public float arrowForce = 3;



        [Header("Bow String Setting")]
        public Transform bowString;//daycung
        public Transform stringInitialPos;//vitribandau
        public Transform stringHanPullPos;//Taykeo
        public Transform stringInitialParent;//vitricha

    }
    [SerializeField]
    public BowSetting bowSetting; 

    [Header("crosshair setting")]
    public GameObject crosshairPrefabs;
    GameObject currentCrossHair;

    Rigidbody currentArrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCrossHair(Vector3 crossHair)
    {
        if (!currentCrossHair)
        {
            currentCrossHair = Instantiate(crosshairPrefabs) as GameObject;
        }
        currentCrossHair.transform.position = crossHair;
        currentCrossHair.transform.LookAt(Camera.main.transform);
    }

    public void Remove()
    {
        if (currentCrossHair)
        {
            Destroy(currentCrossHair);
        }
    }

    public void PickArrow()
    {
        bowSetting.arrowPos.gameObject.SetActive(true);
    }
    public void DisableArrow()
    {
        bowSetting.arrowPos.gameObject.SetActive(false);
    }

    public void PullString()
    {
        bowSetting.bowString.transform.position = bowSetting.stringHanPullPos.position;
        bowSetting.bowString.transform.parent = bowSetting.stringHanPullPos;
    }

    public void ReleaseString()
    {
        bowSetting.bowString.transform.position = bowSetting.stringInitialPos.position;
        bowSetting.bowString.transform.parent = bowSetting.stringInitialParent;

    }
    
    public void Fire(Vector3 hitPoin)
    {
        Vector3 dir = hitPoin - bowSetting.arrowPos.position;
        currentArrow = Instantiate(bowSetting.arrowPrefab, bowSetting.arrowPrefab.position, bowSetting.arrowPos.rotation) as Rigidbody;

        currentArrow.AddForce(dir * bowSetting.arrowForce, ForceMode.VelocityChange);
    }
}
