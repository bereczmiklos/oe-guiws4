using GUIWS4.Models;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIWS4.Logic
{
    public class HeroLogic
    {
        IList<SuperHero> barrack;
        IList<SuperHero> army;
        IMessenger messenger;

        public HeroLogic(IMessenger messenger) //EDITORSERVICE
        {
            this.messenger = messenger;
        }

        public double AvgPower
        {
            get { return Math.Round(army.Count == 0 ? 0 : army.Average(t => t.Power), 2); }
        }

        public double AvgSpeed
        {
            get { return Math.Round(army.Count == 0 ? 0 : army.Average(t=>t.Speed),2); }
        }

        public void SetupCollection(IList<SuperHero> barrack, IList<SuperHero> army)
        {
            this.barrack = barrack;
            this.army = army;
        }

        public void AddToArmy(SuperHero hero)
        {
            army.Add(hero.GetCopy());
        }

        public void RemoveFromArmy(SuperHero hero)
        {
            army.Remove(hero.GetCopy());
        }

        public void EditHero()
        {
            //EDITSERVICE
        }
    }
}
