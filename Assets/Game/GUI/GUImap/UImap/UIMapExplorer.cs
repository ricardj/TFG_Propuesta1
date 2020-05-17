using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapDesign;

public class UIMapExplorer : MonoBehaviour
{

    public delegate void UICellMapChanged();
    UICellMapChanged uICellMapChanged;

    MapManager mapManager;
    UIMapManager uiMapManager;
    PlayerManager playerManager;
    int indexX = 0;
    int indexY = 0;

    public static Dictionary<MapCellType, AudioClip> NavigationSounds;
    public AudioClip plain;
    public AudioClip woods;
    public AudioClip outpost;
    public AudioClip river;
    public AudioClip mountain;


    AudioSource wrongTry;
    AudioSource playerHere;

    // Start is called before the first frame update
    void Awake()
    {
        //WE get the managers we are going to need
        

        NavigationSounds = new Dictionary<MapCellType, AudioClip>();
        NavigationSounds.Add(MapCellType.Plain, plain);
        NavigationSounds.Add(MapCellType.Woods, woods);
        NavigationSounds.Add(MapCellType.Mountain, mountain);
        NavigationSounds.Add(MapCellType.River, river);
        NavigationSounds.Add(MapCellType.Outpost, outpost);
    }

    Vector2[] farBoundingLimits;
    Dictionary<Vector2, Vector2> farSections;
    int farSectionsLength = 16;

    // Start is called before the first frame update
    void Start()
    {
        mapManager = PGameManager.Instance.mapManager.GetComponent<MapManager>();
        uiMapManager = PGameManager.Instance.uiMapManager.GetComponent<UIMapManager>();
        playerManager = PGameManager.Instance.playerManager.GetComponent<PlayerManager>();

        //We get the exploration audios and distribute them
        AudioSource[] mapExplorationAudios = GetComponents<AudioSource>();
        wrongTry = mapExplorationAudios[0];
        playerHere = mapExplorationAudios[1];

        //We subscrive a method to an event.
        uICellMapChanged += CheckIsPlayerThere;

        //We generate the assignations for the ui exploring through angle
        farSections = new Dictionary<Vector2, Vector2>();
        
        float initOffset = (360f / 16f) / 2f;
        farBoundingLimits = new Vector2[farSectionsLength];
        farBoundingLimits[0].x = 360f - initOffset;
        farBoundingLimits[0].y = initOffset;



        //Debug.Log(farBoundingLimits[0].x + " to " + farBoundingLimits[0].y);
        
        for (int i = 1; i < farSectionsLength; i++)
        {
            farBoundingLimits[i].x = farBoundingLimits[i - 1].y;
            farBoundingLimits[i].y = farBoundingLimits[i].x + initOffset * 2.0f;
            //Debug.Log(farBoundingLimits[i].x + " to " + farBoundingLimits[i].y);
        }
        farSections[farBoundingLimits[0]]  = new Vector2(4, 2);
        farSections[farBoundingLimits[1]]  = new Vector2(4, 3);
        farSections[farBoundingLimits[2]]  = new Vector2(4, 4);
        farSections[farBoundingLimits[3]]  = new Vector2(3, 4);
        farSections[farBoundingLimits[4]]  = new Vector2(2, 4);
        farSections[farBoundingLimits[5]]  = new Vector2(1, 4);
        farSections[farBoundingLimits[6]]  = new Vector2(0, 4);
        farSections[farBoundingLimits[7]]  = new Vector2(0, 3);
        farSections[farBoundingLimits[8]]  = new Vector2(0, 2);
        farSections[farBoundingLimits[9]]  = new Vector2(0, 1);
        farSections[farBoundingLimits[10]] = new Vector2(0, 0);
        farSections[farBoundingLimits[11]] = new Vector2(1, 0);
        farSections[farBoundingLimits[12]] = new Vector2(2, 0);
        farSections[farBoundingLimits[13]] = new Vector2(3, 0);
        farSections[farBoundingLimits[14]] = new Vector2(4, 0);
        farSections[farBoundingLimits[15]] = new Vector2(4, 1);



    }



    public bool focusSet = false;
    public enum SightLevel {Adjacent,Near, Far };
    public SightLevel currentSightLevel = SightLevel.Adjacent;
    SightLevel previousSightLevel = SightLevel.Adjacent;
    // Update is called once per frame
    void Update()
    {

        //While the map explorer is active we the player sight
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentSightLevel = SightLevel.Adjacent;

        if (Input.GetKeyDown(KeyCode.Alpha2)) currentSightLevel = SightLevel.Near;
        //if (Input.GetKeyUp(KeyCode.Alpha1)) currentSightLevel = SightLevel.Adjacent;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentSightLevel = SightLevel.Far;
        //if (Input.GetKeyUp(KeyCode.Alpha2)) currentSightLevel = SightLevel.Adjacent;
        

        //We set the focused uiMapCell
        checkFocusedUiMapCell();

    }

    public void checkFocusedUiMapCell()
    {
        int newXCell = 0;
        int newYCell = 0;
        float playerDirectionAngle = 0;
        float initOffset = 0;
        //first we check the sight level
        switch (currentSightLevel)
        {
            case SightLevel.Adjacent:
                newXCell = 2;
                newYCell = 2;
                break;
            case SightLevel.Near:

                //We check 8 posible diferent angles
                playerDirectionAngle = playerManager.getPlayerDirectionAngle();
                initOffset = (360 / 8) / 2;

                if (playerDirectionAngle >= initOffset && playerDirectionAngle <= 180 - initOffset)
                {
                    newYCell = 3;
                    if (playerDirectionAngle >= 90 + initOffset) newXCell = 1;
                    if (playerDirectionAngle <= 90 + initOffset && playerDirectionAngle >= 90 - initOffset) newXCell = 2;
                    if (playerDirectionAngle <= 90 - initOffset) newXCell = 3;
                }
                if(playerDirectionAngle > 180 - initOffset && playerDirectionAngle <= 180 +initOffset 
                    || playerDirectionAngle <= initOffset  
                    || playerDirectionAngle >= 360 - initOffset)
                {
                    newYCell = 2;
                    if (playerDirectionAngle >= 180 -initOffset && playerDirectionAngle <= 180 + initOffset) newXCell = 1;
                    if (playerDirectionAngle <= initOffset || playerDirectionAngle >= 360 - initOffset) newXCell = 3;
                }
                if (playerDirectionAngle >= 180 + initOffset && playerDirectionAngle <= 360 - initOffset)
                {
                    newYCell = 1;
                    if (playerDirectionAngle <= 270 - initOffset) newXCell = 1;
                    if (playerDirectionAngle >= 270 - initOffset && playerDirectionAngle <= 270 + initOffset) newXCell = 2;
                    if (playerDirectionAngle >= 270 + initOffset) newXCell = 3;
                }
                break;
            case SightLevel.Far:
                playerDirectionAngle = playerManager.getPlayerDirectionAngle();
                
                for (int i = 0; i < farSectionsLength; i++)
                {
                    if(i != 0)
                    {
                        if(playerDirectionAngle > farBoundingLimits[i].x && playerDirectionAngle <= farBoundingLimits[i].y)
                        {
                            newXCell = (int)farSections[farBoundingLimits[i]].x;
                            newYCell = (int)farSections[farBoundingLimits[i]].y;
                            break;
                        }
                    }
                    else
                    {
                        if (playerDirectionAngle >= 0 && playerDirectionAngle < farBoundingLimits[0].y || playerDirectionAngle > farBoundingLimits[0].x && playerDirectionAngle <= 360)
                        {
                            newXCell = (int)farSections[farBoundingLimits[0]].x;
                            newYCell = (int)farSections[farBoundingLimits[0]].y;
                            break;
                        }
                    }
                }
                
                break;
        }

        if (newXCell != indexX || newYCell != indexY
        || currentSightLevel != previousSightLevel) 
        {
            setNewFocusedCell(newXCell, newYCell);
            previousSightLevel  = currentSightLevel;
        }
        //Debug.Log(playerDirectionAngle);
        
    }

    public void CheckIsPlayerThere()
    {
        Vector2 playerPosition = mapManager.whereIsPlayer();
        if (playerPosition.Equals(new Vector2(indexX, indexY)))
        {
            playerHere.Play();
        }
    }


    public void deactivateUIMap()
    {
        uiMapManager.uiMapMatrix[indexX][indexY].GetComponent<UIMapCell>().setFocus(false);
        focusSet = false;
    }
       
    public void activateUIMap()
    {
        //TODO: we check where the player is
        MapCellInfo[][] playerSurroundingsMatrix = mapManager.getPlayerSurroundingsMatrix(5, 5);

        //Some debugging.
        //Debug.Log("UI Map updated:");
        //for (int i = 0; i < 5; i++)
        //{
        //    string newLine = "";
        //    for (int j = 0; j < 5; j++)
        //    {
        //        newLine += " " + playerSurroundingsMatrix[i][j].mapCellType.ToString();
        //    }
        //    Debug.Log(newLine);
        //}

        //We Build the map around the player
        uiMapManager.refreshUIMap(playerSurroundingsMatrix);

        //We set the focus on the ui map cell that has to be taken
        //by default the center one
        setNewFocusedCell(2, 2);
    }


    void setNewFocusedCell(int newPosX, int newPosY)
    {
        uiMapManager.uiMapMatrix[indexX][indexY].GetComponent<UIMapCell>().setFocus(false);
        indexX = newPosX;
        indexY = newPosY;
        uiMapManager.uiMapMatrix[indexX][indexY].GetComponent<UIMapCell>().setFocus(true);
        uICellMapChanged();
    }
}

/*
 * OLD CODE
 * 
 * 
        if (Input.GetKeyDown(KeyCode.S))
        {
            
            if (indexX + 1 < uiMapGenerator.mapSizeX)
            {
                uiMapGenerator.uiMapMatrix[indexX][indexY].GetComponent<UIMapCell>().setFocus(false);
                indexX++;
                uiMapGenerator.uiMapMatrix[indexX][indexY].GetComponent<UIMapCell>().setFocus(true);
                uICellMapChanged();
            }
            else
            {
                wrongTry.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            
            if (indexY - 1 > -1)
            {
                uiMapGenerator.uiMapMatrix[indexX][indexY].GetComponent<UIMapCell>().setFocus(false);
                indexY--;
                uiMapGenerator.uiMapMatrix[indexX][indexY].GetComponent<UIMapCell>().setFocus(true);
                uICellMapChanged();
            }
            else
            {
                wrongTry.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            
            if (indexX - 1 > -1)
            {
                uiMapGenerator.uiMapMatrix[indexX][indexY].GetComponent<UIMapCell>().setFocus(false);
                indexX--;
                uiMapGenerator.uiMapMatrix[indexX][indexY].GetComponent<UIMapCell>().setFocus(true);
                uICellMapChanged();
            }
            else
            {
                wrongTry.Play();
            }

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            
            if(indexY + 1 < uiMapGenerator.mapSizeY)
            {
                uiMapGenerator.uiMapMatrix[indexX][indexY].GetComponent<UIMapCell>().setFocus(false);
                indexY++;
                uiMapGenerator.uiMapMatrix[indexX][indexY].GetComponent<UIMapCell>().setFocus(true);
                uICellMapChanged();
            }
            else
            {
                wrongTry.Play();
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            uiMapGenerator.uiMapMatrix[indexX][indexY].GetComponent<UIMapCell>().playInfo();
            uICellMapChanged();
        }

        if((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.Space))
        {
            
            uiMapGenerator.uiMapMatrix[indexX][indexY].GetComponent<UIMapCell>().setFocus(false);
            Vector2 playerPosition = uiMapGenerator.mapManager.GetComponent<MapOperations>().whereIsPlayer();
            indexY = (int)playerPosition.y;
            indexX = (int)playerPosition.x;
            uiMapGenerator.uiMapMatrix[indexX][indexY].GetComponent<UIMapCell>().setFocus(true);
            uICellMapChanged();
        }

        if (Input.anyKey && !focusSet)
        {
            focusSet = true;
            Vector2 playerPosition = uiMapGenerator.mapManager.GetComponent<MapOperations>().whereIsPlayer();
            indexY = (int)playerPosition.y;
            indexX = (int)playerPosition.x;
            uiMapGenerator.uiMapMatrix[indexX][indexY].GetComponent<UIMapCell>().setFocus(true);
            uICellMapChanged();
        }

 *
 * 
 */
