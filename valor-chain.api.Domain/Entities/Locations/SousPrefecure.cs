namespace valor_chain.api.Domain.Entities.Locations
{
    public class SousPrefecture
    {
        public int Id { get; private set; }
        public int RegionId { get; private set; }
        public int PrefectureId { get; private set; }
        public string Name { get; private set; }
        public long Population { get; private set; }
        public int AreaKm2 { get; private set; }
        public string Coordinates { get; private set; }

        public SousPrefecture(int id, int regionId, int prefectureId, string name, long population, int areaKm2, string coordinates)
        {
            Id = id;
            RegionId = regionId;
            PrefectureId = prefectureId;
            Name = name;
            Population = population;
            AreaKm2 = areaKm2;
            Coordinates = coordinates;
        }
    }
}
