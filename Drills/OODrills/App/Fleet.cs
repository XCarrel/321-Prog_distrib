namespace OODrills
{
    public class Fleet
    {
        private readonly List<Car> _cars = new List<Car>();

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public List<Car> CarsOf(string owner)
        {
            List<Car> result = new List<Car>();
            foreach (Car car in _cars)
            {
                if (car.Owner == owner) result.Add(car);
            }
            return result;
        }

        public Car? MostDriven()
        {
            Car? best = null;
            foreach (Car car in _cars)
            {
                if (best == null || car.Mileage > best.Mileage) best = car;
            }
            return best;
        }

        public bool Contains(Car car)
        {
            return _cars.Contains(car);
        }
    }
}
