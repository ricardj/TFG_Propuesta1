using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDesign : MonoBehaviour
{

    public enum MapCellType { Plain, Woods, Mountain, River, Outpost };
    public static Dictionary<string, MapCellType> MapCellTypeTraductor = new Dictionary<string, MapCellType>()
    {
        { "Plain", MapCellType.Plain},
        { "Mountain", MapCellType.Mountain},
        { "Woods", MapCellType.Woods},
        { "River", MapCellType.River},
        { "Outpost", MapCellType.Outpost}
    };

    public static Dictionary<MapCellType, AudioClip> MapCellTypeSounds;
    public AudioClip plain;
    public AudioClip woods;
    public AudioClip outpost;
    public AudioClip river;
    public AudioClip mountain;

    // Start is called before the first frame update
    void Awake()
    {
        MapCellTypeSounds = new Dictionary<MapCellType, AudioClip>();
        MapCellTypeSounds.Add(MapCellType.Plain, plain);
        MapCellTypeSounds.Add(MapCellType.Woods, woods);
        MapCellTypeSounds.Add(MapCellType.Mountain, mountain);
        MapCellTypeSounds.Add(MapCellType.River, river);
        MapCellTypeSounds.Add(MapCellType.Outpost, outpost);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
