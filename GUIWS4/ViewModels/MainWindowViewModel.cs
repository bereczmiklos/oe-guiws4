using GUIWS4.Logic;
using GUIWS4.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUIWS4.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        //LOGIC declaration
        IHeroLogic logic;
        public ObservableCollection<SuperHero> Barrack { get; set; }
        public ObservableCollection<SuperHero> Army { get; set; }

        private SuperHero selectedFromBarracks;

        public SuperHero SelectedFromBarracks
        {
            get { return selectedFromBarracks; }
            set 
            {
                SetProperty(ref selectedFromBarracks, value);
                (AddToArmyCommand as RelayCommand).NotifyCanExecuteChanged();
                (EditTrooperCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private SuperHero selectedFromArmy;

        public SuperHero SelectedFromArmy
        {
            get { return selectedFromArmy; }
            set
            { 
                SetProperty(ref selectedFromArmy, value);
                (RemoveFromArmyCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand AddToArmyCommand { get; set; }
        public ICommand RemoveFromArmyCommand { get; set; }
        public ICommand EditTrooperCommand { get; set; }

        public double AVGPower
        {
            get
            {
                return logic.AvgPower;
            }
        }

        public double AVGSpeed
        {
            get
            {
                return logic.AvgSpeed;
            }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public MainWindowViewModel()
            :this(IsInDesignMode ? null : Ioc.Default.GetService<IHeroLogic>())
        {

        }

        public MainWindowViewModel(IHeroLogic logic)
        {
            this.logic = logic;
            Barrack = new ObservableCollection<SuperHero>();
            Army = new ObservableCollection<SuperHero>();

            Barrack.Add(new SuperHero()
            {
                Name = "SpiderMan",
                Power = 8,
                Speed = 9
            });
            Barrack.Add(new SuperHero()
            {
                Name = "Thor",
                Power = 10,
                Speed = 6
            });
            Barrack.Add(new SuperHero()
            {
                Name = "IronMan",
                Power = 8,
                Speed = 8
            });
            Barrack.Add(new SuperHero()
            {
                Name = "Thanos",
                Power = 10,
                Speed = 3
            });
            Barrack.Add(new SuperHero()
            {
                Name = "Hulk",
                Power = 10,
                Speed = 2
            });

            Army.Add(Barrack[2].GetCopy());
            Army.Add(Barrack[4].GetCopy());

            logic.SetupCollection(Barrack, Army);

            AddToArmyCommand = new RelayCommand(
                () => logic.AddToArmy(SelectedFromBarracks),
                () => SelectedFromBarracks != null
                ) ;

            RemoveFromArmyCommand = new RelayCommand(
                () => logic.RemoveFromArmy(SelectedFromArmy),
                () => SelectedFromArmy != null
                );

            EditTrooperCommand = new RelayCommand(
                () => logic.EditHero(SelectedFromBarracks),
                () => SelectedFromBarracks != null
                );

            Messenger.Register<MainWindowViewModel, string, string>(this, "TrooperInfo", (recipient, msg) =>
             {
                 OnPropertyChanged("AllCost");
                 OnPropertyChanged("AVGPower");
                 OnPropertyChanged("AVGSpeed");
             });
        }
    }
}
