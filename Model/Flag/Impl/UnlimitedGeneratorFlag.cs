﻿using System.Collections.Generic;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace RocketRegions.Model.Flag.Impl
{
    public class UnlimitedGeneratorFlag : BoolFlag
    {
        public override string Description => "Fuel for generators in this region never ends";

        public override void UpdateState(List<UnturnedPlayer> players)
        {
            var generators = Object.FindObjectsOfType<InteractableGenerator>();
            foreach (var generator in generators)
            {
                var pos = generator.gameObject.transform.position;
                if(RegionsPlugin.Instance.GetRegionAt(pos) != Region)
                    continue;

                ushort oldFuel = generator.fuel;
                ushort newFuel = oldFuel;
                if (GetValue<bool>())
                {
                    newFuel = (ushort) (generator.capacity + 1);
                }
                else if (generator.fuel > generator.capacity)
                {
                    newFuel = generator.capacity;
                }

                if(newFuel == oldFuel) //Prevent spamming packets to clients
                    continue;

                generator.tellFuel(newFuel);
            }
        }

        public override void OnRegionEnter(UnturnedPlayer player)
        {

        }

        public override void OnRegionLeave(UnturnedPlayer player)
        {
     
        }
    }
}