using Microsoft.AspNetCore.Mvc;
using valor_chain.api.Application.Handlers;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Entities.Locations;

namespace valor_chain.api.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationsController : ValorChainControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public static List<Region> Regions = new List<Region>()
    {
        new Region(1, "Conakry", 1667864, 450, "9.5379° N, 13.6773° W"),
        new Region(2, "Kindia", 1559185, 28873, "10.0569° N, 12.8341° W"),
        new Region(3, "Boké", 1081445, 31186, "10.9239° N, 14.2900° W"),
        new Region(4, "Faranah", 942733, 35581, "10.0381° N, 10.7448° W"),
        new Region(5, "Kankan", 1986329, 72145, "10.3860° N, 9.3053° W"),
        new Region(6, "Labé", 995717, 22869, "11.3200° N, 12.2800° W"),
        new Region(7, "Mamou", 732117, 17074, "10.3700° N, 11.7700° W"),
        new Region(8, "Nzérékoré", 1663582, 37658, "7.7500° N, 8.6500° W")
    };

        public static List<Prefecture> Prefectures = new List<Prefecture>()
    {
        // Conakry
        new Prefecture(1, 1, "Conakry", 1667864, 450, "9.5379° N, 13.6773° W"),
        // Boké
        new Prefecture(2, 3, "Boffa", 211063, 5050, "10.1800° N, 14.0400° W"),
        new Prefecture(3, 3, "Boké", 449405, 11124, "10.9239° N, 14.2900° W"),
        new Prefecture(4, 3, "Fria", 96527, 2016, "10.0500° N, 13.5700° W"),
        new Prefecture(5, 3, "Gaoual", 194245, 7758, "11.7500° N, 13.2000° W"),
        new Prefecture(6, 3, "Koundara", 130205, 5238, "12.4800° N, 13.3000° W"),
        // Faranah
        new Prefecture(7, 4, "Dabola", 182951, 6350, "10.7500° N, 11.1000° W"),
        new Prefecture(8, 4, "Dinguiraye", 195662, 7965, "11.3000° N, 10.7200° W"),
        new Prefecture(9, 4, "Faranah", 280511, 12966, "10.0381° N, 10.7448° W"),
        new Prefecture(10, 4, "Kissidougou", 283609, 8300, "9.1800° N, 10.1000° W"),
        // Kankan
        new Prefecture(11, 5, "Kankan", 472112, 19750, "10.3860° N, 9.3053° W"),
        new Prefecture(12, 5, "Kérouané", 211017, 7020, "9.2700° N, 9.0200° W"),
        new Prefecture(13, 5, "Kouroussa", 268224, 14050, "10.6500° N, 9.8800° W"),
        new Prefecture(14, 5, "Mandiana", 339527, 12825, "10.6300° N, 8.6800° W"),
        new Prefecture(15, 5, "Siguiri", 695449, 18500, "11.4200° N, 9.1700° W"),
        // Kindia
        new Prefecture(16, 2, "Coyah", 264164, 1275, "9.7000° N, 13.3800° W"),
        new Prefecture(17, 2, "Dubréka", 328418, 4350, "10.0000° N, 13.6500° W"),
        new Prefecture(18, 2, "Forécariah", 244649, 4384, "9.4300° N, 13.0800° W"),
        new Prefecture(19, 2, "Kindia", 438315, 9648, "10.0569° N, 12.8341° W"),
        new Prefecture(20, 2, "Télimélé", 283639, 9216, "10.9000° N, 13.0300° W"),
        // Labé
        new Prefecture(21, 6, "Koubia", 101171, 3725, "11.5800° N, 11.9000° W"),
        new Prefecture(22, 6, "Labé", 318633, 2242, "11.3200° N, 12.2800° W"),
        new Prefecture(23, 6, "Lélouma", 162634, 4275, "11.6000° N, 12.5000° W"),
        new Prefecture(24, 6, "Mali", 290320, 8802, "12.0700° N, 12.3000° W"),
        new Prefecture(25, 6, "Tougué", 122959, 3825, "11.4500° N, 11.7700° W"),
        // Mamou
        new Prefecture(26, 7, "Dalaba", 136320, 3328, "10.7000° N, 12.2500° W"),
        new Prefecture(27, 7, "Mamou", 318738, 9108, "10.3700° N, 11.7700° W"),
        new Prefecture(28, 7, "Pita", 277059, 4638, "11.0500° N, 12.4000° W"),
        // Nzérékoré
        new Prefecture(29, 8, "Beyla", 325482, 13612, "8.6800° N, 8.6500° W"),
        new Prefecture(30, 8, "Guéckédou", 291823, 4750, "8.5500° N, 10.1500° W"),
        new Prefecture(31, 8, "Lola", 175213, 4688, "7.8000° N, 8.3000° W"),
        new Prefecture(32, 8, "Macenta", 298282, 7056, "8.5500° N, 9.4800° W"),
        new Prefecture(33, 8, "Nzérékoré", 396118, 3632, "7.7500° N, 8.6500° W"),
        new Prefecture(34, 8, "Yomou", 176664, 3920, "7.5500° N, 9.2500° W")
    };

        public static List<SousPrefecture> SousPrefectures = new List<SousPrefecture>()
    {
        // Dabola
        new SousPrefecture(1, 4, 7, "Arfamoussaya", 0, 0, ""),
        new SousPrefecture(2, 4, 7, "Banko", 0, 0, ""),
        new SousPrefecture(3, 4, 7, "Bissikrima", 0, 0, ""),
        new SousPrefecture(4, 4, 7, "Dabola-Centre", 0, 0, ""),
        new SousPrefecture(5, 4, 7, "Dogomet", 0, 0, ""),
        new SousPrefecture(6, 4, 7, "Kankama", 0, 0, ""),
        new SousPrefecture(7, 4, 7, "Kindoye", 0, 0, ""),
        new SousPrefecture(8, 4, 7, "Konendou", 0, 0, ""),
        new SousPrefecture(9, 4, 7, "Ndéma", 0, 0, ""),
        // Dinguiraye
        new SousPrefecture(10, 4, 8, "Bantoun", 0, 0, ""),
        new SousPrefecture(11, 4, 8, "Dantilia", 0, 0, ""),
        new SousPrefecture(12, 4, 8, "Bambaya", 0, 0, ""),
        new SousPrefecture(13, 4, 8, "Banora", 0, 0, ""),
        new SousPrefecture(14, 4, 8, "Dialakoro", 0, 0, ""),
        new SousPrefecture(15, 4, 8, "Diatifèrè", 0, 0, ""),
        new SousPrefecture(16, 4, 8, "Dinguiraye-Centre", 0, 0, ""),
        new SousPrefecture(17, 4, 8, "Gagnakaly", 0, 0, ""),
        new SousPrefecture(18, 4, 8, "Kalinko", 0, 0, ""),
        new SousPrefecture(19, 4, 8, "Lansanaya", 0, 0, ""),
        new SousPrefecture(20, 4, 8, "Sélouma", 0, 0, ""),
        // Faranah
        new SousPrefecture(21, 4, 9, "Banian", 0, 0, ""),
        new SousPrefecture(22, 4, 9, "Beindou", 0, 0, ""),
        new SousPrefecture(23, 4, 9, "Faranah-Centre", 0, 0, ""),
        new SousPrefecture(24, 4, 9, "Gnaléah", 0, 0, ""),
        new SousPrefecture(25, 4, 9, "Hérémakonon", 0, 0, ""),
        new SousPrefecture(26, 4, 9, "Kobikoro", 0, 0, ""),
        new SousPrefecture(27, 4, 9, "Marela", 0, 0, ""),
        new SousPrefecture(28, 4, 9, "Passaya", 0, 0, ""),
        new SousPrefecture(29, 4, 9, "Sandéniyah", 0, 0, ""),
        new SousPrefecture(30, 4, 9, "Songoyah", 0, 0, ""),
        new SousPrefecture(31, 4, 9, "Tiro", 0, 0, ""),
        new SousPrefecture(32, 4, 9, "Tindo", 0, 0, ""),
        // Kissidougou
        new SousPrefecture(33, 4, 10, "Albadariah", 0, 0, ""),
        new SousPrefecture(34, 4, 10, "Banama", 0, 0, ""),
        new SousPrefecture(35, 4, 10, "Bardou", 0, 0, ""),
        new SousPrefecture(36, 4, 10, "Beindou", 0, 0, ""),
        new SousPrefecture(37, 4, 10, "Fermessadou-Pombo", 0, 0, ""),
        new SousPrefecture(38, 4, 10, "Firawa-Yomadou", 0, 0, ""),
        new SousPrefecture(39, 4, 10, "Gbangbadou", 0, 0, ""),
        new SousPrefecture(40, 4, 10, "Kissidougou-Centre", 0, 0, ""),
        new SousPrefecture(41, 4, 10, "Koundiatou", 0, 0, ""),
        new SousPrefecture(42, 4, 10, "Manfran", 0, 0, ""),
        new SousPrefecture(43, 4, 10, "Sangardo", 0, 0, ""),
        new SousPrefecture(44, 4, 10, "Yendé-Millimou", 0, 0, ""),
        new SousPrefecture(45, 4, 10, "Yombiro", 0, 0, ""),
        // Kankan
        new SousPrefecture(46, 5, 11, "Balandougou", 0, 0, ""),
        new SousPrefecture(47, 5, 11, "Baté-Nafadji", 0, 0, ""),
        new SousPrefecture(48, 5, 11, "Boula", 0, 0, ""),
        new SousPrefecture(49, 5, 11, "Gbérédou-Baranama", 0, 0, ""),
        new SousPrefecture(50, 5, 11, "Kanfamoriya", 0, 0, ""),
        new SousPrefecture(51, 5, 11, "Kankan-Centre", 0, 0, ""),
        new SousPrefecture(52, 5, 11, "Koumba", 0, 0, ""),
        new SousPrefecture(53, 5, 11, "Mamouroudou", 0, 0, ""),
        new SousPrefecture(54, 5, 11, "Misamana", 0, 0, ""),
        new SousPrefecture(55, 5, 11, "Moribayah", 0, 0, ""),
        new SousPrefecture(56, 5, 11, "Sabadou-Baranama", 0, 0, ""),
        new SousPrefecture(57, 5, 11, "Tinti-Oulen", 0, 0, ""),
        new SousPrefecture(58, 5, 11, "Tokounou", 0, 0, ""),
        new SousPrefecture(59, 5, 11, "Fodecariah balimana", 0, 0, ""),
        new SousPrefecture(60, 5, 11, "Djimbala", 0, 0, ""),
        new SousPrefecture(61, 5, 11, "Djélibakoro", 0, 0, ""),
        // Kérouané
        new SousPrefecture(62, 5, 12, "Banankoro", 0, 0, ""),
        new SousPrefecture(63, 5, 12, "Damaro", 0, 0, ""),
        new SousPrefecture(64, 5, 12, "Kérouané-Centre", 0, 0, ""),
        new SousPrefecture(65, 5, 12, "Komodou", 0, 0, ""),
        new SousPrefecture(66, 5, 12, "Kounsankoro", 0, 0, ""),
        new SousPrefecture(67, 5, 12, "Linko", 0, 0, ""),
        new SousPrefecture(68, 5, 12, "Sibiribaro", 0, 0, ""),
        new SousPrefecture(69, 5, 12, "Soromayah", 0, 0, ""),
        // Kouroussa
        new SousPrefecture(70, 5, 13, "Babila", 0, 0, ""),
        new SousPrefecture(71, 5, 13, "Balato", 0, 0, ""),
        new SousPrefecture(72, 5, 13, "Banfèlè", 0, 0, ""),
        new SousPrefecture(73, 5, 13, "Baro", 0, 0, ""),
        new SousPrefecture(74, 5, 13, "Cisséla", 0, 0, ""),
        new SousPrefecture(75, 5, 13, "Douako", 0, 0, ""),
        new SousPrefecture(76, 5, 13, "Doura", 0, 0, ""),
        new SousPrefecture(77, 5, 13, "Kiniéro", 0, 0, ""),
        new SousPrefecture(78, 5, 13, "Koumana", 0, 0, ""),
        new SousPrefecture(79, 5, 13, "Komola-Koura", 0, 0, ""),
        new SousPrefecture(80, 5, 13, "Kouroussa-Centre", 0, 0, ""),
        new SousPrefecture(81, 5, 13, "Sanguiana", 0, 0, ""),
        new SousPrefecture(82, 5, 13, "Fadou-Saba", 0, 0, ""),
        new SousPrefecture(83, 5, 13, "Kansereah", 0, 0, ""),
        // Mandiana
        new SousPrefecture(84, 5, 14, "Balandougouba", 0, 0, ""),
        new SousPrefecture(85, 5, 14, "Dialakoro", 0, 0, ""),
        new SousPrefecture(86, 5, 14, "Faralako", 0, 0, ""),
        new SousPrefecture(87, 5, 14, "Kantoumania", 0, 0, ""),
        new SousPrefecture(88, 5, 14, "Kiniéran", 0, 0, ""),
        new SousPrefecture(89, 5, 14, "Koudianakoro", 0, 0, ""),
        new SousPrefecture(90, 5, 14, "Koundian", 0, 0, ""),
        new SousPrefecture(91, 5, 14, "Mandiana-Centre", 0, 0, ""),
        new SousPrefecture(92, 5, 14, "Morodou", 0, 0, ""),
        new SousPrefecture(93, 5, 14, "Niantanina", 0, 0, ""),
        new SousPrefecture(94, 5, 14, "Saladou", 0, 0, ""),
        new SousPrefecture(95, 5, 14, "Sansando", 0, 0, ""),
        // Siguiri
        new SousPrefecture(96, 5, 15, "Bankon", 0, 0, ""),
        new SousPrefecture(97, 5, 15, "Doko", 0, 0, ""),
        new SousPrefecture(98, 5, 15, "Franwalia", 0, 0, ""),
        new SousPrefecture(99, 5, 15, "Kiniébakora", 0, 0, ""),
        new SousPrefecture(100, 5, 15, "Kintinian", 0, 0, ""),
        new SousPrefecture(101, 5, 15, "Maléah", 0, 0, ""),
        new SousPrefecture(102, 5, 15, "Naboun", 0, 0, ""),
        new SousPrefecture(103, 5, 15, "Niagassola", 0, 0, ""),
        new SousPrefecture(104, 5, 15, "Niandankoro", 0, 0, ""),
        new SousPrefecture(105, 5, 15, "Norassoba", 0, 0, ""),
        new SousPrefecture(106, 5, 15, "Siguiri-Centre", 0, 0, ""),
        new SousPrefecture(107, 5, 15, "Siguirini", 0, 0, ""),
        new SousPrefecture(108, 5, 15, "Nounkounkan", 0, 0, ""),
        new SousPrefecture(109, 5, 15, "Didi", 0, 0, ""),
        new SousPrefecture(110, 5, 15, "Kourémalé", 0, 0, ""),
        new SousPrefecture(111, 5, 15, "Tomba Kanssa", 0, 0, ""),
        new SousPrefecture(112, 5, 15, "Tomboni", 0, 0, ""),
        new SousPrefecture(113, 5, 15, "fidako", 0, 0, ""),
        new SousPrefecture(114, 5, 15, "mignada", 0, 0, ""),
        new SousPrefecture(115, 5, 15, "Koumandjanbougou", 0, 0, ""),
        new SousPrefecture(116, 5, 15, "diomabana", 0, 0, ""),
        // Beyla
        new SousPrefecture(117, 8, 29, "Beyla-Centre", 0, 0, ""),
        new SousPrefecture(118, 8, 29, "Boola", 0, 0, ""),
        new SousPrefecture(119, 8, 29, "Diarraguerela", 0, 0, ""),
        new SousPrefecture(120, 8, 29, "Diassadou", 0, 0, ""),
        new SousPrefecture(121, 8, 29, "Fouala", 0, 0, ""),
        new SousPrefecture(122, 8, 29, "Gbackédou", 0, 0, ""),
        new SousPrefecture(123, 8, 29, "Gbéssoba", 0, 0, ""),
        new SousPrefecture(124, 8, 29, "Karala", 0, 0, ""),
        new SousPrefecture(125, 8, 29, "Koumandou", 0, 0, ""),
        new SousPrefecture(126, 8, 29, "Mousadou", 0, 0, ""),
        new SousPrefecture(127, 8, 29, "Nionsomoridou", 0, 0, ""),
        new SousPrefecture(128, 8, 29, "Samana", 0, 0, ""),
        new SousPrefecture(129, 8, 29, "Sinko", 0, 0, ""),
        new SousPrefecture(130, 8, 29, "Sokourala", 0, 0, ""),
        new SousPrefecture(131, 8, 29, "Tiéwa", 0, 0, ""),
        // Guéckédou
        new SousPrefecture(132, 8, 30, "Bolodou", 0, 0, ""),
        new SousPrefecture(133, 8, 30, "Fangamadou", 0, 0, ""),
        new SousPrefecture(134, 8, 30, "Guéckédou-Centre", 0, 0, ""),
        new SousPrefecture(135, 8, 30, "Guéndembou", 0, 0, ""),
        new SousPrefecture(136, 8, 30, "Kassadou", 0, 0, ""),
        new SousPrefecture(137, 8, 30, "Koundou", 0, 0, ""),
        new SousPrefecture(138, 8, 30, "Nongoa", 0, 0, ""),
        new SousPrefecture(139, 8, 30, "Ouéndé-Kénéma", 0, 0, ""),
        new SousPrefecture(140, 8, 30, "Tékoulo", 0, 0, ""),
        new SousPrefecture(141, 8, 30, "Terméssadou Djibo", 0, 0, ""),
        // Lola
        new SousPrefecture(142, 8, 31, "Bossou", 0, 0, ""),
        new SousPrefecture(143, 8, 31, "Foumbadou", 0, 0, ""),
        new SousPrefecture(144, 8, 31, "Gama Berema", 0, 0, ""),
        new SousPrefecture(145, 8, 31, "Guéassou", 0, 0, ""),
        new SousPrefecture(146, 8, 31, "Kokota", 0, 0, ""),
        new SousPrefecture(147, 8, 31, "Laine", 0, 0, ""),
        new SousPrefecture(148, 8, 31, "Lola-Centre", 0, 0, ""),
        new SousPrefecture(149, 8, 31, "N'Zoo", 0, 0, ""),
        new SousPrefecture(150, 8, 31, "Tounkarata", 0, 0, ""),
        // Macenta
        new SousPrefecture(151, 8, 32, "Balizia", 0, 0, ""),
        new SousPrefecture(152, 8, 32, "Binikala", 0, 0, ""),
        new SousPrefecture(153, 8, 32, "Bofossou", 0, 0, ""),
        new SousPrefecture(154, 8, 32, "Daro", 0, 0, ""),
        new SousPrefecture(155, 8, 32, "Fassankoni", 0, 0, ""),
        new SousPrefecture(156, 8, 32, "Kouankan", 0, 0, ""),
        new SousPrefecture(157, 8, 32, "Koyama", 0, 0, ""),
        new SousPrefecture(158, 8, 32, "Macenta-Centre", 0, 0, ""),
        new SousPrefecture(159, 8, 32, "N'Zébéla", 0, 0, ""),
        new SousPrefecture(160, 8, 32, "Ourémaï", 0, 0, ""),
        new SousPrefecture(161, 8, 32, "Panziazou", 0, 0, ""),
        new SousPrefecture(162, 8, 32, "Sengbédou", 0, 0, ""),
        new SousPrefecture(163, 8, 32, "Sérédou", 0, 0, ""),
        new SousPrefecture(164, 8, 32, "Vasérédou", 0, 0, ""),
        new SousPrefecture(165, 8, 32, "Watanka", 0, 0, ""),
        // Nzérékoré
        new SousPrefecture(166, 8, 33, "Bounouma", 0, 0, ""),
        new SousPrefecture(167, 8, 33, "Gouécké", 0, 0, ""),
        new SousPrefecture(168, 8, 33, "Kobéla", 0, 0, ""),
        new SousPrefecture(169, 8, 33, "Koropara", 0, 0, ""),
        new SousPrefecture(170, 8, 33, "Koulé", 0, 0, ""),
        new SousPrefecture(171, 8, 33, "N'Zérékoré-Centre", 0, 0, ""),
        new SousPrefecture(172, 8, 33, "Palé", 0, 0, ""),
        new SousPrefecture(173, 8, 33, "Samoé", 0, 0, ""),
        new SousPrefecture(174, 8, 33, "Soulouta", 0, 0, ""),
        new SousPrefecture(175, 8, 33, "Womey", 0, 0, ""),
        new SousPrefecture(176, 8, 33, "Yalenzou", 0, 0, ""),
        // Yomou
        new SousPrefecture(177, 8, 34, "Banié", 0, 0, ""),
        new SousPrefecture(178, 8, 34, "Bheeta", 0, 0, ""),
        new SousPrefecture(179, 8, 34, "Bignamou", 0, 0, ""),
        new SousPrefecture(180, 8, 34, "Bowé", 0, 0, ""),
        new SousPrefecture(181, 8, 34, "Diécké", 0, 0, ""),
        new SousPrefecture(182, 8, 34, "Péla", 0, 0, ""),
        new SousPrefecture(183, 8, 34, "Yomou-Centre", 0, 0, ""),
        // Boffa
        new SousPrefecture(184, 3, 2, "Boffa-Centre", 0, 0, ""),
        new SousPrefecture(185, 3, 2, "Colia", 0, 0, ""),
        new SousPrefecture(186, 3, 2, "Douprou", 0, 0, ""),
        new SousPrefecture(187, 3, 2, "Koba-Tatema", 0, 0, ""),
        new SousPrefecture(188, 3, 2, "Lisso", 0, 0, ""),
        new SousPrefecture(189, 3, 2, "Mankountan", 0, 0, ""),
        new SousPrefecture(190, 3, 2, "Tamita", 0, 0, ""),
        new SousPrefecture(191, 3, 2, "Tougnifily", 0, 0, ""),
        // Boké
        new SousPrefecture(192, 3, 3, "Bintimodia", 0, 0, ""),
        new SousPrefecture(193, 3, 3, "Boké-Centre", 0, 0, ""),
        new SousPrefecture(194, 3, 3, "Dabiss", 0, 0, ""),
        new SousPrefecture(195, 3, 3, "Kamsar", 0, 0, ""),
        new SousPrefecture(196, 3, 3, "Kanfarandé", 0, 0, ""),
        new SousPrefecture(197, 3, 3, "Kolaboui", 0, 0, ""),
        new SousPrefecture(198, 3, 3, "Malapouyah", 0, 0, ""),
        new SousPrefecture(199, 3, 3, "Sangarédi", 0, 0, ""),
        new SousPrefecture(200, 3, 3, "Sansalé", 0, 0, ""),
        new SousPrefecture(201, 3, 3, "Tanènè", 0, 0, ""),
        // Fria
        new SousPrefecture(202, 3, 4, "Banguinet", 0, 0, ""),
        new SousPrefecture(203, 3, 4, "Banguingny", 0, 0, ""),
        new SousPrefecture(204, 3, 4, "Fria-Centre", 0, 0, ""),
        new SousPrefecture(205, 3, 4, "Tormelin", 0, 0, ""),
        // Gaoual
        new SousPrefecture(206, 3, 5, "Foulamory", 0, 0, ""),
        new SousPrefecture(207, 3, 5, "Gaoual-Centre", 0, 0, ""),
        new SousPrefecture(208, 3, 5, "Kakony", 0, 0, ""),
        new SousPrefecture(209, 3, 5, "Koumbia", 0, 0, ""),
        new SousPrefecture(210, 3, 5, "Kounsitel", 0, 0, ""),
        new SousPrefecture(211, 3, 5, "Malanta", 0, 0, ""),
        new SousPrefecture(212, 3, 5, "Touba", 0, 0, ""),
        new SousPrefecture(213, 3, 5, "Wendou M'Bour", 0, 0, ""),
        // Koundara
        new SousPrefecture(214, 3, 6, "Guingan", 0, 0, ""),
        new SousPrefecture(215, 3, 6, "Kamaby", 0, 0, ""),
        new SousPrefecture(216, 3, 6, "Koundara-Centre", 0, 0, ""),
        new SousPrefecture(217, 3, 6, "Sambaïlo", 0, 0, ""),
        new SousPrefecture(218, 3, 6, "Saréboïdo", 0, 0, ""),
        new SousPrefecture(219, 3, 6, "Termessé", 0, 0, ""),
        new SousPrefecture(220, 3, 6, "Youkounkoun", 0, 0, ""),
        // Coyah
        new SousPrefecture(221, 2, 16, "Coyah-Centre", 0, 0, ""),
        new SousPrefecture(222, 2, 16, "Kouriah", 0, 0, ""),
        new SousPrefecture(223, 2, 16, "Manéah", 0, 0, ""),
        new SousPrefecture(224, 2, 16, "Wonkifong", 0, 0, ""),
        // Dubréka
        new SousPrefecture(225, 2, 17, "Badi", 0, 0, ""),
        new SousPrefecture(226, 2, 17, "Dubréka-Centre", 0, 0, ""),
        new SousPrefecture(227, 2, 17, "Faléssadé", 0, 0, ""),
        new SousPrefecture(228, 2, 17, "Khorira", 0, 0, ""),
        new SousPrefecture(229, 2, 17, "Ouassou", 0, 0, ""),
        new SousPrefecture(230, 2, 17, "Tanènè", 0, 0, ""),
        new SousPrefecture(231, 2, 17, "Tondon", 0, 0, ""),
        // Forécariah
        new SousPrefecture(232, 2, 18, "Alassoyah", 0, 0, ""),
        new SousPrefecture(233, 2, 18, "Benty", 0, 0, ""),
        new SousPrefecture(234, 2, 18, "Farmoriyah", 0, 0, ""),
        new SousPrefecture(235, 2, 18, "Forécariah-Centre", 0, 0, ""),
        new SousPrefecture(236, 2, 18, "Kaback", 0, 0, ""),
        new SousPrefecture(237, 2, 18, "Kakossa", 0, 0, ""),
        new SousPrefecture(238, 2, 18, "Kallia", 0, 0, ""),
        new SousPrefecture(239, 2, 18, "Maferenya", 0, 0, ""),
        new SousPrefecture(240, 2, 18, "Moussayah", 0, 0, ""),
        new SousPrefecture(241, 2, 18, "Sikhourou", 0, 0, ""),
        // Kindia
        new SousPrefecture(242, 2, 19, "Bangouya", 0, 0, ""),
        new SousPrefecture(243, 2, 19, "Damankaniah", 0, 0, ""),
        new SousPrefecture(244, 2, 19, "Friguiagbé", 0, 0, ""),
        new SousPrefecture(245, 2, 19, "Kindia-Centre", 0, 0, ""),
        new SousPrefecture(246, 2, 19, "Kolenté", 0, 0, ""),
        new SousPrefecture(247, 2, 19, "Madina-Oula", 0, 0, ""),
        new SousPrefecture(248, 2, 19, "Mambiya", 0, 0, ""),
        new SousPrefecture(249, 2, 19, "Molota", 0, 0, ""),
        new SousPrefecture(250, 2, 19, "Samaya", 0, 0, ""),
        new SousPrefecture(251, 2, 19, "Souguéta", 0, 0, ""),
        new SousPrefecture(252, 2, 19, "Linsan", 0, 0, ""),
        // Télimélé
        new SousPrefecture(253, 2, 20, "Bourouwal", 0, 0, ""),
        new SousPrefecture(254, 2, 20, "Daramagnaki", 0, 0, ""),
        new SousPrefecture(255, 2, 20, "Gougoudjé", 0, 0, ""),
        new SousPrefecture(256, 2, 20, "Koba", 0, 0, ""),
        new SousPrefecture(257, 2, 20, "Kollet", 0, 0, ""),
        new SousPrefecture(258, 2, 20, "Konsotami", 0, 0, ""),
        new SousPrefecture(259, 2, 20, "Missira", 0, 0, ""),
        new SousPrefecture(260, 2, 20, "Santou", 0, 0, ""),
        new SousPrefecture(261, 2, 20, "Sarékali", 0, 0, ""),
        new SousPrefecture(262, 2, 20, "Sinta", 0, 0, ""),
        new SousPrefecture(263, 2, 20, "Sogolon", 0, 0, ""),
        new SousPrefecture(264, 2, 20, "Tarihoye", 0, 0, ""),
        new SousPrefecture(265, 2, 20, "Télimélé-Centre", 0, 0, ""),
        new SousPrefecture(266, 2, 20, "Thionthian", 0, 0, ""),
        new SousPrefecture(267, 2, 20, "Kawessi", 0, 0, ""),
        // Koubia
        new SousPrefecture(268, 6, 21, "Fafaya", 0, 0, ""),
        new SousPrefecture(269, 6, 21, "Gadha-Woundou", 0, 0, ""),
        new SousPrefecture(270, 6, 21, "Koubia-Centre", 0, 0, ""),
        new SousPrefecture(271, 6, 21, "Matakaou", 0, 0, ""),
        new SousPrefecture(272, 6, 21, "Missira", 0, 0, ""),
        new SousPrefecture(273, 6, 21, "Pilimini", 0, 0, ""),
        // Labé
        new SousPrefecture(274, 6, 22, "Dalein", 0, 0, ""),
        new SousPrefecture(275, 6, 22, "Daralabe", 0, 0, ""),
        new SousPrefecture(276, 6, 22, "Diari", 0, 0, ""),
        new SousPrefecture(277, 6, 22, "Dionfo", 0, 0, ""),
        new SousPrefecture(278, 6, 22, "Garambé", 0, 0, ""),
        new SousPrefecture(279, 6, 22, "Hafia", 0, 0, ""),
        new SousPrefecture(280, 6, 22, "Kaalan", 0, 0, ""),
        new SousPrefecture(281, 6, 22, "Kouramandji", 0, 0, ""),
        new SousPrefecture(282, 6, 22, "Labé-Centre", 0, 0, ""),
        new SousPrefecture(283, 6, 22, "Noussy", 0, 0, ""),
        new SousPrefecture(284, 6, 22, "Popodara", 0, 0, ""),
        new SousPrefecture(285, 6, 22, "Sannoun", 0, 0, ""),
        new SousPrefecture(286, 6, 22, "Tountouroun", 0, 0, ""),
        new SousPrefecture(287, 6, 22, "Tarambaly", 0, 0, ""),
        // Lélouma
        new SousPrefecture(288, 6, 23, "Balaya", 0, 0, ""),
        new SousPrefecture(289, 6, 23, "Djountou", 0, 0, ""),
        new SousPrefecture(290, 6, 23, "Hérico", 0, 0, ""),
        new SousPrefecture(291, 6, 23, "Korbè", 0, 0, ""),
        new SousPrefecture(292, 6, 23, "Lafou", 0, 0, ""),
        new SousPrefecture(293, 6, 23, "Lélouma-Centre", 0, 0, ""),
        new SousPrefecture(294, 6, 23, "Linsan", 0, 0, ""),
        new SousPrefecture(295, 6, 23, "Manda", 0, 0, ""),
        new SousPrefecture(296, 6, 23, "Parawol", 0, 0, ""),
        new SousPrefecture(297, 6, 23, "Sagalé", 0, 0, ""),
        new SousPrefecture(298, 6, 23, "Tyanguel-Bori", 0, 0, ""),
        // Mali
        new SousPrefecture(299, 6, 24, "Balaki", 0, 0, ""),
        new SousPrefecture(300, 6, 24, "Donghol Sigon", 0, 0, ""),
        new SousPrefecture(301, 6, 24, "Dougountouny", 0, 0, ""),
        new SousPrefecture(302, 6, 24, "Fougou", 0, 0, ""),
        new SousPrefecture(303, 6, 24, "Gayah", 0, 0, ""),
        new SousPrefecture(304, 6, 24, "Hidayatou", 0, 0, ""),
        new SousPrefecture(305, 6, 24, "Lébékéré", 0, 0, ""),
        new SousPrefecture(306, 6, 24, "Madina Wora", 0, 0, ""),
        new SousPrefecture(307, 6, 24, "Mali-Centre", 0, 0, ""),
        new SousPrefecture(308, 6, 24, "Madina-Salambandé", 0, 0, ""),
        new SousPrefecture(309, 6, 24, "Téliré", 0, 0, ""),
        new SousPrefecture(310, 6, 24, "Touba", 0, 0, ""),
        new SousPrefecture(311, 6, 24, "Yembereng", 0, 0, ""),
        new SousPrefecture(312, 6, 24, "Badougoula", 0, 0, ""),
        // Tougué
        new SousPrefecture(313, 6, 25, "Fatako", 0, 0, ""),
        new SousPrefecture(314, 6, 25, "Fello Koundoua", 0, 0, ""),
        new SousPrefecture(315, 6, 25, "Kansangui", 0, 0, ""),
        new SousPrefecture(316, 6, 25, "Kolangui", 0, 0, ""),
        new SousPrefecture(317, 6, 25, "Kollet", 0, 0, ""),
        new SousPrefecture(318, 6, 25, "Konah", 0, 0, ""),
        new SousPrefecture(319, 6, 25, "Kouratongo", 0, 0, ""),
        new SousPrefecture(320, 6, 25, "Koïn", 0, 0, ""),
        new SousPrefecture(321, 6, 25, "Tangali", 0, 0, ""),
        new SousPrefecture(322, 6, 25, "Tougué-Centre", 0, 0, ""),
        // Dalaba
        new SousPrefecture(323, 7, 26, "Bodié", 0, 0, ""),
        new SousPrefecture(324, 7, 26, "Dalaba-Centre", 0, 0, ""),
        new SousPrefecture(325, 7, 26, "Ditinn", 0, 0, ""),
        new SousPrefecture(326, 7, 26, "Kaala", 0, 0, ""),
        new SousPrefecture(327, 7, 26, "Kankalabé", 0, 0, ""),
        new SousPrefecture(328, 7, 26, "Kébali", 0, 0, ""),
        new SousPrefecture(329, 7, 26, "Koba", 0, 0, ""),
        new SousPrefecture(330, 7, 26, "Mafara", 0, 0, ""),
        new SousPrefecture(331, 7, 26, "Mitty", 0, 0, ""),
        new SousPrefecture(332, 7, 26, "Mombéyah", 0, 0, ""),
        // Mamou
        new SousPrefecture(333, 7, 27, "Bouliwel", 0, 0, ""),
        new SousPrefecture(334, 7, 27, "Dounet", 0, 0, ""),
        new SousPrefecture(335, 7, 27, "Gongorèt", 0, 0, ""),
        new SousPrefecture(336, 7, 27, "Kégnéko", 0, 0, ""),
        new SousPrefecture(337, 7, 27, "Konkouré", 0, 0, ""),
        new SousPrefecture(338, 7, 27, "Mamou-Centre", 0, 0, ""),
        new SousPrefecture(339, 7, 27, "Nyagara", 0, 0, ""),
        new SousPrefecture(340, 7, 27, "Ouré-Kaba", 0, 0, ""),
        new SousPrefecture(341, 7, 27, "Porédaka", 0, 0, ""),
        new SousPrefecture(342, 7, 27, "Saramoussayah", 0, 0, ""),
        new SousPrefecture(343, 7, 27, "Soyah", 0, 0, ""),
        new SousPrefecture(344, 7, 27, "Téguéréya", 0, 0, ""),
        new SousPrefecture(345, 7, 27, "Timbo", 0, 0, ""),
        new SousPrefecture(346, 7, 27, "Tolo", 0, 0, ""),
        // Pita
        new SousPrefecture(347, 7, 28, "Bantignel", 0, 0, ""),
        new SousPrefecture(348, 7, 28, "Bourouwal-Tappé", 0, 0, ""),
        new SousPrefecture(349, 7, 28, "Donghol-Touma", 0, 0, ""),
        new SousPrefecture(350, 7, 28, "Gongorè", 0, 0, ""),
        new SousPrefecture(351, 7, 28, "Ley-Miro", 0, 0, ""),
        new SousPrefecture(352, 7, 28, "Maci", 0, 0, ""),
        new SousPrefecture(353, 7, 28, "Ninguélandé", 0, 0, ""),
        new SousPrefecture(354, 7, 28, "Pita-Centre", 0, 0, ""),
        new SousPrefecture(355, 7, 28, "Sangaréyah", 0, 0, ""),
        new SousPrefecture(356, 7, 28, "Sintali", 0, 0, ""),
        new SousPrefecture(357, 7, 28, "Timbi Madina", 0, 0, ""),
        new SousPrefecture(358, 7, 28, "Timbi-Touny", 0, 0, ""),
        // Conakry (Sous-préfectures de la zone spéciale)
        new SousPrefecture(359, 1, 1, "Dixinn", 0, 0, ""),
        new SousPrefecture(360, 1, 1, "Kaloum", 0, 0, ""),
        new SousPrefecture(361, 1, 1, "Matam", 0, 0, ""),
        new SousPrefecture(362, 1, 1, "Matoto", 0, 0, ""),
        new SousPrefecture(363, 1, 1, "Ratoma", 0, 0, ""),
        new SousPrefecture(364, 1, 1, "Kassa", 0, 0, "")
    };

        public LocationsController(
            UserCommandHandler userCommandHandler,
            ILogger<AuthController> logger)
        {            
            _logger = logger;
        }

        [HttpGet("regions/getall")]
        public async Task<IActionResult> GetAllLocations()
        {
            try
            {
                _logger.LogInformation("Received Get All Regions");
                //var userList = await _userCommandHandler.GetAllUsersHandle(CancellationToken.None);

                var res = new ApiResponse<IEnumerable<Region>>()
                {
                    Category = ApiResponseType.Success,
                    Data = Regions
                };


                return WrappeResponse(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving All Regions.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("prefectures/{regionId}")]
        public async Task<IActionResult> GetPrefectures(int regionId)
        {
            try
            {
                _logger.LogInformation("Received GetPrefecture by regionId");
                //var userList = await _userCommandHandler.GetAllUsersHandle(CancellationToken.None);

                var res = new ApiResponse<IEnumerable<Prefecture>>()
                {
                    Category = ApiResponseType.Success,
                    Data = Prefectures.Where(p => p.RegionId == regionId)
                };


                return WrappeResponse(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Prefecture by regionId.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("sous-prefectures/{prefectureId}")]
        public async Task<IActionResult> GetSousPrefectures(int prefectureId)
        {
            try
            {
                _logger.LogInformation("Received GetSousPrefectures by prefectureId");
                //var userList = await _userCommandHandler.GetAllUsersHandle(CancellationToken.None);

                var res = new ApiResponse<IEnumerable<SousPrefecture>>()
                {
                    Category = ApiResponseType.Success,
                    Data = SousPrefectures.Where(p => p.PrefectureId == prefectureId)
                };

                return WrappeResponse(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving SousPrefecture by regionId.");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
