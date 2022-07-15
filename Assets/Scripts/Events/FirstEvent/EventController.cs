using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [SerializeField]
    GameObject particles;
    [SerializeField]
    GameObject lasersFloor;
    [SerializeField]
    GameObject lasersWallNoDamageUp;
    [SerializeField]
    GameObject lasersWallNoDamageDown;
    [SerializeField]
    GameObject wallDamageUp;
    [SerializeField]
    GameObject wallDamageDown;
    [SerializeField]
    Animator animator;
    [SerializeField]
    float longPause = 2f;
    [SerializeField]
    float InterPause = 1.5f;
    [SerializeField]
    float mediumPause = 1f;
    [SerializeField]
    float shortPause = 0.5f;
    [SerializeField]
    float veryShortPause = 0.4f;
    [SerializeField]
    GameObject moveLaser;
    [SerializeField]
    GameObject moveLaserRight;
    bool moveLaserState=false;
    float z = 0;
    bool fistMoveLaser = true;
    bool secondMoveLaser = true;
    Quaternion initialPosLaser1;
    Quaternion initialPosLaser2;
    [SerializeField]
    GameObject laserDoor;
    void OnEnable()
    {
        TirggerFirstEvent.start += StartFloorLasers;
    }
    void OnDisable()
    {
        TirggerFirstEvent.start -= StartFloorLasers;
    }

    private void Start()
    {
        initialPosLaser1 = moveLaser.transform.rotation;
        initialPosLaser2 = moveLaserRight.transform.rotation;
    }
    void StartFloorLasers()
    {
        particles.SetActive(true);
        StartCoroutine(startDownLasers());
    }

    private void FixedUpdate()
    {
        if (moveLaserState) 
        {
            if (fistMoveLaser)
            {
                z = moveLaser.transform.rotation.eulerAngles.z + 0.5f;
                moveLaser.transform.Rotate(new Vector3(0, 0, 0.5f));
                if (moveLaser.transform.rotation.eulerAngles.z >= 135)
                {
                    fistMoveLaser = false;
                }
            }
            else
            {
                if (secondMoveLaser)
                {
                    secondMoveLaser=false;
                    moveLaser.SetActive(false);
                    moveLaserRight.SetActive(true);
                    
                }
                z = moveLaserRight.transform.rotation.eulerAngles.z - 0.5f;
                moveLaserRight.transform.Rotate(new Vector3(0, 0, -0.5f));
                if (moveLaserRight.transform.rotation.eulerAngles.z <= 40)
                {
                    moveLaserRight.SetActive(false);
                    moveLaserState = false;
                }
            }
            
        }
    }
    IEnumerator startDownLasers()
    {
        yield return new WaitForSeconds(2f);
        particles.SetActive(false);
        lasersFloor.SetActive(true);
        yield return new WaitForSeconds(2f);
        StartCoroutine(StartWallLasers());
        
    }

    IEnumerator StartWallLasers()
    {
        lasersFloor.SetActive(false);
        lasersWallNoDamageDown.SetActive(true);
        yield return new WaitForSeconds(longPause);

        lasersWallNoDamageDown.SetActive(false);
        wallDamageDown.SetActive(true);

        yield return new WaitForSeconds(longPause);
        
        wallDamageDown.SetActive(false);
        lasersWallNoDamageUp.SetActive(true);
        
        yield return new WaitForSeconds(longPause);
        lasersWallNoDamageUp.SetActive(false);
        wallDamageUp.SetActive(true);

        yield return new WaitForSeconds(mediumPause);

        wallDamageUp.SetActive(false);
        particles.SetActive(true);

        yield return new WaitForSeconds(InterPause);

        particles.SetActive(false);
        lasersFloor.SetActive(true);
        lasersWallNoDamageDown.SetActive(true);

        yield return new WaitForSeconds(longPause);
        lasersWallNoDamageDown.SetActive(false);
        wallDamageDown.SetActive(true);
        yield return new WaitForSeconds(shortPause);
        lasersFloor.SetActive(false);

        yield return new WaitForSeconds(shortPause);
        wallDamageDown.SetActive(false);
        moveLaserState = true;
        moveLaser.SetActive(true);
        yield return new WaitForSeconds(longPause);
        lasersWallNoDamageDown.SetActive(true);
        yield return new WaitForSeconds(InterPause);
        lasersWallNoDamageDown.SetActive(false);
        wallDamageDown.SetActive(true);
        yield return new WaitForSeconds(veryShortPause);
        wallDamageDown.SetActive(false);

        for (int i = 0; i < 2; i++)
        {
            lasersFloor.SetActive(false);
            lasersWallNoDamageUp.SetActive(true);
            yield return new WaitForSeconds(InterPause);
            lasersWallNoDamageUp.SetActive(false);
            wallDamageUp.SetActive(true);
            yield return new WaitForSeconds(veryShortPause);
            wallDamageUp.SetActive(false);
            particles.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            particles.SetActive(false);
            lasersFloor.SetActive(true);
            yield return new WaitForSeconds(veryShortPause);
            lasersWallNoDamageDown.SetActive(true);
            yield return new WaitForSeconds(InterPause);
            lasersWallNoDamageDown.SetActive(false);
            wallDamageDown.SetActive(true);
            yield return new WaitForSeconds(veryShortPause);
            wallDamageDown.SetActive(false);
            lasersFloor.SetActive(false);
        }
        moveLaserState = false;
        //animator.SetBool("Open", true);
        laserDoor.SetActive(true);
        GameFlowController.Instance.StartMusicGame();
    }


    public void StopEvent()
    {
        StopAllCoroutines();
        particles.SetActive(false);
        lasersFloor.SetActive(false);
        lasersWallNoDamageUp.SetActive(false);
        lasersWallNoDamageDown.SetActive(false);
        wallDamageDown.SetActive(false);
        wallDamageUp.SetActive(false);
        moveLaserState = false;
        z = 0;
        fistMoveLaser = true;
        secondMoveLaser = true;
        moveLaser.transform.rotation = initialPosLaser1;
        moveLaserRight.transform.rotation = initialPosLaser2;
        moveLaser.SetActive(false);
        moveLaserRight.SetActive(false);
        laserDoor.SetActive(true);
    }
}
