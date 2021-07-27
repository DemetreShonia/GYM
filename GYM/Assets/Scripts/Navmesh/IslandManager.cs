using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HappyBat
{
    public class IslandManager : Singleton<IslandManager>
    {
        [Header("References")]
        [SerializeField] NavMeshSurface _movingIslandNavMeshSurface;
        [SerializeField] NavMeshSurface _staticIslandNavMeshSurface;
        [SerializeField] MoveIsland _moveIsland;

        NavMeshSurface _mainNavMeshSurface; // to build together when collided
        Vector3 _staticIslandPos;
        Vector3 _movingIslandPos;
        void Start()
        {
            _staticIslandPos = _staticIslandNavMeshSurface.transform.position;
            _mainNavMeshSurface = GetComponent<NavMeshSurface>();
        }

        // Update is called once per frame
        void Update()
        {
            CheckIfAreNearAnough();
        }
        void BuildBothNavMeshSurfaces()
        {
            _moveIsland.StopMoving();

            Destroy(_movingIslandNavMeshSurface);
            Destroy(_staticIslandNavMeshSurface);

            _mainNavMeshSurface.BuildNavMesh();
        }
        void CheckIfAreNearAnough()
        {
            _movingIslandPos = _movingIslandNavMeshSurface.transform.position;
            var distOnZ = Mathf.Abs(_movingIslandPos.z - _staticIslandPos.z);
            if(distOnZ <= 20) // es ricxvi ar momwons mara ikos jer ase
            {
                _movingIslandNavMeshSurface.transform.position = new Vector3(_movingIslandPos.x, _movingIslandPos.y, 20f);
                BuildBothNavMeshSurfaces();
            }
            print(distOnZ);
        }
    }
}

