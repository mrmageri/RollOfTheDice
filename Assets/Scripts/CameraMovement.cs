using System;
using UnityEngine;


    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 3f;
        [SerializeField] private Transform playerTransform;

        private void Start()
        {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 0.75f, -10);
        }

        private void Update()
        {
            Moving();
        }

        private void Moving()
        {
            if (transform.position != playerTransform.position)
            {
                Vector3 newPos = new Vector3(playerTransform.position.x, playerTransform.position.y + 0.75f, -10f);
                transform.position = Vector3.Slerp(transform.position, newPos, speed * Time.deltaTime);
            }
        }
    }

