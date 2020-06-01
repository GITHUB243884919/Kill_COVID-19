using System.Collections;
using System.Collections.Generic;
using HillUFrame;
using HillUFrame.MiniGame;
using UnityEngine;

namespace Game
{
	public class EnemyDropDown : MonoBehaviour
	{
        public float baseSpeed = 1f;
        public float acceleration = 1f;
        public float HP = 10;
        float speed;

        bool isActive = false;

        public void Active()
		{
			isActive = true;
            speed = baseSpeed;
        }

		public void Update()
		{
			if (!isActive)
            {
				return;
			}
            speed = speed + acceleration;
            float delta = Time.deltaTime * speed;
            transform.position += Vector3.back * delta;
		}

		void OnCollisionEnter(Collision collision)
		{
            if (collision.gameObject.CompareTag("Bullet"))
            {
                --HP;
                acceleration = acceleration * 0.5f;
                speed = speed * 0.5f;
                if (HP <= 0f)
                {
                    isActive = false;
                    GameObject.Destroy(gameObject);
                    GameObject.Destroy(collision.gameObject);
                }
            }
        }
	}
}
