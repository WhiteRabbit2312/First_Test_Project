using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TraineeGame
{
    public class TestingInput : MonoBehaviour, IMovement
    {
        private bool moveUp;
        private bool moveDown;
        private bool moveLeft;
        private bool moveRight;


        // Update is called once per frame
        void Update()
        {
            Controller();
        }

        public void Controller()
        {
            if (Input.GetKeyDown(KeyCode.A)) moveLeft = true;
            if (Input.GetKeyDown(KeyCode.D)) moveRight = true;
            if (Input.GetKeyDown(KeyCode.W)) moveUp = true;
            if (Input.GetKeyDown(KeyCode.S)) moveDown = true;
        }

        public bool GoLeft() //CheckMoveLeft()
        {
            if (moveLeft)
            {
                moveLeft = false;
                return true;
            }
            return false;
        }

        public bool GoRight()
        {
            if (moveRight)
            {
                moveRight = false;
                return true;
            }
            return false;
        }

        public bool GoUp()
        {
            if (moveUp)
            {
                moveUp = false;
                return true;
            }
            return false;
        }

        public bool GoDown()
        {
            if (moveDown)
            {
                moveDown = false;
                return true;
            }
            return false;
        }
    }
}