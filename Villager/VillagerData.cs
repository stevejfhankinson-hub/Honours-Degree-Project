// Steven Hankinson 21129647

using UnityEngine;

public class VillagerData : MonoBehaviour
{

    public static int[] villagerActions = new int[3];
    public static int PatrolAction = 0;
    public static int EnergyAction = 1;
    public static int WaitingAction = 2;

    public static int[] startBuildingNumber = new int[3];
    public static int[] targetBuildingNumber = new int[3];

    public static int[] homeNumber = new int[3];
    public static int[] building1Number = new int[3];
    public static int[] building2Number = new int[3];

    public static float[] villagerEnergy = new float[3];
    public static bool[] villagerParcel = new bool[3] { false, false, false};
    public static bool[] villagerCafe = new bool[3] { false, false, false };

    public static float maxFood = 20;
    public static Vector3[] house = new Vector3[10];
    public static Vector3[] cafe = new Vector3[2];

    public static bool[] directionNorth = new bool[3];
    public static bool[] directionEast = new bool[3];
    public static bool[] directionSouth = new bool[3];
    public static bool[] directionWest = new bool[3];

    public static bool[] waitEnergy = new bool[3];

    public static bool[] parcelCooldown = new bool[3];

    public static bool[] villagerReset = new bool[3];
    public static int[] villagerSortingOrder = new int[3];
    public static Vector3[] villagerPosition = new Vector3[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
