using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapDesign;

public class MapCellInfo
{
    public MapCellType mapCellType;

    public MapCellInfo(MapCellType mapCellType)
    {
        this.mapCellType = mapCellType;
    }
}
