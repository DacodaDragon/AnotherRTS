using System.Collections.Generic;
using BoneBox.Core;
using UnityEngine;
using System.Linq;

using AnotherRTS.Management.RemappableInput;
using AnotherRTS.Util.Notification;

namespace AnotherRTS.Gameplay.Entities.Units
{
    using Camera = UnityEngine.Camera;
    public class UnitManager : Singleton<UnitManager>
    {
        public event Notify<Unit> OnUnitLost;
        InputManager inputManager;
        Selector selector;
        List<Unit> units = new List<Unit>();
        int m_SelectAllKey;

        public void Start()
        {
            inputManager = InputManager.Instance;
            selector = Selector.Instance;
            m_SelectAllKey = inputManager.GetKeyID("select all");
            units.AddRange(FindObjectsOfType<Unit>());

            for (int i = 0; i < units.Count; i++)
            {
                units[i].OnDeath += RecieveUnitDeath;
            }
        }

        private void RecieveUnitDeath(Unit Context)
        {
            units.Remove(Context);
            OnUnitLost?.Invoke(Context);
        }

        public Unit[] GetAllUnits()
        {
            return units.ToArray();
        }

        public EntityScreenInfo<Unit>[] GetAllUnitsInScreen()
        {
            return GetAllUnitsInScreen(Camera.main);
        }

        public EntityScreenInfo<Unit>[] GetAllUnitsInScreen(Camera cam)
        {
            List<EntityScreenInfo<Unit>> UnitsInScreen = new List<EntityScreenInfo<Unit>>();
            Rect ScreenSize = new Rect(0, 0, Screen.width, Screen.height);
            for (int i = 0; i < units.Count; i++)
            {
                Vector2 Screenpos = cam.WorldToScreenPoint(units[i].transform.position);
                if (ScreenSize.Contains(Screenpos))
                    UnitsInScreen.Add(new EntityScreenInfo<Unit>(Screenpos, units[i]));
            }
            return UnitsInScreen.ToArray();
        }

        public Unit[] GetAllUnitsOfType<Type>() where Type : Unit
        {
            return units.Where(x => x is Type).ToArray();
        }

        public void AddUnits(params Unit[] units)
        {
            this.units.AddRange(units);
        }

        public void RemoveUnits(params Unit[] units)
        {
            for (int i = 0; i < units.Length; i++)
            {
                this.units.Remove(units[i]);
            }
        }

        public void Update()
        {
            if (inputManager.GetKeyDown(m_SelectAllKey))
            {
                selector.SetSelection(units.ToArray());
            }
        }
    }
}