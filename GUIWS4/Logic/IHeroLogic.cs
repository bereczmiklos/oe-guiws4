using GUIWS4.Models;
using System.Collections.Generic;

namespace GUIWS4.Logic
{
    public interface IHeroLogic
    {
        double AvgPower { get; }
        double AvgSpeed { get; }

        void AddToArmy(SuperHero hero);
        void EditHero(SuperHero hero);
        void RemoveFromArmy(SuperHero hero);
        void SetupCollection(IList<SuperHero> barrack, IList<SuperHero> army);
    }
}