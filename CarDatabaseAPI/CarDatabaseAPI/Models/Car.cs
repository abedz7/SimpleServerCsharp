namespace CarDatabaseAPI.Models
{
    public class Car
    {
        public int id { get; set; }
        public string Company {  get; set; }

        public string Model { get; set; }

        public string Car_Number { get; set; }

        public override string ToString()
        {
            return $" car -> id : {id} company : {Company}  model : {Model} car number : {Car_Number} ";
        }
    }
}
