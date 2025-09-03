namespace valor_chain.api.Domain.Entities.Locations
{
    public class Prefecture
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public string Name { get; private set; }
        public long Population { get; private set; }
        public int AreaKm2 { get; private set; }
        public string Coordinates { get; private set; }

        public Prefecture(int id, int regionId, string name, long population, int areaKm2, string coordinates)
        {

            Id = id;
            RegionId = regionId;
            Name = name;
            Population = population;
            AreaKm2 = areaKm2;
            Coordinates = coordinates;
        }
    }
}
