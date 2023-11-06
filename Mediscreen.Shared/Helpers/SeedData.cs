using Mediscreen.Shared.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediscreen.Shared.Helpers
{
    public static class SeedData
    {
        public static Patient GetPatient()
        {
            Random rand = new();
            Patient patient = new()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                GivenName = "Given Name",
                FamilyName = "Family Name",
                DateOfBirth = DateTime.Now.AddYears(rand.Next(-70, 18)),
                Sex = rand.Next(0, 1) == 0 ? "Male" : "Female",
                HomeAddress = "Home Address",
                PhoneNumber = $"+41 {rand.Next(700000000, 799999999)}"
            };
            return patient;
        }
        public static List<Patient> GetPatients(int amount)
        {
            List<Patient> listPatients = new();

            for(int i = 0; i < amount; i++)
            {
                Patient patient = GetPatient();
                listPatients.Add(patient);
            }

            return listPatients;
        }
        public static Note GetNote()
        {
            Note note = new()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                PatientId = ObjectId.GenerateNewId().ToString(),
                CreationDate = DateTime.Now,
                NotesRecommendations = "Test notes"
            };
            return note;
        }
        public static Note GetNote(string noteComment)
        {
            Note note = new()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                PatientId = ObjectId.GenerateNewId().ToString(),
                CreationDate = DateTime.Now,
                NotesRecommendations = noteComment
            };
            return note;
        }
        public static List<Note> GetNotes(int amount)
        {
            List<Note> listNotes = new();

            for (int i = 0; i < amount; i++)
            {
                Note note = GetNote();
                listNotes.Add(note);
            }

            return listNotes;
        }
        public static List<Note> GetNotes(List<string> noteComments)
        {
            List<Note> listNotes = new();

            foreach(string comment in noteComments)
            {
                Note note = GetNote(comment);
                listNotes.Add(note);
            }

            return listNotes;
        }
        public static TriggerTerm GetTriggerTerm(string term)
        {
            return new()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Term = term
            };
        }
        public static List<TriggerTerm> GetTriggers(List<string> terms)
        {
            List<TriggerTerm> triggerTerms = new();
            foreach (string term in terms)
            {
                triggerTerms.Add(new TriggerTerm()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Term = term
                });
            }
            return triggerTerms;
        }
    }
}
