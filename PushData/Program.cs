using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PushData {
    public class HorseShort {
        [Key]
        // name breed sex mid fid date suit height born number curr
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public bool Sex { get; set; }
        public int Mid { get; set; }
        public int Fid { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public DateTime Date { get; set; }
        public string Suit { get; set; }
        public int Height { get; set; }
        public string CountryBorn { get; set; }
        public string Breeder { get; set; }
        public int Ownerid { get; set; }
        public string CurrentCountry { get; set; }
        [NotMapped]
        public List<HorseShort> Progeny { get; set; }
        public string BookNumber { get; set; }
        public object this[string fieldName]
        {
            get
            {
                var field = this.GetType().GetProperty(fieldName);
                var result = field.GetValue(this);
                return result;
            }

        }

        public string GetDate()
        {
            return $"{Date.ToString("yyyy.MM.dd")}";
        }
    }
    public class MainContext : DbContext
    {
        public DbSet<HorseShort> Horses => Set<HorseShort>();
        private string _path;
        public MainContext(string path)
        {
            _path = path;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_path}");
        }
    }
    public class HorseCreator
    {
        int _count;
        List<HorseShort> list;
        string breedWithSpace = "Абиссинская Абстанг Абтенайская Авелинская Австралийская пастушья Австралийская полукровная Австралийская тяжелоупряжная Австрийская полукровная Адаевская Азорская Азербайджанская Албанская Алтайская Альтер-реал Американская верховая Австрийская полукровная Американский квотерхорс Американская кремовая Американский кучерявый башкир Американский пейнтхорс Американская стандартбредная Андалузская Англо-арабская Аппалуза Ара-Аппалуза Арабская Арденская Аргентинская Арьежуаз Ауксуа Ацтекская Баварская полукровная Балеарская Белорусская упряжная Бельгийская полукровная Берберийская Башкирская Битюг Боснийская Брабансон Бразильская спортивная Брамби Бранденбургская Бретонская Будённовская Булонская Великопольская Венгерская полукровная Вестфальская Владимирский тяжеловоз Восточноболгарская Вюртембергская Вятская Ганноверская Гафлинская Гидран Голландская Голштинская Гонтер Гронингенская Дагестанская Датская полукровная Делибоз Дестриэ Джэбе Донская Жемайтская Забайкальская Иберийская Ирландская спортивная Ирландская тяжелоупряжная Исландская Почтовая марка Кабардинская Казахская Калмыцкая Камаргская Камполина Канадская Катхиавари Карабаирская Карабахская Карачаевская Каспийская Кигер-мустанг Кински Кишбер Киргизская Кладрубская Клейдесдаль Клеппер Кливлендская гнедая Крестьянская Кнабструпская Комтойс Коник польский Колорадо-рейнджер Креольская Кубинский иноходец Кустанайская Кушумская Латвийская Липпицианская Литовский тяжеловоз Локайская Лошадь Скалистых гор Лузитанская Малопольская Мангаларга Мареммано Марвари Мезенская Мекленбургская Миссурийский фокстроттер Монгольская Морган Мустанг Новоалександровская тяжелоупряжная Новоалтайская Новокиргизская Нониус Ольденбургская Орловский рысак Пасо-фино Перуанский пасо Першерон Печорская Пинцгауская Польский коник Польский тяжеловоз Португальская спортивная Приобская Русская верховая Русский рысак Русский тяжеловоз Североамериканская башкирская курчавая лошадь Советская тяжелоупряжная Соррайя Старая фламандская Суффолькская Тавдинская Татарская Теннессийская прогулочная Терская Тракененская Трэйт дю Норд Украинская верховая Уэльский коб Финская Флоридский крэкер Французский англо-араб Французский рысак Французский сель Фредериксборгская Фризская Фризская спортивная Фьордская Хакнэ Цыганская Чилийская Чистокровная верховая Шагия Шайрская Шварцвальдская лошадь Шведка Шленская Ютландская Эфиопская Якутская";
        string suitsWithSpace = "Гнедая Серая Белая Рыжая Воронаня Соловая Бурая Чалая";
        string namesWithSpace = "Аарон Аббас  Абд аль-Узза Абдуллах  Абид  Аботур Аввакум Август  Авдей Авель  Авигдор Авксентий Авл Авнер Аврелий Автандил Автоном  Агапит Агафангел Агафодор Агафон Агриппа  Адам Адам  Адиль Адольф  Адонирам Адриан  Азамат  Азат Азиз  Азим Айварс Айдар  Акакий Аквилий Акиндин Акиф Акоп Аксель  Алан Алан Аланус Александр Алексей Алик  Алим  Алипий Алишер Алоиз Альберик Альберт Альбин Альваро Альвиан Альвизе Альфонс Альфред Амадис Амвросий Амедей Амин  Амир  Амр  Анания  Анас  Анастасий Анатолий Андокид Андрей Андроник Аникита Аннерс Анри Ансельм Антипа Антон Антоний Антонин Арам  Арефа Арзуман Аристарх Ариф Аркадий Арсен Арсений Артём  Артемий Артур Арфаксад  Архипп Атанасий Аттик Афанасий Афинагор Афиней Африкан Ахилл  Ахмад  Ахтям Ашот Бадр  Барни Бартоломео Басир Бахтияр Бен  Бехруз Билял Богдан Болеслав Болот  Бонавентура  Борис Борислав  Боян  Бронислав Брячислав Булат  Бурхан  Бямбасурэн В Вадим Валентин Валерий Валерьян Вальдемар Вангьял Варлам Варнава Варсонофий Варфоломей  Василий Вахтанг Велвел Велимир Венансио Вениамин  Венцеслав Верослав Викентий  Виктор  Викторин Вильгельм Винцас Виссарион Виталий Витаутас  Вито Владимир  Владислав Владлен Влас Волк  Володарь Вольфганг Вописк Всеволод Всеслав Вук  Вукол Вышеслав Вячеслав Г Габриеле Гавриил Гай Галактион Гарет Гаспар  Гафур Гвидо Гейдар  Геласий Гельмут Геннадий Генри  Генрих Георге Георгий Гераклид Герберт Герман Германн Геронтий Герхард Гессий Гильем Гинкмар Глеб Гней Гонорий  Горацио Гордей Гостомысл  Градислав Григорий Гримоальд Груди  Гуго Гьялцен";
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string country= "Абхазия Австралия Австрия Азад-Кашмир Азербайджан Албания Алжир Ангилья Ангола Андорра Антигуа Барбуда Аргентина Армения Аруба Афганистан Багамские Острова Бангладеш Барбадос Бахрейн Белиз Белоруссия Бельгия Бенин Болгария Боливия Босния Герцеговина Ботсвана Бразилия Бруней Буркина-Фасо Бурунди Бутан Вануату Ватикан Великобритания Венгрия Венесуэла Восточный Тимор Вьетнам Габон Гаити Гайана Гамбия Гана Гватемала Гвинея Гвинея-Бисау Германия Гондурас Гонконг Государство Палестина Гренада Гренландия Греция Грузия Дания Демократическая Республика КонгоДжибути Доминика Доминиканская Республика Египет Замбия Зимбабве Израиль Индия Индонезия Иордания Ирак Иран Ирландия Исландия Испания Италия ЙеменКабо-Верде Казахстан Камбоджа Камерун Канада Катар Кения Кипр Киргизия Кирибати Китай КНДР(Северная Корея) Колумбия Коморские Острова Косово Коста-Рика Кот-д’Ивуар Куба Кувейт Кюрасао Лаос Латвия Лесото Либерия Ливан Ливия Литва Лихтенштейн Люксембург Маврикий Мавритания Мадагаскар Македония Малави Малайзия Мали Мальдивы Мальта Марокко Маршалловы Острова Мексика Микронезия Мозамбик Молдавия Монако Монголия Мьянма Нагорно-Карабахская Республика Намибия Науру Непал Нигер Нигерия Нидерланды Никарагуа Ниуэ Новая Зеландия Норвегия Объединённые Арабские Эмираты ОманОстрова Кука Пакистан Палау Панама Папуа–Новая Гвинея Парагвай Перу Польша Португалия Пуэрто-Рико Республика Конго Россия Руанда Румыния Сальвадор СамоаСан-МариноСан-Томе Принсипи Саудовская Аравия Сахарская Арабская Демократическая Республика Свазиленд Северный Кипр Сейшельские Острова СенегалСент-Винсент Гренадины Сент-Китс НевисСент-Люсия Сербия Сингапур Синт-Мартен Сирия Словакия Словения Соединённые Штаты Америки Соломоновы Острова Сомали Судан Суринам Сьерра-Леоне Таджикистан Таиланд Танзания Того Тонга Тринидад Тобаго Тувалу Тунис Туркмения Турция Уганда Узбекистан Украина Уругвай Фареры Фиджи Филиппины Финляндия Франция Хорватия Центральноафриканская Республика Чад Черногория Чехия Чили Швейцария Швеция Шри-Ланка Эквадор Экваториальная Гвинея Эритрея Эстония Эфиопия Южная Корея Южная Осетия Южно-Африканская Республика Южный Судан Ямайка Япония";
        List<string> breeds;
        List<string> suits;
        List<string> names;
        List<string> countries;
        Random random;
        public HorseCreator(int count)
        {
            _count = count;
            list = new List<HorseShort>();
            breeds = breedWithSpace.Split(" ").Distinct().ToList();
            suits = suitsWithSpace.Split(" ").ToList();
            while (namesWithSpace.Contains("  "))
                namesWithSpace = namesWithSpace.Replace("  ", " ");
            names = namesWithSpace.Split(" ").ToList();
            countries = country.Split(" ").ToList();
            random = new Random(DateTime.Now.Millisecond);
        }

        public List<HorseShort>GetHorseList()
        {
            Console.WriteLine("Start Generating");
            for(int i = 0; i < _count; i++)
            {
                HorseShort horse = new HorseShort()
                {
                    //  mid fid  
                    Id= i+1,
                    Name = GenerateName(),
                    Breed= GenerateBreed(),
                    Sex=GenerateSex(),
                    Suit=GenerateSuit(),
                    BookNumber=GenerateBookNumber(),
                    CountryBorn=GenerateCountryBorn(),
                    CurrentCountry=GenerateCountryBorn(),
                    Date=GenerateDate(),
                    Height=GenerateHeight(),
                };
                horse.Mid = GenerateMom();
                horse.Fid = GenerateFather();
                if(horse.Mid!=-1)
                {
                    horse.MName = list.Find(x => x.Id == horse.Mid).Name;
                }
                else
                {
                    horse.MName = "";
                }
                if (horse.Fid != -1)
                {
                    horse.FName = list.Find(x => x.Id == horse.Fid).Name;
                }
                else
                {
                    horse.FName = "";
                }
                Console.WriteLine($"Generating {i}/{_count}");
                list.Add(horse);
            }
            return list;
        }
        string GenerateName()
        {
           
            return names[random.Next(0, names.Count-1)];
        }
        string GenerateBreed()
        {
            
            return breeds[random.Next(0, breeds.Count-1)];
        }
        bool GenerateSex()
        {
            return Convert.ToBoolean(random.Next(0, 2));
        }
        int GenerateMom()
        {
            var fList = list.Where(x => x.Sex == false);
            int count = fList.Count();
            if (count > 100)
            {

                return fList.ToList()[random.Next(0, count-1)].Id;
            }
            return -1;
        }
        int GenerateFather()
        {
            var fList = list.Where(x => x.Sex == true);
            int count = fList.Count();
            if (count > 100)
            {

                return fList.ToList()[random.Next(0, count-1)].Id;
            }
            return -1;
        }
        DateTime GenerateDate()
        {

            return new DateTime(random.Next(1960,2015),random.Next(1,12),random.Next(1,28));
        }
        string GenerateSuit()
        {

            return suits[random.Next(0, suits.Count-1)];
        }
        int GenerateHeight()
        {

            return random.Next(110, 140);
        }
        string GenerateCountryBorn()
        {

            return countries[random.Next(0, countries.Count - 1)];
        }
        string GenerateBookNumber()
        {
            string number = "";
            do
            {
                number = $"{chars[random.Next(0, chars.Length - 1)]}{chars[random.Next(0, chars.Length - 1)]}{random.Next(1000000, 9999999)}";
            } while (list.Any(x=> x.BookNumber==number));
            return number;
        }
    }
    class Program {
        public static void Main()
        {
            try
            {
                Console.WriteLine("Enter path to DB:");
                string path = Console.ReadLine();
                Console.WriteLine("Eneter count of Horse:");
                int count = Convert.ToInt32(Console.ReadLine());
                if (path == "default")
                {
                    path = "D:\\HorseBucket\\HorseSite\\horsessiteDB.db";
                }
                Console.WriteLine($"Db open by path:{path}");
                HorseCreator creator = new HorseCreator(count);
                var db = new MainContext(path);
                foreach (var id in db.Horses.Select(e => e.Id))
                {
                    var entity = new HorseShort { Id = id };
                    db.Horses.Attach(entity);
                    db.Horses.Remove(entity);
                }
                db.SaveChanges();
                Console.WriteLine("All data removed succes");
                Console.WriteLine("Press any key");
                Console.ReadKey();
                int i = 1;
                var horseList = creator.GetHorseList();
                Console.WriteLine("Start add to database");
                horseList.ForEach(x =>
                {
                    db.Horses.Add(x);
                    Console.WriteLine($"Added: {i++}/{count}");

                });
                db.SaveChanges();
                Console.WriteLine("DB Savechange");
                Console.WriteLine("Succes");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        
    }
}