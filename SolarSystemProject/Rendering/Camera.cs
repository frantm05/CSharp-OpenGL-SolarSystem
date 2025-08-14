using System;
using OpenGL;

namespace SolarSystemProject
{
    public class Camera
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float RotationX { get; set; } 
        public float RotationY { get; set; } 

        public Camera()
        {
            X = Y = 0;
            Z = -25f;
            RotationX = RotationY = 0f; // Výchozí rotace kamery
        }

        public void Move(float deltaX, float deltaY, float deltaZ)
        {
            X += deltaX;
            Y += deltaY;
            Z += deltaZ;
        }

        public void Rotate(float deltaRotationX, float deltaRotationY)
        {
            RotationX += deltaRotationX;
            RotationY += deltaRotationY;
        }

        public void ApplyTransformations()
        { 
            gl.Translatef(X, Y, Z); // Přesun kamery do správné pozice
            gl.Rotatef(RotationX, 1.0f, 0.0f, 0.0f); // Rotace kolem osy X
            gl.Rotatef(RotationY, 0.0f, 1.0f, 0.0f); // Rotace kolem osy Y
        }
    }
}
