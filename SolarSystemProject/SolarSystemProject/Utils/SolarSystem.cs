using System;
using System.Collections.Generic;

namespace SolarSystemProject
{
    public class SolarSystem
    {
        public Star Sun { get; private set; }
        public List<Planet> Planets { get; private set; } = new List<Planet>();

        private float lastTime; // čas poslední aktualizace

        public SolarSystem()
        {
            InitializeSolarSystem(); 
            lastTime = Environment.TickCount / 1000f; // nastavení počátečního času
        }

        private void InitializeSolarSystem()
        {
            Sun = new Star(1.5f);

            Planet mercury = new Planet
            {
                Name = "Mercury",
                Size = 0.2f,
                OrbitalRadius = 3.0f,
                OrbitalSpeed = .48f,
                RotationSpeed = 10f,
                OrbitAround = Sun,
                color = new float[] { 0.7f, 0.7f, 0.7f, 1.0f }
            };
            Planets.Add(mercury);

            Planet venus = new Planet
            {
                Name = "Venus",
                Size = 0.4f,
                OrbitalRadius = 5.0f,
                OrbitalSpeed = .35f,
                RotationSpeed = 8f,
                OrbitAround = Sun,
                color = new float[] { 0.9f, 0.7f, 0.4f, 1.0f }
            };
            Planets.Add(venus);

            Planet earth = new Planet
            {
                Name = "Earth",
                Size = 0.5f,
                OrbitalRadius = 7.0f,
                OrbitalSpeed = .3f,
                RotationSpeed = 15f,
                OrbitAround = Sun,
                color = new float[] { 0.2f, 0.4f, 0.8f, 1.0f }
            };
            Planets.Add(earth);

            Planet mars = new Planet
            {
                Name = "Mars",
                Size = 0.35f,
                OrbitalRadius = 9.0f,
                OrbitalSpeed = .24f,
                RotationSpeed = 12f,
                OrbitAround = Sun,
                color = new float[] { 0.9f, 0.3f, 0.1f, 1.0f }
            };
            Planets.Add(mars);

            Planet jupiter = new Planet
            {
                Name = "Jupiter",
                Size = 1.0f,
                OrbitalRadius = 12.0f,
                OrbitalSpeed = .13f,
                RotationSpeed = 20f,
                OrbitAround = Sun,
                color = new float[] { 0.8f, 0.7f, 0.5f, 1.0f }
            };
            Planets.Add(jupiter);

            Planet saturn = new Planet
            {
                Name = "Saturn",
                Size = 0.9f,
                OrbitalRadius = 16.0f,
                OrbitalSpeed = .09f,
                RotationSpeed = 18f,
                OrbitAround = Sun,
                color = new float[] { 0.9f, 0.8f, 0.6f, 1.0f }
            };
            Planets.Add(saturn);

            Moon moon = new Moon
            {
                Size = 0.1f,
                OrbitalRadius = 1.0f,
                OrbitalSpeed = 3f,
                RotationSpeed = 5f,
                OrbitAround = earth,
                color = new float[] { 0.7f, 0.7f, 0.7f, 1.0f } 
            };
            earth.Moons.Add(moon);
        }

        public void Update()
        {
            float currentTime = Environment.TickCount / 1000f; // aktuální čas
            float deltaTime = currentTime - lastTime; // rozdíl mezi aktuálním a posledním časem
            lastTime = currentTime; // aktualizace posledního času

            // Aktualizace planet
            foreach (var planet in Planets)
            {
                planet.Update(deltaTime);
            }
        }

        public void Draw()
        {
            Sun.Draw(); // vykreslení slunce

            // Vykreslení planet
            foreach (var planet in Planets)
            {
                planet.Draw();
            }
        }
    }
}
