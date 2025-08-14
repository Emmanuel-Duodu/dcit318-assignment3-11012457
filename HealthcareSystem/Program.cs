using System;
using System.Collections.Generic;
using HealthcareSystem.Models;
using HealthcareSystem.Repositories;

namespace HealthcareSystem
{
    public class HealthSystemApp
    {
        private Repository<Patient> _patientRepo = new Repository<Patient>();
        private Repository<Prescription> _prescriptionRepo = new Repository<Prescription>();
        private Dictionary<int, List<Prescription>> _prescriptionMap = new Dictionary<int, List<Prescription>>();

        // Initialize sample healthcare data
        public void SeedData()
        {
            // Register patients
            _patientRepo.Add(new Patient(1001, "Emma Johnson", 32, "Female"));
            _patientRepo.Add(new Patient(1002, "Michael Chen", 47, "Male"));
            _patientRepo.Add(new Patient(1003, "Sarah Williams", 29, "Female"));

            // Issue prescriptions
            _prescriptionRepo.Add(new Prescription(2001, 1001, "Acetaminophen 500mg", DateTime.Now.AddDays(-7)));
            _prescriptionRepo.Add(new Prescription(2002, 1001, "Vitamin D3", DateTime.Now.AddDays(-4)));
            _prescriptionRepo.Add(new Prescription(2003, 1002, "Amoxicillin 250mg", DateTime.Now.AddDays(-3)));
            _prescriptionRepo.Add(new Prescription(2004, 1003, "Metformin XR", DateTime.Now.AddDays(-2)));
            _prescriptionRepo.Add(new Prescription(2005, 1002, "Lisinopril 10mg", DateTime.Now.AddDays(-1)));
        }

        // Build prescription map by PatientId
        public void BuildPrescriptionMap()
        {
            foreach (var prescription in _prescriptionRepo.GetAll())
            {
                if (!_prescriptionMap.ContainsKey(prescription.PatientId))
                {
                    _prescriptionMap[prescription.PatientId] = new List<Prescription>();
                }
                _prescriptionMap[prescription.PatientId].Add(prescription);
            }
        }

        // Display registered patients
        public void PrintAllPatients()
        {
            Console.WriteLine("Registered Patients:");
            Console.WriteLine(new string('-', 50));
            foreach (var patient in _patientRepo.GetAll())
            {
                Console.WriteLine($"Patient #{patient.Id} | {patient.Name} | Age: {patient.Age} | Gender: {patient.Gender}");
            }
            Console.WriteLine();
        }

        // Display patient's prescription history
        public void PrintPrescriptionsForPatient(int patientId)
        {
            if (_prescriptionMap.ContainsKey(patientId))
            {
                var patient = _patientRepo.GetById(p => p.Id == patientId);
                Console.WriteLine($"Prescription History for {patient?.Name} (ID: {patientId}):");
                Console.WriteLine(new string('-', 60));
                foreach (var prescription in _prescriptionMap[patientId])
                {
                    Console.WriteLine($"Rx #{prescription.Id} | {prescription.MedicationName} | Issued: {prescription.DateIssued:MMM dd, yyyy}");
                }
            }
            else
            {
                Console.WriteLine($"No prescription records found for Patient ID {patientId}");
            }
        }

        // Main application flow
        public static void Main()
        {
            HealthSystemApp app = new HealthSystemApp();

            app.SeedData();
            app.BuildPrescriptionMap();
            app.PrintAllPatients();

            Console.Write("\nEnter a Patient ID to view prescriptions: ");
            if (int.TryParse(Console.ReadLine(), out int patientId))
            {
                app.PrintPrescriptionsForPatient(patientId);
            }
            else
            {
                Console.WriteLine("Invalid Patient ID.");
            }
        }
    }
}
