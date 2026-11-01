namespace OODrills
{
    public class Car
    {
        public static int Count { get; private set; }
        public static int TotalKilometers { get; private set; }

        private readonly int _maxOwnerChanges;
        private int _ownerChanges;
        private string _owner;

        public string Brand { get; }
        public int Mileage { get; private set; }

        public string Owner
        {
            get => _owner;
            set
            {
                if (_ownerChanges >= _maxOwnerChanges)
                    throw new InvalidOperationException($"La voiture ne peut plus changer de propriétaire ({_maxOwnerChanges} changements max)");
                _owner = value;
                _ownerChanges++;
            }
        }

        public int RemainingOwnerChanges => _maxOwnerChanges - _ownerChanges;

        public Car(string brand)
        {
            Brand = brand;
            _owner = "";
            Count++;
        }

        public Car(string brand, string owner, int maxOwnerChanges) : this(brand)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(maxOwnerChanges);
            _owner = owner;
            _maxOwnerChanges = maxOwnerChanges;
        }

        public void Drive(int km)
        {
            Mileage += km;
            TotalKilometers += km;
        }
    }
}
