using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.ConstrainedExecution;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Net.Mail;

namespace FinalProyektBOSS;

//clr inclode cev
public enum LanguageLevel
{
    A1 = 30,
    A2 = 40,
    B1 = 50,
    B2 = 70,
    C1 = 80,
    C2 = 100
}

public class User
{
    public User() { }

    public User(string? name, string? surname, int age, string? city, string? phoneNumber, string? password)
    {
        Name = name;
        Surname = surname;
        Age = age;
        City = city;
        PhoneNumber = phoneNumber;
        Password = password;
    }

    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Age { get; set; }
    public string? City { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }

    public override string ToString()
    {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        return $"Name: {Name} Surname: {Surname} Age: {Age} City: {City} PhoneNumber: {PhoneNumber} {Password}";
    }
}

public class Cv
{
    public Cv() { }

    public Cv(string? specialty, string? schoolName, int universityScore, List<string> language, List<string> companie, LanguageLevel languageLevel, bool honorsDiploma, string? gitLink, string? linkEdin)
    {
        Specilaty = specialty;
        SchoolName = schoolName;
        Universitycore = universityScore;//range****
        Language = language;
        CompanieCount = companie;//sirket adlari 
        this.languageLevel = languageLevel;
        HonorsDiploma = honorsDiploma;
        GitLink = gitLink;
        Linkedln = linkEdin;
    }

    public string? Specilaty { get; set; }
    public string? SchoolName { get; set; }
    public int Universitycore { get; set; }
    public List<string> Language { get; set; }
    public List<string> CompanieCount { get; set; }
    public LanguageLevel languageLevel { get; set; }
    public bool HonorsDiploma { get; set; }
    public string? GitLink { get; set; }
    public string? Linkedln { get; set; }

    public override string ToString()
    {
        return $"\nSpecilaty: {Specilaty} \nScholl Name: {SchoolName} \nUniversity Score: {Universitycore} " +
            $"\nLanguage: {string.Join(", ", Language)} \nCompanie: {string.Join(", ", CompanieCount)} " +
            $"\nLanguage Level: {languageLevel} \nHonors Diploma: {HonorsDiploma} \nGitLink: {GitLink} \nLinkedln: {Linkedln}";
    }
}


public class Worker : User
{
    private static int ID = 0;
    public int id { get; set; }

    public Worker() { id = ++ID; }

    public Worker(string? name, string? surname, string? password, int age, string? city, string? phoneNumber, List<Cv> workerCv, string? gender)
        : base(name: name, surname: surname, age: age, city: city, phoneNumber: phoneNumber, password: password)
    {
        id = ++ID;
        Name = name;
        Surname = surname;
        Age = age;
        City = city;
        PhoneNumber = phoneNumber;
        Password = password;
        WorkerCv = workerCv;
        Gender = gender;
    }

    public string? Gender { get; }
    public List<Cv> WorkerCv { get; set; }


    public override string ToString()
    {
        //string cvInfo = string.Join("\n", WorkerCv.Select(cv => cv.ToString()));

        return $"ID: {id} \n{base.ToString()} \nWorkerCv: \n{WorkerCv} \nGender: {Gender}";
    }
}


public class Employer : User
{
    private static int id = 0;

    public Employer() { Id = ++id; }

    public Employer(List<Vacancia> vacancias, GmaileService gmaileServices, string? name, string? password,
            string? surname, int age, string? city, string? phoneNumber)
            : base(name: name, surname: surname, age: age, city: city, phoneNumber: phoneNumber, password: password)
    {
        Id = ++id;
        Name = name;
        Surname = surname;
        Age = age;
        City = city;
        PhoneNumber = phoneNumber;
        Password = password;
        this.vacancias = vacancias;
        this.gmaileServices = gmaileServices;
    }

    public int Id { get; set; }

    public List<Vacancia> vacancias { get; set; }

    public GmaileService gmaileServices { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}. {base.ToString()}  \nNamizedden telebler: {vacancias} Gamail: {gmaileServices}";
    }
}

public class GmaileService
{
    public GmaileService(string? gmail)
    {
        this.gmail = gmail;
    }

    public string? gmail { get; set; }

    public override string ToString()
    {
        return $"{gmail}";
    }
}

public class Vacancia
{
    public Vacancia(string? vacancieTitle, string? education, string salaryMin, string salaryMax)
    {
        VacancieTitle = vacancieTitle;
        Education = education;
        this.salaryMin = salaryMin;
        this.salaryMax = salaryMax;
        StartDateTime = DateTime.Now;
        EndDateTime = DateTime.Now.AddMonths(1);
    }

    public string? VacancieTitle { get; set; }
    public string? Education { get; set; }
    public string? salaryMin { get; set; }
    public string? salaryMax { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }


    public override string ToString()
    {
        return $"VacanTitle: {VacancieTitle} \nEducation: {Education} \nSalaryMin: {salaryMin} \nSalaryMax{salaryMax} " +
            $"StartDateTime: {StartDateTime.Day}/{StartDateTime.Month}/{StartDateTime.Year} " +
            $"\nEndDateTime: {StartDateTime.Day}/{StartDateTime.Month + 1}/{StartDateTime.Year}";
    }
}


public class SyStem
{
    public SyStem() { }

    public void AddWorkerCv(List<Worker> workers, List<Cv> cv, string? filname)
    {
        LanguageLevel level;

        Console.Write("Specialty:  ");
        string? Specialty = Console.ReadLine();

        Console.Write("School name:  ");
        string? SchoolName = Console.ReadLine();

        Console.Write("University score:  ");
        string? Score = Console.ReadLine();
        int UniversityScore = Convert.ToInt32(Score);

        Console.Write("Honor Diploma (true/false):  ");
        bool HonorDiploma = Convert.ToBoolean(Console.ReadLine());

        Console.Write("GitLink:  ");
        string? GitLink = Console.ReadLine();

        Console.Write("LinkedIn:  ");
        string? LinkedIn = Console.ReadLine();

        List<string> companies = new();

        Console.Write("Number of Companies worked for:  ");
        string? compCount = Console.ReadLine();

        for (int i = 0; i < Convert.ToInt32(compCount); i++)
        {
            Console.Write("Company Name:  ");
            string? CompanyName = Console.ReadLine();

            companies.Add(CompanyName!);
        }

        Console.Write("Number of Languages known:  ");
        string? langCount = Console.ReadLine();
        int LangCount = Convert.ToInt32(langCount);

        List<Cv> newCvs = new List<Cv>();

        while (LangCount > 0)
        {
            List<string> languages = new List<string>();

            Console.Write("Language:  ");
            string? Language = Console.ReadLine();
            languages.Add(Language!);

            string? languageLevel;
            do
            {
                Console.Write("Language Level (A1, A2, B1, B2, C1, C2):  ");
                languageLevel = Console.ReadLine();
            } while (!Enum.TryParse(languageLevel, out level));

            LangCount--;

            Cv newCv = new Cv(Specialty, SchoolName, UniversityScore, languages, companies, level, HonorDiploma, GitLink, LinkedIn);
            newCvs.Add(newCv);
        }

        Console.Write("Name:  ");
        string? Name = Console.ReadLine();

        Console.Write("Surname:  ");
        string? Surname = Console.ReadLine();

        Console.Write("Age:  ");
        string? age = Console.ReadLine();
        int Age = Convert.ToInt32(age);

        Console.Write("Password:  ");
        string? Password = Console.ReadLine();

        Console.Write("City:  ");
        string? City = Console.ReadLine();

        Console.Write("Phone number:  ");
        string? PhoneNumber = Console.ReadLine();

        Console.Write("Gender:  ");
        string? Gender = Console.ReadLine();

        Worker worker = new Worker(Name, Surname, Password, Age, City, PhoneNumber, newCvs, Gender);
        workers.Add(worker);

        List<Worker>? existingWorkers = new List<Worker>();

        if (File.Exists(filname))
        {
            string? json = File.ReadAllText(filname);
            existingWorkers = JsonSerializer.Deserialize<List<Worker>>(json);
        }

        int maxId = existingWorkers!.Count > 0 ? existingWorkers.Max(e => e.id) : 0;
        worker.id = maxId + 1;
        existingWorkers!.Add(worker);

        string updateJson = JsonSerializer.Serialize(existingWorkers, new JsonSerializerOptions() { WriteIndented = true });
        File.WriteAllText(filname!, updateJson);
    }

    public void AddEmployer(string filename)
    {
        List<Vacancia> vacancias = new();

        List<Employer> employers = new();


        //Employer
        Console.WriteLine("Employer");
        Console.Write("Name:  ");
        string? Name = Console.ReadLine();
        Console.Write("Surname:  ");
        string? Surname = Console.ReadLine();
        Console.Write("Age:  ");
        string? age = Console.ReadLine();
        int Age = Console.Read();
        Console.Write("Password:  ");
        string? Password = Console.ReadLine();
        Console.Write("City:  ");
        string? City = Console.ReadLine();
        Console.Write("Phone number:  ");
        string? PhoneNumber = Console.ReadLine();

        Console.Write("Gmail:  ");
        string? gmailService = Console.ReadLine();
        GmaileService Gmailservice = new(gmailService);

        // vacancie
        Console.WriteLine("Vacancie");
        Console.Write("Vacancie Title:  ");
        string? vacancieTitle = Console.ReadLine();
        Console.Write("Education  (Ali), (Orta), (Vacib deyil), (Elmi derecede):  ");
        string? Education = Console.ReadLine();
        Console.Write("Salary min:  ");
        string? salarymin = Console.ReadLine();
        Console.Write("Salary max:  ");
        string? salarymax = Console.ReadLine();


        Vacancia vacancia = new(vacancieTitle: vacancieTitle, education: Education, salaryMin: salarymin!, salaryMax: salarymax!);

        vacancias.Add(vacancia);

        Employer employer = new(vacancias, Gmailservice, name: Name, password: Password, surname: Surname, age: Age, city: City, phoneNumber: PhoneNumber);

        List<Employer> existingEmployers = new List<Employer>();

        if (File.Exists(filename))
        {
            string json = File.ReadAllText(filename);
            existingEmployers = JsonSerializer.Deserialize<List<Employer>>(json) ?? new List<Employer>();
        }

        int maxId = existingEmployers!.Count > 0 ? existingEmployers.Max(e => e.Id) : 0;

        employer.Id = maxId + 1;

        existingEmployers.Add(employer);

        string updatedJson = JsonSerializer.Serialize(existingEmployers, new JsonSerializerOptions() { WriteIndented = true });
        File.WriteAllText(filename, updatedJson);

        Console.WriteLine("you have registered...");
        Console.Clear();
    }

    public void RemoveEmployer(int id, string? filename)
    {
        var json = File.ReadAllText("emplo.json");
        var jsonList = JsonSerializer.Deserialize<List<Employer>>(json);

        var employerToRemove = jsonList!.FirstOrDefault(e => e.Id == id);
        if (employerToRemove != null)
        {
            jsonList!.Remove(employerToRemove);

            var updatedJson = JsonSerializer.Serialize(jsonList, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(filename!, updatedJson);

            Console.WriteLine($"Employer with ID {id} has been removed.");
        }

        else
        {
            Console.WriteLine($"Employer with ID {id} not found.");
        }
    }
}

public static class Filter
{

    public static void CityFilter(string? filename, Worker worker)
    {
        Console.Write("Enter The City: ");
        string? city = Console.ReadLine();

        try
        {
            var json = File.ReadAllText(filename!);
            var employers = JsonSerializer.Deserialize<List<Employer>>(json);

            foreach (var employer in employers!)
            {
                if (employer.City == city)
                {
                    Console.Clear();
                    foreach (var item in employers)
                    {
                        Console.WriteLine($"{item.Id} {item.Name} {item.Surname} {item.PhoneNumber} {item.City} {item.gmaileServices}");
                        foreach (var item1 in item.vacancias)
                        {
                            Console.WriteLine($"{item1.VacancieTitle} {item1.Education} {item1.salaryMin} {item1.salaryMax}");
                        }
                    }

                    Console.Write("Enter The ID: ");
                    string? enteredId = Console.ReadLine();

                    if (int.TryParse(enteredId, out int id))
                    {

                        if (id == employer.Id)
                        {

                            for (int i = 3; i > 0; i--)
                            {
                                Console.WriteLine($"{i}");
                                System.Threading.Thread.Sleep(1000);
                                Console.Clear();
                            }

                            MailMessage mail = new MailMessage();
                            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                            mail.From = new MailAddress(employer.gmaileServices.gmail!);
                            mail.To.Add(employer.gmaileServices.gmail!);
                            mail.Subject = "*Cv Mail*";
                            string cvInfo = string.Join("\n", worker.WorkerCv.Select(cv => cv.ToString()));

                            mail.Body = $"{worker.Name} {worker.Surname}\n{cvInfo}";
                            smtp.Port = 587;
                            smtp.Credentials = new NetworkCredential(employer.gmaileServices.gmail, "bxud vzpz ayod xene");

                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                            Console.WriteLine("📨 Mail sent successfully");
                            System.Threading.Thread.Sleep(1200);
                            Console.Clear();

                        }
                        else
                        {
                            Console.WriteLine("Entered ID does not match any employer.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID entered.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static void SalaryFilter(string? filename, Worker worker)
    {
        Console.Clear();
        Console.Write("salary min: ");
        string? salary = Console.ReadLine();
        int salarymin = Convert.ToInt32(salary);

        try
        {
            var json = File.ReadAllText(filename!);
            var employers = JsonSerializer.Deserialize<List<Employer>>(json);

            foreach (var employer in employers!)
            {
                foreach (var vacancie in employer.vacancias)
                {
                    int Salary = Convert.ToInt32(vacancie.salaryMin);

                    if (Salary >= salarymin)
                    {
                        Console.WriteLine($"{employer.Id} {employer.Name} {employer.Surname} {employer.PhoneNumber} {employer.City} {employer.gmaileServices}");
                        Console.WriteLine($"{vacancie.VacancieTitle} {vacancie.Education} {vacancie.salaryMin} {vacancie.salaryMax}");

                        Console.Write("Enter The ID: ");
                        string? enteredId = Console.ReadLine();

                        if (int.TryParse(enteredId, out int id))
                        {
                            if (id == employer.Id)
                            {
                                for (int i = 3; i > 0; i--)
                                {
                                    Console.WriteLine($"{i}");
                                    System.Threading.Thread.Sleep(1000);
                                    Console.Clear();
                                }

                                MailMessage mail = new MailMessage();
                                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                mail.From = new MailAddress(employer.gmaileServices.gmail!);
                                mail.To.Add(employer.gmaileServices.gmail!);
                                mail.Subject = "*Cv Mail*";
                                string cvInfo = string.Join("\n", worker.WorkerCv.Select(cv => cv.ToString()));

                                mail.Body = $"{worker.Name} {worker.Surname}\n{cvInfo}";
                                smtp.Port = 587;
                                smtp.Credentials = new NetworkCredential(employer.gmaileServices.gmail, "bxud vzpz ayod xene");

                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                                Console.WriteLine("📨 Mail sent successfully");
                                System.Threading.Thread.Sleep(1200);
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("Entered ID does not match any employer.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID entered.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static void VacanyTitleFilter(string? filename, Worker worker)
    {
        Console.Write("Enter Vacany name: ");
        string? vacanyTitle = Console.ReadLine();

        try
        {
            var json = File.ReadAllText(filename!);
            var employers = JsonSerializer.Deserialize<List<Employer>>(json);

            foreach (var employer in employers!)
            {
                foreach (var vacany in employer.vacancias)
                {
                    if (vacany.VacancieTitle == vacanyTitle)
                    {
                        Console.Clear();
                        foreach (var emplo in employers)
                        {
                            Console.WriteLine($"{emplo.Id} {emplo.Name} {emplo.Surname} {emplo.PhoneNumber} {emplo.City} {emplo.gmaileServices}");
                            foreach (var emploVacany in emplo.vacancias)
                            {
                                Console.WriteLine($"{emploVacany.VacancieTitle} {emploVacany.Education} {emploVacany.salaryMin} {emploVacany.salaryMax}");
                            }
                        }

                        Console.Write("Enter The ID: ");
                        string? enteredId = Console.ReadLine();

                        if (int.TryParse(enteredId, out int id))
                        {

                            if (id == employer.Id)
                            {

                                for (int i = 3; i > 0; i--)
                                {
                                    Console.WriteLine($"{i}");
                                    System.Threading.Thread.Sleep(1000);
                                    Console.Clear();
                                }

                                MailMessage mail = new MailMessage();
                                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                mail.From = new MailAddress(employer.gmaileServices.gmail!);
                                mail.To.Add(employer.gmaileServices.gmail!);
                                mail.Subject = "*Cv Mail*";
                                string cvInfo = string.Join("\n", worker.WorkerCv.Select(cv => cv.ToString()));

                                mail.Body = $"{worker.Name} {worker.Surname}\n{cvInfo}";
                                smtp.Port = 587;
                                smtp.Credentials = new NetworkCredential(employer.gmaileServices.gmail, "bxud vzpz ayod xene");

                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                                Console.WriteLine("📨 Mail sent successfully");
                                System.Threading.Thread.Sleep(1200);
                                Console.Clear();

                            }
                            else
                            {
                                Console.WriteLine("Entered ID does not match any employer.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID entered.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

internal class Program
{

    private static void Main(string[] args)
    {
        //List<string> strings = new();
        //strings.Add("Elgun");

        //var json = JsonSerializer.Serialize(strings);
        //var json2 = JsonSerializer.Serialize(strings);


        //File.WriteAllText("emplo.json", json);
        //File.WriteAllText("ws.json", json2);



        List<Worker> workers = new();

        List<Employer> employers = new();

        List<Cv> cvs = new();

        SyStem system = new();

        string logFilePath = "run.log";

        using (StreamWriter writer = new StreamWriter(logFilePath, append: true))
        {
            writer.WriteLine($"{DateTime.Now}: Run verildi sistem ise dusdu");
        }

        while (true)
        {
            string? FileNameWorker = "ws.json";
            string? FileNameEmployer = "emplo.json";


            var jsonWorker = File.ReadAllText(FileNameWorker);
            var jsonWorkerList = JsonSerializer.Deserialize<List<Worker>>(jsonWorker);

            var jsonEmployer = File.ReadAllText(FileNameEmployer);
            var jsonEmployerList = JsonSerializer.Deserialize<List<Employer>>(jsonEmployer);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Employer");
            Console.WriteLine("2. Worker");
            Console.Write("Choose: ");

            string? Choose = Console.ReadLine();

            if (Choose != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
            }

            if (Choose == "1")
            {
                Console.Clear();

                Console.WriteLine("1. Sign in");
                Console.WriteLine("2. Sign up");

                Console.Write("Choose:  ");
                string? makeChoice = Console.ReadLine();
                Console.Clear();

                if (makeChoice == "1")
                {
                    Console.Write("Username: ");
                    string? Username = Console.ReadLine();
                    Console.Write("Password: ");
                    string? Password = Console.ReadLine();
                    Console.Clear();

                    var employer = jsonEmployerList!.FirstOrDefault(item => (item.Name == Username && item.Password == Password));

                    if (employer != null)
                    {
                        string openEmployerFilePath = "run.log";

                        using (StreamWriter writer = new StreamWriter(openEmployerFilePath, append: true))
                        {
                            writer.WriteLine($"{DateTime.Now}: Employer: {employer.Name} {employer.Surname} sisteme giris etdi");
                        }

                        Console.Clear();
                        Console.WriteLine("1. Delete Vacancia");
                        Console.WriteLine("2. New Vacancia");
                        Console.WriteLine("3. Exit");
                        Console.Write("Choose: ");

                        int Id = employer.Id;

                        string? Ch = Console.ReadLine();

                        switch (Ch)
                        {


                            case "1":
                                Console.Clear();
                                Console.WriteLine($"ID: {employer.Id} Name: {employer.Name} Surname: {employer.Surname} PhoneNumber: {employer.PhoneNumber}");
                                system.RemoveEmployer(Id, FileNameEmployer);
                                System.Threading.Thread.Sleep(1000);
                                Console.Clear();
                                break;

                            case "2":

                                Console.Write("Vacancy Title: ");
                                string? vacancyTitle = Console.ReadLine();

                                Console.Write("Education (Ali, Orta, Vacib deyil, Elmi derecede): ");
                                string? education = Console.ReadLine();

                                Console.Write("Salary min: ");
                                string? salaryMin = Console.ReadLine();

                                Console.Write("Salary max: ");
                                string? salaryMax = Console.ReadLine();


                                Vacancia newVacancy = new(vacancyTitle, education, salaryMin!, salaryMax!);

                                List<Vacancia> vacany = new();

                                vacany.Add(newVacancy);


                                Employer employer1 = new(vacany, employer.gmaileServices, employer.Name, employer.Password, employer.Surname, employer.Age, employer.City, employer.PhoneNumber);

                                employer.vacancias.Add(newVacancy);


                                List<Employer> existingEmployers = new List<Employer>();

                                if (File.Exists(FileNameEmployer))
                                {
                                    string json1 = File.ReadAllText(FileNameEmployer);
                                    existingEmployers = JsonSerializer.Deserialize<List<Employer>>(json1) ?? new List<Employer>();
                                }

                                int maxId = existingEmployers!.Count > 0 ? existingEmployers.Max(e => e.Id) : 0;

                                employer1.Id = maxId + 1;

                                existingEmployers.Add(employer1);

                                string updatedJson = JsonSerializer.Serialize(existingEmployers, new JsonSerializerOptions() { WriteIndented = true });
                                File.WriteAllText(FileNameEmployer, updatedJson);

                                Console.WriteLine("Vacancy added successfully!");
                                Thread.Sleep(1000);
                                Console.Clear();
                                break;


                            default:
                                break;
                        }
                    }

                    else
                    {

                        Console.WriteLine("Invalid username or password. Please try again.");
                        Thread.Sleep(500);
                        Console.Clear();
                        system.AddEmployer(FileNameEmployer);

                        string openEmployerFilePath = "run.log";

                        using (StreamWriter writer = new StreamWriter(openEmployerFilePath, append: true))
                        {
                            writer.WriteLine($"{DateTime.Now}: Employer qeydiyyatdan kecdi");
                        }

                        Console.Clear();
                    }
                }
                else if (makeChoice == "2")
                {

                    Thread.Sleep(1000);
                    Console.Clear();
                    system.AddEmployer(FileNameEmployer);

                    string openEmployerFilePath = "run.log";

                    using (StreamWriter writer = new StreamWriter(openEmployerFilePath, append: true))
                    {
                        writer.WriteLine($"{DateTime.Now}: Employer qeydiyyatdan kecdi");
                    }
                    Console.Clear();
                }
            }

            else if (Choose == "2")
            {
                Console.Clear();

                Console.WriteLine("1. Sign in");
                Console.WriteLine("2. Sign up");

                Console.Write("Choose:  ");
                string? makeChoice = Console.ReadLine();

                if (makeChoice == "1")
                {
                    Console.Clear();
                    Console.Write("Username: ");
                    string? username = Console.ReadLine();
                    Console.Write("Password: ");
                    string? password = Console.ReadLine();

                    var worker = jsonWorkerList!.FirstOrDefault(item => item.Name == username && item.Password == password);

                    if (worker != null)
                    {
                        string openEmployerFilePath = "run.log";

                        using (StreamWriter writer = new StreamWriter(openEmployerFilePath, append: true))
                        {
                            writer.WriteLine($"{DateTime.Now}: Worker:  {worker.Name} {worker.Surname} giris etdi");
                        }

                        Console.Clear();

                        Console.WriteLine("1. All Show Vacancia");
                        Console.WriteLine("2. Filter");

                        Console.Write("Make a Choice:  ");
                        string? Choice = Console.ReadLine();

                        if (Choice == "1")
                        {
                            Console.WriteLine("Show all vacancies");
                            Console.Clear();

                            Console.ForegroundColor = ConsoleColor.Green;

                            foreach (var employer in jsonEmployerList!)
                            {
                                Console.WriteLine($"Employer ID: {employer.Id}  Name: {employer.Name} Surname: {employer.Surname} Age: {employer.Age}  " +
                                                     $"City: {employer.City} PhoneNumber: {employer.PhoneNumber}");

                                foreach (var vacancia in employer.vacancias)
                                {

                                    Console.WriteLine($"  Vacancia Title: {vacancia.VacancieTitle}, Education: {vacancia.Education}, " +
                                                        $"SalaryMin: {vacancia.salaryMin}, SalaryMax: {vacancia.salaryMax} " +
                                                        $"StartDateTime: {vacancia.StartDateTime} EndDateTime: {vacancia.EndDateTime}\n\n");
                                }
                            }

                            Console.Write("\nEnter the ID of the employer you want to interact with: ");

                            string? employerIdInput = Console.ReadLine();
                            int id = Convert.ToInt32(employerIdInput);
                            Console.Clear();

                            var selectedEmployer = jsonEmployerList?.FirstOrDefault(e => e.Id == id);

                            if (selectedEmployer != null)
                            {
                                try
                                {
                                    MailMessage mail = new MailMessage();
                                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                    mail.From = new MailAddress(selectedEmployer.gmaileServices.gmail!);
                                    mail.To.Add(selectedEmployer.gmaileServices.gmail!);
                                    mail.Subject = "*Cv Mail*";
                                    string cvInfo = string.Join("\n", worker.WorkerCv.Select(cv => cv.ToString()));

                                    mail.Body = $"{worker.Name} {worker.Surname}\n{cvInfo}";

                                    smtp.Port = 587;
                                    smtp.Credentials = new NetworkCredential(selectedEmployer.gmaileServices.gmail, "bxud vzpz ayod xene");

                                    smtp.EnableSsl = true;
                                    smtp.Send(mail);
                                    Console.WriteLine("Mail sent successfully");
                                }

                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }

                            else
                            {
                                Console.WriteLine("Employer with the given ID not found.");
                                System.Threading.Thread.Sleep(1500);
                                Console.Clear();
                            }
                        }

                        else if (Choice == "2")
                        {
                            Console.Clear();

                            Console.WriteLine("1. Filter Vacany Title");
                            Console.WriteLine("2. Filter Vacany Salary");
                            Console.WriteLine("3. Filter Vacany City");

                            Console.Write("Filter choice:  ");
                            string? filterChoice = Console.ReadLine();

                            switch (filterChoice)
                            {
                                case "1":
                                    Filter.VacanyTitleFilter(FileNameEmployer, worker!);
                                    Console.Clear();
                                    break;

                                case "2":
                                    Filter.SalaryFilter(FileNameEmployer, worker!);
                                    Console.Clear();
                                    break;

                                case "3":
                                    Filter.CityFilter(FileNameEmployer, worker!);
                                    Console.Clear();
                                    break;

                                default:
                                    break;
                            }
                        }
                    }

                    else
                    {

                        Console.WriteLine("You do not have a name in the system, please register");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        system.AddWorkerCv(workers, cvs, FileNameWorker);
                        string openEmployerFilePath = "run.log";

                        using (StreamWriter writer = new StreamWriter(openEmployerFilePath, append: true))
                        {
                            writer.WriteLine($"{DateTime.Now}: Worker qeydiyyatdan kecdi");
                        }

                        Console.Clear();
                    }
                }

                else if (makeChoice == "2")
                {
                    string openEmployerFilePath = "run.log";

                    using (StreamWriter writer = new StreamWriter(openEmployerFilePath, append: true))
                    {
                        writer.WriteLine($"{DateTime.Now}: Worker qeydiyyatdan kecdi");
                    }

                    Console.Clear();
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    system.AddWorkerCv(workers, cvs, FileNameWorker);

                    Console.Clear();
                }
            }
        }
    }
}