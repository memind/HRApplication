namespace IKApplication.Persistance.Seed
{
    public static class SeedDataConstantsandMethods
    {
        public static Random _random = new Random();

        public static readonly List<string> MaleNames = new List<string>()
        {
            "Mehmet",
            "Mustafa",
            "Ahmet",
            "Ali",
            "Hüseyin",
            "Hasan",
            "İbrahim",
            "İsmail",
            "Yusuf",
            "Osman"
        };

        public static readonly List<string> FemaleNames = new List<string>()
        {
            "Fatma",
            "Ayşe",
            "Emine",
            "Hatice",
            "Zeynep",
            "Elif",
            "Meryem",
            "Şerife",
            "Zehra",
            "Sultan"
        };

        public static readonly List<string> Surnames = new List<string>()
        {
            "Yılmaz",
            "Kaya",
            "Demir",
            "Çelik",
            "Şahin",
            "Yıldız",
            "Yıldırım",
            "Öztürk",
            "Aydın",
            "Özdemir"
        };

        public static readonly List<string> Sectors = new List<string>()
        {
            "Ahşap Teknolojisi",
            "Bilişim",
            "Cam, Çimento ve ToprakÇevre",
            "Eğitim",
            "Elektrik ve Elektronik",
            "Enerji",
            "Finans",
            "Gıda",
            "İnşaat",
            "İş ve Yönetimi",
            "Kimya, Petrol, Lastik ve PLastik",
            "Kültür, Sanat ve Tasarım",
            "Maden",
            "Medya, İletişim ve Yayıncılık",
            "Metal",
            "Otomotiv",
            "Sağlık ve Sosyal Hizmetler",
            "Spor ve Rekreasyon",
            "Tarım, Avcılık ve Balıkçılık",
            "Tekstil, Hazır Giyim, Deri",
            "Ticaret (Satış ve Pazarlama)",
            "Toplumsal ve Kişisel Hizmetler",
            "Turizm, Konaklama, Yiyecek-İçecek Hizmetleri",
            "Ulaştırma, Lojistik ve Haberleşme"
        };

        public static readonly List<string> CompanyTypes = new List<string>()
        {
            "Anonim Şirketi",
            "Limited Şirketi",
            "Kollektif Şirketi",
            "Komandit Şirketi",
            "Kooperatif Şirketi"
        };

        public static readonly List<string> DomainNames = new List<string>()
        {
            "google.com",
            "outlook.com",
            "hotmail.com",
            "yahoo.com",
            "yandex.com",
            "microsoft.com"
        };

        public static readonly List<string> Professions = new List<string>()
        {
            "Accountant",
            "Actor/Actress",
            "Architect",
            "Astronomer",
            "Baker",
            "Bricklayer",
            "Busdriver",
            "Butcher",
            "Carpenter",
            "Chef/Cook",
            "Cleaner",
            "Dentist",
            "Designer",
            "Doctor",
            "Electrician",
            "Engineer",
            "Factory worker",
            "Farmer",
            "Fireman/Fire fighter",
            "Fisherman",
            "Florist",
            "Gardener",
            "Hairdresser",
            "Journalist",
            "Judge",
            "Lawyer",
            "Lecturer",
            "Librarian",
            "Lifeguard",
            "Mechanic",
            "Model",
            "Newsreader",
            "Nurse",
            "Optician",
            "Painter",
            "Pharmacist",
            "Photographer",
            "Pilot",
            "Plumber",
            "Politician",
            "Policeman/Policewoman",
            "Postman",
            "Real estate agent",
            "Receptionist",
            "Scientist",
            "Secretary",
            "Shop assistant",
            "Soldier",
            "Tailor",
            "Taxi driver",
            "Teacher",
            "Translator",
            "Traffic warden",
            "Travel agent",
            "Veterinary doctor(Vet)",
            "Waiter/Waitress",
            "Businessman",
            "Dancer",
            "Artist",
            "Bartenders",
        };

        public static readonly List<string> Titles = new List<string>()
        {
            "VP of Sales",
            "National Sales Director",
            "Regional Sales Manager",
            "Sales Representative",
            "Sales Associate",
            "CMO (Chief Marketing Officer)",
            "Marketing Director",
            "Sr. Marketing Manager",
            "Marketing Analyst",
            "Marketing Coordinator",
            "VP of Finance",
            "Procurement Director",
            "Investment Analyst",
            "Credit Analyst",
            "Risk Analyst",
            "VP of Client Services",
            "Account Manager",
            "Customer Success Manager",
            "Customer Service Representative",
            "Support Specialist",
            "COO (Chief Operating Officer)",
            "Director of Business Operations",
            "Operations Supervisor",
            "Sr. Manager of HR",
            "HR Analyst",
            "Director of Information Security",
            "Software Engineer I, II, III",
            "Full Stack Developer",
            "Systems Administrator",
            "Data Analyst",
            "Other Industries:",
            "Registered Nurse",
            "Pharmacy Technician",
            "Physical Therapist",
            "Nursing Assistant",
            "Clinical Laboratory Technician",
            "Occupational Therapy Aide",
            "Administrator",
            "Principal",
            "Registrar",
            "School Counselor",
            "Teacher",
            "Teaching Assistant",
            "General Manager",
            "Guest Services Supervisor",
            "Concierge",
            "Front Desk Associate",
            "Server/Host/Hostess",
            "Hotel Receptionist",
            "Construction Foreman",
            "Safety Director",
            "Project Manager",
            "Contract Administrator",
            "Project Appraisal Engineer",
            "Inspector",
        };

        public static string IdNumber;
        public static int oddSum, evenSum;

        public static string IdNumberGenerator()
        {
            IdNumber = "";
            oddSum = 0;
            evenSum = 0;

            oddSum += _random.Next(8) + 1;
            IdNumber += oddSum;

            for (int i = 2; i < 10; i++)
            {
                int number = _random.Next(9);
                IdNumber += number;
                if (i % 2 == 0)
                    evenSum += number;
                else
                    oddSum += number;
            }

            IdNumber += (oddSum * 7 + evenSum * 9) % 10;
            IdNumber += oddSum * 8 % 10;

            return IdNumber;
        }

        public static DateTime startDate => DateTime.Now.AddYears(18);

        public static DateTime randomDate()
        {
            return startDate.AddDays(_random.Next(35 * 365));
        }

        public static string PhoneNumberGenerator()
        {
            return "+905" + _random.Next(100000000, 999999999);
        }

        public static string StringGenerator(int length)
        {
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string result = "";

            for (int i = 0; i < length; i++)
            {
                int x = _random.Next(str.Length);
                result += str[x];
            }

            return result;
        }
    }
}
