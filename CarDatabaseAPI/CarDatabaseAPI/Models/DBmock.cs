namespace CarDatabaseAPI.Models
{
    public class DBmock
    {
        public List<Car> cars { get; set; }

        public DBmock()
        {
            cars = new List<Car>
            {
                new Car { id = 1, Company = "subaru", Model = "legacy", Car_Number = "75-499-64" },
                 new Car { id = 2, Company = "toyota", Model = "camry", Car_Number = "12-345-67" },
                  new Car { id = 3, Company = "honda", Model = "accord", Car_Number = "78-963-89" },
                  
            };
        }

    }
}
