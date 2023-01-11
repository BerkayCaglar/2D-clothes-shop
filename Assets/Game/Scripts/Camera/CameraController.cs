using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [Tooltip("The player to follow")]
    private Transform Target;

    [Tooltip("The tilemap to follow")]
    private float xMax, xMin, yMax, yMin;

    [Tooltip("The smooth time of the camera")]
    [SerializeField]
    private float smoothTime = 0.002f;

    [Tooltip("The z position of the camera")]
    private float CamZ = -10f;

    [SerializeField]
    private Tilemap Tilemap;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            FindPlayer();
            SetDefaultPositionToPlayer();
        }
        SetTiles();
    }
    void LateUpdate()
    {
        FollowPlayer();
    }
    private void FindPlayer()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FollowPlayer()
    {
        if (Player.Instance == null) return;

        if (!Player.Instance.isOnFade && Target != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(Target.position.x, xMin, xMax), Mathf.Clamp(Target.position.y, yMin, yMax), CamZ), smoothTime);
        }
    }
    private void SetDefaultPositionToPlayer()
    {
        transform.position = new Vector3(Target.position.x, Target.position.y, CamZ);
    }
    private void SetTiles()
    {
        Vector3 minTile = Tilemap.CellToWorld(Tilemap.cellBounds.min);
        Vector3 maxTile = Tilemap.CellToWorld(Tilemap.cellBounds.max);

        SetLimits(maxTile, minTile);
    }
    private void SetLimits(Vector3 maxTile, Vector3 minTile)
    {
        Camera cam = Camera.main;

        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        xMax = maxTile.x - width / 2;
        xMin = minTile.x + width / 2;
        yMax = maxTile.y - height / 2;
        yMin = minTile.y + height / 2;
    }
}
