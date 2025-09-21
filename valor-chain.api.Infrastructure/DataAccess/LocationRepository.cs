using gn_core_entities.Location;
using GnDapper.Interfaces;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Output;
using valor_chain.api.Domain.Ports.Output.Mapper;
using valor_chain.api.Infrastructure.DataAccess.Dtos;
using valor_chain.api.Infrastructure.DataAccess.Mappers;

namespace valor_chain.api.Infrastructure.DataAccess;

public class LocationRepository : ILocationRepository
{
    private readonly IRepository<UserLocationDto> _userLocationRepository;
    private readonly IMapper<UserLocation, UserLocationDto> _userLocationMapper;

    public LocationRepository(IUnitOfWork unitOfWork)
    {
        _userLocationRepository = unitOfWork.Repository<UserLocationDto>();
        _userLocationMapper = new UserLocationMapper();
    }

    public Task<IEnumerable<Region>> GetAllRegionsAsync()
    {
        return Task.FromResult<IEnumerable<Region>>(Regions);
    }

    public Task<Region> GetRegionByIdAsync(int id)
    {
        var region = Regions.Where(r => r.Id == id).FirstOrDefault();
        if (region == null)
            return Task.FromResult<Region>(null);
        return Task.FromResult(region);
    }

    public Task<IEnumerable<Prefecture>> GetAllPrefecturesAsync()
    {
        return Task.FromResult<IEnumerable<Prefecture>>(Prefectures);
    }

    public Task<Prefecture> GetPrefectureByIdAsync(int id)
    {
        var prefecture = Prefectures.Where(r => r.Id == id).FirstOrDefault();
        if (prefecture == null)
            return Task.FromResult<Prefecture>(null);
        return Task.FromResult(prefecture);
    }

    public Task<IEnumerable<Prefecture>> GetPrefecturesByRegionAsync(int regionId)
    {
        var prefecture = Prefectures.Where(r => r.RegionId == regionId);
        if (prefecture == null)
            return Task.FromResult<IEnumerable<Prefecture>>(null);
        return Task.FromResult(prefecture);
    }


    public Task<IEnumerable<SousPrefecture>> GetAllSousPrefecturesAsync()
    {
        return Task.FromResult<IEnumerable<SousPrefecture>>(SousPrefectures);
    }

    public Task<SousPrefecture> GetSousPrefectureByIdAsync(int id)
    {
        var sousPrefecture = SousPrefectures.Where(r => r.Id == id).FirstOrDefault();
        if (sousPrefecture == null)
            return Task.FromResult<SousPrefecture>(null);
        return Task.FromResult(sousPrefecture);
    }

    public Task<IEnumerable<SousPrefecture>> GetSousPrefecturesByPrefectureAsync(int prefectureId)
    {
        var prefecture = SousPrefectures.Where(r => r.PrefectureId == prefectureId);
        if (prefecture == null)
            return Task.FromResult<IEnumerable<SousPrefecture>>(null);
        return Task.FromResult(prefecture);
    }

    public async Task AddUserLocationAsync(UserLocation userLocation)
    {
        await _userLocationRepository.AddWithAutoIdIncrementAsync(_userLocationMapper.ToDto(userLocation));
    }

    public async Task<UserLocation> GetUserLocationAsync(Guid userId)
    {
        var userLocationDto = await _userLocationRepository.GetWithQuery($"SELECT * from  {_userLocationRepository.GetTableName()} WHERE UserId = '{userId}'");
        if (userLocationDto == null || !userLocationDto.Any())
            return null;
        var dto = userLocationDto.First();
        var location = _userLocationMapper.ToEntity(dto);
        location.Region = await GetRegionByIdAsync(dto.RegionId);
        location.Prefecture = await GetPrefectureByIdAsync(dto.PrefectureId);
        location.SousPrefecture = await GetSousPrefectureByIdAsync(dto.SousPrefectureId);

        return location;
    }

    private static List<Region> Regions = new List<Region>()
        {
            new Region(1, "Boké", "GN-B", 1092278, 31186, "-14.4303", "10.9314"),
            new Region(2, "Conakry", "GN-C", 1660973, 450, "-13.7122", "9.5092"),
            new Region(3, "Faranah", "GN-F", 941554, 35581, "-10.7455", "10.0444"),
            new Region(4, "Kankan", "GN-K", 1972537, 72145, "-9.3057", "10.3854"),
            new Region(5, "Kindia", "GN-D", 1561374, 28873, "-12.8655", "10.0569"),
            new Region(6, "Labé", "GN-L", 994458, 22869, "-12.2833", "11.3167"),
            new Region(7, "Mamou", "GN-M", 731188, 17074, "-12.15", "10.3833"),
            new Region(8, "Nzérékoré", "GN-N", 1578030, 37658, "-8.8183", "7.7553")
        };

    private static List<Prefecture> Prefectures = new List<Prefecture>()
        {
            new Prefecture(1, "Boffa", "BOF", 212583, 5050, "-14.0333", "10.1833", 1),
            new Prefecture(2, "Boké", "BOK", 449405, 11124, "-14.5333", "10.9333", 1),
            new Prefecture(3, "Fria", "FRI", 96527, 2016, "-13.5833", "10.3667", 1),
            new Prefecture(4, "Gaoual", "GAO", 194245, 7758, "-13.2", "11.75", 1),
            new Prefecture(5, "Koundara", "KOU", 130205, 5238, "-13.3", "12.4833", 1),

            // Préfecture de la Région de Conakry (RegionId = 2) - Zone Spéciale
            new Prefecture(6, "Conakry", "CKY", 1660973, 450, "-13.7122", "9.5092", 2),

            // Préfectures de la Région de Faranah (RegionId = 3)
            new Prefecture(7, "Dabola", "DAB", 182951, 5508, "-11.1167", "10.7333", 3),
            new Prefecture(8, "Dinguiraye", "DIN", 195691, 7965, "-11.3", "10.7167", 3),
            new Prefecture(9, "Faranah", "FAR", 280611, 12966, "-10.7333", "10.0333", 3),
            new Prefecture(10, "Kissidougou", "KIS", 283778, 8300, "-10.1", "9.1833", 3),

            // Préfectures de la Région de Kankan (RegionId = 4)
            new Prefecture(11, "Kankan", "KAN", 473359, 19750, "-9.3", "10.3833", 4),
            new Prefecture(12, "Kérouané", "KER", 207547, 7020, "-9.2667", "9.0167", 4),
            new Prefecture(13, "Kouroussa", "KOU", 268630, 14050, "-9.65", "10.6333", 4),
            new Prefecture(14, "Mandiana", "MAN", 335999, 12825, "-9.0167", "10.3167", 4),
            new Prefecture(15, "Siguiri", "SIG", 687002, 18500, "-9.1667", "11.4167", 4),

            // Préfectures de la Région de Kindia (RegionId = 5)
            new Prefecture(16, "Coyah", "COY", 263861, 1275, "-13.6833", "9.7", 5),
            new Prefecture(17, "Dubréka", "DUB", 330548, 4350, "-13.5167", "9.7833", 5),
            new Prefecture(18, "Forécariah", "FOR", 242942, 4384, "-13.2833", "9.4333", 5),
            new Prefecture(19, "Kindia", "KIN", 439614, 9648, "-12.85", "10.05", 5),
            new Prefecture(20, "Télimélé", "TEL", 284409, 9216, "-13.0333", "10.9", 5),

            // Préfectures de la Région de Labé (RegionId = 6)
            new Prefecture(21, "Koubia", "KOB", 100170, 3725, "-11.8833", "11.5833", 6),
            new Prefecture(22, "Labé", "LAB", 318938, 2242, "-12.2833", "11.3167", 6),
            new Prefecture(23, "Lélouma", "LEL", 163069, 4275, "-12.9333", "11.9167", 6),
            new Prefecture(24, "Mali", "MAL", 288001, 8802, "-12.3", "12.0833", 6),
            new Prefecture(25, "Tougué", "TOU", 124280, 3825, "-11.6667", "11.45", 6),

            // Préfectures de la Région de Mamou (RegionId = 7)
            new Prefecture(26, "Dalaba", "DAL", 133677, 3328, "-12.25", "10.6833", 7),
            new Prefecture(27, "Mamou", "MAM", 318981, 9108, "-12.15", "10.3833", 7),
            new Prefecture(28, "Pita", "PIT", 278530, 4638, "-12.4", "11.0667", 7),

            // Préfectures de la Région de Nzérékoré (RegionId = 8)
            new Prefecture(29, "Beyla", "BEY", 326082, 13612, "-8.65", "8.2167", 8),
            new Prefecture(30, "Guéckédou", "GUE", 291569, 4750, "-8.5667", "10.1333", 8),
            new Prefecture(31, "Lola", "LOL", 171561, 4688, "-8.5333", "7.8", 8),
            new Prefecture(32, "Macenta", "MAC", 278456, 7056, "-8.5333", "9.4667", 8),
            new Prefecture(33, "Nzérékoré", "NZE", 396949, 3632, "-8.8167", "7.75", 8),
            new Prefecture(34, "Yomou", "YOM", 114371, 3920, "-8.5333", "7.5667", 8)

        };

    private static List<SousPrefecture> SousPrefectures = new List<SousPrefecture>()
        {
            // Préfecture de Boffa (ID: 1)
            new SousPrefecture(1, "Boffa-Centre", "BOF-CEN", 27043, 505, "-14.043", "10.235", 1),
            new SousPrefecture(2, "Colia", "BOF-COL", 37043, 621, "-13.883", "10.35", 1),
            new SousPrefecture(3, "Doupourou", "BOF-DOU", 20450, 450, "-14.216", "10.333", 1),
            new SousPrefecture(4, "Koba-Tatema", "BOF-KOB", 51111, 1200, "-13.7", "9.916", 1),
            new SousPrefecture(5, "Lisso", "BOF-LIS", 12450, 320, "-14.166", "10.083", 1),
            new SousPrefecture(6, "Mankountan", "BOF-MAN", 17450, 780, "-14.133", "10.483", 1),
            new SousPrefecture(7, "Tamita", "BOF-TAM", 13898, 560, "-14.3", "10.4", 1),
            new SousPrefecture(8, "Tougnifili", "BOF-TOU", 33138, 614, "-13.833", "10.133", 1),
            // Préfecture de Boké (ID: 2)
            new SousPrefecture(9, "Boké-Centre", "BOK-CEN", 61449, 1100, "-14.533", "10.933", 2),
            new SousPrefecture(10, "Bintimodia", "BOK-BIN", 25400, 980, "-14.75", "11.05", 2),
            new SousPrefecture(11, "Dabiss", "BOK-DAB", 14300, 750, "-14.6", "10.7", 2),
            new SousPrefecture(12, "Kamsar", "BOK-KAS", 113135, 450, "-14.65", "10.95", 2),
            new SousPrefecture(13, "Kanfarandé", "BOK-KAF", 29400, 1230, "-14.8", "11.2", 2),
            new SousPrefecture(14, "Kolaboui", "BOK-KOL", 57486, 1500, "-14.7", "11.0", 2),
            new SousPrefecture(15, "Malapouyah", "BOK-MAL", 10200, 880, "-14.4", "10.8", 2),
            new SousPrefecture(16, "Sangarédi", "BOK-SAN", 76425, 2500, "-13.983", "11.166", 2),
            new SousPrefecture(17, "Sansalé", "BOK-SAL", 13400, 620, "-14.5", "10.6", 2),
            new SousPrefecture(18, "Tanéné", "BOK-TAN", 48210, 1114, "-14.3", "10.9", 2),
            // Préfecture de Fria (ID: 3)
            new SousPrefecture(19, "Fria-Centre", "FRI-CEN", 61582, 100, "-13.583", "10.366", 3),
            new SousPrefecture(20, "Baguinet", "FRI-BAG", 13745, 800, "-13.4", "10.3", 3),
            new SousPrefecture(21, "Banguingny", "FRI-BAN", 9700, 600, "-13.6", "10.5", 3),
            new SousPrefecture(22, "Tormelin", "FRI-TOR", 11500, 516, "-13.7", "10.2", 3),
            // Préfecture de Gaoual (ID: 4)
            new SousPrefecture(23, "Gaoual-Centre", "GAO-CEN", 29526, 950, "-13.2", "11.75", 4),
            new SousPrefecture(24, "Foulamory", "GAO-FOU", 10100, 800, "-13.1", "11.9", 4),
            new SousPrefecture(25, "Kakoni", "GAO-KAK", 33400, 1200, "-13.0", "11.9", 4),
            new SousPrefecture(26, "Koumbia", "GAO-KMB", 46800, 1600, "-13.5", "11.8", 4),
            new SousPrefecture(27, "Kounsitel", "GAO-KOU", 21300, 1100, "-12.9", "12.1", 4),
            new SousPrefecture(28, "Malanta", "GAO-MAL", 13200, 700, "-13.3", "11.6", 4),
            new SousPrefecture(29, "Touba", "GAO-TOU", 26400, 958, "-13.0", "11.5", 4),
            new SousPrefecture(30, "Wendou M'Bour", "GAO-WEN", 13519, 450, "-13.4", "11.5", 4),
            // Préfecture de Koundara (ID: 5)
            new SousPrefecture(31, "Koundara-Centre", "KDA-CEN", 30550, 800, "-13.3", "12.483", 5),
            new SousPrefecture(32, "Guingan", "KDA-GUI", 13400, 750, "-13.2", "12.7", 5),
            new SousPrefecture(33, "Kamaby", "KDA-KAM", 17800, 900, "-13.1", "12.6", 5),
            new SousPrefecture(34, "Sambailo", "KDA-SAM", 15500, 1100, "-13.0", "12.8", 5),
            new SousPrefecture(35, "Saréboido", "KDA-SAR", 30800, 988, "-12.9", "12.9", 5),
            new SousPrefecture(36, "Termessé", "KDA-TER", 14200, 400, "-13.4", "12.5", 5),
            new SousPrefecture(37, "Youkounkoun", "KDA-YOU", 7955, 300, "-12.8", "12.5", 5),

            // --- Région de Conakry (ID: 2) ---
            // Préfecture de Conakry (ID: 6)
            new SousPrefecture(38, "Kaloum", "CKY-KAL", 62675, 10, "-13.712", "9.509", 6),
            new SousPrefecture(39, "Dixinn", "CKY-DIX", 137450, 30, "-13.673", "9.557", 6),
            new SousPrefecture(40, "Matam", "CKY-MTM", 144340, 50, "-13.643", "9.521", 6),
            new SousPrefecture(41, "Ratoma", "CKY-RAT", 653450, 150, "-13.600", "9.633", 6),
            new SousPrefecture(42, "Matoto", "CKY-MTO", 663058, 210, "-13.566", "9.6", 6),

            // --- Région de Faranah (ID: 3) ---
            // Préfecture de Dabola (ID: 7)
            new SousPrefecture(43, "Dabola-Centre", "DAB-CEN", 38654, 1200, "-11.1167", "10.7333", 7),
            new SousPrefecture(44, "Arfamoussaya", "DAB-ARF", 14230, 800, "-11.2", "10.6", 7),
            new SousPrefecture(45, "Banko", "DAB-BAN", 23450, 900, "-11.0", "10.8", 7),
            new SousPrefecture(46, "Bissikrima", "DAB-BIS", 28640, 750, "-10.9", "10.9", 7),
            new SousPrefecture(47, "Dogomet", "DAB-DOG", 26430, 600, "-11.3", "10.7", 7),
            new SousPrefecture(48, "Kankama", "DAB-KAK", 18760, 550, "-11.1", "10.9", 7),
            new SousPrefecture(49, "Kindoyé", "DAB-KIN", 11230, 300, "-11.4", "10.8", 7),
            new SousPrefecture(50, "Konindou", "DAB-KON", 13457, 408, "-11.2", "10.5", 7),
            new SousPrefecture(51, "N'Déma", "DAB-NDE", 18100, 450, "-11.0", "10.6", 7),
            // Préfecture de Dinguiraye (ID: 8)
            new SousPrefecture(52, "Dinguiraye-Centre", "DIN-CEN", 37988, 1200, "-11.3", "10.7167", 8),
            new SousPrefecture(53, "Banora", "DIN-BAN", 24500, 900, "-11.4", "10.6", 8),
            new SousPrefecture(54, "Dialakoro", "DIN-DIA", 23400, 850, "-11.2", "10.8", 8),
            new SousPrefecture(55, "Diatiféré", "DIN-DIF", 20100, 700, "-11.5", "10.7", 8),
            new SousPrefecture(56, "Gagnakali", "DIN-GAG", 18900, 650, "-11.1", "10.9", 8),
            new SousPrefecture(57, "Kalinko", "DIN-KAL", 32400, 1100, "-11.6", "10.6", 8),
            new SousPrefecture(58, "Lansanya", "DIN-LAN", 15600, 500, "-11.0", "10.5", 8),
            new SousPrefecture(59, "Sélouma", "DIN-SEL", 22703, 2015, "-11.4", "10.9", 8),
            // Préfecture de Faranah (ID: 9)
            new SousPrefecture(60, "Faranah-Centre", "FAR-CEN", 78900, 1500, "-10.7333", "10.0333", 9),
            new SousPrefecture(61, "Banian", "FAR-BAN", 23400, 1100, "-10.6", "10.1", 9),
            new SousPrefecture(62, "Beindou", "FAR-BEI", 16700, 800, "-10.8", "10.2", 9),
            new SousPrefecture(63, "Gnaléah", "FAR-GNA", 14300, 700, "-10.5", "10.3", 9),
            new SousPrefecture(64, "Heremakonon", "FAR-HER", 12300, 600, "-10.9", "10.0", 9),
            new SousPrefecture(65, "Kobikoro", "FAR-KOB", 15600, 750, "-10.4", "10.4", 9),
            new SousPrefecture(66, "Maréla", "FAR-MAR", 25600, 1200, "-10.7", "9.9", 9),
            new SousPrefecture(67, "Passayah", "FAR-PAS", 19800, 900, "-10.8", "9.8", 9),
            new SousPrefecture(68, "Sandéniah", "FAR-SAN", 17800, 850, "-10.6", "9.7", 9),
            new SousPrefecture(69, "Songoyah", "FAR-SON", 14500, 700, "-10.9", "9.6", 9),
            new SousPrefecture(70, "Tiro", "FAR-TIR", 25611, 2816, "-10.5", "9.5", 9),
            // Préfecture de Kissidougou (ID: 10)
            new SousPrefecture(71, "Kissidougou-Centre", "KIS-CEN", 102340, 900, "-10.1", "9.1833", 10),
            new SousPrefecture(72, "Albadaria", "KIS-ALB", 18900, 700, "-10.2", "9.3", 10),
            new SousPrefecture(73, "Banama", "KIS-BAN", 14500, 600, "-10.0", "9.4", 10),
            new SousPrefecture(74, "Bardou", "KIS-BAR", 12300, 500, "-10.3", "9.2", 10),
            new SousPrefecture(75, "Beindou", "KIS-BEI", 16700, 750, "-9.9", "9.5", 10),
            new SousPrefecture(76, "Fermessadou-Pombo", "KIS-FER", 18700, 800, "-10.4", "9.1", 10),
            new SousPrefecture(77, "Firawa", "KIS-FIR", 11200, 450, "-9.8", "9.6", 10),
            new SousPrefecture(78, "Gbangbadou", "KIS-GBA", 13400, 550, "-10.5", "9.0", 10),
            new SousPrefecture(79, "Kouindiadou", "KIS-KOU", 15600, 650, "-9.7", "9.7", 10),
            new SousPrefecture(80, "Manfran", "KIS-MAN", 14300, 600, "-10.6", "8.9", 10),
            new SousPrefecture(81, "Sangardo", "KIS-SAN", 23400, 950, "-9.6", "9.8", 10),
            new SousPrefecture(82, "Yendé-Millimou", "KIS-YEN", 20100, 850, "-9.5", "9.9", 10),
            new SousPrefecture(83, "Yombiro", "KIS-YOM", 12378, 550, "-10.2", "9.0", 10),

            // --- Région de Kankan (ID: 4) ---
            // Préfecture de Kankan (ID: 11)
            new SousPrefecture(84, "Kankan-Centre", "KKN-CEN", 193830, 1200, "-9.3", "10.3833", 11),
            new SousPrefecture(85, "Balandougou", "KKN-BAL", 24500, 900, "-9.4", "10.5", 11),
            new SousPrefecture(86, "Bate-Nafadji", "KKN-BAT", 34500, 1500, "-9.2", "10.6", 11),
            new SousPrefecture(87, "Boula", "KKN-BOU", 16700, 700, "-9.5", "10.3", 11),
            new SousPrefecture(88, "Gberedou-Baranama", "KKN-GBE", 18900, 800, "-9.1", "10.7", 11),
            new SousPrefecture(89, "Guéckédou-Kénièba", "KKN-GUE", 14300, 600, "-9.6", "10.2", 11),
            new SousPrefecture(90, "Karifamoudouyah", "KKN-KAR", 20100, 850, "-9.0", "10.8", 11),
            new SousPrefecture(91, "Koumbri", "KKN-KOU", 12300, 500, "-9.7", "10.1", 11),
            new SousPrefecture(92, "Mamouroudou", "KKN-MAM", 15600, 650, "-8.9", "10.9", 11),
            new SousPrefecture(93, "Missamana", "KKN-MIS", 23400, 1000, "-9.8", "10.0", 11),
            new SousPrefecture(94, "Moribayah", "KKN-MOR", 28700, 1300, "-9.9", "9.9", 11),
            new SousPrefecture(95, "Sabadou-Baranama", "KKN-SAB", 17800, 750, "-8.8", "11.0", 11),
            new SousPrefecture(96, "Tinti-Oulen", "KKN-TIN", 18900, 800, "-9.9", "9.8", 11),
            // Préfecture de Kérouané (ID: 12)
            new SousPrefecture(97, "Kérouané-Centre", "KER-CEN", 36700, 800, "-9.2667", "9.0167", 12),
            new SousPrefecture(98, "Banankoro", "KER-BAN", 65400, 1200, "-9.3", "8.9", 12),
            new SousPrefecture(99, "Damaro", "KER-DAM", 27800, 900, "-9.2", "9.1", 12),
            new SousPrefecture(100, "Komodou", "KER-KOM", 23400, 850, "-9.4", "8.8", 12),
            new SousPrefecture(101, "Kounsankoro", "KER-KOU", 15600, 600, "-9.1", "9.2", 12),
            new SousPrefecture(102, "Linko", "KER-LIN", 12300, 500, "-9.5", "8.7", 12),
            new SousPrefecture(103, "Sibiribaro", "KER-SIB", 25600, 1100, "-9.0", "9.3", 12),
            new SousPrefecture(104, "Soromaya", "KER-SOR", 10747, 1070, "-9.3", "9.4", 12),
            // Préfecture de Kouroussa (ID: 13)
            new SousPrefecture(105, "Kouroussa-Centre", "KSA-CEN", 39400, 1200, "-9.65", "10.6333", 13),
            new SousPrefecture(106, "Babila", "KSA-BAB", 18900, 800, "-9.7", "10.5", 13),
            new SousPrefecture(107, "Balato", "KSA-BAL", 23400, 950, "-9.8", "10.4", 13),
            new SousPrefecture(108, "Banfélé", "KSA-BAN", 20100, 850, "-9.5", "10.7", 13),
            new SousPrefecture(109, "Baro", "KSA-BAR", 15500, 600, "-9.9", "10.3", 13),
            new SousPrefecture(110, "Cisséla", "KSA-CIS", 41200, 1500, "-9.4", "10.8", 13),
            new SousPrefecture(111, "Douako", "KSA-DOU", 23400, 900, "-10.0", "10.2", 13),
            new SousPrefecture(112, "Doura", "KSA-DOR", 18900, 800, "-9.3", "10.9", 13),
            new SousPrefecture(113, "Kiniéro", "KSA-KIN", 28700, 1100, "-10.1", "10.1", 13),
            new SousPrefecture(114, "Komola-Koura", "KSA-KOM", 14300, 600, "-9.2", "11.0", 13),
            new SousPrefecture(115, "Koumana", "KSA-KOU", 13400, 550, "-10.2", "10.0", 13),
            new SousPrefecture(116, "Sanguiana", "KSA-SAN", 21230, 3500, "-9.1", "11.1", 13),
            // Préfecture de Mandiana (ID: 14)
            new SousPrefecture(117, "Mandiana-Centre", "MAN-CEN", 28900, 1000, "-9.0167", "10.3167", 14),
            new SousPrefecture(118, "Balandougouba", "MAN-BAL", 24500, 900, "-9.1", "10.2", 14),
            new SousPrefecture(119, "Dialakoro", "MAN-DIA", 51200, 1500, "-8.9", "10.4", 14),
            new SousPrefecture(120, "Faralako", "MAN-FAR", 18900, 700, "-9.2", "10.1", 14),
            new SousPrefecture(121, "Kantoumania", "MAN-KAN", 15600, 600, "-8.8", "10.5", 14),
            new SousPrefecture(122, "Kiniéran", "MAN-KIN", 37800, 1200, "-9.3", "10.0", 14),
            new SousPrefecture(123, "Koundian", "MAN-KDN", 28700, 1000, "-8.7", "10.6", 14),
            new SousPrefecture(124, "Koundianakoro", "MAN-KDK", 17800, 650, "-9.4", "9.9", 14),
            new SousPrefecture(125, "Morodou", "MAN-MOR", 25600, 950, "-8.6", "10.7", 14),
            new SousPrefecture(126, "Niagassola", "MAN-NIA", 23400, 850, "-9.5", "9.8", 14),
            new SousPrefecture(127, "Sansando", "MAN-SAN", 20100, 750, "-8.5", "10.8", 14),
            new SousPrefecture(128, "Saladou", "MAN-SAL", 13401, 2175, "-8.4", "10.9", 14),
            // Préfecture de Siguiri (ID: 15)
            new SousPrefecture(129, "Siguiri-Centre", "SIG-CEN", 189000, 1500, "-9.1667", "11.4167", 15),
            new SousPrefecture(130, "Bankon", "SIG-BAN", 23400, 900, "-9.2", "11.3", 15),
            new SousPrefecture(131, "Doko", "SIG-DOK", 45600, 1200, "-9.3", "11.2", 15),
            new SousPrefecture(132, "Franwalia", "SIG-FRA", 28700, 1000, "-9.0", "11.5", 15),
            new SousPrefecture(133, "Kiniébakoura", "SIG-KIN", 20100, 750, "-9.4", "11.1", 15),
            new SousPrefecture(134, "Kintinian", "SIG-KNT", 124000, 1300, "-9.5", "11.0", 15),
            new SousPrefecture(135, "Maléa", "SIG-MAL", 34500, 1100, "-8.9", "11.6", 15),
            new SousPrefecture(136, "Naboun", "SIG-NAB", 25600, 950, "-9.6", "10.9", 15),
            new SousPrefecture(137, "Niagassola", "SIG-NIA", 30100, 1000, "-9.7", "10.8", 15),
            new SousPrefecture(138, "Niandankoro", "SIG-NDK", 28700, 950, "-8.8", "11.7", 15),
            new SousPrefecture(139, "Norassoba", "SIG-NOR", 32400, 1050, "-9.8", "10.7", 15),
            new SousPrefecture(140, "Siguirini", "SIG-SGR", 45600, 1200, "-9.9", "10.6", 15),
            new SousPrefecture(141, "Tomba Kanssa", "SIG-TOM", 38902, 5000, "-9.9", "10.5", 15),

            // --- Région de Kindia (ID: 5) ---
            // Préfecture de Coyah (ID: 16)
            new SousPrefecture(142, "Coyah-Centre", "COY-CEN", 77103, 300, "-13.6833", "9.7", 16),
            new SousPrefecture(143, "Kouriah", "COY-KOU", 19800, 250, "-13.6", "9.8", 16),
            new SousPrefecture(144, "Manéah", "COY-MAN", 110450, 400, "-13.5", "9.9", 16),
            new SousPrefecture(145, "Wonkifong", "COY-WON", 56508, 325, "-13.4", "10.0", 16),
            // Préfecture de Dubréka (ID: 17)
            new SousPrefecture(146, "Dubréka-Centre", "DUB-CEN", 131337, 800, "-13.5167", "9.7833", 17),
            new SousPrefecture(147, "Badi", "DUB-BAD", 12400, 500, "-13.4", "9.9", 17),
            new SousPrefecture(148, "Falessade", "DUB-FAL", 10200, 400, "-13.6", "9.7", 17),
            new SousPrefecture(149, "Khorira", "DUB-KHO", 25600, 700, "-13.3", "10.0", 17),
            new SousPrefecture(150, "Ouassou", "DUB-OUA", 15600, 600, "-13.7", "9.6", 17),
            // Préfecture de Dubréka (ID: 17) - suite
            new SousPrefecture(151, "Tanéné", "DUB-TAN", 45600, 900, "-13.2", "10.1", 17),
            new SousPrefecture(152, "Tondon", "DUB-TON", 89861, 1450, "-13.1", "10.2", 17),

            // Préfecture de Forécariah (ID: 18)
            new SousPrefecture(153, "Forécariah-Centre", "FOR-CEN", 21300, 400, "-13.2833", "9.4333", 18),
            new SousPrefecture(154, "Alassoya", "FOR-ALA", 18900, 350, "-13.2", "9.5", 18),
            new SousPrefecture(155, "Benty", "FOR-BEN", 25600, 500, "-13.1", "9.6", 18),
            new SousPrefecture(156, "Farmoriah", "FOR-FAR", 28700, 600, "-13.0", "9.7", 18),
            new SousPrefecture(157, "Kaback", "FOR-KAB", 34500, 700, "-13.3", "9.3", 18),
            new SousPrefecture(158, "Kakossa", "FOR-KAK", 15600, 300, "-13.4", "9.2", 18),
            new SousPrefecture(159, "Kallia", "FOR-KAL", 18900, 400, "-13.5", "9.1", 18),
            new SousPrefecture(160, "Maférinya", "FOR-MAF", 43200, 800, "-13.6", "9.0", 18),
            new SousPrefecture(161, "Moussaya", "FOR-MOU", 20100, 350, "-13.7", "8.9", 18),
            new SousPrefecture(162, "Sikhourou", "FOR-SIK", 16242, 384, "-13.8", "8.8", 18),

            // Préfecture de Kindia (ID: 19)
            new SousPrefecture(163, "Kindia-Centre", "KIN-CEN", 138695, 1000, "-12.85", "10.05", 19),
            new SousPrefecture(164, "Bangouyah", "KIN-BAN", 45600, 800, "-12.9", "10.1", 19),
            new SousPrefecture(165, "Damankanyah", "KIN-DAM", 34500, 700, "-12.7", "10.2", 19),
            new SousPrefecture(166, "Friguiagbé", "KIN-FRI", 28700, 600, "-12.6", "10.3", 19),
            new SousPrefecture(167, "Kolenté", "KIN-KOL", 20100, 500, "-12.5", "10.4", 19),
            new SousPrefecture(168, "Madina-Oula", "KIN-MAD", 32400, 900, "-12.4", "10.5", 19),
            new SousPrefecture(169, "Mambia", "KIN-MAM", 25600, 550, "-12.3", "10.6", 19),
            new SousPrefecture(170, "Molota", "KIN-MOL", 12300, 300, "-13.0", "9.9", 19),
            new SousPrefecture(171, "Samayah", "KIN-SAM", 28700, 600, "-13.1", "9.8", 19),
            new SousPrefecture(172, "Souguéta", "KIN-SOU", 53114, 3748, "-13.2", "9.7", 19),

            // Préfecture de Télimélé (ID: 20)
            new SousPrefecture(173, "Télimélé-Centre", "TEL-CEN", 19800, 800, "-13.0333", "10.9", 20),
            new SousPrefecture(174, "Bourouwal", "TEL-BOU", 25600, 700, "-12.9", "11.0", 20),
            new SousPrefecture(175, "Daramagnaki", "TEL-DAR", 34500, 900, "-12.8", "11.1", 20),
            new SousPrefecture(176, "Gougoudjé", "TEL-GOU", 15600, 400, "-12.7", "11.2", 20),
            new SousPrefecture(177, "Koba", "TEL-KOB", 18900, 500, "-12.6", "11.3", 20),
            new SousPrefecture(178, "Kollet", "TEL-KOL", 28700, 750, "-12.5", "11.4", 20),
            new SousPrefecture(179, "Konsotamy", "TEL-KON", 14300, 350, "-13.1", "10.8", 20),
            new SousPrefecture(180, "Missira", "TEL-MIS", 32400, 850, "-13.2", "10.7", 20),
            new SousPrefecture(181, "Santou", "TEL-SAN", 20100, 550, "-13.3", "10.6", 20),
            new SousPrefecture(182, "Sarekaly", "TEL-SAR", 23400, 600, "-13.4", "10.5", 20),
            new SousPrefecture(183, "Sinta", "TEL-SIN", 16700, 450, "-13.5", "10.4", 20),
            new SousPrefecture(184, "Sogolon", "TEL-SOG", 25600, 650, "-13.6", "10.3", 20),
            new SousPrefecture(185, "Tarassy", "TEL-TAR", 11200, 300, "-12.4", "11.5", 20),
            new SousPrefecture(186, "Thionthian", "TEL-THI", 14329, 916, "-12.3", "11.6", 20),

            // --- Région de Labé (ID: 6) ---
            // Préfecture de Koubia (ID: 21)
            new SousPrefecture(187, "Koubia-Centre", "KBI-CEN", 10200, 400, "-11.8833", "11.5833", 21),
            new SousPrefecture(188, "Fafaya", "KBI-FAF", 20100, 600, "-11.8", "11.6", 21),
            new SousPrefecture(189, "Gadaya", "KBI-GAD", 14300, 500, "-11.9", "11.7", 21),
            new SousPrefecture(190, "Koubi", "KBI-KOU", 12300, 450, "-11.7", "11.8", 21),
            new SousPrefecture(191, "Matakaou", "KBI-MAT", 18900, 700, "-11.6", "11.9", 21),
            new SousPrefecture(192, "Missira", "KBI-MIS", 24500, 875, "-11.5", "12.0", 21),
            new SousPrefecture(193, "Pilimini", "KBI-PIL", 10170, 200, "-11.4", "12.1", 21),
            // Préfecture de Labé (ID: 22)
            new SousPrefecture(194, "Labé-Centre", "LAB-CEN", 141740, 300, "-12.2833", "11.3167", 22),
            new SousPrefecture(195, "Dalein", "LAB-DAL", 18900, 200, "-12.2", "11.4", 22),
            new SousPrefecture(196, "Daralabe", "LAB-DAR", 15600, 180, "-12.3", "11.5", 22),
            new SousPrefecture(197, "Diari", "LAB-DIA", 14300, 170, "-12.4", "11.2", 22),
            new SousPrefecture(198, "Dionfo", "LAB-DIO", 20100, 220, "-12.1", "11.1", 22),
            new SousPrefecture(199, "Garambé", "LAB-GAR", 12300, 150, "-12.0", "11.0", 22),
            new SousPrefecture(200, "Hafia", "LAB-HAF", 16700, 190, "-12.5", "11.6", 22),
            new SousPrefecture(201, "Kaalan", "LAB-KAA", 11200, 130, "-12.6", "11.7", 22),
            new SousPrefecture(202, "Kouramangui", "LAB-KOU", 14300, 160, "-12.7", "11.8", 22),
            new SousPrefecture(203, "Noussy", "LAB-NOU", 13400, 140, "-12.8", "11.9", 22),
            new SousPrefecture(204, "Popodara", "LAB-POP", 25600, 250, "-12.9", "11.0", 22),
            new SousPrefecture(205, "Sannou", "LAB-SAN", 18900, 210, "-12.0", "11.9", 22),
            new SousPrefecture(206, "Tountouroun", "LAB-TOU", 15698, 142, "-11.9", "11.8", 22),
            // Préfecture de Lélouma (ID: 23)
            new SousPrefecture(207, "Lélouma-Centre", "LEL-CEN", 16700, 400, "-12.9333", "11.9167", 23),
            new SousPrefecture(208, "Balaya", "LEL-BAL", 14300, 350, "-12.8", "12.0", 23),
            new SousPrefecture(209, "Diountou", "LEL-DIO", 20100, 500, "-12.7", "12.1", 23),
            new SousPrefecture(210, "Hérico", "LEL-HER", 12300, 300, "-12.6", "12.2", 23),
            new SousPrefecture(211, "Korbé", "LEL-KOR", 15600, 400, "-12.5", "12.3", 23),
            new SousPrefecture(212, "Lafou", "LEL-LAF", 23400, 600, "-12.4", "12.4", 23),
            new SousPrefecture(213, "Linsan", "LEL-LIN", 11200, 280, "-13.0", "11.8", 23),
            new SousPrefecture(214, "Manda", "LEL-MAN", 9800, 250, "-13.1", "11.7", 23),
            new SousPrefecture(215, "Parawol", "LEL-PAR", 13400, 330, "-13.2", "11.6", 23),
            new SousPrefecture(216, "Sagale", "LEL-SAG", 18900, 450, "-13.3", "11.5", 23),
            new SousPrefecture(217, "Tyanguel-Bori", "LEL-TYA", 17269, 425, "-13.4", "11.4", 23),
            // Préfecture de Mali (ID: 24)
            new SousPrefecture(218, "Mali-Centre", "MLI-CEN", 35600, 800, "-12.3", "12.0833", 24),
            new SousPrefecture(219, "Balaki", "MLI-BAL", 15600, 500, "-12.2", "12.1", 24),
            new SousPrefecture(220, "Donghol-Sigon", "MLI-DON", 28700, 900, "-12.1", "12.2", 24),
            new SousPrefecture(221, "Dougountouny", "MLI-DOU", 34500, 1100, "-12.0", "12.3", 24),
            new SousPrefecture(222, "Fougou", "MLI-FOU", 12300, 400, "-11.9", "12.4", 24),
            new SousPrefecture(223, "Gayah", "MLI-GAY", 20100, 650, "-11.8", "12.5", 24),
            new SousPrefecture(224, "Hidayatou", "MLI-HID", 14300, 450, "-12.4", "11.9", 24),
            new SousPrefecture(225, "Lébékére", "MLI-LEB", 18900, 600, "-12.5", "11.8", 24),
            new SousPrefecture(226, "Madina-Wora", "MLI-MAD", 25600, 800, "-12.6", "11.7", 24),
            new SousPrefecture(227, "Salambandé", "MLI-SAL", 17800, 550, "-12.7", "11.6", 24),
            new SousPrefecture(228, "Téliré", "MLI-TEL", 23400, 700, "-12.8", "11.5", 24),
            new SousPrefecture(229, "Touba", "MLI-TOU", 28700, 900, "-12.9", "11.4", 24),
            new SousPrefecture(230, "Yimbéring", "MLI-YIM", 18701, 402, "-11.7", "12.6", 24),
            // Préfecture de Tougué (ID: 25)
            new SousPrefecture(231, "Tougué-Centre", "TUG-CEN", 13400, 300, "-11.6667", "11.45", 25),
            new SousPrefecture(232, "Fello-Koundoua", "TUG-FEL", 12300, 280, "-11.6", "11.5", 25),
            new SousPrefecture(233, "Kansangui", "TUG-KAN", 15600, 350, "-11.5", "11.6", 25),
            new SousPrefecture(234, "Kolangui", "TUG-KOL", 11200, 250, "-11.4", "11.7", 25),
            new SousPrefecture(235, "Kollet", "TUG-KLT", 14300, 330, "-11.3", "11.8", 25),
            new SousPrefecture(236, "Konah", "TUG-KON", 16700, 380, "-11.2", "11.9", 25),
            new SousPrefecture(237, "Kouratongo", "TUG-KOU", 10100, 230, "-11.7", "11.3", 25),
            new SousPrefecture(238, "Koïn", "TUG-KOI", 18900, 420, "-11.8", "11.2", 25),
            new SousPrefecture(239, "Tangali", "TUG-TAN", 9800, 220, "-11.9", "11.1", 25),
            new SousPrefecture(240, "Fatako", "TUG-FAT", 12340, 1065, "-11.8", "11.0", 25),

            // --- Région de Mamou (ID: 7) ---
            // Préfecture de Dalaba (ID: 26)
            new SousPrefecture(241, "Dalaba-Centre", "DAL-CEN", 16500, 300, "-12.25", "10.6833", 26),
            new SousPrefecture(242, "Bodié", "DAL-BOD", 14200, 280, "-12.1", "10.7", 26),
            new SousPrefecture(243, "Ditinn", "DAL-DIT", 13500, 270, "-12.3", "10.8", 26),
            new SousPrefecture(244, "Kaala", "DAL-KAA", 9800, 200, "-12.4", "10.6", 26),
            new SousPrefecture(245, "Kankalabé", "DAL-KAN", 17300, 350, "-12.0", "10.8", 26),
            new SousPrefecture(246, "Kébali", "DAL-KEB", 11200, 230, "-12.2", "10.9", 26),
            new SousPrefecture(247, "Koba", "DAL-KOB", 16700, 340, "-12.3", "10.5", 26),
            new SousPrefecture(248, "Mafara", "DAL-MAF", 8900, 180, "-12.1", "10.9", 26),
            new SousPrefecture(249, "Mitty", "DAL-MIT", 12300, 250, "-12.4", "10.9", 26),
            new SousPrefecture(250, "Mombéyah", "DAL-MOM", 13277, 260, "-12.0", "10.6", 26),
            // Préfecture de Mamou (ID: 27)
            new SousPrefecture(251, "Mamou-Centre", "MAM-CEN", 89450, 800, "-12.15", "10.3833", 27),
            new SousPrefecture(252, "Bouliwel", "MAM-BOU", 23400, 500, "-12.0", "10.5", 27),
            new SousPrefecture(253, "Dounet", "MAM-DOU", 31200, 650, "-11.9", "10.3", 27),
            new SousPrefecture(254, "Gongoret", "MAM-GON", 8900, 200, "-12.3", "10.4", 27),
            new SousPrefecture(255, "Kégnéko", "MAM-KEG", 21300, 450, "-12.2", "10.2", 27),
            new SousPrefecture(256, "Konkouré", "MAM-KON", 13000, 300, "-12.4", "10.3", 27),
            new SousPrefecture(257, "Nyagara", "MAM-NYA", 11200, 250, "-12.0", "10.2", 27),
            new SousPrefecture(258, "Ouré-Kaba", "MAM-OUR", 33400, 700, "-11.8", "10.4", 27),
            new SousPrefecture(259, "Poredaka", "MAM-POR", 20100, 420, "-12.3", "10.6", 27),
            new SousPrefecture(260, "Saramoussaya", "MAM-SAR", 23200, 480, "-11.9", "10.6", 27),
            new SousPrefecture(261, "Soyah", "MAM-SOY", 18900, 400, "-12.1", "10.1", 27),
            new SousPrefecture(262, "Téguéréya", "MAM-TEG", 13431, 280, "-12.2", "10.0", 27),
            new SousPrefecture(263, "Timbo", "MAM-TIM", 12400, 260, "-11.8", "10.7", 27),
            // Préfecture de Pita (ID: 28)
            new SousPrefecture(264, "Pita-Centre", "PIT-CEN", 28700, 500, "-12.4", "11.0667", 28),
            new SousPrefecture(265, "Bantignel", "PIT-BAN", 20100, 350, "-12.3", "11.1", 28),
            new SousPrefecture(266, "Bourouwal-Tappé", "PIT-BOU", 14300, 250, "-12.5", "11.0", 28),
            new SousPrefecture(267, "Dongol-Touma", "PIT-DON", 25600, 450, "-12.2", "11.2", 28),
            new SousPrefecture(268, "Gongoré", "PIT-GON", 16700, 300, "-12.6", "10.9", 28),
            new SousPrefecture(269, "Ley-Miro", "PIT-LEY", 18900, 330, "-12.1", "11.3", 28),
            new SousPrefecture(270, "Maci", "PIT-MAC", 23400, 400, "-12.7", "10.8", 28),
            new SousPrefecture(271, "Ninguélandé", "PIT-NIN", 32400, 550, "-12.0", "11.4", 28),
            new SousPrefecture(272, "Sangaréah", "PIT-SAN", 45600, 800, "-12.8", "10.7", 28),
            new SousPrefecture(273, "Sintali", "PIT-SIN", 14300, 250, "-12.9", "10.6", 28),
            new SousPrefecture(274, "Timbi-Madina", "PIT-TMA", 51200, 900, "-11.9", "11.5", 28),
            new SousPrefecture(275, "Timbi-Touny", "PIT-TTO", 28760, 500, "-11.8", "11.6", 28),

            // --- Région de Nzérékoré (ID: 8) ---
            // Préfecture de Beyla (ID: 29)
            new SousPrefecture(276, "Beyla-Centre", "BEY-CEN", 30124, 1200, "-8.65", "8.2167", 29),
            new SousPrefecture(277, "Boola", "BEY-BOO", 23400, 900, "-8.7", "8.3", 29),
            new SousPrefecture(278, "Diarraguerela", "BEY-DIA", 18900, 700, "-8.8", "8.1", 29),
            new SousPrefecture(279, "Diassodou", "BEY-DIS", 25600, 1000, "-8.5", "8.4", 29),
            new SousPrefecture(280, "Fouala", "BEY-FOU", 14300, 550, "-8.4", "8.5", 29),
            new SousPrefecture(281, "Gbackédou", "BEY-GBA", 28700, 1100, "-8.3", "8.6", 29),
            new SousPrefecture(282, "Gbéssoba", "BEY-GBS", 20100, 800, "-8.9", "8.0", 29),
            new SousPrefecture(283, "Karala", "BEY-KAR", 16700, 650, "-8.2", "8.7", 29),
            new SousPrefecture(284, "Koumandou", "BEY-KOU", 23400, 900, "-8.1", "8.8", 29),
            new SousPrefecture(285, "Moussadou", "BEY-MOU", 15600, 600, "-9.0", "7.9", 29),
            new SousPrefecture(286, "Nionsomoridou", "BEY-NIO", 18900, 750, "-9.1", "7.8", 29),
            new SousPrefecture(287, "Samana", "BEY-SAM", 25600, 1000, "-9.2", "7.7", 29),
            new SousPrefecture(288, "Sinko", "BEY-SIN", 45600, 1800, "-9.3", "7.6", 29),
            new SousPrefecture(289, "Sokourala", "BEY-SOK", 14322, 1212, "-9.4", "7.5", 29),
            // Préfecture de Guéckédou (ID: 30)
            new SousPrefecture(290, "Guéckédou-Centre", "GUE-CEN", 79140, 600, "-8.5667", "10.1333", 30),
            new SousPrefecture(291, "Bolodou", "GUE-BOL", 13400, 300, "-8.6", "10.2", 30),
            new SousPrefecture(292, "Fangamadou", "GUE-FAN", 25600, 500, "-8.5", "10.3", 30),
            new SousPrefecture(293, "Guendembou", "GUE-GUE", 28700, 550, "-8.4", "10.4", 30),
            new SousPrefecture(294, "Kassadou", "GUE-KAS", 20100, 400, "-8.7", "10.0", 30),
            new SousPrefecture(295, "Koundou", "GUE-KOU", 32400, 650, "-8.3", "10.5", 30),
            new SousPrefecture(296, "Nongoa", "GUE-NON", 18900, 380, "-8.8", "9.9", 30),
            new SousPrefecture(297, "Ouéndé-Kénéma", "GUE-OUE", 30100, 600, "-8.2", "10.6", 30),
            new SousPrefecture(298, "Tékoulo", "GUE-TEK", 34500, 700, "-8.1", "10.7", 30),
            new SousPrefecture(299, "Termessadou-Dibo", "GUE-TER", 18969, 470, "-8.0", "10.8", 30),
            // Préfecture de Lola (ID: 31)
            new SousPrefecture(300, "Lola-Centre", "LOL-CEN", 45600, 800, "-8.5333", "7.8", 31),
            new SousPrefecture(301, "Bossou", "LOL-BOS", 14300, 300, "-8.5", "7.9", 31),
            new SousPrefecture(302, "Foumbadou", "LOL-FOU", 18900, 400, "-8.4", "8.0", 31),
            new SousPrefecture(303, "Gama", "LOL-GAM", 16700, 350, "-8.3", "8.1", 31),
            new SousPrefecture(304, "Guéassou", "LOL-GUE", 23400, 500, "-8.2", "8.2", 31),
            new SousPrefecture(305, "Kokota", "LOL-KOK", 12300, 250, "-8.6", "7.7", 31),
            new SousPrefecture(306, "Lainé", "LOL-LAI", 18900, 400, "-8.7", "7.6", 31),
            new SousPrefecture(307, "N'Zoo", "LOL-NZO", 20100, 450, "-8.8", "7.5", 31),
            new SousPrefecture(308, "Tounkarata", "LOL-TOU", 11221, 1288, "-8.9", "7.4", 31),

            // Préfecture de Macenta (ID: 32)
            new SousPrefecture(309, "Macenta-Centre", "MAC-CEN", 65400, 700, "-8.5333", "9.4667", 32),
            new SousPrefecture(310, "Balizia", "MAC-BAL", 14300, 300, "-8.5", "9.5", 32),
            new SousPrefecture(311, "Binikala", "MAC-BIN", 12300, 250, "-8.6", "9.3", 32),
            new SousPrefecture(312, "Bofossou", "MAC-BOF", 16700, 350, "-8.4", "9.6", 32),
            new SousPrefecture(313, "Daro", "MAC-DAR", 18900, 400, "-8.3", "9.7", 32),
            new SousPrefecture(314, "Fassankoni", "MAC-FAS", 15600, 330, "-8.7", "9.2", 32),
            new SousPrefecture(315, "Kouankan", "MAC-KOU", 34500, 750, "-8.2", "9.8", 32),
            new SousPrefecture(316, "Koyamah", "MAC-KOY", 20100, 420, "-8.8", "9.1", 32),
            new SousPrefecture(317, "N'Zébéla", "MAC-NZE", 11200, 230, "-8.9", "9.0", 32),
            new SousPrefecture(318, "Ourémai", "MAC-OUR", 13400, 280, "-8.0", "9.9", 32),
            new SousPrefecture(319, "Panziazou", "MAC-PAN", 9800, 200, "-8.1", "10.0", 32),
            new SousPrefecture(320, "Sengbédou", "MAC-SEN", 18900, 400, "-9.0", "8.9", 32),
            new SousPrefecture(321, "Sérédou", "MAC-SER", 25600, 550, "-9.1", "8.8", 32),
            new SousPrefecture(322, "Vassérédou", "MAC-VAS", 14300, 300, "-9.2", "8.7", 32),
            new SousPrefecture(323, "Watanka", "MAC-WAT", 12366, 556, "-9.3", "8.6", 32),

            // Préfecture de Nzérékoré (ID: 33)
            new SousPrefecture(324, "Nzérékoré-Centre", "NZE-CEN", 195027, 400, "-8.8167", "7.75", 33),
            new SousPrefecture(325, "Bounouma", "NZE-BOU", 23400, 300, "-8.8", "7.8", 33),
            new SousPrefecture(326, "Gouécké", "NZE-GOU", 20100, 250, "-8.7", "7.9", 33),
            new SousPrefecture(327, "Kobéla", "NZE-KOB", 16700, 200, "-8.9", "7.6", 33),
            new SousPrefecture(328, "Koropara", "NZE-KOR", 18900, 230, "-8.6", "8.0", 33),
            new SousPrefecture(329, "Koulé", "NZE-KOU", 25600, 320, "-8.5", "8.1", 33),
            new SousPrefecture(330, "Palé", "NZE-PAL", 14300, 180, "-9.0", "7.5", 33),
            new SousPrefecture(331, "Samoé", "NZE-SAM", 32400, 400, "-9.1", "7.4", 33),
            new SousPrefecture(332, "Soulouta", "NZE-SOU", 18900, 240, "-9.2", "7.3", 33),
            new SousPrefecture(333, "Togbadon", "NZE-TOG", 15600, 190, "-9.3", "7.2", 33),
            new SousPrefecture(334, "Womey", "NZE-WOM", 16249, 932, "-9.4", "7.1", 33),

            // Préfecture de Yomou (ID: 34)
            new SousPrefecture(335, "Yomou-Centre", "YOM-CEN", 29138, 500, "-8.5333", "7.5667", 34),
            new SousPrefecture(336, "Banié", "YOM-BAN", 14300, 300, "-8.5", "7.6", 34),
            new SousPrefecture(337, "Bhééta", "YOM-BHE", 12300, 250, "-8.6", "7.5", 34),
            new SousPrefecture(338, "Bignamou", "YOM-BIG", 16700, 350, "-8.4", "7.7", 34),
            new SousPrefecture(339, "Bowé", "YOM-BOW", 11200, 230, "-8.7", "7.4", 34),
            new SousPrefecture(340, "Diécké", "YOM-DIE", 30100, 600, "-8.3", "7.8", 34),
            new SousPrefecture(341, "Péla", "YOM-PEL", 18963, 1720, "-8.2", "7.9", 34)
        };
}