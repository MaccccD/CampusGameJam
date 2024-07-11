using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastManager : MonoBehaviour
{
    #region PUBLIC FIELDS:
    [Header("SETTINGS: ")]
    public LayerMask rayMask;
    public bool debuggingRays;

    public float rayDistance;

    [Header("ORIGIN ARRAYS: ")]
    public Vector3[] midOrigins;
    public Vector3[] topOrigins;
    public Vector3[] bottomOrigins;
    public Vector3[] frontOrigins;
    public Vector3[] backOrigins;
    public Vector3[] leftOrigins;
    public Vector3[] rightOrigins;
    #endregion

    #region NUMBER OF RAYS PER SIDE:
    [Header("NUMBER OF RAYS PER SIDE: ")]
    [Header("Middle")]
    [Range(3, 25)]
    public int midPointsX = 3;
    [Range(3, 25)]
    public int midPointsZ = 3;
    [Header("Top")]
    [Range(3, 25)]
    public int topPointsX = 3;
    [Range(3, 25)]
    public int topPointsZ = 3;
    [Header("Bottom")]
    [Range(3, 25)]
    public int bottomPointsX = 3;
    [Range(3, 25)]
    public int bottomPointsZ = 3;
    [Header("Front")]
    [Range(3, 25)]
    public int frontPointsX = 3;
    [Range(3, 25)]
    public int frontPointsY = 3;
    [Header("Back")]
    [Range(3, 25)]
    public int backPointsX = 3;
    [Range(3, 25)]
    public int backPointsY = 3;
    [Header("Left")]
    [Range(3, 25)]
    public int leftPointsY = 3;
    [Range(3, 25)]
    public int leftPointsZ = 3;
    [Header("Right")]
    [Range(3, 25)]
    public int rightPointsY = 3;
    [Range(3, 25)]
    public int rightPointsZ = 3;
    #endregion

    #region PRIVATE:
    private CharacterController _controller;
    private CapsuleCollider _collider;

    private Vector3 _size;
    private Vector3 _extents;
    private float _skinWidth;

    private bool _midRaysDownHit;
    #endregion

    void Start()
    {

        if (gameObject.tag == "Player")
        {
            _controller = GetComponent<CharacterController>();
            _size = _controller.bounds.size;
            _extents = _controller.bounds.extents;
            _skinWidth = _controller.skinWidth;
            rayDistance = _skinWidth + (_skinWidth * 0.25f);
        }
        else if (gameObject.tag == "Enemy")
        {
            _collider = GetComponent<CapsuleCollider>();
            _size = _collider.bounds.size;
            _extents = _collider.bounds.extents;
            rayDistance = 0.1f;
            _skinWidth = 0.05f;
        }



        midOrigins = new Vector3[midPointsX * midPointsZ];
        topOrigins = new Vector3[topPointsX * topPointsZ];
        bottomOrigins = new Vector3[bottomPointsX * bottomPointsZ];
        frontOrigins = new Vector3[frontPointsX * frontPointsY];
        backOrigins = new Vector3[backPointsX * backPointsY];
        leftOrigins = new Vector3[leftPointsZ * leftPointsY];
        rightOrigins = new Vector3[rightPointsY * rightPointsZ];
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        GetPoints();
        DebuggingRays();
    }

    void GetPoints()
    {
        GetMidPoints();
        GetTopPoints();
        GetBottomPoints();
        GetFrontPoints();
        GetBackPoints();
        GetLeftPoints();
        GetRightPoints();
    }
    void GetMidPoints()
    {
        int count = 0;
        for (int i = 0; i < midPointsX; i++)
        {
            if (count > midOrigins.Length - 1)
                count = 0;

            float segmentLengthX = _size.x / (midPointsX - 1);
            float xOffset = segmentLengthX * i;

            for (int ii = 0; ii < midPointsZ; ii++)
            {
                float segmentLengthZ = _size.z / (midPointsZ - 1);
                float zOffset = segmentLengthZ * ii;

                midOrigins[count] = new Vector3(transform.localPosition.x + xOffset - _extents.x,
                                                transform.localPosition.y,
                                                transform.localPosition.z + zOffset - _extents.z);

                count++;
            }
        }
    }
    void GetTopPoints()
    {
        int count = 0;
        for (int i = 0; i < topPointsX; i++)
        {
            if (count > topOrigins.Length - 1)
                count = 0;

            float segmentLengthX = _size.x / (topPointsX - 1);
            float xOffset = segmentLengthX * i;

            for (int ii = 0; ii < topPointsZ; ii++)
            {
                float segmentLengthZ = _size.z / (topPointsZ - 1);
                float zOffset = segmentLengthZ * ii;

                topOrigins[count] = new Vector3(transform.localPosition.x + xOffset - _extents.x,
                                                    transform.localPosition.y + _extents.y,
                                                    transform.localPosition.z + zOffset - _extents.z);

                count++;
            }
        }
    }
    void GetBottomPoints()
    {
        int count = 0;
        for (int i = 0; i < bottomPointsX; i++)
        {
            if (count > bottomOrigins.Length - 1)
                count = 0;

            float segmentLengthX = _size.x / (bottomPointsX - 1);
            float xOffset = segmentLengthX * i;

            for (int ii = 0; ii < bottomPointsZ; ii++)
            {
                float segmentLengthZ = _size.z / (bottomPointsZ - 1);
                float zOffset = segmentLengthZ * ii;

                bottomOrigins[count] = new Vector3(transform.position.x + xOffset - _extents.x,
                                                    transform.position.y - _extents.y,
                                                    transform.position.z + zOffset - _extents.z);

                count++;
            }
        }
    }
    void GetFrontPoints()
    {
        int count = 0;
        for (int i = 0; i < frontPointsX; i++)
        {
            if (count > frontOrigins.Length - 1)
                count = 0;

            float segmentLengthX = _size.x / (frontPointsX - 1);
            float xOffset = segmentLengthX * i;

            for (int ii = 0; ii < frontPointsY; ii++)
            {
                float segmentLengthY = _size.y / (frontPointsY - 1);
                float yOffset = segmentLengthY * ii;

                frontOrigins[count] = new Vector3(transform.position.x + xOffset - _extents.x,
                                                    transform.position.y + yOffset - _extents.y,
                                                    transform.position.z + _extents.z);

                count++;
            }

        }
    }
    void GetBackPoints()
    {
        int count = 0;
        for (int i = 0; i < backPointsX; i++)
        {
            if (count > backOrigins.Length - 1)
                count = 0;

            float segmentLengthX = _size.x / (backPointsX - 1);
            float xOffset = segmentLengthX * i;

            for (int ii = 0; ii < backPointsY; ii++)
            {
                float segmentLengthY = _size.y / (backPointsY - 1);
                float yOffset = segmentLengthY * ii;

                backOrigins[count] = new Vector3(transform.position.x + xOffset - _extents.x,
                                                    transform.position.y + yOffset - _extents.y,
                                                    transform.position.z - _extents.z);

                count++;
            }

        }
    }
    void GetLeftPoints()
    {
        int count = 0;
        for (int i = 0; i < leftPointsZ; i++)
        {
            if (count > leftOrigins.Length - 1)
                count = 0;

            float segmentLengthZ = _size.z / (leftPointsZ - 1);
            float zOffset = segmentLengthZ * i;

            for (int ii = 0; ii < leftPointsY; ii++)
            {
                float segmentLengthY = _size.y / (leftPointsY - 1);
                float yOffset = segmentLengthY * ii;

                leftOrigins[count] = new Vector3(transform.position.x - _extents.x,
                                                    transform.position.y + yOffset - _extents.y,
                                                    transform.position.z + zOffset - _extents.z);

                count++;
            }

        }
    }
    void GetRightPoints()
    {
        int count = 0;
        for (int i = 0; i < rightPointsZ; i++)
        {
            if (count > rightOrigins.Length - 1)
                count = 0;

            float segmentLengthZ = _size.z / (rightPointsZ - 1);
            float zOffset = segmentLengthZ * i;

            for (int ii = 0; ii < rightPointsY; ii++)
            {
                float segmentLengthY = _size.y / (rightPointsY - 1);
                float yOffset = segmentLengthY * ii;

                rightOrigins[count] = new Vector3(transform.position.x + _extents.x,
                                                    transform.position.y + yOffset - _extents.y,
                                                    transform.position.z + zOffset - _extents.z);

                count++;
            }

        }
    }

    public bool BottomRaysHit()
    {
        bool rayHitBottom = false;

        int count = 0;
        foreach (Vector3 v in bottomOrigins)
        {
            if (Physics.Raycast(bottomOrigins[count],
                                Vector3.down,
                                rayDistance,
                                rayMask))
            {
                rayHitBottom = true;
                break;
            }
            else
                rayHitBottom = false;

            count++;
        }
        return rayHitBottom;
    }
    public bool MidRaysDownHit()
    {
        //bool rayHitMidDown = false;

        int count = 0;
        foreach (Vector3 v in midOrigins)
        {
            if (Physics.Raycast(midOrigins[count],
                                Vector3.down,
                                _extents.y + _skinWidth,
                                rayMask))
            {
                _midRaysDownHit = true;
                break;
            }
            else
            {
                _midRaysDownHit = false;
            }
            count++;
        }
        return _midRaysDownHit;
    }

    void DebuggingRays()
    {
        if (debuggingRays)
        {
            int count = 0;

            foreach (Vector3 v in midOrigins)
            {
                Debug.DrawRay(midOrigins[count], Vector3.down * rayDistance, Color.red);
                count++;
            }
            count = 0;
            foreach (Vector3 v in topOrigins)
            {
                Debug.DrawRay(topOrigins[count], Vector3.up * rayDistance, Color.red);
                count++;
            }
            count = 0;
            foreach (Vector3 v in bottomOrigins)
            {
                Debug.DrawRay(bottomOrigins[count], Vector3.down * rayDistance, Color.red);
                count++;
            }
            count = 0;
            foreach (Vector3 v in frontOrigins)
            {
                Debug.DrawRay(frontOrigins[count], Vector3.forward * rayDistance, Color.red);
                count++;
            }
            count = 0;
            foreach (Vector3 v in backOrigins)
            {
                Debug.DrawRay(backOrigins[count], Vector3.back * rayDistance, Color.red);
                count++;
            }
            count = 0;
            foreach (Vector3 v in leftOrigins)
            {
                Debug.DrawRay(leftOrigins[count], Vector3.left * rayDistance, Color.red);
                count++;
            }
            count = 0;
            foreach (Vector3 v in rightOrigins)
            {
                Debug.DrawRay(rightOrigins[count], Vector3.right * rayDistance, Color.red);
                count++;
            }
            count = 0;
        }
    }
}
