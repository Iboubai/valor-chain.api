namespace valor_chain.api.Domain.Entities.Locations
{
    public class Region
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public long Population { get; private set; }
        public int AreaKm2 { get; private set; }
        public string Coordinates { get; private set; }

        public Region(int id, string name, long population, int areaKm2, string coordinates)
        {
            Id = id;
            Name = name;
            Population = population;
            AreaKm2 = areaKm2;
            Coordinates = coordinates;
        }
    }
}
