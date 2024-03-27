using ISTL.COMMON.Network;
using ISTL.MODELS.DTO.New.Lookup;
using ISTL.RAB.View;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Entity
{
    public class ComboBoxItems
    {
        public static Dictionary<string, string> arrestedByAgency = new Dictionary<string, string>();
        public static Dictionary<string, string> disposal = new Dictionary<string, string>();
        public static Dictionary<string, string> bloodGroup = new Dictionary<string, string>();
        public static Dictionary<string, string> educationStatus = new Dictionary<string, string>();
        public static Dictionary<string, string> eyeColor = new Dictionary<string, string>();
        public static Dictionary<string, string> maritalStatus = new Dictionary<string, string>();
        public static Dictionary<string, string> relation = new Dictionary<string, string>();
        public static Dictionary<string, string> gender = new Dictionary<string, string>();
        public static Dictionary<string, string> occupation = new Dictionary<string, string>();
        public static Dictionary<string, string> religion = new Dictionary<string, string>();
        public static Dictionary<string, string> priorityListGovt = new Dictionary<string, string>();
        public static Dictionary<string, string> crimeType = new Dictionary<string, string>();
        public static Dictionary<string, string> politicalGroup = new Dictionary<string, string>();
        public static Dictionary<int, string> arresteeType = new Dictionary<int, string>();
        public static Dictionary<string, string> specialCrimeType = new Dictionary<string, string>();
        public static Dictionary<int, string> reportType = new Dictionary<int, string>();
        public static Dictionary<int, string> genders = new Dictionary<int, string>();
        public static Dictionary<string, string> ageRanges = new Dictionary<string, string>();
        public static Dictionary<string, string> warrantType = new Dictionary<string, string>();
        public static Dictionary<string, string> biometricPositions = new Dictionary<string, string>();
        public static Dictionary<string, string> ioRank = new Dictionary<string, string>();
        public static Dictionary<string, string> notEntryReason = new Dictionary<string, string>();
        public static Dictionary<int, string> profileType = new Dictionary<int, string>();
        public static Dictionary<string, string> caseType = new Dictionary<string, string>();

        static ComboBoxItems()
        {
            LoadArrestAgency();
            LoadDisposal();
            LoadBloodGroup();
            LoadEducationStatus();
            LoadEyeColor();
            LoadMaritalStatus();
            LoadRelation();
            LoadGender();
            LoadOccupation();
            LoadReligion();
            LoadPriorityListGovt();
            LoadCrimeType();
            LoadPoliticalGroup();
            LoadArresteeType();
            LoadSpecialCrimeType();
            LoadReportType();
            LoadGenders();
            LoadAgeRange();
            LoadWarrantType();
            LoadBiometricPositions();
            LoadIORank();
            LoadNotEntryReason();
            LoadProfileManagementProfileType();
            LoadNotEntryCaseType();
        }

        private static void LoadReportType()
        {
            reportType.Add(1, "Daily Enrollment Report"); // all disable except unitn and subunit and set current date as system date
            reportType.Add(2, "Criminal Report"); // disable arrest type
            //reportType.Add(3, "Special Criminal Report"); // disable age range
            reportType.Add(4, "Crime Type Wise Report"); // disable all fields except unit and sub-unit
        }

        private static void LoadGenders()
        {
            genders.Add(1, "Male");
            genders.Add(2, "Female");
            genders.Add(3, "Other");
        }

        private static void LoadAgeRange()
        {
            ageRanges.Add("below20", "Below 20");
            ageRanges.Add("twentyToThirty", "20 to 30");
            ageRanges.Add("thirtyToFourty", "30 to 40");
            ageRanges.Add("fourtyToFifthi", "40 to 50");
            ageRanges.Add("aboveFifthi", "Above 50");
        }

        private static void LoadBiometricPositions()
        {
            biometricPositions.Add("IrisBoth", "Iris Both");
            biometricPositions.Add("IrisRight", "Iris Right");
            biometricPositions.Add("IrisLeft", "Iris Left");
        }

        private static void LoadIORank()
        {
            ioRank.Add("si", "SI");
            ioRank.Add("siOperation", "Insp (Operation)");
            ioRank.Add("siInvestigation", "Insp (Investigation)");
            ioRank.Add("oc", "OC");
            ioRank.Add("asp", "ASP");
            ioRank.Add("addlAsp", "Addl SP");
        }

        private static void LoadWarrantType()
        {
            warrantType.Add("GR", "GR");
            warrantType.Add("CR", "CR");
        }

        private static void LoadArrestAgency()
        {
            arrestedByAgency.Add("police", "Police");
            arrestedByAgency.Add("rab", "RAB");
            arrestedByAgency.Add("cid", "CID");
            arrestedByAgency.Add("db", "DB");
        }
        private static void LoadDisposal()
        {
            disposal.Add("Bail", "Bail");
            disposal.Add("Discharged", "Discharged");
            disposal.Add("UnderTrial", "Under Trial");
        }
        private static void LoadBloodGroup()
        {
            bloodGroup.Add("A_Plus", "A+");
            bloodGroup.Add("A_Minus", "A-");
            bloodGroup.Add("B_Plus", "B+");
            bloodGroup.Add("B_Minus", "B-");
            bloodGroup.Add("AB_Plus", "AB+");
            bloodGroup.Add("AB_Minus", "AB-");
            bloodGroup.Add("O_Plus", "O+");
            bloodGroup.Add("O_Minus", "O-");
        }
        private static void LoadEducationStatus()
        {
            educationStatus.Add("NoEducation", "No Education");
            educationStatus.Add("Primary", "Primary");
            educationStatus.Add("PassedClassFive", "Passed Class Five");
            educationStatus.Add("PassedClassEight", "Passed Class Eight");
            educationStatus.Add("SSC", "SSC");
            educationStatus.Add("HSC", "HSC");
            educationStatus.Add("Undergraduate", "Undergraduate");
            educationStatus.Add("Graduate", "Graduate");
            educationStatus.Add("PhD", "PhD");
            educationStatus.Add("Alim", "Alim");
            educationStatus.Add("Dakhil", "Dakhil");
            educationStatus.Add("Fazil", "Fazil");
            educationStatus.Add("Vocational", "Vocational");
            educationStatus.Add("Politechnical", "Politechnical");
            educationStatus.Add("Other", "Other");
        }
        private static void LoadEyeColor()
        {
            eyeColor.Add("Black", "Black");
            eyeColor.Add("Blue", "Blue");
            eyeColor.Add("Brown", "Brown");
        }
        private static void LoadMaritalStatus()
        {
            maritalStatus.Add("None", "Select Marital Status");
            maritalStatus.Add("Single", "Single");
            maritalStatus.Add("Married", "Married");
            maritalStatus.Add("Widowed", "Widowed");
            maritalStatus.Add("Divorced", "Divorced");
        }
        private static void LoadRelation()
        {
            relation.Add("father", "Father");
            relation.Add("mother", "Mother");
            relation.Add("brother", "Brother");
            relation.Add("sister", "Sister");
            relation.Add("daughter", "Daughter");
            relation.Add("son", "Son");
            relation.Add("spouse", "Spouse");
            relation.Add("other", "Other");
        }
        private static void LoadGender()
        {
            gender.Add("Male", "Male");
            gender.Add("Female", "Female");
            gender.Add("Other", "Other");
        }
        private static void LoadOccupation()
        {
            occupation.Add("GovtService", "Govt Service");
            occupation.Add("PrivateService", "Private Service");
            occupation.Add("Doctor", "Doctor");
            occupation.Add("Driver", "Driver");
            occupation.Add("Engineer", "Engineer");
            occupation.Add("Teacher", "Teacher");
            occupation.Add("Lawyer", "Lawyer");
            occupation.Add("Banker", "Banker");
            occupation.Add("Business", "Business");
            occupation.Add("FarmWorker", "Farm Worker");
            occupation.Add("Farmer", "Farmer");
            occupation.Add("Student", "Student");
            occupation.Add("Housewife", "Housewife");
            occupation.Add("DayLabour", "Day Labour");
            occupation.Add("None", "None");
            occupation.Add("Other", "Other");
        }
        private static void LoadReligion()
        {
            religion.Add("None", "Select Religion");
            religion.Add("muslim", "Muslim");
            religion.Add("hindu", "Hindu");
            religion.Add("christian", "Christian");
            religion.Add("buddhist", "Buddhist");
        }

        private static void LoadPriorityListGovt()
        {
            priorityListGovt.Add("1", "None");
            priorityListGovt.Add("2", "Middle");
            priorityListGovt.Add("3", "Top Terror");
            priorityListGovt.Add("4", "Top 30 Selected by Bn");
            priorityListGovt.Add("5", "Wanted");
        }

        private static void LoadCrimeType()
        {
            crimeType.Add("0", "Select Crime Type");
            crimeType.Add("1", "Abduction (অপহরণ)");
            crimeType.Add("2", "Sedition (রাষ্ট্রদ্রোহ)");
            crimeType.Add("3", "Antique Smuggling (প্রত্নতত্ত্ব পাচার)");
            crimeType.Add("4", "Arms Act (অস্ত্র মামলা)");
            crimeType.Add("45", "Arrest Warrant (গ্রেফতারি পরোয়ানা)");
            crimeType.Add("5", "Attempt to Murder (হত্যার চেষ্টা)");
            crimeType.Add("6", "Breach of Trust (বিশ্বাস ভঙ্গ করা)");
            crimeType.Add("7", "Counterfieting Coin and Stamp (মুদ্রা ও স্ট্যাম্প জালিয়াতি)");
            crimeType.Add("8", "Criminal Force and Assault (অপরাধমূলক শক্তি প্রয়োগ)");
            crimeType.Add("9", "Culpable Homicide (নিন্দনীয় নরহত্যা)");
            crimeType.Add("10", "Cyber Crime (সাইবার অপরাধ)");
            crimeType.Add("11", "Death by Negligence (অবহেলাজনিত কারনে মৃত্যু)");
            crimeType.Add("46", "Defendant in FIR (এজাহারভুক্ত আসামী)");
            crimeType.Add("12", "Muggers (ছিনতাইকারী)");
            crimeType.Add("13", "Drug Trafficiking (মাদক সংক্রান্ত মামলা)");
            crimeType.Add("14", "Explosive Act (বিস্ফোরক সংক্রান্ত মামলা)");
            crimeType.Add("15", "Extortion (চাঁদাবাজি মামলা)");
            crimeType.Add("16", "Extremism (মৌলবাদ সংক্রান্ত মামলা)");
            crimeType.Add("17", "False Evidences (মিথ্যা সাক্ষ্যদান সংক্রান্ত মামলা)");
            crimeType.Add("18", "Force Labour (বলপ্রয়োগকারী সংক্রান্ত মামলা)");
            crimeType.Add("19", "Forgery (জালিয়াতি মামলা)");
            crimeType.Add("44", "Fraud Case (প্রতারণা সংক্রান্ত মামলা)");
            crimeType.Add("20", "Gambling (জুয়া)");
            crimeType.Add("21", "Human Trafficking (মানবপাচার)");
            crimeType.Add("22", "Harm (ক্ষতি)");
            crimeType.Add("43", "ICT Act (আইসিটি আইন​)");
            crimeType.Add("47", "Kishor Gang (কিশোর গ্যাং)");
            crimeType.Add("23", "Murder (হত্যা)");
            crimeType.Add("24", "Rape (ধর্ষণ)");
            crimeType.Add("25", "Riot (দাঙ্গা)");
            crimeType.Add("26", "Robbery (ডাকাতি)");
            crimeType.Add("27", "Sea Robber (জলদস্যু)");
            crimeType.Add("28", "Terrorism (সন্ত্রাসবাদ)");
            crimeType.Add("29", "Theft (চোরাচালান সংক্রান্ত মামলা)");
            crimeType.Add("30", "Wanderer (বনদস্যু)");
            crimeType.Add("31", "Women And Child Abuse (নারী ও শিশু নির্যাতন)");
            crimeType.Add("32", "Shorbohara (সর্বহারা)");
            crimeType.Add("33", "Jongi Activity(Ansar Al Islam) (আনসার-আল-ইসলাম)");
            crimeType.Add("34", "Jongi Activity(Ansarullah Bangla Team) (আনসারুল্লাহ-বাংলা-টিম)");
            crimeType.Add("35", "Jongi Activity(Harkatul Jihad) (হরকাতুল জিহাদ)");
            crimeType.Add("36", "Jongi Activity(Hizbut Tahrir) (হিযবুত তাহরীর)");
            crimeType.Add("37", "Jongi Activity(JMJB) (জাগ্রত-মুসলিম-জনতা-বাংলাদেশ)");
            crimeType.Add("38", "Jongi Activity(JMB) (জামাতুল-মুজাহিদিন বাংলাদেশ)");
            crimeType.Add("39", "Jongi Activity(Sahadat Al Hikma) (শাহাদাত-আল-হিকমা)");
            crimeType.Add("40", "Jongi Activity(Allahar Dhol) (আল্লাহার দল)");
            crimeType.Add("41", "Others (অন্যান্য)");
            crimeType.Add("42", "Jongi Activity (জঙ্গী)");
        }

        private static void LoadPoliticalGroup()
        {
            politicalGroup.Add("AwamiLeague", "Bangladesh Awami League");
            politicalGroup.Add("BNP", "Bangladesh Nationalist Party");
            politicalGroup.Add("JatiyaParty_Ershad", "Jatiya Party (Ershad)");
            politicalGroup.Add("WorkersPartyofBangladesh", "Workers Party of Bangladesh");
            politicalGroup.Add("JatiyaSamajtantrikDal", "Jatiya Samajtantrik Dal");
            politicalGroup.Add("BikalpaDharaBangladesh", "Bikalpa Dhara Bangladesh");
            politicalGroup.Add("GanoForum", "Gano Forum");
            politicalGroup.Add("JatiyaParty_Manju", "Jatiya Party (Manju)");
            politicalGroup.Add("BangladesTarikatFederation", "Bangladesh Tarikat Federation");
        }

        private static void LoadArresteeType()
        {
            arresteeType.Add(1, "Mobile Courts");
            //arresteeType.Add(2, "Warrants");
            //arresteeType.Add(3, "Gamblers");
            arresteeType.Add(2, "Contagious Patients");
            arresteeType.Add(3, "Direct Submit In PS");
        }

        private static void LoadSpecialCrimeType()
        {
            specialCrimeType.Add("", "Select Crime Type");
            specialCrimeType.Add("Abduction", "Abduction (অপহরণ)");
            specialCrimeType.Add("Sedition", "Sedition (রাষ্ট্রদ্রোহ)");
            specialCrimeType.Add("AntiqueSmuggling", "Antique Smuggling (প্রত্নতত্ত্ব পাচার)");
            specialCrimeType.Add("ArmsAct", "Arms Act (অস্ত্র মামলা)");
            specialCrimeType.Add("arrestWarrant", "Arrest Warrant (গ্রেফতারি পরোয়ানা)");
            specialCrimeType.Add("AttempToMurder", "Attempt to Murder (হত্যার চেষ্টা)");
            specialCrimeType.Add("BreachOfTrust", "Breach of Trust (বিশ্বাস ভঙ্গ করা)");
            specialCrimeType.Add("CounterfietingCoinandStamp", "Counterfieting Coin and Stamp (মুদ্রা ও স্ট্যাম্প জালিয়াতি)");
            specialCrimeType.Add("CriminalForceandAssault", "Criminal Force and Assault (অপরাধ মূলক শক্তি প্রয়োগ)");
            specialCrimeType.Add("CulpableHomicide", "Culpable Homicide (নিন্দনীয় নরহত্যা)");
            specialCrimeType.Add("CyberCrime", "Cyber Crime (সাইবার অপরাধ)");
            specialCrimeType.Add("DeathByNegligence", "Death by Negligence (অবহেলাজনিত কারনে মৃত্যু)");
            specialCrimeType.Add("defendantInFIR", "Defendant in FIR (এজাহারভুক্ত আসামী)");
            specialCrimeType.Add("Muggers", "Muggers (ছিনতাইকারী)");
            specialCrimeType.Add("DrugTrafficiking", "Drug Trafficiking (মাদক সংক্রান্ত মামলা)");
            specialCrimeType.Add("ExplosiveAct", "Explosive Act (বিস্ফোরক সংক্রান্ত মামলা)");
            specialCrimeType.Add("Extortion", "Extortion (চাঁদাবাজি মামলা)");
            specialCrimeType.Add("Extremism", "Extremism (মৌলবাদ সংক্রান্ত মামলা)");
            specialCrimeType.Add("FalseEvidences", "False Evidences (মিথ্যা সাক্ষ্যদান সংক্রান্ত মামলা)");
            specialCrimeType.Add("ForceLabour", "Force Labour (বলপ্রয়োগকারী সংক্রান্ত মামলা)");
            specialCrimeType.Add("Forgery", "Forgery (জালিয়াতি মামলা)");
            specialCrimeType.Add("fraudCase", "Fraud Case (প্রতারণা সংক্রান্ত মামলা)");
            specialCrimeType.Add("Gambling", "Gambling (জুয়া)");
            specialCrimeType.Add("HumanTafficking", "Human Trafficking (মানবপাচার)");
            specialCrimeType.Add("Harm", "Harm (ক্ষতি)");
            specialCrimeType.Add("iCTAct", "ICT Act (আইসিটি আইন​)");
            specialCrimeType.Add("kishorGang", "Kishor Gang (কিশোর গ্যাং)");
            specialCrimeType.Add("Murder", "Murder (হত্যা)");
            specialCrimeType.Add("Rape", "Rape (ধর্ষণ)");
            specialCrimeType.Add("Riot", "Riot (দাঙ্গা)");
            specialCrimeType.Add("Robbery", "Robbery (ডাকাতি)");
            specialCrimeType.Add("SeaRobber", "Sea Robber (জলদস্যু)");
            specialCrimeType.Add("Terrorism", "Terrorism (সন্ত্রাসবাদ)");
            specialCrimeType.Add("Theft", "Theft (চোরাচালান সংক্রান্ত মামলা)");
            specialCrimeType.Add("Wanderer", "Wanderer (বনদস্যু)");
            specialCrimeType.Add("WomenAndChildAbuse", "Women And Child Abuse (নারী ও শিশু নির্যাতন)");
            specialCrimeType.Add("Shorbohara", "Shorbohara (সর্বহারা)");
            specialCrimeType.Add("AnsarAlIslam", "Jongi Activity(Ansar Al Islam) (আনসার-আল-ইসলাম)");
            specialCrimeType.Add("AnsarullahBanglaTeam", "Jongi Activity(Ansarullah Bangla Team) (আনসারুল্লাহ-বাংলা-টিম)");
            specialCrimeType.Add("HarkatulJihad", "Jongi Activity(Harkatul Jihad) (হরকাতুল জিহাদ)");
            specialCrimeType.Add("HizbutTahrir", "Jongi Activity(Hizbut Tahrir) (হিযবুত তাহরীর)");
            specialCrimeType.Add("JMJB", "Jongi Activity(JMJB) (জাগ্রত-মুসলিম-জনতা-বাংলাদেশ)");
            specialCrimeType.Add("JMB", "Jongi Activity(JMB) (জামাতুল-মুজাহিদিন বাংলাদেশ)");
            specialCrimeType.Add("SahadatAlHikma", "Jongi Activity(Sahadat Al Hikma) (শাহাদাত-আল-হিকমা)");
            specialCrimeType.Add("AllaharDhol", "Jongi Activity(Allahar Dhol) (আল্লাহার দল)");
            specialCrimeType.Add("Others", "Others (অন্যান্য)");
            specialCrimeType.Add("JongiActivity", "Jongi Activity (জঙ্গী)");
        }

        private static void LoadNotEntryReason()
        {
            notEntryReason.Add("handoverToPS", "Directly Handover To PS");
            notEntryReason.Add("death", "Death");
            notEntryReason.Add("wounded", "Severe Wounded");
            notEntryReason.Add("technicalProblem", "Technical Problem");
            notEntryReason.Add("misc", "Others");
		}
		
        private static void LoadProfileManagementProfileType()
        {
            profileType.Add(1, "মাদক");
            profileType.Add(2, "অস্ত্র");
            profileType.Add(3, "রাজনৈতিক");
            profileType.Add(4, "চাঁদাবাজ");
            profileType.Add(5, "সন্ত্রাসী কার্যক্রম");
            profileType.Add(6, "ধর্ষণ");
            profileType.Add(7, "খুন");
            profileType.Add(8, "জঙ্গীবাদ");
            profileType.Add(9, "চোরাচালান");
            profileType.Add(10, "সাইবার ক্রাইম");
            profileType.Add(11, "মানব পাচার");
            profileType.Add(12, "কিশোর গ্যাং");
            profileType.Add(13, "ছিনতাই");
            profileType.Add(14, "প্রতারণা ও জালিয়াতি");
            profileType.Add(15, "অপহরণ");
            profileType.Add(16, "অন্যান্য");
        }

        private static void LoadNotEntryCaseType()
        {
            caseType.Add("regularCase", "Regular Case");
            caseType.Add("warrant", "Warrant");
            caseType.Add("gambling", "Gambling");
            caseType.Add("others", "Others");
        }
    }
}
