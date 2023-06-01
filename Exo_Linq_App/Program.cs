using Exo_Linq_Context;
using System.Text.RegularExpressions;

Console.WriteLine("Exercice Linq");
Console.WriteLine("*************");

DataContext context = new DataContext();

#region Exo 01
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
#endregion


#region Exo 02
// Exercice 2.2 Donner pour chaque étudiant entre 1955 et 1965 le nom, le résultat annuel et la catégorie à laquelle il appartient. La catégorie est fonction du résultat annuel obtenu ; un résultat inférieur à 10 appartient à la catégorie « inférieure », un résultat égal à 10 appartient à la catégorie « neutre », un résultat autre appartient à la catégorie « supérieure ».
var result_2_2a = context.Students
                    .Where(s => s.BirthDate.Year >= 1955 && s.BirthDate.Year <= 1965)
                    .Select(s => new
                    {
                        Nom = s.Last_Name,
                        Resultat = s.Year_Result,
                        Categorie = s.Year_Result < 10 ? "inférieure" : (s.Year_Result > 10 ? "supérieure" : "neutre")
                    });

var result_2_2b = from Student s in context.Students
                  where s.BirthDate.Year >= 1955 && s.BirthDate.Year <= 1965
                  select new
                  {
                      Nom = s.Last_Name,
                      Resultat = s.Year_Result,
                      Categorie = s.Year_Result < 10 ? "inférieure" : (s.Year_Result > 10 ? "supérieure" : "neutre")
                  };


// Exercice 2.6 Ecrire une requête pour présenter le nom, l’id de section et le résultat annuel classé par ordre croissant sur la section de tous les étudiants appartenant aux sections 1010 et 1020 ayant un résultat annuel qui n’est pas compris entre 12 et 18.
var result_2_6a = context.Students
                    .Where(s => s.Section_ID == 1010 || s.Section_ID == 1020)
                    .Where(s => s.Year_Result < 12 || s.Year_Result > 18)
                    .OrderByDescending(s => s.Section_ID)
                    .Select(s => new
                    {
                        Nom = s.Last_Name,
                        Section = s.Section_ID,
                        Resultat = s.Year_Result
                    });

var result_2_6b = context.Students
                    .Where(s => (s.Section_ID == 1010 || s.Section_ID == 1020)
                             && (s.Year_Result < 12 || s.Year_Result > 18))
                    .OrderByDescending(s => s.Section_ID)
                    .Select(s => new
                    {
                        Nom = s.Last_Name,
                        Section = s.Section_ID,
                        Resultat = s.Year_Result
                    });

var result_2_6c = from s in context.Students
                  where (s.Section_ID == 1010 || s.Section_ID == 1020)
                     && (s.Year_Result < 12 || s.Year_Result > 18)
                  orderby s.Section_ID descending
                  select new
                  {
                      Nom = s.Last_Name,
                      Section = s.Section_ID,
                      Resultat = s.Year_Result
                  };


// Exercice 2.7 Ecrire une requête pour présenter le nom, l’id de section et le résultat annuel sur 100 (nommer la colonne ‘result_100’) classé par ordre décroissant du résultat de tous les étudiants appartenant aux sections commençant par 13 et ayant un résultat annuel sur 100 inférieur ou égal à 60.
var result_2_7a = context.Students
                    .Where(s => s.Section_ID.ToString().StartsWith("13")
                        && (s.Year_Result * 5) <= 60)
                    .OrderByDescending(s => s.Year_Result)
                    .Select(s => new
                    {
                        Nom = s.First_Name + " " + s.Last_Name,
                        Section = s.Section_ID,
                        Result_100 = s.Year_Result * 5
                    });

var result_2_7b = from s in context.Students
                  where Regex.IsMatch(s.Section_ID.ToString(), @"^13\d{2}$")
                        && (s.Year_Result * 5) <= 60
                  orderby s.Year_Result descending
                  select new
                  {
                      Nom = s.First_Name + " " + s.Last_Name,
                      Section = s.Section_ID,
                      Result_100 = s.Year_Result * 5
                  };



#endregion