using System;
using System.Collections.Generic;

namespace SolarSystemProject
{
    public class ViewStateManager
    {
        private readonly SolarSystem solarSystem;
        private readonly List<string> planetNames;

        public bool IsRotating { get; private set; } = true;
        public bool IsPlanetSelected { get; private set; } = false;
        public bool ShowMenu { get; private set; } = false;
        public CelestialObject SelectedObject { get; private set; } = null;
        public int SelectedMenuIndex { get; private set; } = 0;
        public IReadOnlyList<string> PlanetNames => planetNames.AsReadOnly();

        public ViewStateManager(SolarSystem solarSystem)
        {
            this.solarSystem = solarSystem ?? throw new ArgumentNullException(nameof(solarSystem));
            this.planetNames = new List<string>();
            InitializePlanetNames();
        }

        private void InitializePlanetNames()
        {
            planetNames.Add("Sun");
            foreach (var planet in solarSystem.Planets)
            {
                planetNames.Add(planet.Name);
            }
        }

        public void ToggleRotation()
        {
            IsRotating = !IsRotating;
        }

        public void ToggleMenu()
        {
            ShowMenu = !ShowMenu;
            SelectedMenuIndex = 0;
        }

        public void HideMenu()
        {
            ShowMenu = false;
        }

        public void MoveMentSelection(int direction)
        {
            if (!ShowMenu) return;

            SelectedMenuIndex = (SelectedMenuIndex + direction + planetNames.Count) % planetNames.Count;
        }

        public void SelectCurrentMenuItem()
        {
            if (!ShowMenu) return;

            if (SelectedMenuIndex == 0)
            {
                SelectedObject = solarSystem.Sun;
            }
            else
            {
                SelectedObject = solarSystem.Planets[SelectedMenuIndex - 1];
            }

            IsPlanetSelected = true;
            ShowMenu = false;
        }

        public void DeselectPlanet()
        {
            IsPlanetSelected = false;
            SelectedObject = null;
        }

    }
}
