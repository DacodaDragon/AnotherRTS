﻿using UnityEngine;
using System.Collections.Generic;

namespace AnotherRTS.Camera
{
    /// <summary>
    /// Container class for Movementcontrollers
    /// </summary>
    public class CameraControlsManager
    {
        // This is an array instead of list
        // for performance reasons, we are iterating
        // on this constantly through the game
        ICameraMovementBehaviour[] movementControls;

        Vector4 smoothedMovement;


        /// <summary>
        /// Add movementcontrollers to the camera (movementbahaviour)
        /// </summary>
        /// <param name="controllers"></param>
        public void Add(params ICameraMovementBehaviour[] controllers)
        {
            if (movementControls != null)
            {
                // Make a list
                List<ICameraMovementBehaviour> newControlList = new List<ICameraMovementBehaviour>();

                // Add current and new things to the list
                newControlList.AddRange(movementControls);
                newControlList.AddRange(controllers);
                
                // Dump list in array
                movementControls = newControlList.ToArray();
            }
            else movementControls = controllers; // Just dump the array in our container if we had none already.
        }

        public void Empty()
        {
            movementControls = null;
        }

        public Vector4 GetMovementRaw()
        {
            if (movementControls == null)
                return Vector4.zero;


            Vector4 moveVector = Vector4.zero;
            for (int i = 0; i < movementControls.Length; i++)
            {
                moveVector += movementControls[i].Move();
            }

            return moveVector;
        }

        public Vector4 GetMovementSmoothed(float SmoothModifier)
        {
            if (movementControls == null)
                return Vector4.zero;


            Vector4 moveVector = Vector4.zero;
            for (int i = 0; i < movementControls.Length; i++)
            {
                moveVector += movementControls[i].Move();
            }

            // TODO: Handle Camera smoothing differently
            smoothedMovement.x += (moveVector.x - smoothedMovement.x) * SmoothModifier;
            smoothedMovement.y += (moveVector.y - smoothedMovement.y) * SmoothModifier;
            smoothedMovement.z += (moveVector.z - smoothedMovement.z) * SmoothModifier;
            smoothedMovement.w += (moveVector.w - smoothedMovement.w) * SmoothModifier;


            return smoothedMovement;
        }
    }

}
