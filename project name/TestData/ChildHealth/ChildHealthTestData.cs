using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace NSS_ChildHealth.TestData.ChildHealth
{
    class ChildHealthTestData
    {
		CultureInfo culture = new CultureInfo("en-GB");

		public string locationBasedTreatementCentre = "Immunisation Centre 1";
        public string healthVisitorBasedLocationCentre = "A6001";
        public string clientTreatmentHistoryTableNoTreamnetHistoryText = "No Immunisation/Treatment Centre and Caseload ID History for this client";

		public string schoolName = "Hallglen Nursery School";

		public string visionScreeningRecallLocation = "UM51 - Recall Location ";

		public string twoFieldsRequiredSearchValidationMessage = "You must populate at least 2 field(s) to perform a search.";
		public string noSearchCriteriaValidationMessage = "No search criteria have been entered. Please enter data to perform search.";

        public class User
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }

            public User(string username, string password, string role)
            {
                Username = username;
                Password = password;
                Role = role;
            }
        }

        public static User ChildHealthAdminUser = new User("nssuser10", "Password1", "SCTR002 - Child Health Administrator");

        public class Client
        {

            public string ChiNumber { get; }
            public string ClientIdentifier { get; }
            public string Fullname { get; }
            public string GiveName { get; }
            public string Surname { get; }
            public DateTime DateOfBirth { get; }
            public string ClientHealthBoard { get; }
            public string ClientsNurse { get; }
            public string ClientsGP { get;  }
			public string FullnameReverse { get;  }
			public string NhsNumber { get; }
			public string Sex { get; }
			public string Postcode { get; }
			public string FirstLineOfAddress { get; }

			public Client(string chiNumber, string clientIdentifier, string givenName, string surname, DateTime dateOfBirth, string clientHealthBoard, string clientsNurse, string clientsGP, string nhsNumber, string sex, string postcode, string firstLineOfAddress)
            {
                ChiNumber = chiNumber;
                ClientIdentifier = clientIdentifier;
                Fullname = givenName + " " + surname;
                GiveName = givenName;
                Surname = surname;
                DateOfBirth = dateOfBirth;
                ClientHealthBoard = clientHealthBoard;
                ClientsNurse = clientsNurse;
                ClientsGP = clientsGP;
				FullnameReverse = surname.ToUpper() + ", " + givenName;
				NhsNumber = nhsNumber;
				Sex = sex;
				Postcode = postcode;
				FirstLineOfAddress = firstLineOfAddress;
			}
         
        }

        //public static Client ClientActive = new Client("1006191011", "1000033", "Donald", "Oliphant", DateTime.Parse("10/06/2019"), null, null, null, "2345678901", "Male", "FK2 0EB");

		public static Client ClientActive = new Client("1006109412", "1000465", "Oliver", "Oldkid", DateTime.Parse("10 June 2010"), "Greater Glasgow and Clyde", null, null, "479/10/376", "Male", "G51 3DL", "847 Govan Road");

		public static Client CarerMum = new Client(null, "1000009", "Mummy", "Test", DateTime.Parse("01 January 2000"), null, null, null, null, "Female", null, null);


		public class NewClient
        {

            bool isMale;
            string dobChi;
            public string dob;
            int bornDaysInPastLimit = 1826;

            readonly string[] femaleForenames = { "Natalie", "Elaine", "Benidicta", "Emily", "Sophie", "Olivia", "Isla", "Jessica", "Ava", "Amelia", "Ella", "Lucy", "Lily", "Grace",
                                            "Chloe", "Freya", "Ellie", "Millie", "Emma", "Anna", "Eva", "Sophia", "Mia", "Charlotte", "Eilidh", "Ruby", "Hannah", "Aria", "Evie",
                                            "Georgia", "Poppy", "Erin", "Katie", "Holly", "Orla", "Layla", "Skye", "Rosie", "Harper", "Maisie", "Leah", "Zoe", "Daisy", "Amber",
                                            "Amy", "Hollie", "Isabella", "Niamh", "Molly" };

            readonly string[] maleForenames = { "Jack", "Oliver", "James", "Lewis", "Alexander", "Charlie", "Lucas", "Logan", "Harris", "Daniel", "Finlay", "Jacob", "Leo", "Mason",
                                            "Noah", "Harry", "Alfie", "Max", "Callum", "Aaron", "Adam", "Thomas", "Ethan", "Rory", "Cameron", "Archie", "Oscar", "Matthew", "Nathan",
                                            "Joshua", "Brodie", "William", "Liam", "Ryan", "Jamie", "Harrison", "Joseph", "Dylan", "Samuel", "Riley", "David", "Ollie", "Andrew", "Connor",
                                            "Luke", "Muhammad", "Jaxon", "Kyle", "Benjamin", "Michael" };

            readonly string[] surnames = { "Smith", "Brown", "Wilson", "Campbell", "Stewart", "Thomson", "Robertson", "Anderson", "Macdonald", "Scott", "Reid", "Murray", "Taylor", "Clark",
                                        "Ross", "Watson", "Morison", "Paterson", "Young", "Mitchell" };

            readonly string[] streetNames = { "High Street", "Station Road", "Main Street", "Park Road", "Church Road", "Church Street", "London Road", "Victoria Road", "Green Lane", "Manor Road",
                                        "Church Lane", "Park Avenue", "The Avenue", "The Crescent", "Queens Road", "New Road", "Grange Road", "Kings Road", "Kingsway", "Windsor Road", "Highfield Road",
                                        "Mill Lane", "Alexander Road", "York Road", "St. John’s Road", "Main Road", "Broadway", "King Street", "The Green", "Springfield Road", "George Street", "Park Lane",
                                        "Victoria Street", "Albert Road", "Queensway", "New Street", "Queen Street", "West Street", "North Street", "Manchester Road", "The Grove", "Richmond Road",
                                        "Grove Road", "South Street", "School Lane", "The Drive", "North Road", "Stanley Road", "Chester Road", "Mill Road" };

			readonly string[] postcodes = { "G51 1AA", "G51 1AB", "G51 1AD", "G51 1AE", "G51 1AF", "G51 1AG", "G51 1AH", "G51 1AJ", "G51 1AL", "G51 1AP", "G43 2DZ", "G43 2EA", "G43 2EB", "G43 2ED", "G43 2EE",
											"G43 2EF", "G43 2EG", "G43 2EP", "G43 2ER", "G43 2EY", "G13 4HL", "G13 4HN", "G13 4HP", "G13 4HQ", "G13 4HR", "G13 4HS", "G13 4HU", "G13 4HW", "G13 4HX", "G13 4HZ",
											"G13 4JA", "G13 4JB", "G13 4JD", "G13 4JE", "G34 0HW", "G34 0HY", "G34 0HZ", "G34 0JA", "G34 0JD", "G34 0JF", "G34 0JH", "G34 0JJ", "G34 0JL", "G34 0JY", "G34 0JZ",
											"G34 0LQ", "G34 0LR" };




			public string ChiNumber { get; }
            public string FullnameReverse { get; }
            public string GiveName { get; }
            public string Surname { get; }
            public string DateOfBirth { get; }            
            public string Sex { get; }
            public string AddressLine2 { get; }
            public string AddressLine4 { get; }
            public string Postcode { get; }
            public string TimeOfBirth { get; }
            public string PlaceOfBirth { get; }
            public string BirthWeight { get; }

            public NewClient()
            {
				SetSex();
				SetRandomDateOfBirth();

				Sex = GetSex();
				GiveName = GetForenameBasedOnSex();
				Surname = GetRandomSurname();
                FullnameReverse = Surname.ToUpper() + ", " + GiveName;
				DateOfBirth = GetDateOfBirth();
				ChiNumber = GetChi();
				AddressLine2 = GetAddressLine2();
                AddressLine4 = "Glasgow";
				Postcode = GetRandomPostcode();  //"G2 3BZ";
                TimeOfBirth = GetRandomTime();
                PlaceOfBirth = "GenChdBirthLocation";
                BirthWeight = GetRandomBirthWeight();

            }

			private string GetDateOfBirth()
			{
				return dob;
			}

			public void SetSex()
			{
				isMale = NextBoolean();
			}

			public string GetChi()
			{
				string secondPart;
				string chiNumber;
				int c;
				do
				{
					secondPart = RandomIntegerGenerate(10, 100).ToString() + GetSexBasedNumberAsString();
					c = CalculateCheckDigit(dobChi + secondPart);
				} while (c == 10);
				chiNumber = dobChi + secondPart + c.ToString();
				return chiNumber;
			}

			public int CalculateCheckDigit(string nineDigitChi)
			{
				int y = 10;
				int t = 0;
				int q;
				int c;
				List<int> indChiNumbers = new List<int>();
				for (int x = 0; x < 9; x++)
				{
					indChiNumbers.Add(y * (Int32.Parse(nineDigitChi.Substring(x, 1))));
					t = (y * (Int32.Parse(nineDigitChi.Substring(x, 1)))) + t;
					y -= 1;
				}
				q = t / 11;
				c = 11 * (q + 1) - t;
				if (c == 11)
					c = 0;

				return c;
			}

			public string GetSex()
			{

				if (isMale)
					return "Male";
				else
					return "Female";
			}

			public string GetForenameBasedOnSex()
			{
				if (isMale)
					return GetRandomMaleForename();
				else
					return GetRandomFemaleForename();
			}

			public string GetRandomFemaleForename()
			{
				string rndFemForename = femaleForenames[RandomIntegerGenerate(0, femaleForenames.Length)];
				return rndFemForename;
			}

			public string GetRandomMaleForename()
			{
				string rndMaleForename = maleForenames[RandomIntegerGenerate(0, maleForenames.Length)];
				return rndMaleForename;
			}

			public string GetRandomSurname()
			{
				string rndSurname = "Auto-" + surnames[RandomIntegerGenerate(0, surnames.Length)];
				return rndSurname;
			}

			public void SetRandomDateOfBirth()
			{
				int daysInPast = RandomIntegerGenerate(1, bornDaysInPastLimit);
				DateTime dobDate = DateTime.Today.AddDays(-daysInPast);
				dobChi = dobDate.ToString("ddMMyy");
				dob = dobDate.ToString("dd/MM/yyyy");

				//dob = DateTime.Today.AddDays(-daysInPast).ToString("ddMMyy");
			}

			public string GetSexBasedNumberAsString()
			{
				if (isMale)
					return GetRandomOddNumber().ToString();
				else
					return GetRandomEvenNumber().ToString();
			}

			public string GetAddressLine2()
			{
				return GetRandomStreetNumber() + " " + GetRandomStreetname();
			}

			public string GetRandomStreetNumber()
			{
				return RandomIntegerGenerate(1, 500).ToString();
			}

			public string GetRandomStreetname()
			{
				string rndStreetname = streetNames[RandomIntegerGenerate(0, streetNames.Length)];
				return rndStreetname;
			}

			public string GetRandomPostcode()
			{
				//string secondLast = GetRandomUpprcaseLetter();
				//Thread.Sleep(15);
				//string last = GetRandomUpprcaseLetter();
				string postcode;
				do
				{
					//postcode = "G" + RandomIntegerGenerate(1, 100) + " " + RandomIntegerGenerate(1, 10) + secondLast + last;
					postcode = postcodes[RandomIntegerGenerate(0, postcodes.Length)];
				} while (!IsPostcodeValid(postcode));
				return postcode;
			}

			private bool IsPostcodeValid(string postcode)
			{
				string expr = @"([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})";
				return Regex.IsMatch(postcode, expr);
			}

			public int GetRandomEvenNumber()
			{
				int rndNum;
				do
				{
					rndNum = RandomIntegerGenerate(2, 9);
				} while (rndNum % 2 != 0);
				return rndNum;
			}

			public string GetRandomTime()
            {
				string hour = RandomIntegerGenerate(1, 24).ToString("D2");
				string minutes = RandomIntegerGenerate(1, 60).ToString("D2");
				return hour +  ":" + minutes;
            }

			public string GetRandomBirthWeight()
            {
				string wholeKilos = RandomIntegerGenerate(3, 6).ToString();
				string decimalKilos = RandomIntegerGenerate(1, 100).ToString("D2");
				return wholeKilos + "." + decimalKilos;
            }

			public int GetRandomOddNumber()
			{
				int rndNum;
				do
				{
					rndNum = RandomIntegerGenerate(1, 10);
				} while (rndNum % 2 == 0);
				return rndNum;
			}

			public bool NextBoolean()
			{
				Random rnd = new Random();
				return Convert.ToBoolean(rnd.Next(0, 2));
			}

			public int RandomIntegerGenerate(int intFrom, int intTo)
			{
				Random rnd = new Random();
				return rnd.Next(intFrom, intTo);
			}

			public string GetRandomUpprcaseLetter()
			{
				string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
				int num = RandomIntegerGenerate(1, letters.Length);
				return letters[num].ToString();
			}
		}
        

    }
}
