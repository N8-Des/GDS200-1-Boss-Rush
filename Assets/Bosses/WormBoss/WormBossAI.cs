using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBossAI : MonoBehaviour
{
    public int SegmentOn = 0;
    bool followersMoving = true;
    public GameObject wormHead;
    bool wrapPart1;
    bool wrapPart2;
    bool wrapPart3;
    bool movePart1;
    bool movePart2;
    bool movePart3;
    int maxSpeedSegment = 5;
    public GameObject leftUpCorner;
    public GameObject rightUpCorner;
    public GameObject leftBotCorner;
    public GameObject rightBotCorner;
    public List<GameObject> FollowingComponents = new List<GameObject>();
    private void Start()
    {
        StartCoroutine(endPhase());
    }

    void Update()
    {
        moveFollowers();
    }
    private IEnumerator WrapAround()
    {
        while (wrapPart1)
        {
            if (Vector2.Distance(wormHead.transform.position, leftBotCorner.transform.position) < 0.01)
            {
                wrapPart1 = false;
                //shoot different projectiles based on time period                
            }
            else
            {
                wormHead.transform.position = Vector3.MoveTowards(wormHead.transform.position, leftBotCorner.transform.position, Time.deltaTime * 5.1f);
                yield return new WaitForEndOfFrame();
            }
        }
        yield return new WaitForSeconds(0.3f); //designed to give the player time to react before the boss moves again.
        wrapPart2 = true;
        float count = 0.0f;
        while (wrapPart2)
        {
            //this makes the boss "wrap" around the player, giving less space to move.
            //during this time, the boss will barrage the player based on the current time period. 
            if (Vector2.Distance(wormHead.transform.position, rightBotCorner.transform.position) < 0.2)
            {
                wrapPart2 = false;
                followersMoving = false;

            }
            else
            {
                //moves the worm in an "arc"
                Vector3 center = leftBotCorner.transform.position + (rightBotCorner.transform.position - leftBotCorner.transform.position) / 2 + Vector3.up * 17.0f;

                if (count < 1.0f)
                {
                    count += 0.17f * Time.deltaTime;
                    Vector3 m1 = Vector3.Lerp(leftBotCorner.transform.position, center, count);
                    Vector3 m2 = Vector3.Lerp(center, rightBotCorner.transform.position, count);
                    wormHead.transform.position = Vector3.Lerp(m1, m2, count);
                }
                yield return new WaitForEndOfFrame();
            }
        }
        wrapPart3 = true;
        while (wrapPart3)
        {
            //shoot barrage of projectiles, different based on time period.
            wrapPart3 = false;
        }
        followersMoving = true;
        StartCoroutine(endPhase());
    }
    private IEnumerator MoveAroundScreen() //the worm will move around the screen, with some big projectile spawner in the middle.
    {        
        Vector3 offsetVertical = new Vector3(0, -3, 0);
        //spawn the projectile spawner
        while (movePart1)
        {
            if (Vector2.Distance(wormHead.transform.position, leftBotCorner.transform.position + offsetVertical) < 0.01)
            {
                movePart1 = false;
            }else
            {
                wormHead.transform.position = Vector3.MoveTowards(wormHead.transform.position, leftBotCorner.transform.position + offsetVertical, Time.deltaTime * 5.5f);
                yield return new WaitForEndOfFrame();
            }
        }
        movePart2 = true;
        while (movePart2)
        {
            if (Vector2.Distance(wormHead.transform.position, rightBotCorner.transform.position + offsetVertical) < 0.01)
            {
                Debug.Log("HUH?");
                movePart2 = false;
            }
            else
            {
                wormHead.transform.position = Vector3.MoveTowards(wormHead.transform.position, rightBotCorner.transform.position + offsetVertical, Time.deltaTime * 3f);
                yield return new WaitForEndOfFrame();
            }          
        }
        movePart3 = true;
        while (movePart3)
        {
            maxSpeedSegment = 15;
            if (Vector2.Distance(wormHead.transform.position, leftUpCorner.transform.position - offsetVertical) < 0.1)
            {
                movePart3 = false;
            }
            else
            {
                wormHead.transform.position = Vector3.MoveTowards(wormHead.transform.position, leftUpCorner.transform.position - offsetVertical, Time.deltaTime * 8f);
                yield return new WaitForEndOfFrame();
            }
        }
        //despawn projectile spawner
        maxSpeedSegment = 5;
        endPhase();
    }
    IEnumerator endPhase()
    {
        yield return new WaitForSeconds(1);
        int randomPhase = Random.Range(1, 3); //subject to change, the gamemanager may pick a few random phases for more unique expiriences.
        switch (randomPhase)
        {
            case 1:
                wrapPart1 = true;
                StartCoroutine(WrapAround());
                break;
            case 2:
                movePart1 = true;
                StartCoroutine(MoveAroundScreen());
                break;


        }

    }

    //Moves the segments of the worm. 
    public void moveFollowers()
    {
        foreach(GameObject bodySegment in FollowingComponents)
        {
            if (followersMoving)
            {
                if (SegmentOn <= FollowingComponents.Count - 1)
                {
                    if (SegmentOn == 30)
                    {
                        bodySegment.transform.localPosition = Vector3.MoveTowards(bodySegment.transform.localPosition, wormHead.transform.localPosition, Time.deltaTime * maxSpeedSegment);
                        Vector3 difference = bodySegment.transform.localPosition - wormHead.transform.localPosition;
                        difference.Normalize();
                        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                        bodySegment.transform.localRotation = Quaternion.Euler(0f, 0f, rotationZ - 90);
                        SegmentOn = 0;
                    }
                    else
                    {
                        bodySegment.transform.localPosition = Vector3.MoveTowards(bodySegment.transform.localPosition, FollowingComponents[SegmentOn + 1].transform.localPosition, Time.deltaTime * maxSpeedSegment);
                        Vector3 difference = bodySegment.transform.localPosition - FollowingComponents[SegmentOn + 1].transform.localPosition;
                        if (Vector3.Distance(bodySegment.transform.position, FollowingComponents[SegmentOn + 1].transform.position) < 0.9f)
                        {
                            bodySegment.transform.position = (bodySegment.transform.position - FollowingComponents[SegmentOn + 1].transform.position).normalized *
                                0.9f + FollowingComponents[SegmentOn + 1].transform.position;
                        }
                        difference.Normalize();
                        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                        bodySegment.transform.localRotation = Quaternion.Euler(0f, 0f, rotationZ - 90);
                        SegmentOn += 1;
                    }
                }
            }
        }
    }
}
