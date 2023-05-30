using Exo_Linq_Context;

Console.WriteLine("Exercice Linq");
Console.WriteLine("*************");

DataContext context = new DataContext();


// Exercice 1.1 Ecrire une requête pour présenter, pour chaque étudiant, le nom de l’étudiant, la date de naissance, le login et le résultat pour l’année de l’ensemble des étudiants.
var result_1_1a = context.Students.Select(s => new
{
    Nom = s.Last_Name,
    DateNaissance = s.BirthDate,
    Login = s.Login,
    Resultat = s.Year_Result
});

var result_1_1b = from Student s in context.Students
                  select new
                  {
                      Nom = s.Last_Name,
                      DateNaissance = s.BirthDate,
                      Login = s.Login,
                      Resultat = s.Year_Result
                  };

Console.WriteLine("1.1)");
foreach (var item in result_1_1b)
{
    Console.WriteLine(item);
}
Console.WriteLine();

// Exercice 1.2 Ecrire une requête pour présenter, pour chaque étudiant, son nom complet (nom et prénom séparés par un espace), son id et sa date de naissance.
var result_1_2a = context.Students
                         .Select(s => new
                         {
                             Id = s.Student_ID,
                             NomComplet = s.First_Name + " " + s.Last_Name,
                             DateNaissance = s.BirthDate.ToShortDateString()
                         });

var result_1_2b = from s in context.Students
                  select new
                  {
                      Id = s.Student_ID,
                      NomComplet = s.First_Name + " " + s.Last_Name,
                      DateNaissance = s.BirthDate.ToShortDateString()
                  };

Console.WriteLine("1.2)");
foreach (var item in result_1_2b)
{
    Console.WriteLine(item);
}
Console.WriteLine();

// Exercice 1.3 Ecrire une requête pour présenter, pour chaque étudiant, dans une seule chaine de caractère l’ensemble des données relatives à un étudiant séparées par le symbole |.
IEnumerable<string> result_1_3a = from s in context.Students
                                 select $"{s.Student_ID}|{s.First_Name}|{s.Last_Name}|{s.BirthDate}|{s.Login}|{s.Section_ID}|{s.Year_Result}|{s.Course_ID}";

IEnumerable<string> result_1_3b = context.Students
                                         .Select(s => $"{s.Student_ID}|{s.First_Name}|{s.Last_Name}|{s.BirthDate}|{s.Login}|{s.Section_ID}|{s.Year_Result}|{s.Course_ID}");

Console.WriteLine("1.3)");
foreach (string item in result_1_3b)
{
    Console.WriteLine(item);
}
Console.WriteLine();